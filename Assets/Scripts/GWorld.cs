using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldObject {Cubicle, Patient};

public class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;

    public static GInventory worldInventory;
    
    static GWorld() {
        world = new WorldStates();
        worldInventory = new GInventory();
    }

    private GWorld() {

    }

    public static GWorld Instance {
        get {
            return instance;
        }
    }

    public WorldStates GetWorld() {
        return world;
    }
}
