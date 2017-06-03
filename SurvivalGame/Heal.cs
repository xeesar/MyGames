using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Heal : MonoBehaviour {
    public float heal = 1;
    public Slider slider;
	// Use this for initialization
	void Start () {
        slider.value = heal;
	}

    public void minHeal(float u)
    {
        heal -= u;
    }
    
    void Update()
    {
        slider.value = heal;
        if(heal<0)
        {
            heal = 0;
        }
        else if(heal == 0)
        {
            Application.LoadLevel(0);
        }
    }
}
