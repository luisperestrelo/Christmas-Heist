using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMomentum : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float maxMomentum = 20f; // Example max speed
    private float currentMomentum; 
    private float displayedMomentum; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public float GetMomentumValue()
    {
        Debug.Log(rb.velocity.magnitude);
        return rb.velocity.magnitude;
    }

    public float GetMaxMomentum()
    {
        return maxMomentum;
    }
}
