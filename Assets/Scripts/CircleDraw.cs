using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[RequireComponent(typeof(LineRenderer))]
public class CircleDraw : MonoBehaviour
{
    float theta_scale = 0.01f;        //Set lower to add more points
    int size; //Total number of points in circle
    public float width = 0.05f;
    public Color color;
    public float radius = 2f;
    public LineRenderer lineRenderer;
 
    void Awake()
    {
        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.positionCount = size;
        lineRenderer.sortingLayerName= "Top";
    }
 
    void Update()
    {
        if (lineRenderer!= null && lineRenderer.enabled == true){
            Vector3 pos;
            float theta = 0f;
            for (int i = 0; i < size; i++)
            {
                theta += (2.0f * Mathf.PI * theta_scale);
                float x = radius * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(theta);
                x += gameObject.transform.position.x;
                y += gameObject.transform.position.y;
                pos = new Vector3(x, y, 0);
                lineRenderer.SetPosition(i, pos);
            }
        }
    }
}