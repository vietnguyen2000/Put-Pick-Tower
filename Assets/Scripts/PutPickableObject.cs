using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPickableObject : AnimateObject, IPutPickable{

    const float DISTANCEPUTDOWN = 0.5f;
    protected bool isOnBag;
    const float distancePutdown = 0.5f;
    public enum PutPickState { Picked, Put };
    public PutPickState PutPickStatus { get; set; }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PutPickStatus = PutPickState.Put;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public void Pickup(Player player, float timePutup)
    {
        anim.SetFloat(Constants.PUTPICKSPEED,Constants.DEFAULTPUTPICKTIME/timePutup);
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0,0.0001f,0);
        col.enabled = false;
        anim.Play(Constants.PICKUP,0);
        isOnBag = true;
        PutPickStatus = PutPickState.Picked;
    }
    public void Putdown(float timePutdown)
    {
        
        anim.SetFloat(Constants.PUTPICKSPEED,Constants.DEFAULTPUTPICKTIME/timePutdown);
        anim.Play(Constants.PUTDOWN,0);
        StartCoroutine(PutdownDelay());
        
    }
    IEnumerator PutdownDelay(){
        yield return new WaitForSeconds(Constants.DEFAULTPUTPICKTIME/anim.GetFloat(Constants.PUTPICKSPEED));
        transform.localPosition = new Vector3(DISTANCEPUTDOWN,0f,0f) ;
        transform.parent = null;
        col.enabled = true;
        isOnBag = false;
        PutPickStatus = PutPickState.Put;
        yield return null;
    }
}
