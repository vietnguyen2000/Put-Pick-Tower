using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : IPutPickable, IAttackable,IUpgradeable
{
    [SerializeField]
    private float timePickup;
    public float TimePickup{
        get => timePickup;
    }
    [SerializeField]
    private float timePutDown;
    public float TimePutdown{
        get => timePutDown;
    }
    [SerializeField]
    private float damage;
    public float Damage{
        get => damage;
        set => damage = value;}
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed{
        get => attackSpeed;
        set => attackSpeed = value;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pickup(Player player)
    {

    }
    public void Putdown()
    {

    }
    public void InflictDamage(IDamageable attackedObject)
    {

    }
    public void Upgrade()
    {

    }
}
