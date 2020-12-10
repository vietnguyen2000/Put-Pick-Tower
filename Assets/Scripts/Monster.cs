using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LiveObject
{
    public Transform[] path;
    private Vector2 position;
    // Start is called before the first frame update
    private int currentPointIndex=1;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (currentPointIndex <= path.Length -1) {
            if (Vector2.Distance(transform.position, path[currentPointIndex].position) < 0.005f) {
                currentPointIndex +=1;
                if (currentPointIndex > path.Length-1) return;
            }
            Move(path[currentPointIndex].position-transform.position,speed);
        }
        else{
            gameObject.SetActive(false);
        }
        
        
    }
    public override void Move(Vector2 direction, float speed)
    // Move object by set velocity to direction*speed and play Walk animation
    {   
        rb.velocity = direction.normalized*speed;
    }
}
