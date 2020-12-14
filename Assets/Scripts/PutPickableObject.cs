using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPickableObject : AnimateObject, IPutPickable{

    const float DISTANCEPUTDOWN = 0.5f;
    public enum PutPickState { Picked, Put };
    public PutPickState PutPickStatus { get; set; }
    const float REQUIREDISTANCE = 0.8f;
    [HideInInspector] public Player player;
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
    public virtual void Pickup(Player player, float timePutup)
    {
        anim.SetFloat(Constants.PUTPICKSPEED,Constants.DEFAULTPUTPICKTIME/timePutup);
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0,0.0001f,0);
        col.enabled = false;
        anim.Play(Constants.PICKUP,0);
        PutPickStatus = PutPickState.Picked;
        this.player = player;
    }
    public virtual void Putdown(float timePutdown)
    {
        
        anim.SetFloat(Constants.PUTPICKSPEED,Constants.DEFAULTPUTPICKTIME/timePutdown);
        anim.Play(Constants.PUTDOWN,0);
        StartCoroutine(PutdownDelay());
        
    }
    IEnumerator PutdownDelay(){
        yield return new WaitForSeconds(Constants.DEFAULTPUTPICKTIME/anim.GetFloat(Constants.PUTPICKSPEED));
        // find a good place
        Vector3 currentPos = player.transform.position;
        RaycastHit2D hit =  Physics2D.Raycast(currentPos,Vector2.right*player.transform.localScale.x,Mathf.Infinity,LayerMask.GetMask("Decorations","Player Ground"));
        Debug.Log(hit);
        float distance = Vector2.Distance(currentPos,hit.point);
        if(distance <=REQUIREDISTANCE){
            currentPos -= new Vector3((REQUIREDISTANCE-distance)*transform.lossyScale.x,0,0);
        }
        player.transform.position = currentPos;
        transform.position = calPositionPutDown(currentPos);
        transform.parent = null;
        col.enabled = true;
        PutPickStatus = PutPickState.Put;
        afterPutdown();
        player = null;
        yield return null;
    }
    protected virtual void afterPutdown(){
        MyCamera.Shake(0.05f,0.1f);
    }
    Vector3 calPositionPutDown(Vector3 playerPos){
        return playerPos + new Vector3(0.5f*player.transform.localScale.x,0,0);
    }
}
