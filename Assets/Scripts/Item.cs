using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public string itemName;

    public bool disableAttribute = false;
    public bool ownableAttribute = false;
    public bool eventAttribute = false;

    public int numberOfEvent = 1;

    [SerializeField]
    public ItemEvent[] events;

    public SoundEffectData[] soundEffectList => GameObject.Find("StageManager").GetComponent<StageManager>().soundEffectList;

    private StageManager stagemanager;
    private GetItemWindow getitemwindow;
    private ItemInventory iteminventory;

    private SpriteRenderer spriterenderer;
    private Collider2D collider;
    private AudioSource audiosource;

    private bool isClicked = false;

    private bool isMoving = false;
    private Vector2 initialPosition;
    private Vector2 destinationPosition;
    private float moveSpeed;

    private bool isChanging = false;
    private Vector2 initialScale;
    private float changeSizeRate;
    private float changeSpeed;


    public void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
        getitemwindow = GameObject.Find("GetItemWindow").GetComponent<GetItemWindow>();
        iteminventory = GameObject.Find("ItemInventory").GetComponent<ItemInventory>();

        spriterenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        audiosource = GetComponent<AudioSource>();

        foreach (var i in events)
        {
            i.eventTrigger = true;
        }
    }

    public void FixedUpdate()
    {
        CheckCondition(events);
        ChangeEnableImage();
        MoveItem();
        ChangeSizeItem();
    }

    public void OnPointerClick(PointerEventData pointer)
    {
        StartCoroutine("ClickSwitch");

        if (ownableAttribute && stagemanager.itemList.Count <= stagemanager.inventorySize)
        {
            stagemanager.itemList.Add(itemName, spriterenderer.sprite);
            getitemwindow.GetItem(itemName, spriterenderer.sprite);
            iteminventory.DisplayItem();
            ownableAttribute = false;
            disableAttribute = true;
        }
    }

    private IEnumerator ClickSwitch()
    {
        isClicked = true;

        yield return new WaitForSeconds(0.5f);

        isClicked = false;
    }

    private void ChangeEnableImage()
    {
        if (disableAttribute)
        {
            spriterenderer.enabled = false;
            collider.enabled = false;
        }
        else
        {
            spriterenderer.enabled = true;
            collider.enabled = true;
        }
    }

    private void MoveItem()
    {
        if (isMoving)
        {
            this.transform.position += (Vector3)destinationPosition / destinationPosition.magnitude * moveSpeed;

            if (((Vector2)this.transform.position - initialPosition).magnitude >= destinationPosition.magnitude)
            {
                isMoving = false;
            }
        }
    }

    private void ChangeSizeItem()
    {
        if (isChanging)
        {
            this.transform.localScale += (new Vector3(1, 1, 0)) * changeSpeed;

            if (((Vector2)this.transform.localScale).magnitude >= (initialScale * changeSizeRate).magnitude)
            {
                isChanging = false;
            }
        }
    }

    private void CheckCondition(ItemEvent[] events)
    {
        if (eventAttribute)
        {
            foreach (var i in events)
            {
                List<bool> sumBool = new List<bool>();

                foreach (var j in i.conditions)
                {
                    sumBool.Add((!j.ifThisClicked || (j.ifThisClicked && isClicked)) &&
                        (!j.ifFlag || (j.ifFlag && stagemanager.GetFlagByName(j.stoodFlagName))) &&
                        (!j.ifHoldItem || (j.ifHoldItem && stagemanager.itemList.ContainsKey(j.holdItemName))));
                }


                if (sumBool.Contains(true) && i.eventTrigger && !isMoving)
                {
                    RaiseEvent(i);

                    if (i.eventType == 1)
                    {
                        i.eventTrigger = false;
                    }

                    foreach (var j in i.conditions)
                    {
                        if (j.ifFlag && j.flagDown)
                        {
                            stagemanager.SetFlagByName(stagemanager.eventFlagList, j.stoodFlagName, false);
                        }

                        if (j.ifHoldItem && j.dropItem)
                        {
                            stagemanager.itemList.Remove(j.holdItemName);
                            iteminventory.DisplayItem();
                        }
                    }
                }
            }
        }
    }
    
    private void RaiseEvent(ItemEvent itemEvent)
    {
        
        if (itemEvent.beToAppear)
        {
            switch (itemEvent.appearingOption)
            {
                case 1: 
                    disableAttribute = false;
                    break;
                case 2: 
                    disableAttribute = true;
                    break;
                case 3: 
                    disableAttribute = !disableAttribute;
                    break;
            }
        }
                    
        if (itemEvent.beAvailable)
        {
            ownableAttribute = true;
        }

        if (itemEvent.beToMove)
        {
            initialPosition = (Vector2)this.transform.position;
            destinationPosition = (Vector2)this.transform.position + itemEvent.moveVector;

            if (itemEvent.moveType == 1)
            {
                this.transform.position = destinationPosition;
            }
            else
            {
                moveSpeed = itemEvent.moveSpeed * Time.deltaTime;
                isMoving = true;
            }
        }

        if (itemEvent.beToChangeSize)
        {
            initialScale = (Vector2)this.transform.localScale;
            changeSizeRate = itemEvent.changeSizeRate;

            if (itemEvent.changeType == 1)
            {
                this.transform.localScale *= changeSizeRate; 
            }
            else
            {
                changeSpeed = itemEvent.changeSpeed * Time.deltaTime;
                isChanging = true;
            }
        }

        if (itemEvent.beToPopUpText)
        {
            GameObject.Find("TextWindow").GetComponent<TextWindow>().SetTexts(itemEvent.poppingUpTexts);
        }

        if (itemEvent.beToSoundEffect)
        {
            audiosource.PlayOneShot(stagemanager.soundEffectList[itemEvent.soundType].soundEffect);
        }

        if (itemEvent.beToFlag)
        {
            switch (itemEvent.flagOption)
            {
                case 1:
                    stagemanager.SetFlagByName(stagemanager.eventFlagList, itemEvent.standingFlagName, true);
                    break;
                case 2:
                    stagemanager.SetFlagByName(stagemanager.eventFlagList, itemEvent.standingFlagName, false);
                    break;
                case 3:
                    stagemanager.SetFlagByName(stagemanager.eventFlagList, itemEvent.standingFlagName, !stagemanager.GetFlagByName(itemEvent.standingFlagName));
                    break;
            }
        }  

        if (itemEvent.beToFlagClear)
        {
            stagemanager.SetFlagByName(stagemanager.clearFlagList, itemEvent.standingClearFlagName, true);
        }
    }
}


[Serializable]
public class ItemEvent
{
    public int eventType = 1;
    public int conditionType = 1;

    [SerializeField]
    public ItemCondition[] conditions;

    public bool eventTrigger = true;

    public bool beToAppear = false;

    public bool beAvailable = false;
    public int appearingOption = 1;

    public bool beToMove = false;
    public Vector2 moveVector = new Vector2(0, 0);
    public int moveType = 0;
    public float moveSpeed = 1;

    public bool beToChangeSize = false;
    public float changeSizeRate = 1;
    public int changeType = 0;
    public float changeSpeed = 1;

    public bool beToPopUpText = false;
    public string[] poppingUpTexts;
    
    public bool beToSoundEffect = false;
    public int soundType; 

    public bool beToFlag = false;
    public string standingFlagName;
    public int flagOption;

    public bool beToFlagClear = false;
    public string standingClearFlagName;

    //image画像変わるevent必要？
}

[Serializable]
public class ItemCondition
{
    public bool ifThisClicked = false;

    public bool ifFlag = false;
    public string stoodFlagName;
    public bool flagDown = true;

    public bool ifHoldItem = false;
    public string holdItemName;
    public bool dropItem = true;
}