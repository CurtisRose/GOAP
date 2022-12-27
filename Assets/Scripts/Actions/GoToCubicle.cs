using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCubicle : GAction
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
        return true;
    }
}