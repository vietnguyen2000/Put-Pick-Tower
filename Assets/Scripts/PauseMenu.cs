using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentTimeScale;
    void Start()
    {
        // gameObject.SetActive(false);
    }
    public void activate(){
        gameObject.SetActive(true);
        currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
    public void deActivae(){
        gameObject.SetActive(false);
        Time.timeScale = currentTimeScale;
    }

}
