using BepInEx;
using UnityEngine;

namespace MonkeView.Behaviours
{
    [BepInDependency("com.dev.gorillatag.scoreboardattributes")] 
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class DetectorObjectPlugin : BaseUnityPlugin
    {
        public void Start() => GorillaTagger.OnPlayerSpawned(Init);
        public void Init()
        {
            MonkeView detector = new GameObject("MonkeViewObject").AddComponent<MonkeView>();
            DontDestroyOnLoad(detector.gameObject);
            Logger.LogInfo("MonkeView loaded");
        }
    }
}
