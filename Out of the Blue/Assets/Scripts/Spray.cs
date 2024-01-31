using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    [SerializeField]
    GameObject projectile = null;

    private void Start()
    {
        Stop();
    }
    public void Fire()
    {
        projectile.SetActive(true);
        projectile.GetComponent<ParticleSystem>().Play();
    }

    public void Stop()
    {
        projectile.SetActive(false);
        projectile.GetComponent<ParticleSystem>().Stop();
    }
}
