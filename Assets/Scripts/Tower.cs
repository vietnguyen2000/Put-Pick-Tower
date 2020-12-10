using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : PutPickableObject, IAttackable,IUpgradeable
{

    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    public float Damage
    {
        get => damage;
        set => damage = value;
    }
    public float AttackSpeed{
        get => attackSpeed;
        set => attackSpeed = value;}
    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InflictDamage(IDamageable attackedObject)
    {
        attackedObject.ReceiveDamage(Damage);
    }
    public void Upgrade()
    {

    }
}
