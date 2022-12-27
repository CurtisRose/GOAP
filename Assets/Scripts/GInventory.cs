using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    private Dictionary<WorldObject, Queue<GameObject>> items = new Dictionary<WorldObject, Queue<GameObject>>();
    public void AddWorldObject(GameObject item, WorldObject type) {
        if (!items.ContainsKey(type)) {
            items.Add(type, new Queue<GameObject>());
        }

        items[type].Enqueue(item);
    }

    public GameObject FindWorldObject(WorldObject type) {
        GameObject item = null;
        if (items.ContainsKey(type) && items[type].Count > 0) {
            item = items[type].Peek();
        }
        return item;
    }

    public GameObject RemoveWorldObject(WorldObject type) {
        GameObject item = null;
        if (items.ContainsKey(type) && items[type].Count > 0) {
            item = items[type].Dequeue();
            if (items[type].Count <= 0) {
                items.Remove(type);
            }
        }
        return item;
    }
}
