using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MyObject
{
    // Start is called before the first frame update
    LiveObject target; // target to which the projectile will fly
    float projectileSpeed; // moving speed of the projectile
    FiringController source; // The source which sends the projectile to the target
    public ParticleSystem die;
    private bool isBreaking = false;

    public void SetupTarget(FiringController source, LiveObject tg, float speed)
    {
        this.target = tg;
        this.source = source;
        projectileSpeed = speed;
        die.transform.parent = transform;
        die.transform.localPosition = Vector3.zero;
    }
    private void OnEnable() {
        isBreaking = false;
        Debug.Log(spriteRenderer.gameObject);
        spriteRenderer.gameObject.SetActive(true);
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (target != null)
        {
            // Only move toward the target if it's alive
            if (target.LivingStatus == LiveObject.Status.Alive)
            {
                Vector3 dir = (target.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg, Vector3.forward);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, projectileSpeed*Time.deltaTime);
                //rb.velocity = dir * projectileSpeed;

                // If the projectile reaches the target
                if ((Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(target.transform.position.x, target.transform.position.y)) <= 0.1f) && !isBreaking)
                {
                    // Bring the projectile back to the pool
                    breakProjecttile();
                    // Inform FiringController that it has reached the target
                    source.TargetReached(target);
                }
            }
            else
            {
                // Bring the projectile back to the pool
                target = null;
                breakProjecttile();

            }
        }

    }
    public void breakProjecttile(){
        isBreaking = true;
        spriteRenderer.gameObject.SetActive(false);
        die.Play();
        StartCoroutine("breakProjecttileCoroutine");
    }
    private IEnumerator breakProjecttileCoroutine(){
        yield return new WaitForSeconds(1.0f);
        Debug.Log("project tile disable");
        gameObject.SetActive(false);
        source.projectilePool.Enqueue(gameObject.transform);
    }
}
