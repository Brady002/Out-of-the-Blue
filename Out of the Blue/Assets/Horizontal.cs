using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal : MonoBehaviour
{
    private Vector2 movementDirection;
    private Rigidbody rb;
    public GameObject stick;
    public ConfigurableJoint joystick;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(stick.transform.rotation.z);
        rb.AddForce(-Vector3.right * stick.transform.rotation.z + Vector3.forward * stick.transform.rotation.x, ForceMode.Force);
    }
}
