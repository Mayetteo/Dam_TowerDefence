using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    public LevelManager levelManager;
    public List<WaveData> waveDatas = new List<WaveData>();
    public float waveCooldownTime = 10; 
    float waveTimer;
    bool isSpawning = false; 
    bool isWaitingNextWave = true; 
    int enemiesWaveSpawned = 0;

    int currentWaveIndex = 0; 

    List<EnemyBase> spawnedEnemies = new List<EnemyBase>();
    float spawRateTime = 10f;
    float spawnRateTimer = 0;

    void Update()
    {
        if (isWaitingNextWave)
        {
            waveTimer += Time.deltaTime;
            if(waveTimer > waveCooldownTime)
            {
                isWaitingNextWave = false;
                isSpawning = true; 
                StartWave();
            }
        }

        if (isSpawning )
        {
            spawnRateTimer += Time.deltaTime;
            if(spawnRateTimer > spawRateTime)
            {
                spawnRateTimer = 0; 
                EnemyBase enemyToSpawn = waveDatas[currentWaveIndex].GetRandomEnemy();
                EnemyBase newEnemy = Instantiate(enemyToSpawn, levelManager.pathPoints[0].position, Quaternion.identity);
                spawnedEnemies.Add(newEnemy);
                enemiesWaveSpawned++;
                if(enemiesWaveSpawned >= waveDatas[currentWaveIndex].enemiesAmount)
                {
                    isSpawning = false;
                }
            }
        }

    }

    void StartWave()
    {
        spawRateTime = waveDatas[currentWaveIndex].GetSpawnRateTime();
        spawnRateTimer = 0;
        enemiesWaveSpawned = 0;

    }

    public void OnEnemyDie(EnemyBase deadEnemy)
    {
        if (spawnedEnemies.Contains(deadEnemy))
        {
            spawnedEnemies.Remove(deadEnemy);
        }
        //Debug.Log("nemici rimasti "+spawnedEnemies.Count);
        if(spawnedEnemies. Count <= 0)
        {
            waveTimer = 0;
            currentWaveIndex++;
            if(currentWaveIndex < waveDatas.Count)
            {
                isWaitingNextWave = true; 
            }
        }
    }


}
