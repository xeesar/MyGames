using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InfoBar : MonoBehaviour
{
    public GameObject pause;
    public MotionBlur mv;
    public GameObject inv;
    public Image staminabar;
    public Image hungrybar;
    public Image thirstbar;
    public Image healbar;
    public GameObject craft;
    private float puuk = 100;
    float heal = 100;
    float stamina = 100;
    float hungry = 100;
    float thirst = 100;
    float cold = 0;
    int minus = 1;
    public GameObject player;
    private HandItem item;
    public Transform rHand;

    CursorLockMode wantedMode;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        staminabar.fillAmount = 1;
        hungrybar.fillAmount = 1;
        thirstbar.fillAmount = 1;
        healbar.fillAmount = 1;
        InvokeRepeating("minusFood", 5f, 5f);
        InvokeRepeating("minusStamina", 1f, 1f);
    }
    void minusFood()
    {
        hungry -= 1;
        thirst -= 1;
        if (hungry < 50)
        {
            heal -= minus;
        }
        else if (thirst < 50)
        {
            minus += 1;
            heal -= minus;
        }
        else if (thirst > 50)
        {
            minus = 1;
        }
        else if (hungry > 50)
        {
            minus = 1;
        }
    }
    public void minHeal(int Count)
    {
        heal = heal - Count;
    }
    /*public void addHeal(int Count)
    {
        heal += Count;
    }*/
    void minusStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift)&&Input.GetKey(KeyCode.W))
        {
            if (stamina > 0)
            {
                stamina -= 5;
            }
        }
        else if (stamina < 100)
        {
            stamina += 1;

        }
    }
    public void addFood(int count,string options)
    {
        if (options == "hungry")
        {
            hungry += count;
            if (hungry > 100)
            {
                hungry = 100;
            }
        }
        else if (options == "heal")
        {
            hungry += count;
            if (hungry > 100)
            {
                hungry = 100;
            }

            heal += count;
            if(heal>100)
            {
                heal = 100;
            }
        }

    }
    public void minFood(int cout)
    {
        hungry -= cout;
        heal -= cout;
        if (hungry < 0)
        {
            hungry = 0;
        }
    }
    public void addHand(HandItem it)
    {
        if (item != null)
        {
            item.transform.SetParent(null);
            item.gameObject.AddComponent<Rigidbody>();
        }
        it.transform.SetParent(rHand);
        it.transform.position= rHand.position;
        //it.transform.rotation = Quaternion.Euler(it.rotation);
        Destroy(it.GetComponent<Rigidbody>());
        item = it;
    }
    // Update is called once per frame
    void Update()
    {
        if (inv.active | craft.active|pause.active)
        {
            wantedMode = CursorLockMode.None;
            Cursor.lockState = wantedMode;
            Cursor.visible = (CursorLockMode.Locked != wantedMode);
        }
        else
        {
            wantedMode = CursorLockMode.Locked;
            Cursor.lockState = wantedMode;
            Cursor.visible = (CursorLockMode.Locked != wantedMode);
        }
        staminabar.fillAmount = stamina/100;
        hungrybar.fillAmount = hungry/100;
        thirstbar.fillAmount = thirst/100;
        healbar.fillAmount = heal / 100;

        if (staminabar.fillAmount > 1)
        {
            staminabar.fillAmount = 1;
        }
        else if (staminabar.fillAmount < 0)
        {
            staminabar.fillAmount = 0;
        }

        if (thirstbar.fillAmount > 1)
        {
            thirstbar.fillAmount = 1;
        }
        else if (thirstbar.fillAmount < 0)
        {
            thirstbar.fillAmount = 0;
        }
        if (healbar.fillAmount <=0)
        {
            wantedMode = CursorLockMode.None;
            Cursor.lockState = wantedMode;
            Cursor.visible = (CursorLockMode.Locked != wantedMode);
            Application.LoadLevel(0);
        }
        if(heal<=50)
        {
            mv.enabled = true;
            mv.blurAmount = 0.4f;
        }
        else if(heal<=10)
        {
            mv.blurAmount = 0.7f;
        }
        else if(heal > 50)
        {
            mv.enabled = false;
        }
    }
}
