using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UIElements;

public class Submarine : MonoBehaviour
{
    [Header("Upgrades")]
    public Upgrades upgrade;

    [Header("Movement")]
    private Rigidbody rb;
    public HingeJoint forwardLever;
    public HingeJoint sidewaysLever;
    public HingeJoint ascentLever;
    public HingeJoint rotationLever;
    private float engineSpeed;

    [Header("UI")]
    public TMP_Text salvageScreen;
    public UnityEngine.UI.Slider oxygenBarUI;
    [SerializeField] private int currentSalvageValue = 0;
    [SerializeField] private GameObject harpoonButton;
    private Rigidbody buttonrb;

    [Header("Oxygen")]
    private float maxO2;
    private float currentO2;
    public bool oxygenDraining = true;
    private bool O2Damage = false;
    [SerializeField] private float hurtTimer = 3.5f;

    [Header("Damage")]
    [SerializeField] private GameObject[] holes = new GameObject[4];
    private int currentDamageAmount;
    public bool devDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        buttonrb = harpoonButton.GetComponent<Rigidbody>();
        RecoverSalvage(0);
        ApplyUpgrades();
        currentO2 = maxO2;
        oxygenBarUI.maxValue = maxO2;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Oxygen();
        if(devDamage)
        {
            TakeDamage(1);
        }
        oxygenBarUI.value = currentO2;
    }

    private void Move()
    {
        float vertical = ascentLever.angle / ascentLever.limits.max;
        float horizontal = sidewaysLever.angle / sidewaysLever.limits.max;
        float forward = forwardLever.angle / forwardLever.limits.max;
        float yRotate = rotationLever.angle / rotationLever.limits.max;
        if (vertical < .1 & vertical > -.11)
        {
            vertical = 0;
        }
        if (horizontal < .1 & horizontal > -.1)
        {
            horizontal = 0;
        }
        if (forward < .1 & forward > -.1)
        {
            forward = 0;
        }
        if (yRotate < .1 & yRotate > -.1)
        {
            yRotate = 0;
        }

        rb.AddForce(rb.transform.forward * (forward * engineSpeed) + rb.transform.right * horizontal + rb.transform.up * vertical, ForceMode.Force);
        buttonrb.AddForce((rb.transform.up * vertical) * buttonrb.mass, ForceMode.Force);

        rb.AddForceAtPosition(new Vector3(yRotate / 4, 0f, 0f), new Vector3(0f, 0f, 1f));
        rb.AddForceAtPosition(new Vector3(-yRotate / 4, 0f, 0f), new Vector3(0f, 0f, -1f));
    }

    private void Oxygen()
    {
        if(oxygenDraining)
        {
            if (currentO2 > 0)
            {
                currentO2 -= Time.deltaTime * (currentDamageAmount + 1);
            }
            else
            {
                if (!O2Damage)
                {
                    O2Damage = true;
                }
                hurtTimer -= Time.deltaTime;
                if (hurtTimer < 0)
                {
                    hurtTimer = 3.5f;
                    TakeDamage(1);
                }
            }
        } else
        {
            if(O2Damage)
            {
                O2Damage = false;
            }

            if(currentO2 < maxO2)
            {
                currentO2 += Time.deltaTime * 10f;
            }
        }
        
        
    }

    public void TakeDamage(int amount)
    {
        if(currentDamageAmount != holes.Length)
        {
            for (int a = 0; a < amount; a++)
            {
                int index = Random.Range(0, holes.Length);
                while (holes[index].active == true)
                {
                    if (index == holes.Length)
                    {
                        index = 0;
                    } else
                    {
                        index++;
                    }
                    
                }
                holes[index].SetActive(true);
                currentDamageAmount++;
            }
        }
        

        if(currentDamageAmount > holes.Length)
        {
            currentDamageAmount = holes.Length;
        } else if(currentDamageAmount < 0)
        {
            currentDamageAmount = 0;
        }
        devDamage = false;
    }

    public void HealDamage()
    {
        currentDamageAmount--;
    }

    public void RecoverSalvage(int value)
    {
        currentSalvageValue += value;
        salvageScreen.text = "Salavage: " + currentSalvageValue.ToString();
    }

    private void ApplyUpgrades()
    {
        maxO2 = upgrade.oxygenTank[upgrade.oxygenLevel];
        engineSpeed = upgrade.engineSpeed[upgrade.engineLevel];
    }

    private void OnCollisionEnter(Collision collision)
    {
        float speed = rb.velocity.magnitude;
        if(speed <= 1.5f)
        {
            TakeDamage(1);
        } else if(speed > 1.5 && speed <= 2.5f )
        {
            TakeDamage(2);
        } else if(speed > 2.5f)
        {
            TakeDamage(3);
        }
    }
}
