using SGGames.Scripts.Modifiers;
using UnityEngine;

public class TestModifierForEnemy : MonoBehaviour
{
    public Modifier[] Modifiers;
    private bool m_isTriggered;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_isTriggered) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            m_isTriggered = true;
            var handler = other.gameObject.GetComponentInChildren<EnemyModifierHandler>();
            if (handler == null) return;
            foreach (var modifier in Modifiers)
            {
                handler.RegisterModifier(modifier);
            }
        }
    }
}
