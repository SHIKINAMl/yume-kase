#pragma warning disable 0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemForStage4 : MonoBehaviour, IPointerClickHandler
{
  public bool disableAttribute = false;
  public bool ownableAttribute = false;
  public bool eventAttribute = false;

  public string itemName;
  public string itemText;

  public int numberOfEvent = 1;

  [SerializeField]
  public ItemEventForStage4[] events;

  public bool waitEvent = false;

  public SoundEffectData[] soundEffectList => GameObject.Find("StageManager").GetComponent<StageManager>().soundEffectList;

  private StageManager stagemanager;
  private GetItemWindow getitemwindow;
  private ItemInventory iteminventory;

  private SpriteRenderer spriterenderer;
  private Collider2D collider;
  private AudioSource audiosource;

  private Image blinderPanel;

  private bool isClicked = false;

  private bool isMoving = false;
  private Vector2 initialPosition;
  private Vector2 destinationPosition;
  private float moveSpeed;

  private bool isChanging = false;
  private Vector2 initialScale;
  private float changeSizeRate;
  private float changeSpeed;

  private bool isOnEvent = false;
  private bool wasOnEvent = false;

  private bool isFadingOut = false;
  private bool isFadingIn = false;


  public void Start()
  {
    stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
    getitemwindow = GameObject.Find("GetItemWindow").GetComponent<GetItemWindow>();
    iteminventory = GameObject.Find("ItemInventory").GetComponent<ItemInventory>();

    spriterenderer = GetComponent<SpriteRenderer>();
    collider = GetComponent<Collider2D>();
    audiosource = GetComponent<AudioSource>();

    blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();

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
    FadeOutItem();
    fadeInItem();

    if (waitEvent)
    {

      if (isOnEvent && isOnEvent != wasOnEvent)
      {
        blinderPanel.enabled = true;
        blinderPanel.color = new Color(0, 0, 0, 0);
      }

      else if (!isOnEvent && isOnEvent != wasOnEvent)
      {
        blinderPanel.enabled = false;
      }
    }

    wasOnEvent = isOnEvent;
  }

  public void OnPointerClick(PointerEventData pointer)
  {
    StartCoroutine("ClickSwitch");

    if (ownableAttribute && stagemanager.itemList.Count < stagemanager.inventorySize)
    {
      stagemanager.itemList.Add(new ItemData(itemName, spriterenderer.sprite, itemText));
      getitemwindow.DisplayGetItem(itemName, spriterenderer.sprite);
      ownableAttribute = false;
      disableAttribute = true;
    }
  }

  private IEnumerator ClickSwitch()
  {
    isClicked = true;
    CheckCondition(events);

    yield return null;

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
        isOnEvent = false;
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
        isOnEvent = false;
      }
    }
  }

  private void FadeOutItem()
  {
    if (isFadingOut)
    {
      spriterenderer.color -= new Color(0, 0, 0, 0.02f);

      if (spriterenderer.color.a == 0)
      {
        disableAttribute = true;
        isFadingOut = false;
        isOnEvent = false;
      }
    }
  }

  private void fadeInItem()
  {
    if (isFadingIn)
    {
      spriterenderer.color += new Color(0, 0, 0, 0.02f);

      if (spriterenderer.color.a == 1)
      {
        isFadingIn = false;
        isOnEvent = false;
      }
    }
  }

  private void CheckCondition(ItemEventForStage4[] events)
  {
    if (eventAttribute)
    {
      foreach (var i in events)
      {
        List<bool> sumBool = new List<bool>();

        foreach (var j in i.conditions)
        {

          //ループ終了条件確認
          if ((i.isLoop) &&
            (j.isThisClickedWhileLoop && isClicked) ||
            (j.isBeFlaggedWhileLoop && CheckFlags(j.stoodFlagNamesWhileLoop)) ||
            (j.isHoldItemWhileLoop && CheckItems(j.holdItemNamesWhileLoop))
          )
          {

            Debug.Log("루프가 종료되었습니다");
            StopCoroutine(LoopEvent(i));
            i.isBreakLoop = true;
          }

          sumBool.Add((!j.ifThisClicked || (j.ifThisClicked && isClicked)) &&
              (!j.ifDoNoting || (j.ifDoNoting)) &&
              (!j.ifFlag || (j.ifFlag && CheckFlags(j.stoodFlagNames))) &&
              (!j.ifHoldItem || (j.ifHoldItem && CheckItems(j.holdItemNames))) &&
              (!j.ifAffordInventory || (j.ifAffordInventory && stagemanager.itemList.Count <= stagemanager.inventorySize - j.numberOfEmpty)));
        }

        if (sumBool.Contains(true) && i.eventTrigger && !isMoving &&
            (i.gettingOption != 2 || stagemanager.itemList.Count < stagemanager.inventorySize))
        {

          StartCoroutine(RaiseEvent(i));

          if (i.eventType == 1)
          {
            i.eventTrigger = false;
          }

          foreach (var j in i.conditions)
          {
            if (j.ifFlag)
            {
              foreach (var k in j.stoodFlagNames)
              {
                if (k.boolean)
                {
                  stagemanager.SetFlagByName(stagemanager.eventFlagList, k.name, false);
                }
              }
            }

            if (j.ifHoldItem)
            {
              foreach (var k in j.holdItemNames)
              {
                if (k.boolean)
                {
                  stagemanager.itemList.RemoveAt(stagemanager.GetItemIndex(k.name));
                }
              }
            }
          }
        }
      }
    }
  }

  private IEnumerator LoopEvent(ItemEventForStage4 itemEvent)
  {

    while ((!itemEvent.isBreakLoop))
    {
      yield return new WaitForSeconds(itemEvent.loopTime);

      if(itemEvent.isBreakLoop) yield break;
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
          case 4:
            isFadingOut = true;
            break;
          case 5:
            isFadingIn = true;
            disableAttribute = false;
            spriterenderer.color -= new Color(0, 0, 0, spriterenderer.color.a);
            break;
        }
      }

      if (itemEvent.beAvailable)
      {
        if (itemEvent.gettingOption == 1)
        {
          ownableAttribute = true;
        }

        else
        {
          stagemanager.itemList.Add(new ItemData(itemName, spriterenderer.sprite, itemText));
          getitemwindow.DisplayGetItem(itemName, spriterenderer.sprite);
          ownableAttribute = false;
          disableAttribute = true;
        }
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

      if (!itemEvent.beToMove && !itemEvent.beToChangeSize && (!itemEvent.beToAppear || !(itemEvent.appearingOption == 4 || itemEvent.appearingOption == 5)))
      {
        isOnEvent = false;
      }

      itemEvent.isLoop = false;

    }
  }


  private IEnumerator RaiseEvent(ItemEventForStage4 itemEvent)
  {

    isOnEvent = true;
    yield return new WaitForSeconds(itemEvent.waittingTime);

    if (itemEvent.eventType == 3 && !itemEvent.isLoop)
    {
      itemEvent.isLoop = true;
      StartCoroutine(LoopEvent(itemEvent));
    }

    while(itemEvent.isLoop) yield return null;

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
        case 4:
          isFadingOut = true;
          break;
        case 5:
          isFadingIn = true;
          disableAttribute = false;
          spriterenderer.color -= new Color(0, 0, 0, spriterenderer.color.a);
          break;
      }
    }

    if (itemEvent.beAvailable)
    {
      if (itemEvent.gettingOption == 1)
      {
        ownableAttribute = true;
      }

      else
      {
        stagemanager.itemList.Add(new ItemData(itemName, spriterenderer.sprite, itemText));
        getitemwindow.DisplayGetItem(itemName, spriterenderer.sprite);
        ownableAttribute = false;
        disableAttribute = true;
      }
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

    if (!itemEvent.beToMove && !itemEvent.beToChangeSize && (!itemEvent.beToAppear || !(itemEvent.appearingOption == 4 || itemEvent.appearingOption == 5)))
    {
      isOnEvent = false;
    }


  }

  private bool CheckFlags(SetDataForStage4[] flagList)
  {
    foreach (var flagData in flagList)
    {
      if (!stagemanager.GetFlagByName(flagData.name))
      {
        return false;
      }
    }

    return true;
  }

  private bool CheckItems(SetDataForStage4[] itemList)
  {
    foreach (var itemData in itemList)
    {
      if (!stagemanager.CheckItemName(itemData.name))
      {
        return false;
      }
    }

    return true;
  }
}


