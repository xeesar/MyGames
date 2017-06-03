using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {
	public Transform canvas;
    public Transform help;
    public Transform old;
	public GameObject player;
    public GameObject fire;
	public Item item;


	// Use this for initialization
	void Start () 
	{
		canvas = GameObject.Find("Canvas").transform;
		player = GameObject.FindGameObjectWithTag("Player");
        fire = GameObject.FindGameObjectWithTag("Fire1");
    }

	public void OnBeginDrag (PointerEventData eventData)
	{
		old = transform.parent;
		transform.SetParent(canvas);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}
	
	public void OnEndDrag (PointerEventData eventData)
	{
		GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == canvas)
        {
            transform.SetParent(old);
        }
        if (item.drop == "coock")
        {
            fire.BroadcastMessage("use", this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item.drop == "food")
            {
                player.BroadcastMessage("use", this);
                player.BroadcastMessage("Audio", this);
            }
            else if (item.type == "otrava")
            {
                player.BroadcastMessage("use", this);
            }
            else if (item.drop == "hand")
            {
                player.BroadcastMessage("use", this);
            }
            else if (item.drop == "drop")
            {
                player.BroadcastMessage("remove", this);
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item.drop == "coock")
            {
                fire.BroadcastMessage("use", this);
            }
            if (item.drop == "coockfood")
            {
                Item it = item;
                player.BroadcastMessage("ad", it);
                fire.BroadcastMessage("Remove", it);
            }
        }
    }
}
