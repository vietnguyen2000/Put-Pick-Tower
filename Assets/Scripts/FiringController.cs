using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FiringController : MonoBehaviour
{
    [SerializeField] Transform pfProjectile;    // Prefab of the projectile
    [SerializeField] float poolingCoefficient;  // Coefficient of the projectile pool
    [SerializeField] float projectileSpeed; // Moving speed of the projectile
    [SerializeField] LayerMask monsterLayer; // Monster layer to detect monster around its attack range
    public Vector3 poolPosition { get; set; }   // Position of the projectile pool
    public Queue<Transform> projectilePool { get; set; }    // A queue contains all the projectile pool 


    int numberOfProjectiles; // number of projectile in pool
    Tower tower;
    float cooldown; // Delay between attacks, equal to 1/AttackSpeed
    int projectileNeeded; // Calculate the projectile needed to kill a monster, equal to Hp/Damage
    Monster target;
    private void OnEnable() {
        target = null;    
    }
    void Start()
    {
        tower = GetComponentInParent<Tower>();
        projectilePool = new Queue<Transform>();
        poolPosition = transform.position * 100f;
        cooldown = 0;

        SetUpProjectilePool();
    }
    // Update is called once per frame
    void Update()
    {
        if (tower.PutPickStatus == PutPickableObject.PutPickState.Put)
            ScanAndAttack();
        else
            target = null;
    }
    void SetUpProjectilePool()
    {
        numberOfProjectiles = (int)(tower.AttackSpeed * poolingCoefficient);

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            var x = Instantiate(pfProjectile, poolPosition, Quaternion.identity);
            x.gameObject.SetActive(false);
            projectilePool.Enqueue(x);
        }
    }
    void ScanAndAttack()
    {
        // If there's no target at the moment
        // Detect all monsters in attack range and attack the one which is closest to the end point.
        if (target == null || projectileNeeded <= 0 || target.LivingStatus == LiveObject.Status.Dead)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(tower.transform.position, tower.AttackRange, monsterLayer);
            Monster[] targets = new Monster[colliders.Length];
            for (int i = 0; i < colliders.Length; i++)
            {
                targets[i] = colliders[i].gameObject.GetComponentInParent<Monster>();
            }
            // Debug.Log(targets);
            // Order by distance to the end point
            targets = targets.OrderBy(x => x.DistanceToEnd).ToArray();
            if (targets.Length>0){
                target = targets[0];
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
                Attack(target);
                projectileNeeded--;
                // After attacking the target with enough projectiles or the target is out of attack range, stop attacking
                if (projectileNeeded <= 0 || Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(target.transform.position.x, target.transform.position.y)) > tower.AttackRange)
                {
                    target = null;
                }
                cooldown = 1 / tower.AttackSpeed;
            }
        }
    }
    void Attack(LiveObject target)
    {
        // In case we run out of projectiles in pool :))
        if (projectilePool.Count <= 0){
            var x = Instantiate(pfProjectile, poolPosition, Quaternion.identity);
            x.gameObject.SetActive(false);
            projectilePool.Enqueue(x);
        }

        // Get projectile from the object pool and set its position to the firing point
        Transform projectile = projectilePool.Dequeue();
        projectile.gameObject.SetActive(true);
        projectile.position = transform.position;
        projectile.GetComponent<Projectile>().SetupTarget(this, target, projectileSpeed);

        FindObjectOfType<AudioManager>().Play("Shoot");

    }
    // When get informed that the projectile has reached the target
    public void TargetReached(LiveObject target)
    {
        tower.InflictDamage(target);
        // MyCamera.Shake(0.05f,0.05f);
    }
}
