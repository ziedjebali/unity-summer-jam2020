using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    [SerializeField]
    PlayerMovement m_PlayerMovement;
    
    Rigidbody rb;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (m_PlayerMovement.ShadowMovement.Count != m_PlayerMovement.MaxMovementCount)
        {
            // Do nothing, the initial delay is not over.
        }
        else
        {
            var moveInfo = m_PlayerMovement.ShadowMovement.Peek();
            rb.drag = moveInfo.dragValue;
            rb.AddForce(moveInfo.forceValue);
        }
    }
}
