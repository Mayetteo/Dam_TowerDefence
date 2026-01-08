using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WaveData
{
    public List <EnemyBase> enemies = new List<EnemyBase>();
    public int enemiesAmount; 
    public float totalSpawnTime = 6; 

    public EnemyBase GetRandomEnemy()
    {
        int randomEnemyIndex = UnityEngine.Random.Range(0, enemies.Count  -1);
        return enemies[randomEnemyIndex];
    }

    public float GetSpawnRateTime()
    {
        float spawRateTime = totalSpawnTime / enemiesAmount;
        return spawRateTime;
    }
}
