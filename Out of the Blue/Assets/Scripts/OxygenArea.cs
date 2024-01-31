using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Submarine Submarine))
        {
            Submarine.oxygenDraining = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Submarine Submarine))
        {
            Submarine.oxygenDraining = true;
        }
    }
}
