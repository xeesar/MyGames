using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class StartMenu : MonoBehaviour {
    public GameObject sound;
    public Button startText;
    public Button exitText;
    public Button optionText;
    public Button loadbtn;
    public GameObject optionspanel;
    public Toggle fscreen;
    public Slider slider;
    public Color Enter;
    public Color Exit;
    public Image[] img = new Image[5];
    public GameObject[] options = new GameObject[3];
    public Dropdown[] opt = new Dropdown[5];
    bool flag = false;
    bool screen = true;
    int j = 0;
    // Use this for initialization
    void Start () {
        if (!File.Exists(Application.dataPath + "/saves/playersave.sv"))
            loadbtn.enabled = false;
        else loadbtn.enabled = true;
            slider.value = AudioListener.volume;
        //Quality Settings
        opt[0].value = QualitySettings.GetQualityLevel();
        opt[1].value = QualitySettings.shadowCascades;
        //Anti Aliasing
        if (QualitySettings.antiAliasing == 0)
        {
            opt[2].value = 0;
        }
        else if (QualitySettings.antiAliasing == 2)
        {
            opt[2].value = 1;
        }
        else if (QualitySettings.antiAliasing == 4)
        {
            opt[2].value = 2;
        }
        else if (QualitySettings.antiAliasing == 8)
        {
            opt[2].value = 3;
        }
        // Shadow Distance 
        if (QualitySettings.shadowDistance == 150)
        {
            opt[3].value = 3;
        }
        else if (QualitySettings.shadowDistance == 100)
        {
            opt[3].value = 2;
        }
        else if (QualitySettings.shadowDistance == 50)
        {
            opt[3].value = 1;
        }
        else if (QualitySettings.shadowDistance == 0)
        {
            opt[3].value = 0;
        }
        for (int i = 0;i<5;i++)
        {
            img[i].color = Exit;
        }
        for (int i = 0; i < 3; i++)
        {
            options[i].SetActive(false);
        }
        Close();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        optionText = optionText.GetComponent<Button>();
	}
	void Update()
    {

    }
    // Update is called once per frame
    public void MousePress(int i)
    {
        flag = true;
        if(i!=j)
        {
            img[j].color = Exit;
            if (j >= 0 && j < 3)
            {
                options[j].SetActive(false);
            }
        }
        img[i].color = Enter;
        j = i;
        if (i >= 0 && i < 3)
        {
            options[i ].SetActive(true);
        }
    }
    public void Settings(int i)
    {
        //Quality
        if (i == 0)
        {
            QualitySettings.SetQualityLevel(opt[i].value);
        }
        else if(i == 1)
        {
            QualitySettings.shadowCascades = opt[i].value+1;
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
        else if(i == 3)
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
        else if( i == 4 )
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
        //quality end
        //Sound 
    }
    public void Toggle ()
    {
        if (screen == true)
        {
            screen = false;
        }
        else screen = true;
        Screen.fullScreen = screen;
        Debug.Log(screen);

    }
    public void audiosettings()
    {
        AudioListener.volume = slider.value;
    }
    public void Close()
    {
        optionspanel.SetActive(false);
        for(int i = 0;i<5;i++)
        {
            img[i].color = Exit;
        }
    }

    public void MouseEnter(int i)
    {
        img[i].color = Enter;
    }

    public void MouseExit(int i)
    {
        if (flag == false)
        {
            img[i].color = Exit;
        }
        else if( flag == true)
        {
            if(i!=j)
            {
                img[i].color = Exit;
            }
        }
    }

	public void ExitPress()
    {
        Application.Quit();
    }

    public void OptionPress()
    {
        optionspanel.SetActive(true);
    }

    public void StartLevel()
    {
        Application.LoadLevel(1);
    }
}
