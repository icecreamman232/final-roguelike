using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class ModifierUIGroupPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform m_background;
        [SerializeField] private ModifierUIDescription modifierDescTemplate;
        
        public void ResetView()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        
        
        public void Show(WeaponData data)
        {
            if (data.ModifierList.Length <= 0) return;

            for (int i = 0; i < data.ModifierList.Length; i++)
            {
                var desc = Instantiate(modifierDescTemplate, transform);
                desc.FillDescription(data.ModifierList[i].Description);
                var curBGSize = m_background.sizeDelta;
                curBGSize.y += desc.GetContentHeight();
                m_background.sizeDelta = curBGSize;
            }
        }
        
        public void Show(HelmetData data)
        {
            if (data.ModifierList.Length <= 0) return;

            for (int i = 0; i < data.ModifierList.Length; i++)
            {
                var desc = Instantiate(modifierDescTemplate, transform);
                desc.FillDescription(data.ModifierList[i].Description);
                var curBGSize = m_background.sizeDelta;
                curBGSize.y += desc.GetContentHeight();
                m_background.sizeDelta = curBGSize;
            }
        }
        
        public void Show(AccessoriesData data)
        {
            if (data.ModifierList.Length <= 0) return;

            for (int i = 0; i < data.ModifierList.Length; i++)
            {
                var desc = Instantiate(modifierDescTemplate, transform);
                desc.FillDescription(data.ModifierList[i].Description);
                var curBGSize = m_background.sizeDelta;
                curBGSize.y += desc.GetContentHeight();
                m_background.sizeDelta = curBGSize;
            }
        }
        
        public void Show(ArmorData data)
        {
            if (data.ModifierList.Length <= 0) return;

            for (int i = 0; i < data.ModifierList.Length; i++)
            {
                var desc = Instantiate(modifierDescTemplate, transform);
                desc.FillDescription(data.ModifierList[i].Description);
                var curBGSize = m_background.sizeDelta;
                curBGSize.y += desc.GetContentHeight();
                m_background.sizeDelta = curBGSize;
            }
        }
        
        public void Show(GlovesData data)
        {
            if (data.ModifierList.Length <= 0) return;

            for (int i = 0; i < data.ModifierList.Length; i++)
            {
                var desc = Instantiate(modifierDescTemplate, transform);
                desc.FillDescription(data.ModifierList[i].Description);
                var curBGSize = m_background.sizeDelta;
                curBGSize.y += desc.GetContentHeight();
                m_background.sizeDelta = curBGSize;
            }
        }
        
        public void Show(BootsData data)
        {
            if (data.ModifierList.Length <= 0) return;

            for (int i = 0; i < data.ModifierList.Length; i++)
            {
                var desc = Instantiate(modifierDescTemplate, transform);
                desc.FillDescription(data.ModifierList[i].Description);
                var curBGSize = m_background.sizeDelta;
                curBGSize.y += desc.GetContentHeight();
                m_background.sizeDelta = curBGSize;
            }
        }
        
        public void Show(CharmData data)
        {
            if (data.ModifierList.Length <= 0) return;

            for (int i = 0; i < data.ModifierList.Length; i++)
            {
                var desc = Instantiate(modifierDescTemplate, transform);
                desc.FillDescription(data.ModifierList[i].Description);
                var curBGSize = m_background.sizeDelta;
                curBGSize.y += desc.GetContentHeight();
                m_background.sizeDelta = curBGSize;
            }
        }
    }
}

