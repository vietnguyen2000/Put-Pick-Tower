using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    public SpriteRenderer rink;
    private Vector3 _originalPos;
    public static MyCamera _instance;

    public static void Shake (float duration, float amount) {
        _instance.StopAllCoroutines();
        _instance.StartCoroutine(_instance.cShake(duration, amount));
    }

    public IEnumerator cShake (float duration, float amount) {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (startTime < endTime && Time.timeScale > 0f) {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            startTime += Time.deltaTime;

            yield return null;
        }

    transform.localPosition = _originalPos;
}

	// Use this for initialization
	void Start () {
        rink.transform.parent = null;
        transform.position = rink.transform.position;
        
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = rink.bounds.size.x / rink.bounds.size.y;

        if(screenRatio >= targetRatio){
            Camera.main.orthographicSize = rink.bounds.size.y / 2;
        }else{
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = rink.bounds.size.y / 2 * differenceInSize;
            Debug.Log((Camera.main.orthographicSize/2f - rink.bounds.size.y / 4f)/2f);
            transform.position = transform.position + new Vector3(0f,Camera.main.orthographicSize - rink.bounds.size.y / 2f,0f);
        }
        _originalPos = transform.localPosition;
        _instance = this;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;

	}
    private void OnDrawGizmos() {
        Vector3 bottomLeft = rink.transform.position + new Vector3(-rink.bounds.size.x/2, -rink.bounds.size.y / 2,0);
        Vector3 bottomRight = rink.transform.position + new Vector3(rink.bounds.size.x/2, -rink.bounds.size.y / 2,0);
        Vector3 topLeft = rink.transform.position + new Vector3(-rink.bounds.size.x/2, rink.bounds.size.y / 2,0);
        Vector3 topRight = rink.transform.position + new Vector3(rink.bounds.size.x/2, rink.bounds.size.y / 2,0);
        Gizmos.DrawLine(bottomLeft,bottomRight);
        Gizmos.DrawLine(bottomLeft,topLeft);
        Gizmos.DrawLine(topLeft,topRight);
        Gizmos.DrawLine(bottomRight,topRight);
    }
}