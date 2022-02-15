using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    public Transform t;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        
        rb.velocity = Vector3.forward * 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (t.position.z >= 11.5)
        {
            rb.velocity = Vector3.back * 3;
        } else if (t.position.z <= -11.5)
        {
            rb.velocity = Vector3.forward * 3;
        }
    }
}
