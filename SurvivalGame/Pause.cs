using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    public GameObject pausemenu;
    public GameObject optips;
    public GameObject label;
    public GameObject[] optmass = new GameObject[3];
    public Dropdown[] opt = new Dropdown[5];
    private int pred;
    private bool fl = false;
    public Slider slider;

    void Start () {
        pausemenu.SetActive(false);
        optips.SetActive(false);
        label.SetActive(true);
	}
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausemenu.active)
            {
                pausemenu.SetActive(false);
            }
            else
            {
                pausemenu.SetActive(true);
                optips.SetActive(false);
                label.SetActive(true);
            }
        }
	}
    public void AudioSetings()
    {
        AudioListener.volume = slider.value;
    }
    public void Settings(int i)
    {
        //Quality
        if (i == 0)
        {
            QualitySettings.SetQualityLevel(opt[i].value);
        }
        else if (i == 1)
        {
            QualitySettings.shadowCascades = opt[i].value + 1;
        }
        else if (i == 2)
        {
            if (opt[i].value == 0)
            {
                QualitySettings.antiAliasing = 0;
            }
            else if (opt[i].value == 1)
            {
                QualitySettings.antiAliasing = 2;
            }
            else if (opt[i].value == 2)
            {
                QualitySettings.antiAliasing = 4;
            }
            else if (opt[i].value == 3)
            {
                QualitySettings.antiAliasing = 8;
            }
        }
        else if (i == 3)
        {
            if (opt[i].value == 0)
            {
                QualitySettings.shadowDistance = 0;
            }
            else if (opt[i].value == 1)
            {
                QualitySettings.shadowDistance = 50;
            }
            else if (opt[i].value == 2)
            {
                QualitySettings.shadowDistance = 100;
            }
            else if (opt[i].value == 3)
            {
                QualitySettings.shadowDistance = 150;
            }
        }
        else if (i == 4)
        {
            if (opt[i].value == 0)
            {
                Screen.SetResolution(800, 600, true);
            }
            else if (opt[i].value == 1)
            {
                Screen.SetResolution(1024, 768, true);
            }
            else if (opt[i].value == 2)
            {
                Screen.SetResolution(1366, 768, true);
            }
        }
    }
        //quality end
    public void ButtonClick(int count)
    {
        if (count == 1) pausemenu.SetActive(false);
        else if (count == 3) Application.LoadLevel(0);
        else if (count == 2)
        {
            optips.SetActive(true);
            optmass[2].SetActive(true);
            label.SetActive(false);
        }
    }

    public void OptButtonClick(int i)
    {
        if (fl == true)
        {
            optmass[pred].SetActive(false);
        }
        else optmass[2].SetActive(false);
        if (i<3)
        {
            optmass[i].SetActive(true);
            pred = i;
            Debug.Log(i);
            fl = true;
        }
        else if(i==3)
        {
            optips.SetActive(false);
            label.SetActive(true);
            fl = false;
        }
    }
}
