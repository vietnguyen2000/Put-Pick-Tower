using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : LiveObject
{
    public Action CollectCoin;
    public float CarrySpeed{
        get => NormalSpeed*1.5f;
    }
    public float NormalSpeed = 2.0f;
    public JoystickController Controller{
        get => controller;
    }
    protected bool isPutPicking{
        get{
            if (countPutPickTime > 0) return true;
            else return false;
        }
    }
    public float TimePutPick{
        get => putpickTime;
        set => putpickTime = Mathf.Clamp(value,0.2f,0.5f);
    }
    public Sprite playerIcon;
    public GameObject foodPrint;
    [SerializeField]private float putpickTime = 0.5f;
    [SerializeField]private float countPutPickTime;
    [SerializeField] protected JoystickController controller;
    [SerializeField] protected PutPickableObject putpickableObject;
    [Header("Detect Tower")]
    [SerializeField] private float distanceDetect;
    [SerializeField] private float highDetect;
    [SerializeField] private float pady;
    private SpriteMask visibleMask;

    private Vector2 positionOfRectDetect{
        get {
            return new Vector2(transform.position.x + distanceDetect*(int)FaceDirection/2,
                                        transform.position.y + pady);
        }
    }
    private Vector2 sizeOfRectDetect{
        get {
            return new Vector2(distanceDetect,highDetect);
        }
    }
    [Header("Maximum level")]
    public float[] HPLevel;
    public float[] SpeedLevel;
    public float[] PutpickSpeedLevel;
    public int currentHPLevel=0,
                currentSpeedLevel=0,
                currentPutpickSpeedLevel=0;
    protected override void Start()
    {
        isAlwaysVisible = true;
        base.Start();
        controller = (JoystickController)FindObjectOfType<JoystickController>();
        if (controller == null) controller = GetComponentInChildren<JoystickController>();
        visibleMask = shadowVisible.gameObject.AddComponent<SpriteMask>();
        visibleMask.alphaCutoff = 1;
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        visibleMask.sprite = shadowVisible.sprite;
    }
    protected override void Update()
    {
        if (LivingStatus == Status.Dead) return;
        base.Update();
        if (countPutPickTime > 0){
            countPutPickTime -= Time.deltaTime;
        }
        else{
            RaycastHit2D[] hits = Physics2D.BoxCastAll(positionOfRectDetect, sizeOfRectDetect,0,Vector2.zero);
            for (int i = 0 ; i < hits.Length;i++){
                RaycastHit2D hit = hits[i];
                Collider2D other = hit.collider;
                if (other.tag == Constants.PUTPICKABLE){
                    if (putpickableObject == null)
                        if (controller.OnPutPick){
                            PickObjectUp(other.GetComponentInParent<Tower>());
                            return;
                        }
                }
            }
            if(putpickableObject != null){
                if (controller.OnPutPick){
                    PutObjectdown();
                    return;
                }
            }
            if(controller.Direction != Vector2.zero) Move(controller.Direction,speed);
            else Stop();
        }
    }
    public override void Move(Vector2 direction, float speed){
        if (putpickableObject == null){
            base.Move(direction,speed);
        }
        else{
            anim.Play(Constants.RUN,0);
            if (direction.x > 0) FaceDirection = FaceDirectionType.Right;
            else if (direction.x <0) FaceDirection = FaceDirectionType.Left;
            putpickableObject.Anim.Play(Constants.RUNONBAG);
            rb.velocity = direction*speed;

            //FindObjectOfType<AudioManager>().Play("PlayerMove");
        }
    }
    public override void Stop()
    {
        base.Stop();
        if (putpickableObject != null){
            putpickableObject.Anim.Play(Constants.IDLEONBAG);
        }
        

    }
    protected void PutObjectdown()
    {
        if (putpickableObject != null){
            Stop();
            countPutPickTime = putpickTime;
            putpickableObject.Putdown(putpickTime);
            anim.Play(Constants.PUTDOWN,0);

            putpickableObject = null;
            speed = NormalSpeed;
            foodPrint.SetActive(false);
        }
        else Debug.Log("NuLL object to put down!!!!");
    }
    public void PickObjectUp(PutPickableObject o){
        if (putpickableObject == null){
            Stop();
            putpickableObject = o;
            putpickableObject.FaceDirection = FaceDirection;
            countPutPickTime = putpickTime;
            anim.Play(Constants.PICKUP,0);
            
            o.Pickup(this, putpickTime);
            speed = CarrySpeed;
            if (o.gameObject.layer == LayerMask.NameToLayer("Tower")){
                gameManager.currentTower = o.gameObject.GetComponent<Tower>();
            }
            foodPrint.SetActive(true);
        }
        else Debug.Log("Null object to pick up!!!");
        

    }
    public override void ReceiveDamage(float damage)
    {
        this.hp -= damage;
        if (this.hp <= 0f)
        {
            this.hp = 0f;
            LivingStatus = Status.Dead;
            if(putpickableObject != null) putpickableObject.Putdown(putpickTime);
            Stop();
            anim.Play("Deadth",0);
            gameManager.lose();
            gameManager.spawnMonsterManager.enabled=false;
        }
        anim.Play("Hurt",1);
        MyCamera.Shake(0.05f,0.15f);
    }
    public void UpgradeHP(){
        currentHPLevel+=1;
        MaxHp = HPLevel[currentHPLevel];
    }
    public void UpgradeSpeed(){
        currentSpeedLevel+=1;
        NormalSpeed = SpeedLevel[currentSpeedLevel];
    }
    public void UpgradePutpickSpeed(){
        currentPutpickSpeedLevel+=1;
        TimePutPick = PutpickSpeedLevel[currentPutpickSpeedLevel];
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(new Vector3(positionOfRectDetect.x,positionOfRectDetect.y, transform.position.z),
                                        new Vector3(sizeOfRectDetect.x,sizeOfRectDetect.y,0));
    }
}
