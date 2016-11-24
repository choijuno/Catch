using UnityEngine;
using System.Collections;

public class Spawn_SafeZone : MonoBehaviour {

    public GameObject[] SpawnPoint_Safezone;

    public int Spawn_count;

    int[] clear_time= { 5, 5, 5 };

	// Use this for initialization
	void Start () {
        shuffle_point();
        spawn_zone();        
	}
	
    void shuffle_point(){
        for (int i = 0; i < SpawnPoint_Safezone.Length; i++)
        {
            GameObject target = SpawnPoint_Safezone[i];
            int randomIndex = Random.Range(i, SpawnPoint_Safezone.Length);
            SpawnPoint_Safezone[i] = SpawnPoint_Safezone[randomIndex];
            SpawnPoint_Safezone[randomIndex] = target;
        }
        for (int i = 0; i < SpawnPoint_Safezone.Length; i++)
        {
            Debug.Log(SpawnPoint_Safezone[i]);
        }                
    }

    void spawn_zone()
    {
        for (int i = 0; i < Spawn_count; i++)
        {            
            SpawnPoint_Safezone[i].SetActive(true);
                                    
        }
    }

    void thief_clear()
    {
       
    }
}
