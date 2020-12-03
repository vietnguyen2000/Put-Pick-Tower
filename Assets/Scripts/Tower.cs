using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : AnimateObject, IPutPickable, IAttackable,IUpgradeable
{

    const float distancePutdown = 0.5f;
    public float TimePickup{
        get => Constants.TIMEPICKUP;
    }
    public float TimePutdown{
        get => Constants.TIMEPUTDOWN;
    }
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
    public void Pickup(Player player)
    {
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        col.enabled = false;
        anim.Play(Constants.PICKUP);
    }
    public void Putdown()
    {
        object[] parms = new object[1]{Constants.TIMEPUTDOWN};
        StartCoroutine("PutdownDelay", parms);
    }
    IEnumerator PutdownDelay(object[] parms){
        yield return new WaitForSeconds((float)parms[0]);
        transform.localPosition = new Vector3(distancePutdown,0f,0f) ;
        transform.parent = null;
        col.enabled = true;
        yield return null;
    }
    public void InflictDamage(IDamageable attackedObject)
    {

    }
    public void Upgrade()
    {

    }
}
