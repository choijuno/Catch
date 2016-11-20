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
		while (true) {
			transform.position = new Vector3(Mathf.Lerp(transform.position.x,Player.transform.position.x,0.1f), transform.position.y,Mathf.Lerp(transform.position.z,Player.transform.position.z,0.1f));
			yield return new WaitForSeconds (0.006f);
		}
	}
}
