using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyObject : MonoBehaviour, ISpriteRenderer, ICollider
{
    public enum FaceDirectionType{
        Right = 1,
        Left = -1
    }
    public Collider2D Collider{
        get => col;
    }
    public SpriteRenderer SpriteRenderer{
        get => spriteRenderer;
    }
    protected Collider2D col;
    protected SpriteRenderer spriteRenderer;
    public virtual FaceDirectionType FaceDirection { 
        get{
            if (transform.localScale.x < 0.0f) return FaceDirectionType.Left;
            else return FaceDirectionType.Right;
        }
        set{
            if (value == FaceDirectionType.Left) {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else{
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    protected virtual  void Start() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        col = GetComponentInChildren<Collider2D>();
    }

}