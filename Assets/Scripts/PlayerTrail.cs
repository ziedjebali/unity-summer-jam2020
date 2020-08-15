using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField] float m_MaxTrailTimer = 3f;
    
    [Header("References")]
    [SerializeField] GameObject m_TrailPrefab = null;
    
    bool m_IsTrailing;
    Queue<GameObject> m_TrailObjects;
    Queue<MovementInfo> m_TrailMovement;
    
    public int TrailMaxCount { get; private set; }

    
    void Start()
    {
        m_IsTrailing= false;
        m_TrailObjects = new Queue<GameObject>();
        m_TrailMovement = new Queue<MovementInfo>();
        
        TrailMaxCount = (int)(m_MaxTrailTimer * 50); // 50 calls per second of FixedUpdate
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            m_IsTrailing = true;
            ClearTrail();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            m_IsTrailing = false;
    }

    public void AddTrail(MovementInfo moveInfo)
    {
        if (m_IsTrailing)
        {
            var playerTransform = transform; // apparently multiple property access is inefficient
            var trail = Instantiate(m_TrailPrefab, playerTransform.position, playerTransform.rotation);

            m_TrailObjects.Enqueue(trail);
            m_TrailMovement.Enqueue(moveInfo);
            
            if (m_TrailMovement.Count == TrailMaxCount)
            {
                Destroy(m_TrailObjects.Dequeue());
                m_TrailMovement.Dequeue();
            }
        }
    }

    public Queue<MovementInfo> GetTrailMovement()
    {
        var trail = new Queue<MovementInfo>(m_TrailMovement);
        ClearTrail();
        
        return trail;
    }

    void ClearTrail()
    {
        while (m_TrailObjects.Count != 0)
        {
            Destroy(m_TrailObjects.Dequeue());
        }
        
        m_TrailMovement = new Queue<MovementInfo>();
    }
}
