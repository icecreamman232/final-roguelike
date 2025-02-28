using System;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Build Version")]
    public class BuildVersion : ScriptableObject
    {
        [SerializeField] private string m_buildPath;
        [SerializeField] private int m_majorVersion;
        [SerializeField] private int m_minorVersion;
        [SerializeField] private int m_platformIndicator;
        [SerializeField] private string m_buildDate;
        [SerializeField] private string m_buildNumber;
        
        public string BuildPath => m_buildPath;
        
        public string GetCurrentBuildNumber()
        {
            return m_buildNumber;
        }

        public string IncreaseBuildNumber(bool increaseMajor = false, bool increaseMinor = true)
        {
            if (increaseMajor)
            {
                m_majorVersion++;
            }

            if (increaseMinor)
            {
                m_minorVersion++;
            }
            CreateBuildNumber();
            return m_buildNumber;
        }

        [ContextMenu("Test")]
        private void CreateBuildNumber()
        {
            m_buildDate = DateTime.Now.ToString("yyyyMMdd");
            m_buildNumber = $"{m_majorVersion}.{m_platformIndicator}.{m_minorVersion}.{m_buildDate}";
        }
    }
}

