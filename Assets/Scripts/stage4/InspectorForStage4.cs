#pragma warning disable 0618

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor (typeof(ItemForStage4))]
public class ItemEditorForStage4 : Editor
{
    public override void OnInspectorGUI() 
    {
        ItemForStage4 item = target as ItemForStage4;
        var events = serializedObject.FindProperty("events");


        EditorGUILayout.Space(20);

        item.disableAttribute = EditorGUILayout.Toggle("画像を非表示->", item.disableAttribute);
        item.ownableAttribute = EditorGUILayout.Toggle("入手出来る->", item.ownableAttribute);

        if (item.ownableAttribute)
        {
            EditorGUI.indentLevel++; 
            item.itemName = EditorGUILayout.TextField("アイテムの名前->", item.itemName);

            var style = new GUIStyle(EditorStyles.textArea)
            {
                wordWrap = true
            };

            EditorGUILayout.LabelField("アイテムの説明");
            item.itemText = EditorGUILayout.TextArea(item.itemText, style);

            EditorGUI.indentLevel--;
            EditorGUILayout.Space(10);
        }

        item.eventAttribute = EditorGUILayout.Toggle("イベントを起こす->", item.eventAttribute);

        if (item.eventAttribute)
        {
            EditorGUI.indentLevel++; 
            item.waitEvent = EditorGUILayout.Toggle("イベント中、操作をできないようにする", item.waitEvent);
            item.numberOfEvent = EditorGUILayout.IntField("イベントの数", item.numberOfEvent);
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

                    item.events[i].eventType = EditorGUILayout.IntPopup("イベントの実行可能回数", item.events[i].eventType, new string[]{"１回", "無限"}, new int[]{1, 2});

                    item.events[i].conditionType = EditorGUILayout.IntPopup("条件のタイプ", item.events[i].conditionType, new string[]{"and条件のみ", "(and条件) or (and条件)", "(and条件) or (and条件) or　(and条件)"}, new int[]{1, 2, 3});

                    item.events[i].waittingTime = EditorGUILayout.FloatField("最初のイベント発生までの待機時間（秒単位）->", item.events[i].waittingTime);

                    if (item.events[i].conditions.Length != item.events[i].conditionType)
                    {              
                        Array.Resize<ItemConditionForStage4>(ref item.events[i].conditions, item.events[i].conditionType);
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

                            item.events[i].conditions[j].ifDoNothing = EditorGUILayout.Toggle("何もしなくても実行される", item.events[i].conditions[j].ifDoNothing);
                            item.events[i].conditions[j].ifThisClicked = EditorGUILayout.Toggle("このオブジェクトが押されたら", item.events[i].conditions[j].ifThisClicked);
                            item.events[i].conditions[j].ifFlag = EditorGUILayout.Toggle("特定のフラグがたったら", item.events[i].conditions[j].ifFlag);

                            if (item.events[i].conditions[j].ifFlag && item.events[i].conditions[j].stoodFlagNames != null)
                            {
                                EditorGUI.indentLevel++;
                                item.events[i].conditions[j].numberOfFlag = EditorGUILayout.IntField("条件フラグの数->", item.events[i].conditions[j].numberOfFlag);
                                

                                if (item.events[i].conditions[j].stoodFlagNames.Length != item.events[i].conditions[j].numberOfFlag)
                                {
                                    Array.Resize<SetDataForStage4>(ref item.events[i].conditions[j].stoodFlagNames, item.events[i].conditions[j].numberOfFlag);
                                }
                                else
                                {
                                    for (var k = 0; k < item.events[i].conditions[j].numberOfFlag; k++)
                                    {
                                        EditorGUILayout.LabelField($"フラグその{k+1}");
                                        EditorGUI.indentLevel++;
                                        item.events[i].conditions[j].stoodFlagNames[k].name = EditorGUILayout.TextField("そのフラグの名前->", item.events[i].conditions[j].stoodFlagNames[k].name);
                                        item.events[i].conditions[j].stoodFlagNames[k].boolean = EditorGUILayout.Toggle("そのフラグを折る->", item.events[i].conditions[j].stoodFlagNames[k].boolean);
                                        EditorGUI.indentLevel--;
                                    }
                                }
                                EditorGUI.indentLevel--;
                            }

                            item.events[i].conditions[j].ifHoldItem = EditorGUILayout.Toggle("特定のアイテムを持っていたら", item.events[i].conditions[j].ifHoldItem);

                            if (item.events[i].conditions[j].ifHoldItem && item.events[i].conditions[j].holdItemNames != null)
                            {
                                EditorGUI.indentLevel++; 
                                item.events[i].conditions[j].numberOfItem = EditorGUILayout.IntField("条件アイテムの数->", item.events[i].conditions[j].numberOfItem);
                                
                                if (item.events[i].conditions[j].holdItemNames.Length != item.events[i].conditions[j].numberOfItem)
                                {
                                    Array.Resize<SetDataForStage4>(ref item.events[i].conditions[j].holdItemNames, item.events[i].conditions[j].numberOfItem);
                                }
                                else
                                {
                                    for (var k = 0; k < item.events[i].conditions[j].numberOfItem; k++)
                                    {
                                        EditorGUILayout.LabelField($"アイテムその{k+1}");
                                        EditorGUI.indentLevel++;
                                        item.events[i].conditions[j].holdItemNames[k].name = EditorGUILayout.TextField("そのアイテムの名前->", item.events[i].conditions[j].holdItemNames[k].name);
                                        item.events[i].conditions[j].holdItemNames[k].boolean = EditorGUILayout.Toggle("そのアイテムを消費する->", item.events[i].conditions[j].holdItemNames[k].boolean);
                                        EditorGUI.indentLevel--;
                                    }
                                }

                                EditorGUI.indentLevel--;
                            }

                            item.events[i].conditions[j].ifAffordInventory = EditorGUILayout.Toggle("インベントリに空きがあったら", item.events[i].conditions[j].ifAffordInventory);

                            if (item.events[i].conditions[j].ifAffordInventory)
                            {
                                EditorGUI.indentLevel++;
                                item.events[i].conditions[j].numberOfEmpty = (int)EditorGUILayout.Slider("必要な空きの数->", item.events[i].conditions[j].numberOfEmpty, 1, 6);
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
                            item.events[i].appearingOption = EditorGUILayout.IntPopup("オプション->", item.events[i].appearingOption, new string[]{"現れる", "消える", "反転する", "フェイドアウト", "フェイドイン", "無効化される"}, new int[]{1, 2, 3, 4, 5, 6});
                            
                            EditorGUI.indentLevel--;        
                        }


                        item.events[i].beAvailable = EditorGUILayout.Toggle("このアイテムが入手可能になる", item.events[i].beAvailable);

                        if (item.events[i].beAvailable)
                        {
                            EditorGUI.indentLevel++;
                            item.events[i].gettingOption = EditorGUILayout.IntPopup("オプション->", item.events[i].gettingOption, new string[]{"クリックで入手", "すぐに入手"}, new int[]{1, 2});
                            EditorGUI.indentLevel--;
                        }

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

                            int listSize = item.events[i].poppingUpTexts.Length;

                            listSize = EditorGUILayout.IntField("文章の数->", listSize);

                            if (item.events[i].poppingUpTexts.Length != listSize)
                            {
                                item.events[i].poppingUpTexts = new string[listSize];
                            }
                            else
                            {
                                for (int j = 0; j < listSize; j++)
                                {
                                    item.events[i].poppingUpTexts[j] = EditorGUILayout.TextField($"表示する文章（その{j+1}）->", item.events[i].poppingUpTexts[j]);
                                }
                            }

                            EditorGUI.indentLevel--;
                        }

                        item.events[i].beToSoundEffect = EditorGUILayout.Toggle("効果音を鳴らす", item.events[i].beToSoundEffect);

                        if (item.events[i].beToSoundEffect)
                        {
                            EditorGUI.indentLevel++;
                            item.events[i].soundType = EditorGUILayout.IntPopup("効果音の種類->", item.events[i].soundType, (from j in item.soundEffectList select j.soundName).ToArray(), Enumerable.Range(0, item.soundEffectList.Length).ToArray());

                            EditorGUI.indentLevel--;
                        }

                        
                        item.events[i].isSetActive = EditorGUILayout.Toggle("特定のオブジェクトを有効化する/無効化する", item.events[i].isSetActive);

                        if(item.events[i].isSetActive){
                            EditorGUI.indentLevel++; 
                            item.events[i].setActiveInt = EditorGUILayout.IntPopup("オプション->", item.events[i].setActiveInt, new string[]{"有効化する", "無効化する"},new int[]{1, 2});
                            
                            if(item.events[i].setActiveInt == 1){
                                EditorGUI.indentLevel++; 
                                
                                item.events[i].setActiceObject = EditorGUILayout.TextField("そのオブジェクトの名前", item.events[i].setActiceObject);
                                item.events[i].parentName = EditorGUILayout.TextField("そのオブジェクトの親オブジェクトの名前", item.events[i].parentName);
                                EditorGUI.indentLevel--;
                            }

                            if(item.events[i].setActiveInt == 2){
                                EditorGUI.indentLevel++; 
                                item.events[i].setActiceObject = EditorGUILayout.TextField("そのオブジェクトの名前", item.events[i].setActiceObject);
                                EditorGUI.indentLevel--;
                            }

                            EditorGUI.indentLevel--;
                        }

                        item.events[i].beToFlag = EditorGUILayout.Toggle("特定のフラグが立つ/折れる", item.events[i].beToFlag);

                        if (item.events[i].beToFlag)
                        {
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;

                            item.events[i].flagCount = EditorGUILayout.IntField("立つフラグの数->", item.events[i].flagCount);

                            for(int j = 0; j < item.events[i].flagCount; i++){
                                item.events[i].standingFlagName[j] = EditorGUILayout.TextField("そのフラグの名前->", item.events[i].standingFlagName[j]);
                                item.events[i].flagOption[j] = EditorGUILayout.IntPopup("オプション->", item.events[i].flagOption[j], new string[]{"立つ", "折れる", "反転する"}, new int[]{1, 2, 3});
                            }

                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                        }

                        item.events[i].beToFlagClear = EditorGUILayout.Toggle("特定のクリアフラグが立つ", item.events[i].beToFlagClear);

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

