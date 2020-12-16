using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISpriteRenderer{
    SpriteRenderer SpriteRenderer{get;}
}
public interface IStatistic{
    float Hp{get;}
    float Speed{get;}
}
public interface IDamageable{
    //void Damage(float damage);
    void ReceiveDamage(float Damage);
    void Die();
}
public interface IMoveable{
    Vector2 Direction{get;}
    Vector2 Velocity{get;}
    void Move(Vector2 direction, float speed);
    void Stop();
}
public interface IAnimation{
    Animator Anim{get;}
}
public interface IPutPickable{
    void Pickup(Player player, float timePutup);
    void Putdown(float timePutdown);
}

public interface ICollider{
    Collider2D Collider{get;}
}
public interface IAttackable{
    float Damage{get;set;}
    float AttackSpeed{get;set;}
    float AttackRange { get; set; }
    void InflictDamage(IDamageable attackedObject);
}
public interface IUpgradeable{
    void Upgrade();
}
// còn nữa 