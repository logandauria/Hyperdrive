using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Control the physics of the portal button
public class ButtonPhysics : MonoBehaviour
{
    // How far the button needs to move to trigger
    [SerializeField]
    private float threshold = .1f;
    [SerializeField]
    private float deadZone = 0.025f;

    private bool isPressed;
    private Vector3 startPos;

    // attached joint
    private ConfigurableJoint joint;

    // Events that will trigger when pressed and released
    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        if(isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    /// <summary>
    /// Return the current y position value of the button
    /// </summary>
    private float GetValue()
    {
        var value = Mathf.Abs(startPos.y - transform.localPosition.y) / joint.linearLimit.limit;
        if(Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    /// <summary>
    /// Trigger given event when pressed
    /// </summary>
    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    /// <summary>
    /// Trigger given event when released
    /// </summary>
    private void Released ()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
