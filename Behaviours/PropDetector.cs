using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ScoreboardAttributes;

namespace MonkeView.Behaviours
{
    public class MonkeView : MonoBehaviourPunCallbacks
    {
        private readonly Dictionary<string, string> ModKeyNames = ModKeys.ModNames;

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
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                UpdatePlayerModsAttribute(player);
            }
        }

        private void UpdatePlayerModsAttribute(Player player)
        {
            List<string> detectedMods = new List<string>();

            foreach (var pair in ModKeyNames)
            {
                if (player.CustomProperties.TryGetValue(pair.Key, out object value) && value != null)
                {
                    detectedMods.Add(pair.Value);
                }
            }

            string attributeText = detectedMods.Count > 0
                ? string.Join(", ", detectedMods)
                : "<color=red>No Detected Mods</color>";

            Registry.AddAttribute(player, attributeText);
        }
    }
}

// if there is a way to improve the code, please do so or i will figure out how to improve it later