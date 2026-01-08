using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Settings")]
    public float range = 1.5f; 
    public int damage = 1;
    public float shootRate = 1f;

    float shootTimer = 0;

    [Header("Components")]
    public SpriteRenderer towerSpriteRenderer;
    public SpriteRenderer rangeSpriteRenderer;
    public TowerTrigger trigger; 

    void Awake()
    {
        rangeSpriteRenderer.transform.localScale = new Vector3 (range * 2, range *2, 1);
    }

    void Update()
    {
        if(shootTimer < shootRate)
        {
            shootTimer += Time.deltaTime;
        }

        if(trigger.IsEnemyInView() && shootTimer >= shootRate)
        {            
            Shoot();
            shootTimer = 0;
        }
    }

    public void Shoot()
    {
        List <EnemyBase> enemiesInView = trigger.GetEnemiesInView();

        int biggerPathIndex = -1; 
        EnemyBase target = null;
        foreach (EnemyBase enemy in enemiesInView)
        {
            if(enemy.GetTargetPathIndex() > biggerPathIndex)
            {
                biggerPathIndex = enemy.GetTargetPathIndex();
                target = enemy; 
            }
        }

        if (target!=null)
        {
            target.Hit(damage);
        }
        
        // Decide the target enemy 
        Debug.Log("BAAANG");
    }

}
