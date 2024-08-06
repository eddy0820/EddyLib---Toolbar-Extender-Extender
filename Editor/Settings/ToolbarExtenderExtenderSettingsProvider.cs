using UnityEditor;
using UnityEngine.UIElements;

namespace EddyLib.ToolbarExtenderExtender.Editor
{
    
internal class ToolbarExtenderExtenderSettingsProvider : SettingsProvider
{
    SerializedObject toolbarExtenderExtenderSettings;

    public ToolbarExtenderExtenderSettingsProvider(string path, SettingsScope scope = SettingsScope.Project) : base(path, scope) {}

    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        toolbarExtenderExtenderSettings = ToolbarExtenderExtenderSettings.GetSerializedSettings();
    }

    public override void OnGUI(string searchContext)
    {
        EditorGUILayout.PropertyField(toolbarExtenderExtenderSettings.FindProperty("toolbarSceneEntries"));
        toolbarExtenderExtenderSettings.ApplyModifiedPropertiesWithoutUndo();
    }

    [SettingsProvider]
    public static SettingsProvider CreateGameSettingsSystemSettingsProvider()
    {
        var provider = new ToolbarExtenderExtenderSettingsProvider("Project/EddyLib/Toolbar Extender Extender", SettingsScope.Project);
        return provider;
    }
}

}
