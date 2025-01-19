#if UNITY_EDITOR
using System.Collections.Generic;
using SGGames.Scripts.Data;
using UnityEditor;
using UnityEngine;

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
            List<AttributeTierProgression> progressions = new List<AttributeTierProgression>();
            
            for (int i = 1; i < lines.Length; i++)
            {
                if(lines[i] == "") continue;
                progressions.Add(new AttributeTierProgression
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
            
            var data = AssetDatabase.LoadAssetAtPath<AttributeTierProgressionData>("Assets/_Data/Progression/AttributeTierProgression.asset");
            data.SetData(progressions);
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
        [MenuItem("SGGames/Import/Equipment Rarity Progression")]
        public static void ImportEquipmentRarityProgressionData()
        {
            var dataAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/_Data/CSV-Data/EquipmentRarityProgression.csv");
            var lines = dataAsset.text.Split("\n");
            List<EquipmentTierProgression> progressions = new List<EquipmentTierProgression>();
            
            for (int i = 1; i < lines.Length; i++)
            {
                if(lines[i] == "") continue;
                progressions.Add(new EquipmentTierProgression
                {
                    AreaIndex = i,
                    TierChance = new float[]
                    {
                        float.Parse(lines[i].Split(',')[1]),
                        float.Parse(lines[i].Split(',')[2]),
                        float.Parse(lines[i].Split(',')[3]),
                        float.Parse(lines[i].Split(',')[4]),
                        float.Parse(lines[i].Split(',')[5]),
                    }
                });
            }
            
            var data = AssetDatabase.LoadAssetAtPath<EquipmentTierProgressionData>("Assets/_Data/Progression/Equipment Tier Progression Data.asset");
            data.SetData(progressions);
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

#endif
