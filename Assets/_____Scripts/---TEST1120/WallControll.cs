using UnityEngine;
using System.Collections;

public class WallControll : MonoBehaviour {

    public GameObject Real_Wall;
    bool Wall_st = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Wall_st == true)
        {
            Invoke("Real_Wall.SetActive(false);", 2.0f);
        }
	
	}


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Real_Wall.SetActive(true);
            Wall_st = true;            
        }
    }

    


    }
