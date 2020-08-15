using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_Acceleration = 20f;
    [SerializeField] float m_MaxSpeed = 10f;
    [SerializeField] float m_Deceleration = 5f;
    
//    [Header("TESTING")]
//    [SerializeField] float m_CompensationThreshold = 0f;
//    [SerializeField] float m_CompensationSmoothing = 0.5f;
    
    [Header("References")]
    [SerializeField] PlayerTrail m_PlayerTrail = null;
    
    Rigidbody m_Rigidbody;
    
    // Movement
    Vector3 m_Movement;
    float m_BaseDrag;
    
    public bool MovementEnabled = false;
    
    public void ToggleMovement()
    {
        if (MovementEnabled)
        {
            MovementEnabled = false;
        }else
        {
            MovementEnabled = true;
        }
    }

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_BaseDrag = m_Rigidbody.drag;
    }

    void Update()
    {
        if (MovementEnabled)
        {
            m_Movement.x = Input.GetAxisRaw("Horizontal");
            m_Movement.z = Input.GetAxisRaw("Vertical");
            m_Movement.y = 0f;
        }
    }

    void FixedUpdate()
    {
        MovementInfo moveInfo;

        // Compute acceleration
        var direction = m_Movement.normalized;
        var velocityToAdd = direction * m_Acceleration * Time.fixedDeltaTime;
        var newVelocity = m_Rigidbody.velocity + velocityToAdd;
        
        // Compute deceleration
        if (Mathf.Approximately(m_Movement.x, 0.0f))
            newVelocity.x = ComputeDeceleration(m_Rigidbody.velocity.x);
        if (Mathf.Approximately(m_Movement.z, 0.0f))
            newVelocity.z = ComputeDeceleration(m_Rigidbody.velocity.z);
        
        // Set new velocity
        if (newVelocity.magnitude < m_MaxSpeed)
        {
            moveInfo.velocityValue = newVelocity;
            m_Rigidbody.velocity = newVelocity;
        }
        else
        {
            moveInfo.velocityValue = m_Rigidbody.velocity;
        }
        m_PlayerTrail.AddTrail(moveInfo);
        
//        // Not holding down any move buttons
//        if (Mathf.Approximately(m_Movement.x, 0.0f) && Mathf.Approximately(m_Movement.z, 0.0f))
//        {
//            m_Rigidbody.velocity = new Vector3(ComputeDeceleration(m_Rigidbody.velocity.x), 0f, ComputeDeceleration(m_Rigidbody.velocity.z));
//            
//            moveInfo.dragValue = m_Deceleration;
//            moveInfo.forceValue = Vector3.zero;
//        }
//        else
//        {
//            Vector3 forceToAdd = Vector3.zero;
//            
//            // Holding down only one direction
//            if (Mathf.Approximately(m_Movement.x, 0.0f) || Mathf.Approximately(m_Movement.z, 0.0f))
//                forceToAdd = m_Rigidbody.velocity.magnitude < m_MaxSpeed ? m_Movement.normalized * m_Force : Vector3.zero;
//            
//            // Holding down only multiple directions
//            else
//            {
//                var velocityDifference = Mathf.Abs(m_Rigidbody.velocity.x) - Mathf.Abs(m_Rigidbody.velocity.z);
//
//
//
//                if (velocityDifference > m_CompensationThreshold) // Should compensate towards Z
//                {
//                    var newDirection = new Vector3(m_CompensationSmoothing * m_Movement.x, 0, m_Movement.z);
//                    forceToAdd = m_Rigidbody.velocity.magnitude < m_MaxSpeed ? newDirection.normalized * m_Force : Vector3.zero;
//                }
//                else if (velocityDifference < m_CompensationThreshold) // Should compensate towards X
//                {
//                    var newDirection = new Vector3(m_Movement.x, 0, m_CompensationSmoothing * m_Movement.z);
//                    forceToAdd = m_Rigidbody.velocity.magnitude < m_MaxSpeed ? newDirection.normalized * m_Force : Vector3.zero;
//                }
//                else // No compensation needed
//                    forceToAdd = m_Rigidbody.velocity.magnitude < m_MaxSpeed ? m_Movement.normalized * m_Force : Vector3.zero;
//            }
//            
//            m_Rigidbody.drag = m_BaseDrag;
//            m_Rigidbody.AddForce(forceToAdd);
//            
//            moveInfo.dragValue = m_BaseDrag;
//            moveInfo.forceValue = forceToAdd;
//        }
//        
//
//        moveInfo.velocityValue = m_Rigidbody.velocity;
//        m_PlayerTrail.AddTrail(moveInfo);
    }
    
    float ComputeDeceleration(float speed)
    {
        return Mathf.Sign(speed) * Mathf.Max(0f, Mathf.Abs(speed) - m_Deceleration * Time.fixedDeltaTime);
    }
}
