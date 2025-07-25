using Photon.Realtime;
using ScoreboardAttributes;

namespace MonkeView.RigHooks
{
    public static class RigHooks
    {
        public static void OnPlayerRigAssigned(Player player)
        {
            if (MonkeView.Instance != null)
            {
                MonkeView.Instance.UpdatePlayerModsAttribute(player);
            }
        }

        public static void OnPlayerRigDestroyed(Player player)
        {
            Registry.RemoveAttribute(player);
        }
    }
}
