
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using SGGames.Scripts.Data;
using SGGames.Scripts.Enemies;
using SGGames.Scripts.Rooms;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtension
{
    public class CreateRoomDesignEditorWindow : EditorWindow
    {
        private RoomDifficultType m_difficultType;
        private GameObject m_roomPrefab;
        private int m_areaIndex;
        private int m_postFixIndex;
        
        [MenuItem("SGGames/Create Room Tool")]
        public static void OpenWindow()
        {
            var window = GetWindow<CreateRoomDesignEditorWindow>("Create Room Design Editor");
            window.Show();
        }

        private void OnGUI()
        {
            m_difficultType = (RoomDifficultType)EditorGUILayout.EnumPopup("Difficulty",m_difficultType);
            m_roomPrefab = (GameObject)EditorGUILayout.ObjectField("Room Prefab", m_roomPrefab, typeof(GameObject), false);
            m_areaIndex = EditorGUILayout.IntField("Area Index", m_areaIndex);
            m_postFixIndex = EditorGUILayout.IntField("Post-Fix Index", m_postFixIndex);
            
            EditorGUILayout.Space(30);
            if (GUILayout.Button("Create Room Data"))
            {
                SaveRoomDesign();
                m_postFixIndex++;
            }
        }

        private void SaveRoomDesign()
        {
            var enemyPrefabs = FindObjectsOfType(typeof(EnemyController), true);
            var roomDataAsset = ScriptableObject.CreateInstance<RoomData>();
            roomDataAsset.RoomPrefab = m_roomPrefab;
            var enemyCount = enemyPrefabs.Length;
            roomDataAsset.EnemyList = new EnemySpawnInfo[enemyCount];
            for (int i = 0; i < enemyCount; i++)
            {
                var prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(enemyPrefabs[i]);
                
                var asset = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                var pos = ((EnemyController)enemyPrefabs[i]).gameObject.transform.position;
                roomDataAsset.EnemyList[i] = new EnemySpawnInfo
                {
                    EnemyPrefab = asset,
                    SpawnPosition = pos
                };
            }
            AssetDatabase.CreateAsset(roomDataAsset, $"Assets/_Data/RoomData/Area-{m_areaIndex}/{m_roomPrefab.name}-{m_difficultType}-{m_postFixIndex}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}

#endif
