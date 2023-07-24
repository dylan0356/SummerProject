using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    [SerializeField] GameObject flyerEnemy;

    private float flyerInterval = 2.5f;


    
    void Start()
    {
        StartCoroutine(spawnEnemy(flyerInterval, flyerEnemy));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
    
}
