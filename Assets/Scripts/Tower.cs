using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : IPickupable, IPutdownable, IAttackable,IUpgradeable
{
    [SerializeField]
    private float _timePickup;
    public float timePickup{
        get => _timePickup;
        set => _timePickup = value;}
    [SerializeField]
    private float _timePutDown;
    public float timePutdown{
        get => _timePutDown;
        set => _timePutDown = value;}
    [SerializeField]
    private float _damage;
    public float damage{
        get => _damage;
        set => _damage = value;}
    [SerializeField]
    private float _attackSpeed;
    public float attackSpeed{
        get => _attackSpeed;
        set => _attackSpeed = value;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pickup()
    {

    }
    public void Putdown()
    {

    }
    public void InflictDamage(LiveObject attackedObject)
    {

    }
    public void Upgrade()
    {

    }
}
