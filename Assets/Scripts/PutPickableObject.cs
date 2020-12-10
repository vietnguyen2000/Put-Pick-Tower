using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPickableObject : AnimateObject, IPutPickable{

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
    void Update()
    {
        
    }
    public void Pickup(Player player, float timePutup)
    {
        anim.SetFloat(Constants.PUTPICKSPEED,Constants.DEFAULTPUTPICKTIME/timePutup);
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        col.enabled = false;
        anim.Play(Constants.PICKUP,0);
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
        transform.localPosition = new Vector3(distancePutdown,0f,0f) ;
        transform.parent = null;
        col.enabled = true;
        PutPickStatus = PutPickState.Put;
        yield return null;
    }
}
