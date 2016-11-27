using UnityEngine;
using System.Collections;

public class WallControll : MonoBehaviour {

    public GameObject Real_Wall;
    bool Wall_st = false;
    
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Wall_st == true)
        {
            Invoke("Wall_On", 3.0f);
        }

    }

    void Wall_On()
    {
        Real_Wall.SetActive(false);
        Wall_st = false;
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Real_Wall.SetActive(true);
            Wall_st = true;
            Debug.Log(Wall_st + "Wall_st");            
        }
    }

    

    


    }
