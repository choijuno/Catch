using UnityEngine;
using System.Collections;

public class CamControl1120 : MonoBehaviour {

    public GameObject Player;
	public bool signCheck = false;

	void Start () {
		StartCoroutine ("Finding");
    }

	IEnumerator Finding() {
		while (true) {

			if (signCheck) {
				StartCoroutine ("Follow");
				signCheck = false;
			} 

			yield return new WaitForSeconds (0.006f);
		}
	}

	IEnumerator Follow() {
		yield return new WaitForSeconds (2f);
		while (true) {

			transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
			yield return new WaitForSeconds (0.006f);
		}
	}
}
