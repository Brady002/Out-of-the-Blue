using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class HarpoonVeh : MonoBehaviour
{

    public Transform orientation;
    public GameObject follow;
    public GameObject harpoon;
    public HingeJoint zRotationLever;
    public HingeJoint yRotationLever;
    private Rigidbody rb;
    private float yRotate;
    private float xRotate;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        yRotate = rb.transform.rotation.y;
        xRotate = rb.transform.rotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        xRotate += zRotationLever.angle / zRotationLever.limits.max;
        xRotate = Mathf.Clamp(xRotate, 0f, 30f);
        yRotate += yRotationLever.angle / yRotationLever.limits.max;
        rb.transform.rotation = Quaternion.Euler(Mathf.Clamp(xRotate, 0f, 30f), yRotate, 0);
        rb.transform.position = follow.transform.position;


    }

    public void LaunchHarpoon()
    {
        harpoon.GetComponent<Harpoon>().Fire();
        
    }
}
