using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public int[] oxygenTank = new int[4] {120, 180, 240, 300};
    public int oxygenLevel = 0;
    public float[] engineSpeed = new float[3] { 1f, 1.5f, 2f };
    public int engineLevel = 0;
}
