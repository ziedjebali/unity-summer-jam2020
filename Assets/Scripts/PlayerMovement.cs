using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_Force = 20f;
    [SerializeField] float m_MaxSpeed = 10f;
    [SerializeField] float m_Deceleration = 5f;
    
    [Header("References")]
    [SerializeField] PlayerTrail m_PlayerTrail = null;
    
    Rigidbody m_Rigidbody;
    
    // Movement
    Vector3 m_Movement;
    float m_BaseDrag;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_BaseDrag = m_Rigidbody.drag;
    }

    void Update()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        m_Movement.z = Input.GetAxisRaw("Vertical");
        m_Movement.y = 0f;
    }

    void FixedUpdate()
    {
        MovementInfo moveInfo;
        
        if (Mathf.Approximately(m_Movement.x, 0.0f) && Mathf.Approximately(m_Movement.z, 0.0f))
        {
            m_Rigidbody.drag = m_Deceleration;
            
            moveInfo.dragValue = m_Deceleration;
            moveInfo.forceValue = Vector3.zero;
        }
        else
        {
            m_Rigidbody.drag = m_BaseDrag;

            var normalizedMovement = m_Movement.normalized;
            var forceToAdd = m_Rigidbody.velocity.magnitude < m_MaxSpeed ? normalizedMovement * m_Force : Vector3.zero;
            m_Rigidbody.AddForce(forceToAdd);
            
            moveInfo.dragValue = m_BaseDrag;
            moveInfo.forceValue = forceToAdd;
        }

        moveInfo.velocityValue = m_Rigidbody.velocity;
        m_PlayerTrail.AddTrail(moveInfo);
    }
}
