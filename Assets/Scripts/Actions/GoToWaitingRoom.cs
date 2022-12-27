using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingRoom : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState(WorldStateEnum.IsWaiting, 1);
        GWorld.worldInventory.AddWorldObject(this.gameObject, WorldObject.Patient);
        agentBeliefs.ModifyState(WorldStateEnum.AtHospital, 1);
        return true;
    }
}
