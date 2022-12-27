using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatPatient : GAction
{
    public override bool PrePerform()
    {
        target = agent.FindInInventory(WorldObject.Cubicle);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.worldInventory.AddWorldObject(target, WorldObject.Cubicle);
        agent.RemoveFromInventory(WorldObject.Cubicle, "Post Treat Patient");
        return true;
    }
}
