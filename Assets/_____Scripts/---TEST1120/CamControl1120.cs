using UnityEngine;
using System.Collections;

public class CamControl1120 : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Follow");
    }
	
	// Update is called once per frame
	void Update () {
		
    }

	IEnumerator Follow() {
		yield return new WaitForSeconds (2f);
		while (true) {

			transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
			yield return new WaitForSeconds (0.006f);
		}
	}
}
