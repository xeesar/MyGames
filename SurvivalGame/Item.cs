using UnityEngine;
using System.Collections;
using System;

public class Item : MonoBehaviour {
    public int id;
	public string type;
    public string drop;
    public string active;
    public string sprite;
	public string prefab;
    public string use;
    public int heal;
    public string options;
    public string names;

    public void Start()
    {
    }
    public Item clone()
    {
        Item item = new GameObject().AddComponent<Item>();
        item.id = id;
        item.type = type;
        item.drop = drop;
        item.active = active;
        item.sprite = sprite;
        item.prefab = prefab;
        item.use = use;
        item.heal = heal;
        item.options = options;
        item.name = names;
        return item;
    }
}
