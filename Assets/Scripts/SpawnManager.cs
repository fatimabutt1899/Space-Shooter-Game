using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    private bool _StopSpawning=false;
    [SerializeField]
    private GameObject[] powerup;
    [SerializeField]
    // Start is called before the first frame update
     public void StartSpawning()
        {
        StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnPowerupRoutine());

        }

    private IEnumerator SpawnEnemyRoutine()// for enemy
    {
        yield return new WaitForSeconds(3.0f);
        while(_StopSpawning==false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.15f, 9.15f), 7.1f, 0);
            GameObject NewEnemy= Instantiate(_EnemyPrefab,posToSpawn,Quaternion.identity);
            NewEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }
    private IEnumerator SpawnPowerupRoutine() // for power ups
    {
        yield return new WaitForSeconds(3.0f);
        while(_StopSpawning==false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.15f, 9.15f), 7.1f, 0);
            Instantiate(powerup[Random.Range(0, 3)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f,8f));
        }
    }

    public void OnPlayerDeath()
    {
        _StopSpawning = true;
    }

}
