using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LiveObject
{
    //public enum State {InPool, OnMap};
    public Transform[] path;
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
    protected override void Update()
    {
        base.Update();
        if (currentPointIndex <= path.Length -1) {
            if (Vector2.Distance(transform.position, path[currentPointIndex].position) < 0.005f) {
                currentPointIndex +=1;
                if (currentPointIndex > path.Length-1) return;
            }
            Move(path[currentPointIndex].position-transform.position,speed);
        }
        else{
            GameObject.Destroy(gameObject);
        }
        
        
    }
    public override void Move(Vector2 direction, float speed)
    // Move object by set velocity to direction*speed and play Walk animation
    {   
        rb.velocity = direction.normalized*speed;
    }
    public override void ReceiveDamage(float damage)
    {
        //Debug.Log("Hp: " + Hp.ToString() + " plus by " + damage.ToString());
        this.hp -= damage;
        if (this.hp <= 0f)
        {
            transform.position *= 100f;
            LivingStatus = Status.Dead;
        }
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
