﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace CnControls
{
    /// <summary>
    /// Simple button class
    /// Handles press, hold and release, just like a normal button
    /// </summary>
    public class SimpleButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
		public NetworkView nv;

		public int chaNum;

		public GameObject[] Bullets;

		public int bulletMax;

		public int bulletCount;
		int bulletNum;

		bool reloadCheck;
		public float reloadTime;
		float reloadTime_in;





        /// <summary>
        /// The name of the button
        /// </summary>
        public string ButtonName = "Shoot";




        /// <summary>
        /// Utility object that is registered in the system
        /// </summary>
        private VirtualButton _virtualButton;
        
        /// <summary>
        /// It's pretty simple here
        /// When we enable, we register our button in the input system
        /// </summary>
        private void OnEnable()
        {
            _virtualButton = _virtualButton ?? new VirtualButton(ButtonName);
            CnInputManager.RegisterVirtualButton(_virtualButton);
        }

        /// <summary>
        /// When we disable, we unregister our button
        /// </summary>
        private void OnDisable()
        {
            CnInputManager.UnregisterVirtualButton(_virtualButton);
        }

        /// <summary>
        /// uGUI Event system stuff
        /// It's also utilised by the editor input helper
        /// </summary>
        /// <param name="eventData">Data of the passed event</param>
        public void OnPointerUp(PointerEventData eventData)
		{
            _virtualButton.Release();
        }

        /// <summary>
        /// uGUI Event system stuff
        /// It's also utilised by the editor input helper
        /// </summary>
        /// <param name="eventData">Data of the passed event</param>

		public void OnPointerDown(PointerEventData eventData)
        {
			Debug.Log ("btnDown");


			if (bulletCount < bulletMax && !reloadCheck) {
				Bullets [bulletCount].SetActive (true);
				bulletCount++;

				if (bulletCount >= bulletMax) {
					reloadCheck = true;
					bulletCount = 0;
					Invoke ("reload", reloadTime_in);
				}
			}
			//nv.RPC ("RPCOnPointerDown", RPCMode.AllBuffered, Bullets[0].gameObject);


            _virtualButton.Press();
        }

		public void RPCOnPointerDown(PointerEventData eventData)
		{
			Debug.Log ("btnDown");


			_virtualButton.Press();
		}


		public void reload() {
			reloadCheck = false;
		}

    }
}