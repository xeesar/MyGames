using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public Transform target;
	public float mouseSpeed = 2f;
	Vector3 offset;
	Camera cam;

	// Use this for initialization
	void Start () {
		if(target != null) offset = transform.position - target.position;
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null) transform.position = target.position + offset;
		if(Input.GetMouseButton(1))
		{
			float mg = Input.GetAxis("Mouse X");
			float mv = Input.GetAxis("Mouse Y");
			if(mg  != 0 || mv != 0)
			{
				if(mg != 0)  transform.RotateAround(target.transform.position, Vector3.up, -mg * mouseSpeed);
				if(mv != 0)  transform.RotateAround(target.transform.position, transform.right, mv * mouseSpeed);
				offset = transform.position - target.position;
			}
		}
		float sw = Input.GetAxis("Mouse ScrollWheel");
		if(sw != 0)
		{
			cam.fieldOfView += sw * 10;
		}
	}
}
