using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
	public List<Item> list;
    public List<GameObject> waitObjects;
    public GameObject inventory;
	public GameObject container;
    public GameObject pause;
    public GameObject craft;
    public GameObject firepanel;
    public InfoBar controller;
    public GameObject pickitem;
    public BlurEffect blur;
    public Effect_Water water;
    public GameObject helptext;
    public AudioClip audios;
    public AudioClip eat;
    public GameObject c_cam;
    Item item;
    // Use this for initialization
    void Start () {
		list = new List<Item>();
        waitObjects = new List<GameObject>();
        controller = GetComponent<InfoBar>();
    }
	
	// Update is called once per frame
	void Update () {
            if (inventory.active != true && !c_cam.active)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,2.5f))
                {              
                    if (hit.collider.gameObject.name != "FO_Terrain")
                    {
                    if (hit.collider.gameObject.GetComponent<Item>() != null)
                    {
                        helptext.GetComponent<Text>().text = "E to pick " + hit.collider.gameObject.GetComponent<Item>().names;
                        helptext.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            if (list.Count < inventory.transform.childCount)
                            {
                                Item ittem = hit.collider.gameObject.GetComponent<Item>();
                                if (ittem.use == "pickup")
                                {
                                    if (ittem != null)
                                    {
                                        pickitem = Resources.Load<GameObject>(ittem.prefab);
                                    }
                                    if (pickitem != null)
                                    {
                                        item = pickitem.GetComponent<Item>();
                                    }
                                    helptext.SetActive(false);
                                    if (item != null)
                                    {
                                        AudioSource audio = this.gameObject.GetComponent<AudioSource>();
                                        audio.PlayOneShot(audios);
                                        list.Add(item);
                                        GameObject g = hit.collider.gameObject;
                                        g.SetActive(false);
                                        waitObjects.Add(g);
                                        if (item.drop == "food" && item.active == "")
                                        {
                                            Invoke("resp", 30);
                                        }
                                        if (item.type == "rock" && item.active == "")
                                        {
                                            Invoke("resp", 20);
                                        }
                                        if (item.type == "wood" && item.active == "")
                                        {
                                            Invoke("resp", 15);
                                        }
                                        else if (item.type == "hand")
                                        {
                                            Destroy(hit.collider.gameObject);
                                        }
                                        else if (ittem.active == "no")
                                        {
                                            Destroy(hit.collider.gameObject);
                                        }
                                    }
                                }
                            }
                        }
                    }             
                }
            }
            else helptext.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.I))
		{
			if(inventory.activeSelf)
			{
				inventory.SetActive(false);
                for (int i = 0; i < inventory.transform.childCount; i++)
				{
					if(inventory.transform.GetChild(i).transform.childCount > 0)
					{
						Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
					}
				}
			}
			else
			{
                inventory.SetActive(true);
                int count = list.Count;
				for(int i = 0; i < count; i++)
				{
					Item it = list[i];

					if(inventory.transform.childCount >= i)
					{
						GameObject img = Instantiate<GameObject>(container);
						img.transform.SetParent(inventory.transform.GetChild(i).transform);
                        img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.sprite);
                        img.GetComponent<Drag>().item = it;
					}
					else break;
				}
			}
		}
        if(inventory.activeSelf|craft.activeSelf|pause.activeSelf)
        {
            blur.enabled = true;
        }
        else
        {
            if (water.water != true)
            {
                blur.enabled = false;
            }
        }
	}

    void resp()
    {
        if (waitObjects.Count > 0)
        {
            GameObject g = waitObjects[0];
            g.SetActive(true);
            waitObjects.RemoveAt(0);
            if (waitObjects.Count > 1)
            {
                waitObjects[0] = waitObjects[1];
            }
        }
    }

    public void add(Item item)
    {
        if (list.FindIndex(x => x == item) == -1) list.Add(item);
    }
    void Audio(Drag dr)
    {
        AudioSource audio = this.gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(eat);
    }

    void ad(Item items)
    {
        items.drop = "food";
        list.Add(items);
        inventory.SetActive(false);
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            if (inventory.transform.GetChild(i).transform.childCount > 0)
            {
                Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
            }
        }
        inventory.SetActive(true);
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            Item it = list[i];

            if (inventory.transform.childCount >= i)
            {
                GameObject img = Instantiate<GameObject>(container);
                img.transform.SetParent(inventory.transform.GetChild(i).transform);
                img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.sprite);
                img.GetComponent<Drag>().item = it;
            }
            else break;
        }
    }
    public void removeItem(Item item)
    {
        list.Remove(item);
    }
	void use(Drag drag)
	{
        bool use = false;

		if(drag.item.type == "food")
		{
			controller.addFood(drag.item.heal,drag.item.options);
            use = true;
		}
        else if (drag.item.type == "otrava")
        {
            controller.minFood(drag.item.heal);
            use = true;
        }
        if (drag.item.type == "hand")
        {
            Debug.Log(drag.item);
            HandItem myitem = Instantiate<GameObject>(Resources.Load<GameObject>(drag.item.prefab)).GetComponent<HandItem>();
            controller.addHand(myitem);
            myitem.transform.localRotation = Quaternion.Euler(myitem.rotation);
            use = true;
        }
        if (use == true)
        {
            firepanel.SetActive(false);
            list.Remove(drag.item);
            Destroy(drag.gameObject);
            use = false;
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                for (int i = 0; i < inventory.transform.childCount; i++)
                {
                    if (inventory.transform.GetChild(i).transform.childCount > 0)
                    {
                        Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
                    }
                }
            }
        }
    }

	void remove(Drag drag)
	{
        bool dr = false;
        if (drag.item.type == "wood")
        {
            dr = true;
        }
        else if (drag.item.type == "rock")
        {
            dr = true;
        }
        else if (drag.item.drop == "coock")
        {
            Item ittt = drag.item;
            list.Remove(ittt);
            inventory.SetActive(false);
            for (int i = 0; i < inventory.transform.childCount; i++)
            {
                if (inventory.transform.GetChild(i).transform.childCount > 0)
                {
                    Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
                }
            }
            inventory.SetActive(true);
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                Item it = list[i];

                if (inventory.transform.childCount >= i)
                {
                    GameObject img = Instantiate<GameObject>(container);
                    img.transform.SetParent(inventory.transform.GetChild(i).transform);
                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.sprite);
                    img.GetComponent<Drag>().item = it;
                }
                else break;
            }
        }
        if (dr == true)
        {
            Item it = drag.item;
            GameObject newo = Instantiate<GameObject>(Resources.Load<GameObject>(it.prefab));
            newo.AddComponent<Rigidbody>();
            newo.transform.position = transform.position + transform.forward + transform.up;
            Destroy(drag.gameObject);
            list.Remove(it);
            Item neo = newo.GetComponent<Item>();
            neo.active = "no";
            dr = false;
        }
    }
}
