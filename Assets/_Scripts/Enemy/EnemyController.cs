using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected EnemyBrain m_currentBrain;
        
        public EnemyBrain CurrentBrain => m_currentBrain;

        public void SetActiveBrain(EnemyBrain newBrain)
        {
            m_currentBrain = newBrain;
        }
    }
}

