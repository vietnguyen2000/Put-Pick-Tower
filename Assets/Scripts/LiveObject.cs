using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveObject : MonoBehaviour, IStatistic, IDamageable, IMoveable
{
    [SerializeField]
    private float _hp;
    public float hp{
        get => _hp;
        set => _hp = value;}
    [SerializeField]
    private float _speed;    
    public float speed{
        get => _speed;
        set => _speed = value;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float damage)
    {

    }
    public void Kill()
    {

    }
    public void Move(Vector2 vector, float speed)
    {

    }
}
