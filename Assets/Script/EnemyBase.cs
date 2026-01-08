using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    LevelManager levelManager;
    int targetPathIndex = 0; 

    public Rigidbody2D rb2d;

    public float speed = 20f;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public bool isSideSpriteFacingRight;

    void Awake()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    void Update()
    {

        Vector3 targetPosition = levelManager.pathPoints[targetPathIndex].position; 

        if(Vector3.Distance(transform.position, targetPosition) < 0.15f)
        {
            if(targetPathIndex + 1 < levelManager.pathPoints.Count)
            {
                targetPathIndex++;
                
            }
            
        }

    }

    void FixedUpdate()
    {

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
}
