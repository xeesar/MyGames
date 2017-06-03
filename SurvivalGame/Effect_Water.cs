using UnityEngine;
using System.Collections;

public class Effect_Water : MonoBehaviour {

    public float distance;
    public BlurEffect blur;
    public bool water = false;

	void Start () {

	}

	void Update () {
        if (water == true)
        {
            blur.enabled = true;
        }
        else if(water == false)
        {
            blur.enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            water = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag =="Player")
        water = false;
    }
}
