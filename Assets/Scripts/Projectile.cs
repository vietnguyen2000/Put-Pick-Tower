using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    LiveObject target; // target to which the projectile will fly
    float projectileSpeed; // moving speed of the projectile
    FiringController source; // The source which sends the projectile to the target
    public void SetupTarget(FiringController source, LiveObject tg, float speed)
    {
        this.target = tg;
        this.source = source;
        projectileSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Only move toward the target if it's alive
            if (target.LivingStatus == LiveObject.Status.Alive)
            {
                Vector3 dir = (target.transform.position - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, projectileSpeed*Time.deltaTime);
                //rb.velocity = dir * projectileSpeed;

                // If the projectile reaches the target
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(target.transform.position.x, target.transform.position.y)) <= 0.1f)
                {
                    // Bring the projectile back to the pool
                    transform.position = source.poolPosition; 
                    source.projectilePool.Enqueue(gameObject.transform);
                    // Inform FiringController that it has reached the target
                    source.TargetReached(target);
                }
            }
            else
            {
                // Bring the projectile back to the pool
                target = null;
                transform.position = source.poolPosition;
                source.projectilePool.Enqueue(gameObject.transform);
            }
        }

    }
}
