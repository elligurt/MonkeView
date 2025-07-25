using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ScoreboardAttributes;

namespace MonkeView
{
    public class MonkeView : MonoBehaviourPunCallbacks
    {
        public static MonkeView Instance { get; private set; }

        private readonly Dictionary<string, string> modKeyNames = ModKeys.ModNames;

        private void Awake()
        {
            Instance = this;
        }

        public override void OnJoinedRoom()
        {
            UpdateAllPlayerAttributes();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdatePlayerModsAttribute(newPlayer);
        }

        public override void OnPlayerPropertiesUpdate(Player player, Hashtable changedProps)
        {
            UpdatePlayerModsAttribute(player);
        }

        private void UpdateAllPlayerAttributes()
        {
            Player[] players = PhotonNetwork.PlayerList;
            for (int i = 0; i < players.Length; i++)
            {
                UpdatePlayerModsAttribute(players[i]);
            }
        }

        public void UpdatePlayerModsAttribute(Player player)
        {
            string attributeText = BuildModsAttributeText(player);
            Registry.AddAttribute(player, attributeText);
        }

        private string BuildModsAttributeText(Player player)
        {
            List<string> detectedMods = new List<string>();

            foreach (KeyValuePair<string, string> pair in modKeyNames)
            {
                object value;
                if (player.CustomProperties.TryGetValue(pair.Key, out value) && value != null)
                {
                    detectedMods.Add(pair.Value);
                }
            }

            return detectedMods.Count > 0
                ? string.Join(", ", detectedMods)
                : "<color=red>No Detected Mods</color>";
        }
    }
}
