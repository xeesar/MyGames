using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCraftControl : MonoBehaviour {

    //Craft
    public GameObject categories;
    public GameObject elements;
    public GameObject descriptions;
    public Elements element = new Elements();
    public Descriptions Desc = new Descriptions();
    public BlurEffect blur;
    CursorLockMode wantedMode;
    int predid = 0;
    int predid2 = 0;
    //Elements
    [System.Serializable]
    public class Elements
    {
        public Panels[] panel;
    }
    [System.Serializable]
    public class Panels
    {
        public GameObject panels;
    }
    //Descriptions
    [System.Serializable]
    public class Descriptions
    {
        public DescElem[] descinf;
    }
    [System.Serializable]
    public class DescElem
    {
        public GameObject descElem;
    }
    //End.
    void Start () {
        categories.SetActive(false);
        elements.SetActive(false);
        descriptions.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (categories.activeSelf)
            {
                categories.SetActive(false);
                elements.SetActive(false);
                descriptions.SetActive(false);
            }
            else
            {
                categories.SetActive(true);
            }
        }
    }

    public void ButtonClick(int id)
    {
        element.panel[predid].panels.SetActive(false);
        predid = id;
        elements.SetActive(true);
        element.panel[id].panels.SetActive(true);
    }

    public void ButtonClick2(int id)
    {
        Desc.descinf[predid2].descElem.SetActive(false);
        predid2 = id;
        descriptions.SetActive(true);
        Desc.descinf[id].descElem.SetActive(true);
    }
}
