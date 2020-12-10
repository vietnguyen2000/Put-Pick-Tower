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
    public float Range{
        get => range;
    }
    private float range=1f;
    public Sprite towerIcon;
    // Start is called before the first frame update
    protected override void Start()
    {
        isAlwaysVisible = true;
        base.Start();
        shadowVisible.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        towerIcon = spriteRenderer.sprite;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public void InflictDamage(IDamageable attackedObject)
    {

    }
    public void Upgrade()
    {

    }
}
