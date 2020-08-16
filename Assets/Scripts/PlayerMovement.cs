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

    public GameManager gm;
    
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
        if(transform.position.y <= -5)
        {
            gm.ResetScene();
        }

        if (MovementEnabled)
        {
            m_Movement.x = Input.GetAxisRaw("Horizontal");
            m_Movement.z = Input.GetAxisRaw("Vertical");
            m_Movement.y = 0f;
        }
    }

    void FixedUpdate()
    {
        if (MovementEnabled)
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
        }
        
        
    }
    
    float ComputeDeceleration(float speed)
    {
        return Mathf.Sign(speed) * Mathf.Max(0f, Mathf.Abs(speed) - m_Deceleration * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            gm.ResetScene();
        }
    }
}
