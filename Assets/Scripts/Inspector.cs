using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor (typeof(StageManager))]
public class StageManagerEditor : Editor
{
    private bool clearFlagButton;

    private bool eventFlagButton;

    private bool BGMButton;

    public override void OnInspectorGUI()
    {
        StageManager stagemanager = target as StageManager;        

        stagemanager.numberOfRoom = EditorGUILayout.IntField("部屋の数", stagemanager.numberOfRoom);

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("クリアフラグ");
        EditorGUILayout.BeginHorizontal();

        if (stagemanager.clearFlagList.Length != stagemanager.numberOfClearFlag)
        {
            Array.Resize<FlagData>(ref stagemanager.clearFlagList, stagemanager.numberOfClearFlag);
        }
        else
        {
            if (!clearFlagButton)
            {
                clearFlagButton = GUILayout.Button(EditorGUIUtility.TrIconContent("d_forward"), "RL FooterButton", GUILayout.Width(16));
                stagemanager.numberOfClearFlag = EditorGUILayout.IntField("フラグの数->", stagemanager.numberOfClearFlag);
                EditorGUILayout.EndHorizontal ();
                EditorGUI.indentLevel++;
            }
            else
            {
                clearFlagButton = !GUILayout.Button(EditorGUIUtility.TrIconContent("icon dropdown"), "RL FooterButton", GUILayout.Width(16));
                stagemanager.numberOfClearFlag = EditorGUILayout.IntField("フラグの数->", stagemanager.numberOfClearFlag);
                EditorGUILayout.EndHorizontal ();
                EditorGUI.indentLevel++;

                foreach (var i in stagemanager.clearFlagList)
                {
                    if (!string.IsNullOrEmpty(i.flagName))
                    {
                        EditorGUILayout.HelpBox("\n フラグ : " + i.flagName + "\n", MessageType.None);
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("フラグの名前を決めてください", MessageType.Warning);
                    }

                    EditorGUI.indentLevel++;
                    i.flagName = EditorGUILayout.TextField("フラグの名前", i.flagName);
                    i.flag = EditorGUILayout.Toggle("フラグの状態", i.flag);
                    EditorGUILayout.Space(5);
                    EditorGUI.indentLevel--;
                }
            }
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("イベントフラグ");
        EditorGUILayout.BeginHorizontal();

        if (stagemanager.eventFlagList.Length != stagemanager.numberOfEventFlag)
        {
            Array.Resize<FlagData>(ref stagemanager.eventFlagList, stagemanager.numberOfEventFlag);
        }
        else
        {
            if (!eventFlagButton)
            {
                eventFlagButton = GUILayout.Button(EditorGUIUtility.TrIconContent("d_forward"), "RL FooterButton", GUILayout.Width(16));
                stagemanager.numberOfEventFlag = EditorGUILayout.IntField("フラグの数->", stagemanager.numberOfEventFlag);
                EditorGUILayout.EndHorizontal ();
                EditorGUI.indentLevel++;
            }
            else
            {
                eventFlagButton = !GUILayout.Button(EditorGUIUtility.TrIconContent("icon dropdown"), "RL FooterButton", GUILayout.Width(16));
                stagemanager.numberOfEventFlag = EditorGUILayout.IntField("フラグの数->", stagemanager.numberOfEventFlag);
                EditorGUILayout.EndHorizontal ();
                EditorGUI.indentLevel++;

                foreach (var i in stagemanager.eventFlagList)
                {
                    if (!string.IsNullOrEmpty(i.flagName))
                    {
                        EditorGUILayout.HelpBox("\n フラグ : " + i.flagName + "\n", MessageType.None);
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("フラグの名前を決めてください", MessageType.Warning);
                    }

                    EditorGUI.indentLevel++;
                    i.flagName = EditorGUILayout.TextField("フラグの名前", i.flagName);
                    i.flag = EditorGUILayout.Toggle("フラグの状態", i.flag);
                    EditorGUILayout.Space(5);
                    EditorGUI.indentLevel--;
                }
            }
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("BGM");

        EditorGUILayout.BeginHorizontal();

        if (stagemanager.BGMList.Length != stagemanager.numberOfBGM)
        {
            Array.Resize<BGMData>(ref stagemanager.BGMList, stagemanager.numberOfBGM);
        }
        else
        {
            
            if (!BGMButton)
            {
                BGMButton = GUILayout.Button(EditorGUIUtility.TrIconContent("d_forward"), "RL FooterButton", GUILayout.Width(16));
                stagemanager.numberOfBGM = EditorGUILayout.IntField("BGMの数->", stagemanager.numberOfBGM);
                EditorGUILayout.EndHorizontal ();
                EditorGUI.indentLevel++;
            }
            else
            {
                BGMButton = !GUILayout.Button(EditorGUIUtility.TrIconContent("icon dropdown"), "RL FooterButton", GUILayout.Width(16));
                stagemanager.numberOfBGM = EditorGUILayout.IntField("BGMの数->", stagemanager.numberOfBGM);
                EditorGUILayout.EndHorizontal ();
                EditorGUI.indentLevel++;

                foreach (var i in stagemanager.BGMList)
                {
                    if (Array.IndexOf(stagemanager.BGMList, i) == 0)
                    {
                        EditorGUILayout.HelpBox("\n初期BGM\n", MessageType.None);
                        EditorGUI.indentLevel++;
                        i.BGM = (AudioClip)EditorGUILayout.ObjectField("流す曲", i.BGM, typeof(AudioClip));
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("\n"+(Array.IndexOf(stagemanager.BGMList, i)+1)+"回目のBGM\n", MessageType.None);
                        EditorGUI.indentLevel++;
                        i.BGM = (AudioClip)EditorGUILayout.ObjectField("流す曲", i.BGM, typeof(AudioClip));
                        i.flagName = EditorGUILayout.TextField("変更条件のフラグ", i.flagName);
                    }

                    EditorGUILayout.Space(5);
                    EditorGUI.indentLevel--;
                }
            }
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("入手アイテム一覧");
        EditorGUI.indentLevel++;
        
        foreach (var i in stagemanager.itemList)
        {
            if (!string.IsNullOrEmpty(i))
            {
                EditorGUILayout.TextField(i);
            }
        }
    }
}

[CustomEditor (typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        Item item = target as Item;
        var events = serializedObject.FindProperty("events");

        item.itemName = EditorGUILayout.TextField("アイテムの名前->", item.itemName);

        EditorGUILayout.Space(20);

        item.disableAttribute = EditorGUILayout.Toggle("画像を非表示->", item.disableAttribute);
        item.ownableAttribute = EditorGUILayout.Toggle("入手出来る->", item.ownableAttribute);
        item.eventAttribute = EditorGUILayout.Toggle("イベントを起こす->", item.eventAttribute);


        if (item.eventAttribute)
        {
            EditorGUI.indentLevel++; 
            item.numberOfEvent = EditorGUILayout.IntPopup("イベントの数", item.numberOfEvent, new string[]{"one", "two", "three"}, new int[]{1, 2, 3});
            EditorGUI.indentLevel++; 

            if (events.arraySize != item.numberOfEvent)
            {              
                events.arraySize = item.numberOfEvent;

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();

                item.numberOfEvent = events.arraySize;
            }
            else
            {
                for (var i = 0; i < item.numberOfEvent; i++)
                {

                    EditorGUILayout.HelpBox($"\nイベント{i+1}\n", MessageType.None);
                    EditorGUI.indentLevel++;

                    item.events[i].conditionType = EditorGUILayout.IntPopup("条件のタイプ", item.events[i].conditionType, new string[]{"and条件のみ", "(and条件) or (and条件)", "(and条件) or (and条件) or　(and条件)"}, new int[]{1, 2, 3});

                    if (item.events[i].conditions.Length != item.events[i].conditionType)
                    {              
                        Array.Resize<ItemCondition>(ref item.events[i].conditions, item.events[i].conditionType);
                    }
                    else
                    {
                        for (var j = 0; j < item.events[i].conditionType; j++)
                        {
                            EditorGUI.indentLevel--;


                            if (item.events[i].conditionType == 1)
                            {
                                EditorGUILayout.HelpBox("イベントの条件を決めてください", MessageType.None);  
                            }
                            else
                            {
                                EditorGUILayout.HelpBox($"イベントのand条件その{j+1}を決めてください", MessageType.None);  
                            }

                            EditorGUI.indentLevel++; 

                            item.events[i].conditions[j].ifThisClicked = EditorGUILayout.Toggle("このオブジェクトが押されたら", item.events[i].conditions[j].ifThisClicked);
                            item.events[i].conditions[j].ifFlag = EditorGUILayout.Toggle("特定のフラグがたったら", item.events[i].conditions[j].ifFlag);

                            if (item.events[i].conditions[j].ifFlag)
                            {
                                EditorGUI.indentLevel++; 
                                item.events[i].conditions[j].stoodFlagName = EditorGUILayout.TextField("そのフラグの名前->", item.events[i].conditions[j].stoodFlagName);
                                EditorGUI.indentLevel--;
                            }

                            item.events[i].conditions[j].ifHoldItem = EditorGUILayout.Toggle("特定のアイテムを持っていたら", item.events[i].conditions[j].ifHoldItem);

                            if (item.events[i].conditions[j].ifHoldItem)
                            {
                                EditorGUI.indentLevel++; 
                                item.events[i].conditions[j].holdItemName = EditorGUILayout.TextField("そのアイテムの名前->", item.events[i].conditions[j].holdItemName);
                                EditorGUI.indentLevel--;
                            }


                            EditorGUILayout.Space(5);
                        }

                        EditorGUI.indentLevel--;
                        EditorGUILayout.HelpBox("起きるイベントを決めてください", MessageType.None);  
                        EditorGUI.indentLevel++; 

                        item.events[i].beToAppear = EditorGUILayout.Toggle("このアイテムが現れる/消える", item.events[i].beToAppear);

                        if (item.events[i].beToAppear)
                        {
                            EditorGUI.indentLevel++; 
                            item.events[i].appearingOption = EditorGUILayout.IntPopup("オプション->", item.events[i].appearingOption, new string[]{"現れる", "消える", "反転する"}, new int[]{1, 2, 3});
                            EditorGUI.indentLevel--;        
                        }

                        item.events[i].beAvailable = EditorGUILayout.Toggle("このアイテムが入手可能になる", item.events[i].beAvailable);

                        item.events[i].beToMove = EditorGUILayout.Toggle("このアイテムが移動する", item.events[i].beToMove);

                        if (item.events[i].beToMove)
                        {
                            EditorGUI.indentLevel++; 
                            item.events[i].moveVector = EditorGUILayout.Vector2Field("移動するベクトル->", item.events[i].moveVector);
                            item.events[i].moveType = EditorGUILayout.IntPopup("移動方法" ,item.events[i].moveType, new string[]{"瞬間移動", "等速移動"}, new int[]{1, 2});

                            if (item.events[i].moveType == 2)
                            {
                                EditorGUI.indentLevel++;
                                item.events[i].moveSpeed = EditorGUILayout.Slider("移動速度->", item.events[i].moveSpeed, 0, 10);
                                EditorGUI.indentLevel--;
                            }
                            EditorGUI.indentLevel--;
                        }

                        item.events[i].beToChangeSize = EditorGUILayout.Toggle("このアイテムの大きさが変化する", item.events[i].beToChangeSize);

                        if (item.events[i].beToChangeSize)
                        {
                            EditorGUI.indentLevel++; 
                            item.events[i].changeSizeRate = EditorGUILayout.Slider("変化の倍率->", item.events[i].changeSizeRate, 0, 5);
                            item.events[i].changeType = EditorGUILayout.IntPopup("変化方法" ,item.events[i].changeType, new string[]{"瞬間変化", "等速変化"}, new int[]{1, 2});

                            if (item.events[i].changeType == 2)
                            {
                                EditorGUI.indentLevel++;
                                item.events[i].changeSpeed = EditorGUILayout.Slider("変化速度->", item.events[i].changeSpeed, 0, 10);
                                EditorGUI.indentLevel--;
                            }
                            EditorGUI.indentLevel--;

                        }

                        item.events[i].beToPopUpText = EditorGUILayout.Toggle("ポップアップテキストを表示する", item.events[i].beToPopUpText);

                        if (item.events[i].beToPopUpText)
                        {
                            EditorGUI.indentLevel++;
                            item.events[i].poppingUpText = EditorGUILayout.TextField("表示するテキスト->", item.events[i].poppingUpText);

                            EditorGUI.indentLevel--;
                        }

                        item.events[i].beToSoundEffect = EditorGUILayout.Toggle("効果音を鳴らす", item.events[i].beToSoundEffect);

                        if (item.events[i].beToSoundEffect)
                        {
                            EditorGUI.indentLevel++;
                            item.events[i].soundType = EditorGUILayout.TextField("鳴らす効果音の種類->", item.events[i].soundType);

                            //その他の場合、ドラッグ＆ドロップで音声ファイルを指定できるようにする。
                            EditorGUI.indentLevel--;
                        }

                        item.events[i].beToFlag = EditorGUILayout.Toggle("特定のフラグがたつ", item.events[i].beToFlag);

                        if (item.events[i].beToFlag)
                        {
                            EditorGUI.indentLevel++; 
                            item.events[i].standingFlagName = EditorGUILayout.TextField("そのフラグの名前->", item.events[i].standingFlagName);
                            EditorGUI.indentLevel--;
                        }

                        item.events[i].beToFlagClear = EditorGUILayout.Toggle("特定のクリアフラグがたつ", item.events[i].beToFlagClear);

                        if (item.events[i].beToFlagClear)
                        {
                            EditorGUI.indentLevel++; 
                            item.events[i].standingClearFlagName = EditorGUILayout.TextField("そのフラグの名前->", item.events[i].standingClearFlagName);
                            EditorGUI.indentLevel--;
                        }

                        EditorGUI.indentLevel--;
                        EditorGUILayout.Space(20);
                    }
                }
            }
        }

        EditorUtility.SetDirty(target);
    }
}

[CustomEditor (typeof(ChangeRoom))]
public class ChangeRoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChangeRoom changeRoom = target as ChangeRoom;

        int[] numberOfRoomInt = Enumerable.Range(1, changeRoom.numberOfRoom).ToArray();
        string[] numberOfRoomString = (from i in numberOfRoomInt select i.ToString()).ToArray();

        EditorGUILayout.Space(10);
       
        changeRoom.destinationRoom = EditorGUILayout.IntPopup("移動先の部屋番号->", changeRoom.destinationRoom, numberOfRoomString, numberOfRoomInt);
       
        EditorGUILayout.Space(10);

        changeRoom.isFlagType = EditorGUILayout.Toggle("特定のフラグが立つまで非表示->", changeRoom.isFlagType);

        if (changeRoom.isFlagType)
        {
            EditorGUI.indentLevel++;
            changeRoom.flagName = EditorGUILayout.TextField("そのフラグ->", changeRoom.flagName);
        }
    }
}