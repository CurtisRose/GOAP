using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GAgentVisual))]
[CanEditMultipleObjects]
public class GAgentVisualEditor : Editor 
{


    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        GAgentVisual agent = (GAgentVisual) target;
        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GAgent>().currentAction);
        GUILayout.Label("Actions: ");
        foreach (GAction action in agent.gameObject.GetComponent<GAgent>().actions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<WorldStateEnum, int> preConditions in action.preConditionsDict)
                pre += preConditions.Key + ", ";
            foreach (KeyValuePair<WorldStateEnum, int> postCondition in action.postConditionsDict)
                eff += postCondition.Key + ", ";

            GUILayout.Label("====  " + action.actionName + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> goal in agent.gameObject.GetComponent<GAgent>().goals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<WorldStateEnum, int> subGoal in goal.Key.sGoals)
                GUILayout.Label("=====  " + subGoal.Key);
        }
        serializedObject.ApplyModifiedProperties();
    }
}