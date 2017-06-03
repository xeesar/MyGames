using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionGame : MonoBehaviour {

    public string FileName;
    public int WordsColl;

    public void GameOption(string filename)
    {
        FileName = filename;
        if (filename == "Words.txt") WordsColl = 125439;
        else if (filename == "Ziwot.txt") WordsColl = 192;
        else if (filename == "Rast.txt") WordsColl = 127;
    }

}
