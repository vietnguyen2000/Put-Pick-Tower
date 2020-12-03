using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyObject : MonoBehaviour, ISpriteRenderer
{
    public enum FaceDirectionType{
        Right = 1,
        Left = -1
    }
    public SpriteRenderer SpriteRenderer{
        get => spriteRenderer;
    }
    protected SpriteRenderer spriteRenderer;
    public FaceDirectionType FaceDirection{
        get{
            if (spriteRenderer.flipX) {
                return FaceDirectionType.Left;
            }
            else return FaceDirectionType.Right;
        }
        set{
            if (value == FaceDirectionType.Right){
                spriteRenderer.flipX = false;
            }
            else{
                spriteRenderer.flipX = true;
            }
        }
    }

    protected virtual  void Start() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

}