using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField] float m_TrailTimerInSeconds = 2f;
    
    [Header("References")]
    [SerializeField] TrailRenderer m_TrailRenderer = null;
    [SerializeField] GameObject m_ClonePrefab = null;
    
    bool m_IsTrailing;
    Queue<MovementInfo> m_TrailMovement;
    
    public int TrailMaxCount { get; private set; }

    
    void Start()
    {
        m_TrailRenderer = GetComponent<TrailRenderer>();
        m_TrailRenderer.emitting = false;
        
        m_IsTrailing= false;
        m_TrailMovement = new Queue<MovementInfo>();
        
        TrailMaxCount = (int)(m_TrailTimerInSeconds * 50); // 50 calls per second of FixedUpdate
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            m_IsTrailing = true;
            m_TrailRenderer.emitting = true;
            ClearTrail();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            m_IsTrailing = false;
            m_TrailRenderer.emitting = false;
        }

        if (Input.GetKeyDown("space"))
        {
            SpawnClone();
        }
    }
    
    void SpawnClone()
    {
        var playerTransform = transform; // apparently multiple property access is inefficient
        var clone = Instantiate(m_ClonePrefab, playerTransform.position, playerTransform.rotation);
        var cm = clone.GetComponent<CloneMovement>();
        cm.SetMovements(new Queue<MovementInfo>(GetTrailMovement()));
    }

    public void AddTrail(MovementInfo moveInfo)
    {
        if (m_IsTrailing)
        {
            m_TrailMovement.Enqueue(moveInfo);
            if (m_TrailMovement.Count == TrailMaxCount)
                m_TrailMovement.Dequeue();
        }
    }

    Queue<MovementInfo> GetTrailMovement()
    {
        var trail = new Queue<MovementInfo>(m_TrailMovement);
        ClearTrail();
        
        return trail;
    }
    
    

    void ClearTrail()
    {
        m_TrailMovement = new Queue<MovementInfo>();
        m_TrailRenderer.Clear();
    }
}
