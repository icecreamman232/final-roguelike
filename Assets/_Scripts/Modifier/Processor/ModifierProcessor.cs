using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class ModifierProcessor : MonoBehaviour
    {
        [SerializeField] protected bool m_isProcessing;
        
        protected virtual void Update()
        {
            if (!m_isProcessing) return;
        }
    }

    public interface IOneTimeModifierProcessor
    {
        public void TriggerModifier();
    }

    public interface IOverTimeModifierProcessor
    {
        public void StartModifier();
        public void StopModifier();
    }
}

