using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDay : MonoBehaviour {

    //150 - пол оборота солнца
    //300 - полный оборот

    //300сек - 12 часов
    //25сек -1час игровой
    //1.2 - каждую секунду добавлять

    public Text TimeText;
    public Image Weather;
    public Sprite[] WeatherMass;

    private int Hours = 15;
    private float Minutes = 0;

    private string hours;
    private string minutes;
    

	// Use this for initialization
	void Start () {
        TimeText.text = Hours.ToString() + ":" + Mathf.Round(Minutes).ToString();
        InvokeRepeating("TimeTimer",1f,1f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Hours == 23)
            Weather.sprite = WeatherMass[1];
        else if (Hours == 8)
            Weather.sprite = WeatherMass[0];
        if (Hours < 10)
            hours = "0" + Hours.ToString();
        else
            hours = Hours.ToString();

        if (Mathf.Round(Minutes) < 10)
            minutes = "0" + Mathf.Round(Minutes).ToString();
        else
            minutes = Mathf.Round(Minutes).ToString();

        TimeText.text = hours + ":" + minutes;
    }

    private void TimeTimer()
    {
        if (Hours == 24)
            Hours = 00;
        Minutes += 1.2f;
        if (Minutes >= 60)
        {
            Hours += 1;
            Minutes = 00;
        }
    }
}
