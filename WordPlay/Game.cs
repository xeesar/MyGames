using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public GameObject soundoptions;
    public GameObject options;
    public Animator anim;
    public InputField infd;
    public Text titletext;
    public GameObject titlet;
    public Text posledslowo;
    public Text posledbukwa;
    public GameObject posleds;
    public GameObject posledb;
    public Button button;
    public Text User;
    public Text Score;
    public AudioClip[] sound;
    public AudioSource soundplayer;
    //Timer-----------------------
    public Text Timer;
    public GameObject TimerObject;
    int timecounter = 0;
    bool timerflag = true;
    //----------EndTimer---------
    string[] word;
    string fileName;

    char[] text;
    List<string> wordslist = new List<string>();
    bool flag = false;
    bool botflag = false;
    bool flag1 = false;
    string BotWord;
    int score = 0;
    public GameObject comphod;
	void Start () {
        TimerObject.SetActive(true);
        timecounter = 0;
        InvokeRepeating("Time", 1f, 1f);
        comphod.SetActive(false);
        options = GameObject.Find("Options");
        soundoptions = GameObject.Find("SoundOPtions");
        soundplayer.volume = soundoptions.GetComponent<SoundCOntroller>().sound;
        word = new string[options.GetComponent<OptionGame>().WordsColl];
        fileName = options.GetComponent<OptionGame>().FileName;
        anim.SetBool("flag", false);
        titlet.SetActive(false);
        posleds.SetActive(false);
        posledb.SetActive(false);
        int counter = 0;
        StreamReader file = new StreamReader(fileName,Encoding.Default);
        while ((word[counter] = file.ReadLine()) != null)
        {
            counter++;
        }
        file.Close();
    }
	
	void Update () { 
        if(!soundplayer.isPlaying)
        {
            int x = Random.Range(0, 3);
            soundplayer.clip = sound[x];
            soundplayer.Play(); 
        }
        if(timecounter==60)
        {
            TimerObject.SetActive(false);
            timerflag = false;
            timecounter = 0;
            comphod.SetActive(true);
            Timer.text = timecounter.ToString();
            Invoke("CompFind", 5f);
        }
    }


    void Time()
    {
        if (timerflag == true)
        {
            timecounter += 1;
            Timer.text = timecounter.ToString();
        }
    }

    public void Enter()
    {
            text = new char[infd.text.Length];
        if (infd.text != "" && infd.text != " ")
        {
            if (posledb.active)
            {
                if (posledbukwa.text != infd.text[0].ToString())
                {
                    flag1 = false;
                }
                else flag1 = true;
            }
            else flag1 = true;
            if (flag1)
            {
                Find();
                if (flag)
                {
                    posledslowo.text = infd.text;
                    text = infd.text.ToCharArray();
                    if (infd.text[infd.text.Length - 1].ToString() == "ь"||infd.text[infd.text.Length - 1].ToString() == "ы") posledbukwa.text = infd.text[infd.text.Length - 2].ToString();
                    else posledbukwa.text = infd.text[infd.text.Length - 1].ToString();
                    User.text = infd.text;
                    wordslist.Add(infd.text);
                    infd.text = "";
                    button.enabled = false;
                    score++;
                    Score.text = score.ToString();
                    posledb.SetActive(true);
                    posleds.SetActive(true);
                    timerflag = false;
                    TimerObject.SetActive(false);
                    comphod.SetActive(true);
                    Invoke("CompFind", 5f);
                }
                else
                {
                    titlet.SetActive(true);
                    anim.SetBool("flag", true);
                    Invoke("AnimOff", 1f);
                    Invoke("activefolse", 2f);
                }
            }
            else
            {
                titletext.text = "Слово должно начинаться на последнюю букву предыдущего слова";
                infd.text = "";
                button.enabled = true;
                titlet.SetActive(true);
                anim.SetBool("flag", true);
                Invoke("AnimOff", 1f);
                Invoke("activefolse", 2f);
                flag1 = true;
            }
        }
    }

    void AnimOff()
    {
        anim.SetBool("flag", false);
    }

    void activefolse()
    {
        titlet.SetActive(false);
    }

    void Find()
    {
        for(int i = 0;i<word.Length;i++)
        {
            if (infd.text == word[i])
            {
                flag = true;

                foreach (var n in wordslist)
                {

                    if (infd.text == n)
                    {
                        flag = false;
                        titletext.text = "Такое слово уже было введено";
                        break;
                    }
                }
                break;
            }
            else
            {
                titletext.text = "Такого слова нет либо есть грамматические ошибки";
                if(User.text=="")
                {
                    posledb.SetActive(false);
                    posleds.SetActive(false);
                }
                flag = false;
            }
        }
    }

    void FindBot(char lastbukwa)
    {
        Debug.Log(lastbukwa);

        for (int i = 0; i < word.Length; i++)
        {
            botflag = false;
            BotWord = word[i];
            if (lastbukwa == BotWord[0])
            {
                botflag = true;
                Debug.Log("1");
                foreach(var n in wordslist)
                {
                    Debug.Log(n);
                    if (n == BotWord)
                    {
                        botflag = false;
                        break;
                    }
                    else botflag = true;
                }
            }
            if (botflag) break;
        }
        if(!botflag)
        {
            titletext.text = "Я не знаю больше слов на букву " + posledbukwa.text;
            titlet.SetActive(true);
            anim.SetBool("flag", true);
            Invoke("AnimOff", 1f);
            Invoke("activefolse", 2f);
        }
    }

    void CompFind()
    {
        comphod.SetActive(true);
        botflag = false;
        char lastword = posledbukwa.text[0];
        FindBot(lastword);
        if (botflag == true)
        {
            posledslowo.text = BotWord;
            if (BotWord[BotWord.Length - 1].ToString() == "ь" || BotWord[BotWord.Length - 1].ToString() == "ы")
            {
                posledbukwa.text = BotWord[BotWord.Length - 2].ToString();
            }
            else
            {
                Debug.Log(BotWord);
                posledbukwa.text = BotWord[BotWord.Length - 1].ToString();
            }
            User.text = BotWord;
            wordslist.Add(BotWord);
            button.enabled = true;
            comphod.SetActive(false);
            timerflag = true;
            timecounter = 0;
            TimerObject.SetActive(true);
        }
    }
}
