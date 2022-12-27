using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    public Text states;

    void LateUpdate() {
        Dictionary<WorldStateEnum,int> worldStates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach(KeyValuePair<WorldStateEnum, int> state in worldStates) {
            states.text += state.Key + ", " + state.Value + "\n";
        }
    }
}
