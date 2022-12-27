using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal(WorldStateEnum.HasTreated, 1, false);
        goals.Add(s1, 3);
        SubGoal s2 = new SubGoal(WorldStateEnum.IsWaiting, 1, false);
        goals.Add(s2, 1);
    }
}
