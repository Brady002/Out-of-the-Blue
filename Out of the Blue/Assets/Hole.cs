using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private float duration = 4f;
    private float currentDuration;
    public Submarine Submarine;

    private void Start()
    {
        Appear();
    }
    private void Appear()
    {
        currentDuration = duration;
        this.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<Spray>())
        {
            if (currentDuration > 0)
            {
                currentDuration -= Time.deltaTime;
            }
            else
            {
                this.gameObject.SetActive(false);
                Submarine.HealDamage();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Ey");
        currentDuration = duration;
    }
}
