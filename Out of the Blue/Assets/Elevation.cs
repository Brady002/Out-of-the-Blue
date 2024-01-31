using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject lever;
    private HingeJoint hinge;
    private float min;
    private float max;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hinge = lever.GetComponent<HingeJoint>();
        min = hinge.limits.min;
        max = hinge.limits.max;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.up * hinge.angle / max, ForceMode.Force);
    }
}
