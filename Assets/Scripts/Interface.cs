using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatistic{
    float hp{get;set;}
    float speed{get;set;}
}
public interface IDamageable{
    void Damage(float damage);
    void Kill();
}
public interface IMoveable{
    void Move(Vector2 vector, float speed);
}
public interface IPickupable{
    float timePickup{get;set;}
    void Pickup();
}
public interface IPutdownable{
    float timePutdown{get;set;}
    void Putdown();
}
public interface IAttackable{
    float damage{get;set;}
    float attackSpeed{get;set;}
    void InflictDamage(LiveObject attackedObject);
}
public interface IUpgradeable{
    void Upgrade();
}
// còn nữa 