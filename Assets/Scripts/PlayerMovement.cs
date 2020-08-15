using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_Force = 20f;
    [SerializeField] float m_MaxSpeed = 10f;
    [SerializeField] float m_Deceleration = 5f;
    
    [Header("Cloning")]
    [SerializeField] float m_CloneTimer = 3f; // In seconds
    [SerializeField] GameObject m_ClonePrefab = null;

    Rigidbody m_Rigidbody;
    
    Vector3 m_Movement;
    float m_BaseDrag;
    
    public Queue<MovementInfo> m_AccumulatedMovement { get; private set; }
    public Queue<MovementInfo> m_ShadowMovement { get; private set; }
    public int m_MaxMovementCount { get; private set; }

    void Start()
    {
        m_AccumulatedMovement = new Queue<MovementInfo>();
        m_ShadowMovement = new Queue<MovementInfo>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_BaseDrag = m_Rigidbody.drag;
        m_MaxMovementCount = (int)(m_CloneTimer * 50); // 50 calls per second of FixedUpdate
    }

    void Update()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        m_Movement.z = Input.GetAxisRaw("Vertical");
        m_Movement.y = 0f;

        if (Input.GetKeyDown("space"))
            SpawnClone();
    }

    void FixedUpdate()
    {
        MovementInfo moveInfo;
        
        if (Mathf.Approximately(m_Movement.x, 0.0f) && Mathf.Approximately(m_Movement.z, 0.0f))
        {
            m_Rigidbody.drag = m_Deceleration;
            
            moveInfo.dragValue = m_Deceleration;
            moveInfo.forceValue = new Vector3();
        }
        else
        {
            m_Rigidbody.drag = m_BaseDrag;

            var normalizedMovement = m_Movement.normalized;
            var forceToAdd = m_Rigidbody.velocity.magnitude < m_MaxSpeed ? normalizedMovement * m_Force : new Vector3();
            m_Rigidbody.AddForce(forceToAdd);
            
            moveInfo.dragValue = m_BaseDrag;
            moveInfo.forceValue = forceToAdd;
        }

        AccumulateMovement(moveInfo);
    }

    void SpawnClone()
    {
        var playerTransform = transform; // apparently multiple property access is inefficient
        var clone = Instantiate(m_ClonePrefab, playerTransform.position, playerTransform.rotation);
        var cm = clone.GetComponent<CloneMovement>();
        cm.presetMovements = new Queue<MovementInfo>(m_AccumulatedMovement);
        m_AccumulatedMovement = new Queue<MovementInfo>();
    }

    void AccumulateMovement(MovementInfo nextMovement)
    {
        if (m_AccumulatedMovement.Count == m_MaxMovementCount)
            m_AccumulatedMovement.Dequeue();
        m_AccumulatedMovement.Enqueue(nextMovement);
        
        if (m_ShadowMovement.Count == m_MaxMovementCount)
            m_ShadowMovement.Dequeue();
        m_ShadowMovement.Enqueue(nextMovement);
    }
}
