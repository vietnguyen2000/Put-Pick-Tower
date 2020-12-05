using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : PutPickableObject, IAttackable,IUpgradeable
{
    public float Damage{
        get => damage;
        set => damage = value;}
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    public float AttackSpeed{
        get => attackSpeed;
        set => attackSpeed = value;}
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

    }
    public void Upgrade()
    {

    }
}
