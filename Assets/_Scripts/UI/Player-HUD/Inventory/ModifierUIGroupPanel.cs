using SGGames.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGGames.Scripts.UI
{
    public class ModifierUIGroupPanel : MonoBehaviour
    { 
        [SerializeField] private ModifierUIDescription modifierDescTemplate;

        public void Show(WeaponData data)
        {
            if (data.ModifierList.Length <= 0) return;

            for (int i = 0; i < data.ModifierList.Length; i++)
            {
                var desc = Instantiate(modifierDescTemplate, transform);
                desc.FillDescription(data.ModifierList[i].Description);
            }
        }
    }
}

