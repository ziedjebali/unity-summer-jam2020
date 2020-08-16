using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    const float k_Cooldown = 0.5f;
    
    [SerializeField] float m_TrailTimerInSeconds = 2f;
    
    [Header("References")]
    [SerializeField] TrailRenderer m_TrailRenderer = null;
    [SerializeField] GameObject m_ClonePrefab = null;
    
    bool m_IsTrailing;
    int m_TrailMaxCount;
    Queue<MovementInfo> m_TrailMovement;
    bool m_OnCooldown = false;

    
    void Start()
    {
        m_TrailRenderer = GetComponent<TrailRenderer>();
        m_TrailRenderer.emitting = false;
        
        m_IsTrailing= false;
        m_TrailMovement = new Queue<MovementInfo>();
        
        m_TrailMaxCount = (int)(m_TrailTimerInSeconds * 50); // 50 calls per second of FixedUpdate
    }

    void Update()
    {
        if (ShouldStartTrail())
        {
            m_IsTrailing = true;
            m_TrailRenderer.emitting = true;
            ClearTrail();
        }
        
        if (ShouldStopTrail())
        {
            m_IsTrailing = false;
            m_TrailRenderer.emitting = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !m_OnCooldown)
        {
            m_OnCooldown = true;
            SpawnClone();
            StartCoroutine(DoCooldown());
        }
    }
    
    public void AddTrail(MovementInfo moveInfo)
    {
        if (m_IsTrailing)
        {
            if (!m_TrailMovement.Any() && Mathf.Approximately(moveInfo.velocityValue.magnitude, 0.0f))
                return;
            
            
            m_TrailMovement.Enqueue(moveInfo);
            if (m_TrailMovement.Count > m_TrailMaxCount)
                m_TrailMovement.Dequeue();
        }
    }
    
    void SpawnClone()
    {
        var playerTransform = transform; // apparently multiple property access is inefficient
        var clone = Instantiate(m_ClonePrefab, playerTransform.position, playerTransform.rotation);
        var cm = clone.GetComponent<CloneMovement>();
        cm.SetMovements(new Queue<MovementInfo>(GetTrailMovement()));
    }

    Queue<MovementInfo> GetTrailMovement()
    {
        var trail = new Queue<MovementInfo>(m_TrailMovement);
        
        return trail;
    }
    
    void ClearTrail()
    {
        m_TrailMovement = new Queue<MovementInfo>();
        m_TrailRenderer.Clear();
    }

    bool ShouldStartTrail()
    {
        return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
    }

    bool ShouldStopTrail()
    {
        return Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) || m_TrailMovement.Count == m_TrailMaxCount;
    }
    
    IEnumerator DoCooldown()
    {
        yield return new WaitForSeconds(k_Cooldown);
        m_OnCooldown = false;
    }
}
