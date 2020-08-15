using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
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
//            m_Rigidbody.drag = moveInfo.dragValue;
//            m_Rigidbody.AddForce(moveInfo.forceValue);
            m_Rigidbody.velocity = moveInfo.velocityValue;
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero;
        }
    }

    public void SetMovements(Queue<MovementInfo> value)
    {
        m_PresetMovements = value;
    }
}
