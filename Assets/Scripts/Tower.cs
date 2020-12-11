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
    // public float Range{
    //     get => range;
    // }
    // private float range=1f;
    public Sprite towerIcon;
    private CapsuleCollider2D capsuleCol;
    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        isAlwaysVisible = true;
        base.Start();
        shadowVisible.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        towerIcon = spriteRenderer.sprite;
        capsuleCol = GetComponentInChildren<CapsuleCollider2D>();
    }

    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }
    protected virtual void FixedUpdate(){
        if (PutPickStatus== PutPickState.Put){
            RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)transform.position+capsuleCol.offset,capsuleCol.size,0,Vector2.zero);
            foreach(RaycastHit2D hit in hits){
                if(hit.transform.tag == "UpgradeStage"){
                    Debug.Log("asdasd");
                    UpgradeStage x = hit.collider.gameObject.GetComponentInParent<UpgradeStage>();
                    x.Activate(gameManager.player,this);
                }
            }
        }
    }
    public void InflictDamage(IDamageable attackedObject)
    {
        attackedObject.ReceiveDamage(Damage);
    }
    public void Upgrade()
    {

    }

}
