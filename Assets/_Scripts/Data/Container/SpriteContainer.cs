using UnityEngine;

namespace SGGames.Scripts.Common
{
    [CreateAssetMenu(fileName = "Sprite Container", menuName = "SGGames/Container/Sprite")]
    public class SpriteContainer : GenericContainer<Sprite>
    {
        public override Sprite GetItemAtIndex(int index)
        {
            return m_container[index];
        }
    }
}

