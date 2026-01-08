using System.Collections.Generic;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    List<EnemyBase> enemiesInView = new List<EnemyBase>();

    public List<EnemyBase> GetEnemiesInView()
    {
        return enemiesInView;
    }

    public bool IsEnemyInView()
    {
        return enemiesInView.Count>0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            if(enemy != null)
            {
                enemiesInView.Add(enemy);
            }     
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
           if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            if(enemy != null)
            {
                enemiesInView.Remove(enemy);
            }     
        }
    }
}
