using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class HeartUIController : MonoBehaviour
    {
        [SerializeField] private GameObject m_emptyVisual;
        [SerializeField] private GameObject m_fullVisual;

        public void SetEmpty()
        {
            m_emptyVisual.SetActive(true);
            m_fullVisual.SetActive(false);
        }

        public void SetFull()
        {
            m_emptyVisual.SetActive(false);
            m_fullVisual.SetActive(true);
        }
    }
}

