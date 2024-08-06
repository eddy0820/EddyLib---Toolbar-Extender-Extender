using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityToolbarExtender;
using System.Linq;

namespace EddyLib.ToolbarExtenderExtender.Editor
{

static class ToolbarStyles
{
    public static readonly GUIStyle commandButtonStyle;

    static ToolbarStyles()
    {
        commandButtonStyle = new GUIStyle("Command")
        {
            fontSize = 16,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageAbove,
            fontStyle = FontStyle.Bold,
            stretchHeight = true,
            stretchWidth = true,
        };
    }
}

static class OnToolbarGUIHelper
{
    internal static void ShowButtons(List<ToolbarSceneEntry> entries)
    {
        entries.ForEach(entry =>
        {
            if(GUILayout.Button(new GUIContent(entry.ButtonText, entry.Scene), ToolbarStyles.commandButtonStyle))
            {
                SceneHelper.StartScene(entry.Scene);
            }
        });
    }
}

[InitializeOnLoad]
static class SceneSwitchLeftButton
{
    static SceneSwitchLeftButton()
    {
        ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
    }

    private static void OnToolbarGUI()
    {
        GUILayout.FlexibleSpace();

        ToolbarExtenderExtenderSettings settings = ToolbarExtenderExtenderSettings.GetOrCreateSettings();
        OnToolbarGUIHelper.ShowButtons(settings.ToolbarSceneEntries.Where(entry => entry.Position == EToolbarSceneEntryPosition.Left).ToList());  
    }
}

[InitializeOnLoad]
static class SceneSwitchRightButton
{
    static SceneSwitchRightButton()
    {
        ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
    }

    static void OnToolbarGUI()
    {
        GUILayout.FlexibleSpace();
        
        ToolbarExtenderExtenderSettings settings = ToolbarExtenderExtenderSettings.GetOrCreateSettings();
        OnToolbarGUIHelper.ShowButtons(settings.ToolbarSceneEntries.Where(entry => entry.Position == EToolbarSceneEntryPosition.Right).ToList());
    }
}

static class SceneHelper
{
    private static string sceneToOpen;

    public static void StartScene(string sceneName)
    {
        if(EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }

        sceneToOpen = sceneName;
        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        if (sceneToOpen == null ||
            EditorApplication.isPlaying || EditorApplication.isPaused ||
            EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }

        EditorApplication.update -= OnUpdate;

        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            // need to get scene via search because the path to the scene
            // file contains the package version so it'll change over time
            string[] guids = AssetDatabase.FindAssets("t:scene " + sceneToOpen, null);

            if(guids.Length == 0)
            {
                Debug.LogWarning("Couldn't find scene file");
            }
            else
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                EditorSceneManager.OpenScene(scenePath);
            }
        }

        sceneToOpen = null;
    }
}

}
