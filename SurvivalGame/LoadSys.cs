using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadSys : MonoBehaviour {

    public  bool isload = false;

    private void Start()
    {
        DontDestroyOnLoad(this);
        if (File.Exists(Application.dataPath + "/saves/playersave.sv"))
            isload = true;
        else
            isload = false;
    }

    public void ifload()
    {
        Application.LoadLevel(1);
    }
}
