using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldState{
    public WorldStateEnum key;
    public int value;
}

public enum WorldStateEnum{IsWaiting, IsTreatingPatient, HasArrived, HasRegistered, IsTreated, HasTreated, AtHospital, AtCubicle, GotPatient, AtHome};

public class WorldStates
{
    public Dictionary<WorldStateEnum, int> states;

    public WorldStates() {
        states = new Dictionary<WorldStateEnum, int>();
    }

    public bool HasState(WorldStateEnum key) {
        return states.ContainsKey(key);
    }

    void AddState(WorldStateEnum key, int value) {
        states.Add(key, value);
    }

    public void ModifyState(WorldStateEnum key, int value) {
        if (states.ContainsKey(key)) {
            states[key] += value;
            if (states[key] <= 0) {
                RemoveState(key);
            }
        }
        else {
            states.Add(key, value);
        }
    }

    public void RemoveState(WorldStateEnum key) {
        if (states.ContainsKey(key)) {
            states.Remove(key);
        }
    }

    public void SetState(WorldStateEnum key, int value) {
        if (states.ContainsKey(key)) {
            states[key] = value;
        } 
        else {
            states.Add(key,value);
        }
    }

    public Dictionary<WorldStateEnum, int> GetStates() {
        return states;
    }
}
