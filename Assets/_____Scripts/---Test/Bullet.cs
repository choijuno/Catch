using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public GameObject lookStick;

	public bool shootCheck;

	public float Timer;
	float timer_in;
	public float Speed;
	float speed_in;

	MeshRenderer myMesh;


	// Use this for initialization
	void Start () {
		
		timer_in = Timer;
		speed_in = Speed * 0.01f;

		StartCoroutine ("ready");
		shootCheck = false;
		//gameObject.SetActive (false);
	}

	void resetPos(){
		transform.localPosition = lookStick.transform.position;
		transform.rotation = new Quaternion (lookStick.transform.rotation.x,lookStick.transform.rotation.y,lookStick.transform.rotation.z,lookStick.transform.rotation.w);
	}

	IEnumerator ready(){

		while (true) {

			if (shootCheck) {
				resetPos ();
				StartCoroutine ("shoot");
				StopCoroutine ("ready");
			}

			yield return null;
		}
	}


	IEnumerator shoot(){
		timer_in = Timer;

		while (true) {
			
			transform.Translate (0, 0, speed_in);
			/*
			if (!shootCheck) {
				timer_in = 0;
				resetPos ();
				StartCoroutine ("ready");
				StopCoroutine ("shoot");
			}
			*/

			timer_in = timer_in - Time.deltaTime;

			if (timer_in <= 0) {
				shootCheck = false;
				timer_in = 0;
				resetPos ();
				StartCoroutine ("ready");
				StopCoroutine ("shoot");
			}

			yield return null;
		}

	}

	void OnTriggerEnter(Collider target){
		


	}
}
