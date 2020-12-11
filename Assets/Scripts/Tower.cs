using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : PutPickableObject, IAttackable,IUpgradeable
{

    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    public FiringController firePowerSource;
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
    private  CircleDraw circleDrawRange;
    [Header("Maximum level")]
    public float[] DamageLevel;
    public float[] AttackSpeedLevel;
    public float[] AttackRangeLevel;
    public int currentDamageLevel=0,
                currentAttackSpeedLevel=0,
                currentAttackRangeLevel=0;
    // Start is called before the first frame update
    protected override void Start()
    {
        isAlwaysVisible = true;
        base.Start();
        shadowVisible.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        towerIcon = spriteRenderer.sprite;
        capsuleCol = GetComponentInChildren<CapsuleCollider2D>();
        circleDrawRange = GetComponentInChildren<CircleDraw>();
    }

    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        circleDrawRange.radius = attackRange;
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
    public override void Pickup(Player player, float timePutup)
    {
        base.Pickup(player, timePutup);
        firePowerSource.gameObject.SetActive(false);
        circleDrawRange.lineRenderer.enabled = false;
    }
    protected override void afterPutdown()
    {
        base.afterPutdown();
        firePowerSource.gameObject.SetActive(true);
        circleDrawRange.lineRenderer.enabled = true;
    }
    public void UpgradeDamage(){
        currentDamageLevel+=1;
        Damage = DamageLevel[currentDamageLevel];
    }
    public void UpgradeAttackRate(){
        currentAttackSpeedLevel+=1;
        AttackSpeed = AttackSpeedLevel[currentAttackSpeedLevel];
    }
    public void UpgradeAttackRange(){
        currentAttackRangeLevel+=1;
        AttackRange = AttackRangeLevel[currentAttackRangeLevel];
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,AttackRange);
    }
}
