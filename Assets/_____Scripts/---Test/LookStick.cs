using UnityEngine;
using System.Collections;

public class LookStick : MonoBehaviour {
	public NetworkView nv;
	public GameObject Stick;


	void Start () {
	
	}
	void Update () {
		if (Mathf.Abs(Stick.transform.localPosition.x-146.5f) >= 10 || Mathf.Abs(Stick.transform.localPosition.y-140f) >= 10)
			transform.LookAt (Stick.transform);
	
	}
}
