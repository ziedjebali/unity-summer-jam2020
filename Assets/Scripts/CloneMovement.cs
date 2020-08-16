using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    const float k_DelayBeforeDestroy = 1.0f;
    
    [SerializeField] int m_EndFramesOmitted = 7;

    Rigidbody m_Rigidbody;
    Queue<MovementInfo> m_PresetMovements;
    
    
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (m_PresetMovements.Count > m_EndFramesOmitted)
        {
            var moveInfo = m_PresetMovements.Dequeue();
            m_Rigidbody.velocity = moveInfo.velocityValue;
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero;
            StartCoroutine(InitializeTurret());
        }
    }

    public void SetMovements(Queue<MovementInfo> value)
    {
        m_PresetMovements = value;
    }
    
    IEnumerator InitializeTurret()
    {
        yield return new WaitForSeconds(k_DelayBeforeDestroy);
        Destroy(gameObject);
    }
}
