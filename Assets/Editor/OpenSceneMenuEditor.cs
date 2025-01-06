#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;


namespace SGGames.Scripts.EditorExtensions
{
    public static class OpenSceneMenuEditor
    {
        [MenuItem("SGGames/Scene/Room Editor")]
        public static void OpenRoomEditorScene()
        {
            EditorSceneManager.OpenScene("Assets/_Scenes/RoomDesignScene.unity", OpenSceneMode.Single);
        }
        
        [MenuItem("SGGames/Scene/Gameplay")]
        public static void OpenGameplayScene()
        {
            EditorSceneManager.OpenScene("Assets/_Scenes/GameplayScene.unity", OpenSceneMode.Single);
        }
    }
}

#endif
