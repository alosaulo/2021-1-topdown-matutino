using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{
    public SpawnController EnemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyPrefabs.isReadyToSpawn == true)
        {
            StartCoroutine("CountSpawn");
        }
    }

    IEnumerator CountSpawn()
    {
        EnemyPrefabs.isReadyToSpawn = false;
        Instantiate(EnemyPrefabs.prefabEnemy, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(EnemyPrefabs.spawnTime);
        EnemyPrefabs.isReadyToSpawn = true;
    }

}
