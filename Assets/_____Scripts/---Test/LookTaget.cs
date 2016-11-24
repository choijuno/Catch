using UnityEngine;
using System.Collections;

public class LookTaget : MonoBehaviour {
	public GameObject lookStick;
	// Use this for initialization
	void Start () {
		lookStick = GameObject.Find ("lookStick").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = new Quaternion (lookStick.transform.rotation.x,lookStick.transform.rotation.y,lookStick.transform.rotation.z,lookStick.transform.rotation.w);
	}
}
