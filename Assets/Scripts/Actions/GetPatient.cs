using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{
    [SerializeField]
    GameObject resource;

    public override bool PrePerform()
    {
        target = GWorld.worldInventory.RemoveWorldObject(WorldObject.Patient);
        if (target == null) {
            return false;
        }

        resource = GWorld.worldInventory.RemoveWorldObject(WorldObject.Cubicle);
        if(resource != null) {
            agent.AddToInventory(resource, WorldObject.Cubicle);
        } else {
            GWorld.worldInventory.AddWorldObject(target, WorldObject.Patient);
            target = null;
            return false;
        }

        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState(WorldStateEnum.IsWaiting, -1);
        if (target) {
            target.GetComponent<GAgent>().AddToInventory(resource, WorldObject.Cubicle);
        }
        return true;
    }

    public override bool IsAchievable() {
        GameObject potentialPatient = GWorld.worldInventory.FindWorldObject(WorldObject.Patient);
        if (potentialPatient == null) {
            //Debug.Log("Did Not Find a Patient");
            return false;
        }
        //Debug.Log("Found a Patient");
        return true;
    }
}
