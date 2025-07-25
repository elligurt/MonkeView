using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using MonkeView.RigHooks;

namespace GorillaInfoWatch.Patches
{
    [HarmonyPatch(typeof(RigContainer)), HarmonyWrapSafe]
    internal class RigContainerPatches
    {
        [HarmonyPatch(typeof(RigContainer), "OnRigDisable")]
        internal class RigContainerPatch
        {
            [HarmonyPostfix]
            public static void DisablePatch(RigContainer __instance)
            {
                PhotonView view = __instance.GetComponent<PhotonView>();
                if (view == null) return;

                Player player = view.Owner;
                if (player != null)
                {
                    RigHooks.OnPlayerRigDestroyed(player);
                }
            }
        }

        [HarmonyPatch(typeof(RigContainer), "OnRigDisable")]
        public static void DisablePatch(RigContainer __instance)
        {
            PhotonView view = __instance.GetComponent<PhotonView>();
            if (view == null) return;

            Player player = view.Owner;
            if (player != null)
            {
                RigHooks.OnPlayerRigDestroyed(player);
            }
        }
    }
}
