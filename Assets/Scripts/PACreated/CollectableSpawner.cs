using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{

    [SerializeField] GameObject SpawnCollectItem;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> spawnpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("CollectSpawnLocation"));


        GameObject NewItem;
        int randomSpawn =0;
        Vector3 SpawnLoc;
        for (int i =0; i < 5; i++)
        {
            randomSpawn=Random.Range(0, spawnpoints.Count);
            SpawnLoc = spawnpoints[randomSpawn].transform.position;
            NewItem = Instantiate(SpawnCollectItem, SpawnLoc, Quaternion.Euler(new Vector3(90,0,0)));
            if (!NewItem.activeSelf)
            {
                NewItem.SetActive(true);
            }

            NewItem.transform.SetParent(spawnpoints[randomSpawn].transform, true);
            spawnpoints.RemoveAt(randomSpawn);
            if(spawnpoints.Count == 0)
            {
                break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
