using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectableObject
{
    protected override void Start()
    {
        base.Start();
        randomPosition();
    }
    protected override void Collected(Player player){
        player.CollectCoin();
        FindObjectOfType<AudioManager>().Play("CollectCoin");
        randomPosition();
    }
}
