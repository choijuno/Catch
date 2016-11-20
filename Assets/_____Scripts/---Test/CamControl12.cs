using UnityEngine;
using System.Collections;

public class CamControl12 : MonoBehaviour {

    public GameObject Player;

    // Use this for initialization
    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = Player.transform.position;
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
    }
}
