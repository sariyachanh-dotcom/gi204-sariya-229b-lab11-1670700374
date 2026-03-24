using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    public static List<Gravity> otherObjectsList;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectsList == null )
        {
            otherObjectsList = new List<Gravity>();
        }
        otherObjectsList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectsList)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRB = other.rb;

        Vector3 direction = rb.position - otherRB.position;

        float distance = direction.magnitude;

        if (distance == 0f) { return; }

        float forceMagnitude = G * (rb.mass * otherRB.mass) / Mathf.Pow(distance, 2);

        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRB.AddForce(gravityForce);
    }

}
