using UnityEngine;

public class Goal : MonoBehaviour
{
    const string k_CharacterLayerName = "Main Character";

    public bool IsPressed { get; private set; }

    int m_InsideCount;
    
    
    // TODO: Find a better way, some edge cases don't work (e.g. when clone is destroyed while inside the goal)
    void OnTriggerEnter(Collider other)
    {
        if (IsPlayerOrClone(other))
        {
            ++m_InsideCount;
            
            // First one to get inside
            if (m_InsideCount == 1)
            {
                var sr = GetComponentInChildren<SpriteRenderer>();
                sr.color = Color.green;
                IsPressed = true;
            }
        }
    }
    void OnTriggerExit (Collider other)
    {
        if (IsPlayerOrClone(other))
        {
            --m_InsideCount;
            if (m_InsideCount == 0)
            {
                var sr = GetComponentInChildren<SpriteRenderer>();
                sr.color = Color.red;
                IsPressed = false;
            }
        }
    }

    bool IsPlayerOrClone(Collider other)
    {
        return other.gameObject.layer == LayerMask.NameToLayer(k_CharacterLayerName) && !other.CompareTag("Shadow");
    }
}
