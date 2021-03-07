using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class LiveObject : AnimateObject, IStatistic, IDamageable, IMoveable
{
    public float MaxHp{
        get => maxHp;
        set{
            if (value >= maxHp){
                hp += value - maxHp;
                maxHp = value;
            }
            else{
                maxHp = value;
                if (hp>maxHp){
                    hp = maxHp;
                }
            }
        }
    }
    public enum Status {Alive, Dead};
    public Status LivingStatus { get; set; }
    public float Hp{
        get => hp;
        set => hp = Mathf.Clamp(value,0,maxHp);
    }
    public float Speed{
        get => speed;
    }
    public Vector2 Direction{get => rb.velocity.normalized;}
    public Vector2 Velocity{ get => rb.velocity;}
    
    public Rigidbody2D Rigidbody{
        get => rb;
    }
    [SerializeField] protected float maxHp;        
    protected float hp;        
    [SerializeField] protected float speed;    
    [SerializeField] protected Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (rb == null) rb= GetComponent<Rigidbody2D>();
        hp = maxHp;
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public virtual void Damage(float damage)
    {

    }
    public virtual void ReceiveDamage(float damage)
    {
        this.hp -= damage;
        if (this.hp <= 0f)
        {
            Die();
            FindObjectOfType<AudioManager>().Play("SkeletonDeath");
        }
        anim.Play("Hurt",1);
    }
    public virtual void Die()
    {
        gameObject.SetActive(false);
        LivingStatus = Status.Dead;
    }
    public virtual void Move(Vector2 direction, float speed)
    // Move object by set velocity to direction*speed and play Walk animation
    {
        anim.Play(Constants.WALK,0);
        if (direction.x > 0) FaceDirection = FaceDirectionType.Right;
        else if (direction.x <0) FaceDirection = FaceDirectionType.Left;
        
        rb.velocity = direction*speed;
    }

    public virtual void Stop()
    // Stop function will set velocity to zero and play Idle animation
    {
        anim.Play(Constants.IDLE,0);
        rb.velocity = Vector2.zero;
    }
    protected virtual void OnEnable(){}
    protected virtual void OnDisable(){}
}
