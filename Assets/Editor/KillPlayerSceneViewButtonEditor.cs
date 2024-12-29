
#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

public class KillPlayerSceneViewButtonEditor : Editor
{
    private static GUIStyle m_style = new GUIStyle();
    
    //[MenuItem("SGGames/Show Scene Tools")]
    public static void Enable()
    {
        SceneView.duringSceneGui += OnSceneGUIDrawn;
        m_style = GUI.skin.label;
    }

    //[MenuItem("SGGames/Hide Scene Tools")]
    public static void Disable()
    {
        SceneView.duringSceneGui -= OnSceneGUIDrawn;
    }

    private void OnEnable()
    {
        m_style = GUI.skin.label;
    }


    private static void OnSceneGUIDrawn(SceneView sceneview)
    {
        Handles.BeginGUI();

        m_style.alignment = TextAnchor.MiddleCenter;
        if (GUI.Button(new Rect(0f, 0f, 200f, 50f), "Kill Player",m_style)) 
        {
            Debug.Log("Button Clicked");
        }
        Handles.EndGUI();
    }
}

#endif
