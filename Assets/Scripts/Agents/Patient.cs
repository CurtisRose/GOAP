using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : GAgent
{
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal(WorldStateEnum.IsTreated, 1, true);
        goals.Add(s1, 5);
        SubGoal s2 = new SubGoal(WorldStateEnum.AtHome, 1, false);
        goals.Add(s2, 1);
    }
}
