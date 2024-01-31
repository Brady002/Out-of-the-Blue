using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvage : MonoBehaviour
{

    public int value;
    private bool speared = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Harpoon>())
        {
            speared = true;
        }
        if (other.TryGetComponent<Submarine>(out Submarine submarine))
        {
            submarine.RecoverSalvage(value);
            Destroy(gameObject);
        }
    }
}
