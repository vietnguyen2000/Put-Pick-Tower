﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LiveObject
{
    const float CARRYSPEED = 3.0f;
    const float NORMALSPEED = 2.0f;
    public JoystickController Controller{
        get => controller;
    }
    protected bool isPutPicking{
        get{
            if (putpickTime > 0) return true;
            else return false;
        }
    }
    private float putpickTime;
    [SerializeField] protected JoystickController controller;
    [SerializeField] protected Tower pickupableObject;
    [Header("Detect Tower")]
    [SerializeField] private float distanceDetect;
    [SerializeField] private float highDetect;
    [SerializeField] private float pady;
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
    protected override void Start()
    {
        base.Start();
        if (controller == null) controller = GetComponentInChildren<JoystickController>();
    }
    protected override void Update()
    {
        base.Update();
        if (putpickTime > 0){
            putpickTime -= Time.deltaTime;
        }
        else{
            RaycastHit2D[] hits = Physics2D.BoxCastAll(positionOfRectDetect, sizeOfRectDetect,0,Vector2.zero);
            for (int i = 0 ; i < hits.Length;i++){
                RaycastHit2D hit = hits[i];
                Collider2D other = hit.collider;
                if (other.tag == Constants.PUTPICKABLE){
                    if (pickupableObject == null)
                        if (controller.OnPutPick){
                            PickObjectUp(other.GetComponentInParent<Tower>());
                            return;
                        }
                }
            }
            if(pickupableObject != null){
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
        if (pickupableObject == null){
            base.Move(direction,speed);
        }
        else{
            anim.Play(Constants.RUN,0);
            if (direction.x > 0) FaceDirection = FaceDirectionType.Right;
            else if (direction.x <0) FaceDirection = FaceDirectionType.Left;
            pickupableObject.Anim.Play(Constants.RUNONBAG);
            rb.velocity = direction*speed;
        }
    }
    public override void Stop()
    {
        base.Stop();
        if (pickupableObject != null){
            pickupableObject.Anim.Play(Constants.IDLEONBAG);
        }
        

    }
    protected void PutObjectdown()
    {
        if (pickupableObject != null){
            Stop();
            anim.Play(Constants.PUTDOWN,0);
            pickupableObject.Anim.Play(Constants.PUTDOWN,0);
            putpickTime = pickupableObject.TimePutdown;
            pickupableObject.Putdown();
            pickupableObject = null;
            speed = NORMALSPEED;
        }
        else Debug.Log("NuLL object to put down!!!!");
    }
    protected void PickObjectUp(Tower o){
        if (pickupableObject == null){
            Stop();
            pickupableObject = o;
            pickupableObject.FaceDirection = FaceDirection;
            anim.Play(Constants.PICKUP,0);
            putpickTime = o.TimePickup;
            o.Pickup(this);
            speed = CARRYSPEED;
        }
        else Debug.Log("Null object to pick up!!!");
        

    }
    private void OnTriggerStay2D(Collider2D other) {
        
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(new Vector3(positionOfRectDetect.x,positionOfRectDetect.y, transform.position.z),
                                        new Vector3(sizeOfRectDetect.x,sizeOfRectDetect.y,0));
    }
}