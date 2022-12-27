using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubicle : MonoBehaviour
{
    void Start()
    {
        GWorld.worldInventory.AddWorldObject(this.gameObject, WorldObject.Cubicle);
    }
}
