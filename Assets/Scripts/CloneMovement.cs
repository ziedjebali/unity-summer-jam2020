using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    public Queue<Vector3> presetMovements;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (presetMovements.Any())
            rb.MovePosition(rb.position +  presetMovements.Dequeue());
    }
}
