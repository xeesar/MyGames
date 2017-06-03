using UnityEngine;
using System.Collections;

public class Video : MonoBehaviour {
    public bool isLoad = false;
    
    void Start()
    {
    }

	void Update ()
    {
        if (isLoad == true)
        {
            Application.LoadLevel(1);
        }
    
	}

}
