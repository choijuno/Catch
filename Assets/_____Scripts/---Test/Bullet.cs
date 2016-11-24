using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public GameObject lookStick;

	private bool shootCheck;

	public float Timer;
	float timer_in;
	public float Speed;
	float speed_in;

	MeshRenderer myMesh;

	public GameObject Button;



	void Start() {
		
	}

	void Awake() {
		lookStick = GameObject.Find ("shootStartPos");
		Button = GameObject.Find ("Button");
		resetPos ();

		timer_in = Timer;
		speed_in = Speed * 0.01f;


	}

	void Update() {
		if (!shootCheck) {
			resetPos ();
			shootCheck = true;
		}


		transform.Translate (0, 0, speed_in);

		timer_in = timer_in - Time.deltaTime;

		if (timer_in <= 0) {
			shootCheck = false;
			transform.localPosition = new Vector3 (0, 100, 0);
			timer_in = Timer;
			//StartCoroutine ("ready");
			gameObject.SetActive (false);
			//StopCoroutine ("shoot");
		}
	}

	public void resetPos(){
		 
		transform.localPosition = lookStick.transform.position;
		transform.rotation = new Quaternion (lookStick.transform.rotation.x,lookStick.transform.rotation.y,lookStick.transform.rotation.z,lookStick.transform.rotation.w);
	}






	void OnTriggerEnter(Collider target){
		


	}
}
