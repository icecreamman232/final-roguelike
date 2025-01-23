using UnityEngine;

namespace SGGames.Scripts.Common
{
    [CreateAssetMenu(fileName = "Sprite Container", menuName = "SGGames/Container/Sprite")]
    public class SpriteContainer : ScriptableObject
    {
        [SerializeField] private Sprite[] m_spriteContainer;
        public Sprite GetSprite(int index) => m_spriteContainer[index];
    }
}

