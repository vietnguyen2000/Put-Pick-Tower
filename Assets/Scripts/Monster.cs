using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LiveObject
{
    public float damage;
    public Transform[] path;
    public int killScore;
    //public St state = State.InPool;
    private Vector2 position;
    public float DistanceToEnd{
        get{
            float res = Vector2.Distance(transform.position,path[currentPointIndex].position);
            for (int i = currentPointIndex; i < currentPointIndex-1; i++){
                res += Vector2.Distance(path[i].position,path[i+1].position);
            }
            return res;
        }
    }
    // Start is called before the first frame update
    private int currentPointIndex=1;
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnEnable() {
        base.OnEnable();
        currentPointIndex = 1;
        LivingStatus = LiveObject.Status.Alive;
        hp = maxHp;
    }
    protected override void OnDisable() {
        base.OnDisable();
        LivingStatus = LiveObject.Status.Dead;
    }
    protected override void Update()
    {
        base.Update();
        if (currentPointIndex <= path.Length -1) {
            Move(path[currentPointIndex].position-transform.position,speed);
            if (Vector2.Distance(transform.position, path[currentPointIndex].position) < 0.005f) {
                currentPointIndex +=1;
            }

        }
        else{
            gameManager.player.ReceiveDamage(damage);
            gameObject.SetActive(false);
        }
        
        
    }
    public override void Move(Vector2 direction, float speed)
    // Move object by set velocity to direction*speed and play Walk animation
    {   
        rb.velocity = direction.normalized*speed;
    }
    public override void Die()
    {
        base.Die();
        gameManager.MonsterKillScore(killScore);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Hit");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit");
    }
}
