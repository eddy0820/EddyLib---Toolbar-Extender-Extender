using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEditor;

namespace EddyLib.ToolbarExtenderExtender.Editor
{

internal class ToolbarExtenderExtenderSettings : ScriptableObject
{
    const string SETTINGS_ASSETS_PATH = "Assets/Resources/EddyLib/EddyLib.ToolbarExtenderExtender.asset";

    [ReadOnly, SerializeField] List<ToolbarSceneEntry> toolbarSceneEntries = new();
    public List<ToolbarSceneEntry> ToolbarSceneEntries => toolbarSceneEntries;

    internal static ToolbarExtenderExtenderSettings GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<ToolbarExtenderExtenderSettings>(SETTINGS_ASSETS_PATH);
        
        if(settings == null)
        {
            if(!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            if(!AssetDatabase.IsValidFolder("Assets/Resources/EddyLib"))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "EddyLib");
            }

            settings = CreateInstance<ToolbarExtenderExtenderSettings>();
            AssetDatabase.CreateAsset(settings, SETTINGS_ASSETS_PATH);

            AssetDatabase.SaveAssets();
        }


        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }
}

}
