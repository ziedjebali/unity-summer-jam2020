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
        if (m_PlayerMovement.m_ShadowMovement.Count != m_PlayerMovement.m_MaxMovementCount)
        {
            // Do nothing, the initial delay is not over.
        }
        else
        {
            var moveInfo = m_PlayerMovement.m_ShadowMovement.Peek();
            rb.drag = moveInfo.dragValue;
            rb.AddForce(moveInfo.forceValue);
        }
    }
}
