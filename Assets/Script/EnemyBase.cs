using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    LevelManager levelManager;
    bool active = true; 
    int targetPathIndex = 0; 

    public Rigidbody2D rb2d;

    public float speed = 20f;
    public int playerHitDamage =1;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public bool isSideSpriteFacingRight;

    public int MaxHP = 2;

    int currentHP = 2;

    bool isDead = false;

    void Awake()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
        currentHP = MaxHP;
    }

    public int GetTargetPathIndex()
    {
        return targetPathIndex;
    }

    void Update()
    {

        if (!active)
        {
            return; 
        }

        Vector3 targetPosition = levelManager.pathPoints[targetPathIndex].position; 

        if(Vector3.Distance(transform.position, targetPosition) < 0.15f)
        {
            if(targetPathIndex + 1 < levelManager.pathPoints.Count)
            {
                targetPathIndex++;

            }
            else
            {
                TargetReached();
            }
            
        }

    }

    void FixedUpdate()
    {
        if (!active)
        {
            return;
        }

        Vector3 targetPosition = levelManager.pathPoints[targetPathIndex].position;
        Vector3 currentPosition = transform.position; 

        Vector3 direction = (targetPosition - currentPosition).normalized;

        rb2d.linearVelocity = direction * speed;

        animator.SetFloat("XNormalizedSpeed", direction.x);
        animator.SetFloat("YNormalizedSpeed", direction.y);

        if (isSideSpriteFacingRight)
        {
            spriteRenderer.flipX = (direction.x < 0 );
        }
        else
        {
            spriteRenderer.flipX = (direction.x > 0 );
        }

    }

    public void Hit(int damage)
    {
        currentHP -= damage;
        spriteRenderer.color = Color.red; 
        Invoke("ResetColor", 0.1f);
        if(currentHP <= 0)
        {
            DestoryMe();            
        }
    }


    void ResetColor()
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void DestoryMe()
    {
        EnemySpawner enemySpawner = FindFirstObjectByType<EnemySpawner>();
        if(enemySpawner != null)
        {
            enemySpawner.OnEnemyDie(this);
        }
        active = false;
        rb2d.linearVelocity = Vector3.zero; 
        isDead = true; 
        animator.SetBool("Dead", isDead);
        Invoke("Despawn",1f);

        }

    public void TargetReached()
    {
        active = false;
        DestoryMe();
        PlayerManager playerManager = FindFirstObjectByType<PlayerManager>();
        if(playerManager != null)
        {
            playerManager.PlayerHit(playerHitDamage);
        }
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

}
