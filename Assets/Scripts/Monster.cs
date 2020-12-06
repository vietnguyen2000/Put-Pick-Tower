using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LiveObject
{
    public Transform[] path;
    public Transform monster;

    private Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        if (path.Length == 0)
        {
            Debug.LogError("No path referenced.");
        }
        position = gameObject.transform.position;
    }
}
