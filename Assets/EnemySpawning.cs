using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    [SerializeField] GameObject flyerEnemy;

    private float flyerInterval = 10f;
    private int lastScore;
    private int difficultyScaler = 30;

    void Start()
    {
        StartCoroutine(spawnEnemy(flyerInterval, flyerEnemy));
        lastScore = 0;
    }


    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        Debug.Log("Spawned enemy");
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);
        if (interval != 1) {
            if (ScoreScript.scoreVal >= lastScore + difficultyScaler) {
            interval--;
            lastScore = ScoreScript.scoreVal;
            }
        }
        StartCoroutine(spawnEnemy(interval, enemy));
    }
    
}
