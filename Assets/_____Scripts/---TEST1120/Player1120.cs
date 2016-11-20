﻿using UnityEngine;
using System.Collections;
using CnControls;

public class Player1120 : MonoBehaviour {

    int chaNum = 1;

	public float speed = 0.02f;

	public Rigidbody mybody;


	public NetworkView networkview;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	public Renderer myrenderer;

    float police_HP=5.0f;
    bool safe = false;


    
    GameObject gameCamera;

	void Start(){
		mybody = GetComponent<Rigidbody> ();
		if (networkview.isMine) {
			gameCamera = GameObject.Find ("Main Camera");
			gameCamera.GetComponent<CamControl1120> ().Player = gameObject;
			gameCamera.GetComponent<CamControl1120> ().signCheck = true;
		}
        Debug.Log(police_HP);
        Debug.Log(safe);
        
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
        if (safe==true)
        {
            police_HP = police_HP - (1 * Time.deltaTime);
            Debug.Log("죽어감" + police_HP);
        }
        if (safe==false && police_HP < 5.0f && police_HP > 0.0f)
        {
            police_HP = police_HP + (1 * Time.deltaTime);
            Debug.Log("살아남" + police_HP);
        }
        if(police_HP>5)
        {
            police_HP = 5;
        }
        if (safe == true && police_HP <= 0)
        {
            police_HP = 0;
            Debug.Log("끝" + police_HP);
        }
    }
	void InputMovement()
	{
		mybody.transform.position = new Vector3 (mybody.transform.position.x + (CnInputManager.GetAxis ("Horizontal") * 0.05f), 0.7f, mybody.transform.position.z + (CnInputManager.GetAxis ("Vertical") * 0.05f));
        
        if (Input.GetKey(KeyCode.W))
            mybody.MovePosition(mybody.position + Vector3.forward * speed * Time.smoothDeltaTime);
		if (Input.GetKey(KeyCode.S))
			mybody.MovePosition(mybody.position - Vector3.forward * speed * Time.smoothDeltaTime);
        if (Input.GetKey(KeyCode.D))
			mybody.MovePosition(mybody.position + Vector3.right * speed * Time.smoothDeltaTime);
        if (Input.GetKey(KeyCode.A))
            mybody.MovePosition(mybody.position - Vector3.right * speed * Time.smoothDeltaTime);
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

    void OnCollisionEnter(Collision col)
    {
       
        if(col.gameObject.CompareTag("wall"))
            Debug.Log("앙");        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("safezone"))
        {
            safe = false;
            Debug.Log(safe);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("safezone"))
        {
            safe = true;
            Debug.Log(safe);
        }
    }
}
