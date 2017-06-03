using UnityEngine;
using System.Collections;

public class AnimController : MonoBehaviour {

    public Animator start;
    public Animator continiue;
    public Animator options;
    public Animator exit;

	void Start () {
        start.SetBool("flag", false);
        continiue.SetBool("flag", false);
        options.SetBool("flag", false);
        exit.SetBool("flag", false);


    }

    public void StartPointerEnter()
    {
        start.SetBool("flag", true);
    }
    public void StartPointerExit()
    {
        start.SetBool("flag", false);
    }

    public void ContiniuePointerEnter()
    {
        continiue.SetBool("flag", true);
    }
    public void ContiniuePointerExit()
    {
        continiue.SetBool("flag", false);
    }

    public void OptPointerEnter()
    {
        options.SetBool("flag", true);
    }
    public void OptPointerExit()
    {
        options.SetBool("flag", false);
    }

    public void ExitPointerEnter()
    {
        exit.SetBool("flag", true);
    }
    public void ExitPointerExit()
    {
        exit.SetBool("flag", false);
    }
}
