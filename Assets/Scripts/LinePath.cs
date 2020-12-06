using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmosSelected() {
        int count = transform.childCount;
        for (int i = 0 ; i < count; i++){
            if (i>0){
            Gizmos.DrawLine(transform.GetChild(i-1).position, transform.GetChild(i).position)    ;
            }
            
        }
    }
}
