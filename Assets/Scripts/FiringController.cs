using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FiringController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform pfProjectile;    // Prefab of the projectile
    [SerializeField] float poolingCoefficient;  // Coefficient of the projectile pool
    [SerializeField] float projectileSpeed; // Moving speed of the projectile
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] Transform endPoint;
    public Vector3 poolPosition { get; set; }   // Position of the pool
    public Queue<Transform> projectilePool { get; set; }    // A queue contains all the created projectiles 

    Queue<GameObject> detectedEnemies;
    int numberOfProjectiles;
    Tower tower;
    Player player;
    float cooldown; // Delay between attacks
    int projectileNeeded; // Calculate the projectile needed to kill a monster
    LiveObject target;
    //Vector2 firepointPos, enemyPos;
    void Awake()
    {
        tower = GetComponentInParent<Tower>();
        player = FindObjectOfType<Player>();
        projectilePool = new Queue<Transform>();
        detectedEnemies = new Queue<GameObject>();
        poolPosition = transform.position * 100f;
        cooldown = 0;

        SetUpProjectilePool();

        //firepointPos = new Vector2(transform.position.x, transform.position.y);
        //enemyPos = new Vector2(0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (tower.PutPickStatus == PutPickableObject.PutPickState.Put)
            ScanAndAttack();
    }
    void ScanAndAttack()
    {
        // If there's no target at the moment
        if (target == null || projectileNeeded <= 0 || target.LivingStatus == LiveObject.Status.Dead)
        {

            /*            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, tower.AttackRange, monsterLayer);
                        colliders.OrderBy(x => Vector3.Distance(endPoint.position, x.transform.position));
                        for (int i = 0; i < colliders.Length; i++)
                        {
                            if (colliders[i].gameObject.GetComponentInParent<Monster>() != null)
                            {
                                target = colliders[i].gameObject.GetComponentInParent<Monster>();
                                //Debug.Log("Aimed");
                                break;
                            }
                        }*/

            // Then scan for a target, we can use the OverlapCircleAll and sort to target the one that is
            // closest to the end point.

            Collider2D collider = Physics2D.OverlapCircle(transform.position, tower.AttackRange, monsterLayer);

            if (collider != null)
            {
                target = collider.gameObject.GetComponentInParent<Monster>();
                if (target != null)
                    projectileNeeded = (int)Mathf.Ceil(target.Hp / tower.Damage);
            }

        }
        else
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                Shoot(target);
                projectileNeeded--;
                if (projectileNeeded <= 0 || Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(target.transform.position.x, target.transform.position.y)) > tower.AttackRange)
                    target = null;
                cooldown = 1 / tower.AttackSpeed;
            }
        }
    }
    void SetUpProjectilePool()
    {
        numberOfProjectiles = (int)(tower.AttackSpeed * poolingCoefficient);
        //numberOfProjectiles = 5;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            projectilePool.Enqueue(Instantiate(pfProjectile, poolPosition, Quaternion.identity));
        }
    }
    /*Transform CreateNewProjectile()
    {
        return Instantiate(pfProjectile, poolPosition, Quaternion.identity);
    }*/
    void Shoot(LiveObject target)
    {
        if (projectilePool.Count <= 0)
            projectilePool.Enqueue(Instantiate(pfProjectile, poolPosition, Quaternion.identity));

        Transform projectile = projectilePool.Dequeue();
        projectile.position = transform.position;
        projectile.GetComponent<Projectile>().SetupTarget(this, target, projectileSpeed);

        cooldown = 1f;
    }
    public void TargetReached(LiveObject target)
    {
        tower.InflictDamage(target);
    }
}
