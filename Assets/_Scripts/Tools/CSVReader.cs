#if UNITY_EDITOR
using SGGames.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.Tools
{
    public class CSVReader 
    {
        [MenuItem("SGGames/Tools/Import Constant")]
        public static void ImportConstantData()
        {
            var constantDataAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/_Data/constant-data.csv");
            var lines = constantDataAsset.text.Split("\n");
            
            var max_atk_spd                        = float.Parse(lines[0].Split(',')[1]);
            var str_to_regeneration_rate           = float.Parse(lines[1].Split(',')[1]);
            var str_to_health                      = float.Parse(lines[2].Split(',')[1]);
            var agi_to_atk_spd                     = float.Parse(lines[3].Split(',')[1]);
            
            var data = AssetDatabase.LoadAssetAtPath<ConstantData>("Assets/_Data/ConstantData.asset");
            data.SetData(max_atk_spd, str_to_regeneration_rate,str_to_health,agi_to_atk_spd);
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

#endif
