using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node {
    public Node parent;
    public float cost;
    public Dictionary<WorldStateEnum,int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<WorldStateEnum,int> allStates, GAction action) {
        this.parent = parent;
        this.cost = cost;
        // Creates a copy, instead of a reference;
        this.state = new Dictionary<WorldStateEnum,int>(allStates);
        this.action = action;
    }

    public Node(Node parent, float cost, Dictionary<WorldStateEnum,int> worldStates, Dictionary<WorldStateEnum,int> beliefStates, GAction action) {
        this.parent = parent;
        this.cost = cost;
        // Creates a copy, instead of a reference;
        this.state = new Dictionary<WorldStateEnum,int>(worldStates);
        foreach(KeyValuePair<WorldStateEnum, int> belief in beliefStates) {
            if (!this.state.ContainsKey(belief.Key)) {
                this.state.Add(belief.Key, belief.Value);
            }
        }
        this.action = action;
    }
}

public class GPlanner {
    public Queue<GAction> Plan(List<GAction>actions, Dictionary<WorldStateEnum,int> goal, WorldStates beliefStates) {
        List<GAction> usableActions = new List<GAction>();
        foreach (GAction action in actions) {
            if(action.IsAchievable()) {
                usableActions.Add(action);
            }
        }
        List<Node> nodes = new List<Node>();

        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), beliefStates.GetStates(), null);

        bool success = BuildGraph(start, nodes, usableActions, goal);
        
        if(!success) {
            //Debug.Log("No Plan");
            return null;
        }

        Node cheapest = null;
        foreach (Node node in nodes) {
            if (cheapest == null) {
                cheapest = node;
            }
            else {
                if (node.cost < cheapest.cost) {
                    cheapest = node;
                }
            }
        }

        List<GAction> result = new List<GAction>();
        Node cheapestNode = cheapest;
        while (cheapestNode != null) {
            if (cheapestNode.action != null) {
                result.Insert(0, cheapestNode.action);
            }
            cheapestNode = cheapestNode.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach(GAction action in result) {
            queue.Enqueue(action);
        }

        /*Debug.Log("The Plan is: ");
        foreach(GAction action in queue) {
            Debug.Log(action.actionName);
        }*/

        return queue;
    }
    
    private bool BuildGraph(Node parent, List<Node> nodes, List<GAction> usableActions, Dictionary<WorldStateEnum,int> goal) {
        bool foundPath = false;
        
        foreach(GAction action in usableActions) {
            if(action.IsAchievableGiven(parent.state) && action.IsAchievable()) {
                Dictionary<WorldStateEnum,int> currentState = new Dictionary<WorldStateEnum, int>(parent.state);
                foreach(KeyValuePair<WorldStateEnum,int> effects in action.postConditionsDict) {
                    if (!currentState.ContainsKey(effects.Key)) {
                        currentState.Add(effects.Key, effects.Value);
                    }
                }
                
                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                if (GoalAchieved(goal, currentState)) {
                    nodes.Add(node);
                    foundPath = true;
                } else {
                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, nodes, subset, goal);
                    if (found) {
                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }

    private bool GoalAchieved(Dictionary<WorldStateEnum,int> goals, Dictionary<WorldStateEnum,int> state) {
        foreach(KeyValuePair<WorldStateEnum,int> goal in goals) {
            if (!state.ContainsKey(goal.Key)) {
                return false;
            }
        }
        return true;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction remove) {
        List<GAction> subset = new List<GAction>();
        foreach(GAction action in actions) {
            if(!action.Equals(remove)) {
                subset.Add(action);
            }
        }
        return subset;
    }
}
