using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<WorldStateEnum,int> sGoals;
    public bool remove;
    public SubGoal(WorldStateEnum s, int i, bool r) {
        sGoals = new Dictionary<WorldStateEnum, int>();
        sGoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    GInventory inventory = new GInventory();
    public WorldStates beliefs = new WorldStates();

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    [SerializeField] List<WorldObject> inventoryDebugList;

    protected void Start()
    {
        inventoryDebugList = new List<WorldObject>();
        GAction[] gActions = GetComponents<GAction>();
        foreach(GAction gAction in gActions) {
            actions.Add(gAction);
        }
    }

    bool invoked = false;
    void CompleteAction() {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    void LateUpdate()
    {
        if (currentAction != null && currentAction.running) {
            //Debug.Log(gameObject.name + " Executing Step");
            if (/*currentAction.agent.hasPath &&*/ currentAction.navMeshAgent.remainingDistance < 1.0f) {
                
                //Debug.Log("Action Running");//Debug.Log("Has Path and at destination");
                if(!invoked) {
                    //Debug.Log("Completing Action");
                    //Debug.Log(gameObject.name + " Invoking occured");
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            } else {
                if (currentAction.navMeshAgent.hasPath) {
                    //Debug.Log("Has Path");
                }
                if (currentAction.navMeshAgent.remainingDistance < 2.0f) {
                    //Debug.Log("At destination");
                }
            }
            return;
        }

        if (planner == null || actionQueue == null) {
            //Debug.Log(gameObject.name + " Making New Plan");
            planner = new GPlanner();

            // Order goals by value?
            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> subgoal in sortedGoals) {
                actionQueue = planner.Plan(actions, subgoal.Key.sGoals, beliefs);
                if (actionQueue != null) {
                    currentGoal = subgoal.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0 && !currentAction.running) {
            //Debug.Log(gameObject.name + " Goal was Achieved");
            if(currentGoal.remove) {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0) {
            //Debug.Log(gameObject.name + " Next Step");
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform()) {
                //Debug.Log(gameObject.name + " Right after pre perform");
                if (currentAction.target == null && currentAction.targetTag != "") {
                    //Debug.Log(gameObject.name + " Finding Target By Tag");
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }

                if(currentAction.target != null) {
                    //Debug.Log(gameObject.name + " Setting Target");
                    currentAction.running = true;
                    currentAction.navMeshAgent.SetDestination(currentAction.target.transform.position);
                }
            }
            else {
                actionQueue = null;
            }
        }
    }

    public void AddToInventory(GameObject item, WorldObject type, string words = " ") {
        //Debug.Log(gameObject.name + " Adding " + type.ToString() + " To Inventory" + words);
        inventory.AddWorldObject(item, type);
        inventoryDebugList.Add(type);
    }

    public GameObject RemoveFromInventory(WorldObject type, string words = " ") {
        //Debug.Log(gameObject.name + " Removing " + type.ToString() + " From Inventory" + words);
        inventoryDebugList.Remove(type);
        return inventory.RemoveWorldObject(type);
    }

    public GameObject FindInInventory(WorldObject type) {
        return inventory.FindWorldObject(type);
    }
}
