using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public GameObject dp1;
    public GameObject dont2;
    public GameObject dontDestroy;
    public Slider audioslider;
    public AudioSource audiosettings;
    public SoundCOntroller soundcontroller;
    public GameObject optiobj;



    public void Start()
    {
        audioslider.value = audiosettings.volume;
    }

    public void Update()
    {
        if(optiobj.active == true)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                optiobj.SetActive(false);
            }
        }

        if (dp1.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Escape)) dp1.SetActive(false);
        }
    }

    public void SoundContrl()
    {
        audiosettings.volume = audioslider.value;
        soundcontroller.sound = audioslider.value;
    }

    public void ButtonStarClick()
    {
        dp1.SetActive(true);
    }

    public void ButtonReturnClicl()
    {

    }

    public void ButtonOptionClicl()
    {
        optiobj.SetActive(true);
    }

    public void ButtonStand()
    {
        DontDestroyOnLoad(dontDestroy);
        DontDestroyOnLoad(dont2);
        Application.LoadLevel(1);
    }

    public void ButtonZiwot()
    {
        DontDestroyOnLoad(dont2);
        DontDestroyOnLoad(dontDestroy);
        Application.LoadLevel(1);
    }
    public void ButtonRast()
    {
        DontDestroyOnLoad(dont2);
        DontDestroyOnLoad(dontDestroy);
        Application.LoadLevel(1);
    }

    public void ButtonExitClick()
    {
        Application.Quit();
    }
}
