using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class fire : MonoBehaviour {

    public GameObject firepanel;
    public GameObject container;
    public GameObject player;
    public bool firee;
    public GameObject cooking;
    public List<Item> list;
    public Reciple[] reciples = new Reciple[5];
    GameObject g;
    int ic = 0;
    private bool flagok = false;
    [System.Serializable]
    public class Reciple
    {
        public GameObject item;
        public RecipleMaterial materials;

    }

    [System.Serializable]
    public class RecipleMaterial
    {
        public GameObject[] recmat = new GameObject[1];

    }
    void Start () {
        firee = false;
        list = new List<Item>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (firepanel.activeSelf)
            {
                if (firepanel.transform.GetChild(1).childCount == 1&&flagok == false)
                {
                    Destroy(firepanel.transform.GetChild(1).transform.GetChild(0).gameObject);
                }
                if (firepanel.transform.GetChild(0).childCount == 1 && flagok == false)
                {
                    Destroy(firepanel.transform.GetChild(0).transform.GetChild(0).gameObject);
                }
                firepanel.SetActive(false);
            }
            else
            {
                if (firee == true)
                {
                    firepanel.SetActive(true);
                }
            }
        }
        if(firee == false)
        {
            firepanel.SetActive(false);
        }
    }

     public void use(Drag drag)
    {
        if (firepanel.active == true)
        {
            if (firepanel.transform.GetChild(0).childCount <= 1)
            {
               if (drag.item.transform.parent != firepanel.transform.GetChild(0).transform)
                {
                    for (int i = 0; i < reciples.Length; i++)
                    {
                        if (drag.item == reciples[i].item.GetComponent<Item>())
                        {
                            if (firepanel.transform.GetChild(0).childCount == 0)
                            {
                                Item its = drag.item;
                                GameObject img = Instantiate<GameObject>(container);
                                img.transform.SetParent(firepanel.transform.GetChild(0).transform);
                                img.GetComponent<Image>().sprite = Resources.Load<Sprite>(its.sprite);
                                img.GetComponent<Drag>().item = its;
                            }
                            list.Add(drag.item);
                            ic = i;
                            flagok = true;
                            player.BroadcastMessage("remove", drag);
                            cooking.SetActive(true);
                            Invoke("startcook", 10f);
                        }
                    }
                }
            }
        }
    }
    void Remove(Item items)
    {
        list.Remove(items);
        cooking.SetActive(false);
        Destroy(firepanel.transform.GetChild(1).transform.GetChild(0).gameObject);
        flagok = false;
    }
    void startcook()
    {
        coock(ic);
    }
    void coock(int c)
    {
        Debug.Log("Text");
        int count = list.Count;
        for (int i = 0; i < count;i++)
        {
            g = list[i].gameObject;
            Debug.Log("Test" + g.tag);
            if( g.tag== "meat")
            {
                list.Remove(g.GetComponent<Item>());
                Destroy(firepanel.transform.GetChild(0).transform.GetChild(0).gameObject);
                Debug.Log("EndCoock");
                Item it = reciples[c].materials.recmat[0].GetComponent<Item>();
                it.drop = "coockfood";
                GameObject img = Instantiate<GameObject>(container);
                img.transform.SetParent(firepanel.transform.GetChild(1).transform);
                img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.sprite);
                img.GetComponent<Drag>().item = it;
                list.Add(it);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            firee = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        firee = false;
    }
}
