using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshhold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    private bool isPressed;
    private Vector3 startPos;
    public float maxDistance;
    private bool returning;

    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if(!isPressed && GetValue() + threshhold >= 1)
        {
            Pressed();
        }

        if (isPressed && GetValue() - threshhold <= 0)
        {
            Released();
        }

        if(transform.localPosition.y > deadZone || transform.localPosition.y < -deadZone)
        {
            returning = true;
            
        }

        if(returning)
        {
            if(transform.localPosition.y != startPos.y)
            {
                float currentY = transform.localPosition.y;
                float ease = 0;
                ease += Time.deltaTime * 5;
                transform.localPosition = new Vector3(startPos.x, Mathf.Lerp(currentY, startPos.y, ease), startPos.z);
            } else
            {
                returning = false;
            }
            
        }
    }

    private float GetValue()
    {
        float value = Vector3.Distance(startPos, transform.localPosition) / maxDistance;

        if(Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
