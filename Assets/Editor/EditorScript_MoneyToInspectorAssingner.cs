using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MoneyParentAndArrayManager))]
[RequireComponent(typeof(MoneyParentAndArrayManager))]
public class EditorScript_MoneyToInspectorAssingner : Editor
{

    public override void OnInspectorGUI()
    {

        MoneyParentAndArrayManager moneyParentAndArrayManager = (MoneyParentAndArrayManager)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Assign Cash Prefabs in Inspector Array"))
        {
            //GameObject[] children = moneyParentAndArrayManager.ParentMoneyGO.gameObject.GetComponentsInChildren<GameObject>(); //hold all the cash prefabs in the hierarchy here
            Transform[] children = moneyParentAndArrayManager.ParentMoneyGO.transform.GetComponentsInChildren<Transform>(true); //hold all the cash prefabs in the hierarchy here

            int assignmentIndex = 0;
            for (int i = 1; i < children.Length; i++) //skip the parent, assign the next object in the children array to required array, then skip the next three iterations and repeat
            {
                if (i % 4 == 1) //only assignes 1, 5, 9, 13, 17, 21...
                {
                    moneyParentAndArrayManager.TableToAssignMoneysTo.moneys[assignmentIndex] = children[i].gameObject; //assigning of cash prefabs to inspector happens here
                    assignmentIndex++;
                }

            }
        }

    }


}
