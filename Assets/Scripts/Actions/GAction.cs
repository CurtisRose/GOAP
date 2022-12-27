using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public string targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] postConditions;
    public NavMeshAgent navMeshAgent;
    public Dictionary<WorldStateEnum, int> preConditionsDict;
    public Dictionary<WorldStateEnum, int> postConditionsDict;

    public WorldStates agentBeliefs;
    
    protected GAgent agent;
    
    public bool running = false;

    public GAction() {
        preConditionsDict = new Dictionary<WorldStateEnum, int>();
        postConditionsDict = new Dictionary<WorldStateEnum, int>();

    }

    public void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (preConditions != null) {
            foreach (WorldState worldState in preConditions) {
                preConditionsDict.Add(worldState.key, worldState.value);
            }
        }

        if (postConditions != null) {
            foreach (WorldState worldState in postConditions) {
                postConditionsDict.Add(worldState.key, worldState.value);
            }
        }

        agent = GetComponent<GAgent>();
        agentBeliefs = GetComponent<GAgent>().beliefs;
    }

    public virtual bool IsAchievable() {
        return true;
    }

    public virtual bool IsAchievableGiven(Dictionary<WorldStateEnum, int> conditions) {
        foreach (KeyValuePair<WorldStateEnum, int> preCondition in preConditionsDict) {
            if (!conditions.ContainsKey(preCondition.Key)) {
                return false;
            }
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
