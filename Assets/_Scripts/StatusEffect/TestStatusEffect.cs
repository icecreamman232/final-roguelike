using SGGames.Scripts.StatusEffects;
using UnityEngine;

public class TestStatusEffect : MonoBehaviour
{
    public StatusEffectData statusEffectData;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var statusEffectHandler = other.GetComponentInChildren<StatusEffectHandler>();
            statusEffectHandler.AddStatus(statusEffectData);
        }
    }
}
