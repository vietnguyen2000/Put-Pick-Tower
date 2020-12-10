using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    LiveObject target;
    float projectileSpeed;
    Rigidbody2D rb;
    FiringController source;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetupTarget(FiringController source, LiveObject tg, float speed)
    {
        this.target = tg;
        this.source = source;
        //transform.eulerAngles = new Vector3(0, 0)
        projectileSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {

            if (target.LivingStatus == LiveObject.Status.Alive)
            {
                Vector3 dir = (target.transform.position - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.1f);
                //rb.velocity = dir * projectileSpeed;
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(target.transform.position.x, target.transform.position.y)) <= 0.1f)
                {
                    //Debug.Log("Reach target");
                    transform.position = source.poolPosition;
                    source.projectilePool.Enqueue(gameObject.transform);
                    source.TargetReached(target);
                }
            }
            else
            {
                target = null;
                transform.position = source.poolPosition;
                source.projectilePool.Enqueue(gameObject.transform);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Hit enemy" + collision.gameObject.name);
        target = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
