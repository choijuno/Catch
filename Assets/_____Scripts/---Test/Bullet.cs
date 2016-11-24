using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public NetworkView networkview;

	public GameObject lookStick;

	public bool shootCheck;

	public float Timer;
	float timer_in;
	public float Speed;
	float speed_in;

	MeshRenderer myMesh;

	public GameObject Button;

	public Rigidbody mybody;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;


	[RPC]
	void Start() {
		mybody = GetComponent<Rigidbody> ();
		networkview = GetComponent<NetworkView> ();
	}
	[RPC]
	void Awake() {
		lookStick = GameObject.Find ("shootStartPos");
		Button = GameObject.Find ("Button");
		resetPos ();

		timer_in = Timer;
		speed_in = Speed * 0.01f;


	}
	[RPC]
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
	[RPC]
	public void resetPos(){
		 
		transform.localPosition = lookStick.transform.position;
		transform.rotation = new Quaternion (lookStick.transform.rotation.x,lookStick.transform.rotation.y,lookStick.transform.rotation.z,lookStick.transform.rotation.w);
	}



	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting) {
			//syncPosition = mybody.position;
			stream.Serialize (ref syncPosition);

			//syncVelocity = mybody.position;
			stream.Serialize (ref syncVelocity);
		} else {
			stream.Serialize (ref syncPosition);
			stream.Serialize (ref syncVelocity);

			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;

			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = mybody.position;
		}
	}


	void OnTriggerEnter(Collider target){
		


	}
}
