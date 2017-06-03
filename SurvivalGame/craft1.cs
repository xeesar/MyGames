using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class craft1 : MonoBehaviour {

    public Item[] masob;
    public Canvas craft;
    public Text text;
    public Inventory invent;
    public GameObject inventory;
    public GameObject container;
    public GameObject anitext;
    public GameObject anitext2;
    int matRec;
    int z;
    public Reciple[] reciples = new Reciple[5];
    int bi;

    [System.Serializable]
   public class Reciple
    {
        public GameObject item;
        public RecipleMaterial materials;

    }

    [System.Serializable]
    public class RecipleMaterial
    {
        public GameObject[] recmat = new GameObject[5];

    }

    void Start () {
        Canvas craft = GetComponent<Canvas>();
        Text text = GetComponent<Text>();
    }
	
	void Update () {
	
	}
   public void Button1press(int n)
    {
        Craft(n);
    }

    public void Craft(int m)
    {
        bool craft = false;
        bi = invent.list.Count;
        matRec = reciples[m].materials.recmat.Length;
        masob = new Item[bi];
        Item[] ite = new Item[matRec];
        z = 0;
        if (invent.list.Count >= matRec)
        {
            for (int i = 0; i < matRec; i++)
            {
                GameObject obj = reciples[m].materials.recmat[i];
                bool flag = true;
                for (int j = 0; flag == true; j++)
                {
                    if (j < invent.list.Count)
                    {
                        if (invent.list[j] != null)
                        {
                            Item it = invent.list[j];
                            GameObject its = Resources.Load<GameObject>(it.prefab);
                            if (its == obj)
                            {
                                z += 1;
                                ite[i] = invent.list[j];
                                masob[i] = invent.list[j];
                                invent.list.Remove(ite[i]);
                                flag = false;
                                if (z == matRec)
                                {
                                    Item item = reciples[m].item.GetComponent<Item>();
                                    if (item != null)
                                    {
                                        craft = true;
                                        invent.list.Add(item);
                                        anitext.SetActive(true);
                                        Invoke("anibool", 3f);
                                    }
                                }
                            }
                        }
                    }
                    else break;
                }
                if (i == matRec - 1 && z != matRec)
                {
                    for (int k = 0; k < masob.Length; k++)
                    {
                        if (masob[k] != null)
                        {
                            invent.list.Add(masob[k]);
                        }
                    }
                }
            }
            if (craft == false)
            {
                anitext2.SetActive(true);
                Invoke("anibool", 3f);
            }
        }
        else
        {
            anitext2.SetActive(true);
            Invoke("anibool", 3f);
        }
    }
    //public void Respinvent()
    //{
    //    if (inventory.activeSelf)
    //    {
    //        inventory.SetActive(false);
    //        for (int i = 0; i < inventory.transform.childCount; i++)
    //        {
    //            if (inventory.transform.GetChild(i).transform.childCount > 0)
    //            {
    //                Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        int count = invent.list.Count;
    //        for (int i = 0; i < count; i++)
    //        {
    //            Item it = invent.list[i];

    //            if (inventory.transform.childCount >= i)
    //            {

    //                GameObject img = Instantiate<GameObject>(container);
    //                img.transform.SetParent(inventory.transform.GetChild(i).transform);
    //                img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.sprite);
    //                img.GetComponent<Drag>().item = it;
    //            }
    //            else break;
    //        }
    //    }
    //}
    void anibool()
    {
        if (anitext.activeSelf) { anitext.SetActive(false); }

        if (anitext2.activeSelf) { anitext2.SetActive(false); }
    }
}
