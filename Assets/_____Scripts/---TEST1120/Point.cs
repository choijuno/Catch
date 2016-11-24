using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {

    float point_time = 5.0f;


	// Use this for initialization
	void Start () {
        Debug.Log("Point_Script");
    }
	
	// Update is called once per frame
	void Update () {
        if (point_time < 0)
        {
            Debug.Log("clear");
            //clear
            point_time = 0;
        }	
	}


    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            point_time = point_time - (1 * Time.deltaTime);
            Debug.Log(point_time);            
        }
    }
}
