using BepInEx;
using UnityEngine;

namespace MonkeView.Behaviours
{
    [BepInDependency("com.dev.gorillatag.scoreboardattributes")]
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class DetectorObjectPlugin : BaseUnityPlugin
    {
        void Start()
        {
            GameObject detector = new GameObject("MonkeViewObject");
            detector.AddComponent<MonkeView>();
            DontDestroyOnLoad(detector);
            Logger.LogInfo("MonkeView loaded");
        }
    }
}
