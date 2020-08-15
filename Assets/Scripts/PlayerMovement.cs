using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float m_MoveSpeed = 5f;
    
    [Header("Cloning")]
    [SerializeField] float m_CloneTimer = 3f; // In seconds

    [Header("References")]
    [SerializeField] Rigidbody m_Rigidbody = null;
    [SerializeField] GameObject m_ClonePrefab = null;

    Vector3 m_Movement;
    Queue<Vector3> m_AccumulatedMovement = new Queue<Vector3>();
    int m_MaxAccumulatedMovementCount;

    void Start()
    {
        m_MaxAccumulatedMovementCount = (int)(m_CloneTimer * 50); // 50 calls per second of FixedUpdate
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
        Vector3 nextMovement = m_Movement * m_MoveSpeed * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + nextMovement);

        if (m_AccumulatedMovement.Count > m_MaxAccumulatedMovementCount)
            m_AccumulatedMovement.Dequeue();
        m_AccumulatedMovement.Enqueue(nextMovement);
    }

    void SpawnClone()
    {
        var playerTransform = transform; // apparently multiple property access is inefficient
        var clone = Instantiate(m_ClonePrefab, playerTransform.position, playerTransform.rotation);
        var cm = clone.GetComponent<CloneMovement>();
        cm.presetMovements = new Queue<Vector3>(m_AccumulatedMovement);
        m_AccumulatedMovement = new Queue<Vector3>();
    }
}
