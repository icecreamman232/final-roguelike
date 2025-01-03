#if UNITY_EDITOR
using System.Collections.Generic;
using SGGames.Scripts.Data;
using UnityEditor;
using UnityEngine;
using AttributeRarityProgressionData = SGGames.Scripts.Data.AttributeRarityProgressionData;

namespace SGGames.Scripts.Tools
{
    public class CSVReader 
    {
        [MenuItem("SGGames/Import/Constant")]
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

        [MenuItem("SGGames/Import/Attribute Tier Progression")]
        public static void ImportAttributeTierProgressionData()
        {
            var dataAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/_Data/CSV-Data/AttributeTierProgression.csv");
            var lines = dataAsset.text.Split("\n");
            List<AttributeRarityProgression> progressions = new List<AttributeRarityProgression>();
            
            for (int i = 1; i < lines.Length; i++)
            {
                if(lines[i] == "") continue;
                progressions.Add(new AttributeRarityProgression
                {
                    Level = int.Parse(lines[i].Split(',')[0]),
                    TierChance = new float[]
                    {
                        float.Parse(lines[i].Split(',')[1]),
                        float.Parse(lines[i].Split(',')[2]),
                        float.Parse(lines[i].Split(',')[3]),
                        float.Parse(lines[i].Split(',')[4]),
                    }
                });
            }
            
            var data = AssetDatabase.LoadAssetAtPath<AttributeRarityProgressionData>("Assets/_Data/Progression/AttributeRarityProgression.asset");
            data.SetData(progressions);
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

#endif
