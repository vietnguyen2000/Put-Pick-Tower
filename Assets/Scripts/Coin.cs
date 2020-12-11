using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin :CollectableObject
{
    [HideInInspector]public Vector3[] availablePlace;
    protected override void Start()
    {
        base.Start();
        randomPosition();
    }
    public void randomPosition(){
        var rand = Random.Range(0,availablePlace.Length-1);
        transform.position = availablePlace[rand] + new Vector3(0.25f,0.25f,0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            Player player = other.gameObject.GetComponentInParent<Player>();
            player.CollectCoin();
            randomPosition();
        }
    }
}
