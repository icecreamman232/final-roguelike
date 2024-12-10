using SGGames.Scripts.Modifier;
using UnityEngine;

public class ModifierTester : MonoBehaviour
{
    [SerializeField] private GameObject m_target;
    [SerializeField] private MovementModifierInfo m_movementModifier;
    
    [ContextMenu("Register Movement Modifier")]
    private void TriggerMovementModifier()
    {
        var handler = m_target.GetComponentInChildren<ModifierHandler>();
        handler.RegisterModifier(m_movementModifier);
    }
}
