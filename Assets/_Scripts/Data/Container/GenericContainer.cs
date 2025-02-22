using System.Collections.Generic;
using UnityEngine;

namespace SGGames.Scripts.Common
{
    public abstract class GenericContainer<T> :ScriptableObject
    {
        [SerializeField] protected List<T> m_container;
        public abstract T GetItemAtIndex(int index);
    }
}

