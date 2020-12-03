using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MyObject, IAnimation
{
    public Animator Anim{
        get => anim;
    }
    [SerializeField] protected Animator anim;
    protected override void Start()
    {
        base.Start();
        if (anim == null) anim = GetComponentInChildren<Animator>();
    }

}