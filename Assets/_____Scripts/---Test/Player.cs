using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 10f;

	public Rigidbody mybody;

	public NetworkView networkview;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	public Renderer myrenderer;

	GameObject gameCamera;

	void Start(){
		mybody = GetComponent<Rigidbody> ();
		gameCamera = GameObject.Find ("Main Camera");
		gameCamera.GetComponent<CamControl> ().Player = gameObject;
	}

	void Update()
	{
		if (networkview.isMine) {
			InputMovement ();
			InputColorChange ();
		} else {
			SyncedMovement ();
		}





		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.CompareTag ("left")) {
					mybody.MovePosition(mybody.position - Vector3.right * speed * Time.deltaTime);
				}
				if (hit.collider.gameObject.CompareTag ("right")) {
					mybody.MovePosition(mybody.position + Vector3.right * speed * Time.deltaTime);
				}
			}
		}

	}

	void InputMovement()
	{
		if (Input.GetKey(KeyCode.W))
			mybody.MovePosition(mybody.position + Vector3.forward * speed*10 * Time.deltaTime);

		if (Input.GetKey(KeyCode.S))
			mybody.MovePosition(mybody.position - Vector3.forward * speed*10 * Time.deltaTime);

		if (Input.GetKey(KeyCode.D))
			mybody.MovePosition(mybody.position + Vector3.right * speed*10 * Time.deltaTime);

		if (Input.GetKey(KeyCode.A))
			mybody.MovePosition(mybody.position - Vector3.right * speed*10 * Time.deltaTime);
	}

	private void InputColorChange(){
		if (Input.GetKeyDown (KeyCode.R)) {
			ChangeColorTo (new Vector3 (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f)));
		}
	}

	[RPC] void ChangeColorTo(Vector3 Color){
		myrenderer.material.color = new Color (Color.x, Color.y, Color.z, 1f);

		if (networkview.isMine) {
			networkview.RPC ("ChangeColorTo", RPCMode.OthersBuffered, Color);
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting) {
			syncPosition = mybody.position;
			stream.Serialize (ref syncPosition);

			syncVelocity = mybody.position;
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

	private void SyncedMovement(){
		syncTime += Time.deltaTime;
		mybody.position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}
}
