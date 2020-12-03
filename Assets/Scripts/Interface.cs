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
    void Damage(float damage);
    void Kill();
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
    float TimePickup{get;}
    float TimePutdown{get;}
    void Pickup(Player player);
    void Putdown();
}
public interface ICollider{
    Collider2D Collider{get;}
}
public interface IAttackable{
    float Damage{get;set;}
    float AttackSpeed{get;set;}
    void InflictDamage(IDamageable attackedObject);
}
public interface IUpgradeable{
    void Upgrade();
}
// còn nữa 