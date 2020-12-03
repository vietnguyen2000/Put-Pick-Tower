using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LiveObject
{
    const string PUTDOWN = "Putdown";
    const string PICKUP = "Pickup";
    const string RUN = "Run";
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
    protected IPutPickable pickupableObject;
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
            if(controller.Direction != Vector2.zero) Move(controller.Direction,speed);
            else Stop();
        }
    }
    public override void Move(Vector2 direction, float speed){
        if (pickupableObject != null){
            base.Move(direction,speed);
        }
        else{
        anim.Play(RUN,0);
        if (direction.x > 0) FaceDirection = FaceDirectionType.Right;
        else if (direction.x <0) FaceDirection = FaceDirectionType.Left;
        
        rb.velocity = direction*speed;
        }
    }
    protected void PutObjectdown()
    {
        if (pickupableObject != null){
            Stop();
            anim.Play(PUTDOWN,0);
            putpickTime = pickupableObject.TimePutDown;
            pickupableObject.Putdown();
            
        }
        else Debug.Log("NuLL object to put down!!!!");
    }
    protected void PickObjectUp(IPutPickable pickupableObject){
        if (pickupableObject != null){
            Stop();
            anim.Play(PICKUP,0);
            putpickTime = pickupableObject.TimePickup;
            pickupableObject.Pickup(this);
        }
        else Debug.Log("Null object to pick up!!!");
        

    }
}
