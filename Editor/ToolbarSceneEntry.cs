using System;
using NaughtyAttributes;
using UnityEngine;

namespace EddyLib.ToolbarExtenderExtender.Editor
{

[Serializable]
internal class ToolbarSceneEntry
{
    [Scene, SerializeField] string scene;
    public string Scene => scene;

    [SerializeField] string buttonText;
    public string ButtonText => buttonText;

    [SerializeField] EToolbarSceneEntryPosition position;
    public EToolbarSceneEntryPosition Position => position;
}

}
