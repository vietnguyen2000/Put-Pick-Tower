using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MyObject
{
    [HideInInspector]public Vector3[] availablePlace;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void randomPosition(){
        var rand = Random.Range(0,availablePlace.Length-1);
        Vector3 newpos = availablePlace[rand] + new Vector3(0.25f,0.25f,0f);
        Collider2D hit = Physics2D.OverlapCircle(newpos,0.125f);
        if (hit) randomPosition();
        else transform.position = availablePlace[rand] + new Vector3(0.25f,0.25f,0f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            Player player = other.gameObject.GetComponentInParent<Player>();
            Collected(player);
        }
    }
    protected virtual void Collected(Player player){

    }
}
