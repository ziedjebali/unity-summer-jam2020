using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    const float k_DelayBeforeDestroy = 1.0f;
    
    [SerializeField] float m_EndOffset = 0.4f;
    [SerializeField] int m_EndOffsetCount = 1;
    [SerializeField] float m_StartOffset = 1;

    Rigidbody m_Rigidbody;
    Queue<MovementInfo> m_PresetMovements;

    Vector3 lastDir;
    int didOffsetCount = 0;
    
    
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        // continue last movement a bit more
        var list = m_PresetMovements.ToArray().ToList();
        for (int i = m_PresetMovements.Count - 1; i >= 0; ++i)
        {
            if (Mathf.Approximately(list[i].velocityValue.magnitude, 0.0f))
                continue;
            else
            {
                lastDir = list[i].velocityValue.normalized;
                break;
            }
        }
        
        var firstMove = m_PresetMovements.Peek();
        var firstDir = firstMove.velocityValue.normalized;

        var offsetVector = firstDir * m_StartOffset;
        m_Rigidbody.MovePosition(transform.position+offsetVector);
        
        
    }
    
    void FixedUpdate()
    {
        if (m_PresetMovements.Any())
        {
            var moveInfo = m_PresetMovements.Dequeue();
            
            m_Rigidbody.velocity = moveInfo.velocityValue;
        }
        else
        {
            if (didOffsetCount < m_EndOffsetCount)
            {
                ++didOffsetCount;
                var offsetVector = lastDir * m_EndOffset;
                m_Rigidbody.MovePosition(transform.position + offsetVector);

                return;
            }
            
            m_Rigidbody.velocity = Vector3.zero;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    public void SetMovements(Queue<MovementInfo> value)
    {
        m_PresetMovements = value;
    }
    
    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(k_DelayBeforeDestroy);
        Destroy(gameObject);
    }
}
