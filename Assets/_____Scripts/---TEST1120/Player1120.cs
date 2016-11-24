using UnityEngine;
using System.Collections;
using CnControls;

public class Player1120 : MonoBehaviour {

	public GameObject Controller;
    GameObject look;

    int chaNum = 1;

	public float speed;


	public Rigidbody mybody;


	public NetworkView networkview;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	public Renderer myrenderer;

    float HP=5.0f;
    bool safe = false;


    
    GameObject gameCamera;

	void Start(){
		
		if (Application.loadedLevelName == "Test1120") {
			Controller = GameObject.Find ("Button");
			Controller.GetComponent<CnControls.SimpleButton> ().chaNum = chaNum;
		}

		speed = speed * 0.01f;
		mybody = GetComponent<Rigidbody> ();
		if (networkview.isMine) {
			gameCamera = GameObject.Find ("Main Camera");
			gameCamera.GetComponent<CamControl1120> ().Player = gameObject;
			gameCamera.GetComponent<CamControl1120> ().signCheck = true;
		}
        Debug.Log(HP);
        Debug.Log(safe);
        
    }

    void Update()
	{
        if(HP == 0)
        {
            //death
        }


		if (networkview.isMine) {
			InputMovement ();
			//InputColorChange ();
		} else {    
			SyncedMovement ();
		}

        if (safe == true)
        {
            HP = HP - (1 * Time.deltaTime);
            Debug.Log("죽어감" + HP);
        }
        if (safe == false && HP < 5.0f && HP > 0.0f)
        {
            HP = HP + (1 * Time.deltaTime);
            Debug.Log("살아남" + HP);
        }
        if (HP > 5)
        {
            HP = 5;
        }
        if (safe == true && HP <= 0)
        {
            HP = 0;
            Debug.Log("끝" + HP);
        }
    }


	void InputMovement()
	{
		mybody.transform.position = new Vector3 (mybody.transform.position.x + (CnInputManager.GetAxis ("Horizontal") * speed), 0.7f, mybody.transform.position.z + (CnInputManager.GetAxis ("Vertical") * speed));
        
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

    void look_at()
    {
       // go.GetCompont<Point>().
    }

}
