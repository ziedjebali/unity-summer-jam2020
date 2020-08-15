using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    public Queue<MovementInfo> presetMovements;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (presetMovements.Any())
        {
            var moveInfo = presetMovements.Dequeue();
            rb.drag = moveInfo.dragValue;
            rb.AddForce(moveInfo.forceValue);
        }
    }
}
