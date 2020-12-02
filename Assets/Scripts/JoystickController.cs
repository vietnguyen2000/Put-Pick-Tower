using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    // Start is called before the first frame update
    public float Horizontal { get { return Input.GetAxis("Horizontal") != 0 ? Input.GetAxis("Horizontal") : joystick.Horizontal; } }
    public float Vertical { get { return Input.GetAxis("Vertical") != 0 ? Input.GetAxis("Vertical") : joystick.Vertical; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }
    public bool OnPutPick { get { return joystick.OnJoystickUp || Input.GetKeyDown(KeyCode.Space);}}
    [SerializeField] private Joystick joystick;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnPutPick)
        {
            Debug.Log("Put Down successfull!!!!");
        }
    }
}
