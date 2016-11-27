using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnController : MonoBehaviour {

	//menu_panels
	public GameObject justOpen;
	public GameObject justClose;

	public GameObject option_panel;
	public GameObject vs_panel;
	public GameObject room_panel;
	public GameObject search_panel;

	public Text bgm_txt;
	public Text sound_txt;

	void _00__open_btn () {
		justOpen.SetActive (true);
	}
	void _00__close_btn () {
		justClose.SetActive (false);
	}
	void _00__openClose_btn () {
		justOpen.SetActive (true);
		justClose.SetActive (false);
	}
	void _00__() {

	}


	public void _01__solo_btn () {
		Application.LoadLevel ("Catch_1");
	}
	public void _01__vs_btn () {
		_00__open_btn ();
	}
	public void _01__option () {
		_00__open_btn ();
	}
	public void _01__exit () {
		Application.Quit ();
	}

	public void _02__BGM_btn () {
		if (bgm_txt.text.Substring (5, 1) == "n") {
			bgm_txt.text = "BGM Off";
		} else {
			bgm_txt.text = "BGM On";
		}
	}
	public void _02__Sound_btn () {
		if (sound_txt.text.Substring (7, 1) == "n") {
			sound_txt.text = "Sound Off";
		} else {
			sound_txt.text = "Sound On";
		}
	}
	public void _02__back_btn () {
		_00__close_btn ();
	}

	public void _11__host_btn () {
		_00__open_btn ();
	}
	public void _11__guest_btn () {
		_00__open_btn ();
	}

	public void _12__back_btn () {
		_00__openClose_btn ();
	}
}
