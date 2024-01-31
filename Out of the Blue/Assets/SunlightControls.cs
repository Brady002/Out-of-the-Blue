using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SunlightControls : MonoBehaviour
{
    public Camera cam;
    public Light sun;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sun.intensity = cam.transform.position.y;
    }
}
