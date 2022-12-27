using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreated : GAction
{
    public override bool PrePerform()
    {
        target = agent.FindInInventory(WorldObject.Cubicle);
        if (target == null) {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        agent.RemoveFromInventory(WorldObject.Cubicle);
        return true;
    }
}
