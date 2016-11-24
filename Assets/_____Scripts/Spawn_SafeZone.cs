using UnityEngine;
using System.Collections;

public class Spawn_SafeZone : MonoBehaviour {

    public GameObject[] SpawnPoint_Safezone;

    public int Spawn_count;

    public bool arrow_test = false;


    // Use this for initialization
    void Start () {
        Debug.Log("Spawnstart");
        shuffle_point();
        spawn_zone();
                
        Debug.Log("arrow_test" + arrow_test);
    }
	
    public void shuffle_point(){
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

    public void spawn_zone()
    {
        for (int i = 0; i < Spawn_count; i++)
        {
            SpawnPoint_Safezone[i].SetActive(true);
        }
        Debug.Log("spawnzone");
    }

    
}