[Serializable]
public class ItemEventForStage4
{
  public int eventType = 1;
  public int conditionType = 1;
  public float waittingTime = 0;
  public float loopTime = 0;

  public bool isLoop = false;
  public bool isBreakLoop = false;

  [SerializeField]
  public ItemConditionForStage4[] conditions;

  public bool eventTrigger = true;

  public bool beToAppear = false;
  public int appearingOption = 1;

  public bool beAvailable = false;
  public int gettingOption = 1;

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
public class ItemConditionForStage4
{
  public bool ifThisClicked = false;
  public bool ifDoNoting = false;
  public bool ifFlag = false;

  //ループ関連
  public bool isBreakLoop = false;
  public bool isThisClickedWhileLoop = false;
  public bool isBeFlaggedWhileLoop = false;
  public bool isHoldItemWhileLoop = false;
  public int numberOfFlagWhileLoop;
  public int numberOfItemWhileLoop;
  public SetDataForStage4[] stoodFlagNamesWhileLoop;
  public SetDataForStage4[] holdItemNamesWhileLoop;



  public int numberOfFlag;
  public SetDataForStage4[] stoodFlagNames;

  public bool ifHoldItem = false;
  public int numberOfItem;
  public SetDataForStage4[] holdItemNames;

  public bool ifAffordInventory = false;
  public int numberOfEmpty = 1;
}

[Serializable]
public class SetDataForStage4
{
  public string name;

  public bool boolean;

  public SetDataForStage4(string name, bool boolean)
  {
    this.name = name;
    this.boolean = boolean;
  }
}