using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public float currentSpeed;
    public Text speedText;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentSpeed = (int)(rb.velocity.magnitude * 3.6f);

        speedText.text = "Speed: " + currentSpeed.ToString() + " KPH";
    }
}
