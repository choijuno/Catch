using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject controller;
	public GameObject Black_Panel;

	void Start () {
		

		switch (Application.loadedLevelName) {

		case "00_rogo":
			
			Invoke ("rogoNext", 4f);

			break;

		case "01_Main":
			Black_Panel.SetActive (true);
			StartCoroutine ("mainup");

			break;

		case "Catch_1":
			Black_Panel.SetActive (true);

			StartCoroutine ("gameup");	

			break;

		}
	}


	IEnumerator mainup() {
		while(true) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit ();
			}
			yield return new WaitForSeconds(0.006f);
		}
	}

	IEnumerator gameup() {
		while(true) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Time.timeScale = 1;
				Application.LoadLevel ("01_Main");
			}
			yield return new WaitForSeconds(0.006f);
		}
	}

	void playerSet() {
		

	}


	void rogoNext() {
		Application.LoadLevel ("01_Main");
	}
}
