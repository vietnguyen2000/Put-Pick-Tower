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
    public GameManager GameManager{
        get=>gameManager;
    }
    protected GameManager gameManager;
    public bool isAlwaysVisible = false;
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
    protected SpriteRenderer shadowVisible;
    protected virtual  void Start() {
        gameManager = (GameManager)FindObjectOfType<GameManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        col = GetComponentInChildren<Collider2D>();
        if (isAlwaysVisible){
            GameObject shadowGameObject = new GameObject("ShadowOf"+gameObject.name);
            shadowGameObject.transform.parent = spriteRenderer.transform;
            shadowGameObject.transform.localPosition = Vector3.zero;
            shadowVisible = shadowGameObject.AddComponent<SpriteRenderer>();
            shadowVisible.sortingLayerName = "Top";
            shadowVisible.color = new Color(1,1,1,0.5f);
        }
    }
    protected virtual void Update(){
        
    }
    protected virtual void LateUpdate(){
        if (isAlwaysVisible) shadowVisible.sprite = spriteRenderer.sprite;
    }

}