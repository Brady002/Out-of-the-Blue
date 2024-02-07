using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    private GameObject spear;
    private Rigidbody rb;
    public Transform spearTip;
    public Transform origin;
    private bool launched;

    public float harpoonSpeed = 30f;
    public float harpoonRange = 10f;
    public bool canRetract;
    // Start is called before the first frame update
    void Start()
    {
        spear = this.gameObject;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if((origin.position - spear.transform.position).magnitude > harpoonRange && launched)
        {
            rb.velocity = Vector3.zero;
            Vector3 elasticityVector = (origin.transform.position - spear.transform.position).normalized;
            Vector3 randomSpin = (Vector3.right * Random.Range(-1f, 1f)) + (Vector3.up * Random.Range(-1f, 1f));
            rb.AddForce(elasticityVector * 0.1f, ForceMode.Impulse);
            rb.freezeRotation = false;
            rb.AddForceAtPosition(randomSpin * 1f, spearTip.position);
            canRetract = true;

        }
        if((origin.position - spear.transform.position).magnitude < 1 && !launched)
        {
            spear.transform.parent = origin.transform;
            rb.velocity = Vector3.zero;
            spear.transform.position = origin.transform.position;
            spear.transform.rotation = origin.transform.rotation;
            canRetract = false;
        }
    }

    public void Fire()
    {
        if(!canRetract)
        {
            HarpoonLaunch();
        } else
        {
            RetractHarpoon();
        }
    }

    private void HarpoonLaunch()
    {
        if(!launched)
        {
            launched = true;
            spear.transform.parent = null;
            rb.freezeRotation = true;
            rb.AddForce(transform.forward * harpoonSpeed, ForceMode.Impulse);
        }
        
    }

    private void RetractHarpoon()
    {
        if(canRetract)
        {
            rb.velocity = Vector3.zero;
            
            Vector3 directionBack = (origin.transform.position - spear.transform.position);
            spear.transform.rotation = Quaternion.LookRotation(-directionBack);
            rb.freezeRotation = true;
            rb.AddForce(directionBack.normalized * harpoonSpeed, ForceMode.Impulse);
            launched = false;
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = Vector3.zero;
        if (other.GetComponent<Salvage>()) {
            Transform salvage = other.GetComponent<Transform>();
            salvage.transform.parent = spearTip.transform;
        }
        canRetract = true;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!canRetract)
        {
            rb.velocity = Vector3.zero;
            canRetract = true;
        }
        
    }
}
