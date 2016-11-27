using UnityEngine;
using System.Collections;

public class Wall_OnOff : MonoBehaviour {




	
    void Awake()
    {

        Invoke("Wall_On", 2.0f);
        
    }
    
    void Wall_On()
    {
        this.gameObject.SetActive(false);
        
    }
}
