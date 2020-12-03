using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveObject : MyObject, IStatistic, IDamageable, IMoveable, IAnimation
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    
    public float Hp{
        get => hp;
    }
    public float Speed{
        get => speed;
    }
    public Vector2 Direction{get => rb.velocity.normalized;}
    public Vector2 Velocity{ get => rb.velocity;}
    public Rigidbody2D Rigidbody{
        get => rb;
    }
    public Animator Anim{
        get => anim;
    }

    [SerializeField] protected float hp;        
    [SerializeField] protected float speed;    

    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (anim == null) anim = GetComponentInChildren<Animator>();
        if (rb == null) rb= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    public virtual void Damage(float damage)
    {

    }
    public virtual void Kill()
    {

    }
    public virtual void Move(Vector2 direction, float speed)
    // Move object by set velocity to direction*speed and play Walk animation
    {
        anim.Play(WALK,0);
        if (direction.x > 0) FaceDirection = FaceDirectionType.Right;
        else if (direction.x <0) FaceDirection = FaceDirectionType.Left;
        
        rb.velocity = direction*speed;
    }

    public virtual void Stop()
    // Stop function will set velocity to zero and play Idle animation
    {
        anim.Play(IDLE,0);
        rb.velocity = Vector2.zero;
    }
}
