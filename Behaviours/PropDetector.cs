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
        public override void OnJoinedRoom() => UpdateAllPlayerAttributes();
        public override void OnPlayerEnteredRoom(Player newPlayer) => UpdatePlayerModsAttribute(newPlayer);
        public override void OnPlayerPropertiesUpdate(Player player, Hashtable changedProps) => UpdatePlayerModsAttribute(player);

        private void UpdateAllPlayerAttributes() => PhotonNetwork.PlayerList.ForEach(x => UpdatePlayerModsAttribute(x));

        private void UpdatePlayerModsAttribute(Player player)
        {
            var customProps = player.CustomProperties;
            var detectedMods = new List<string>();

            var modKeyNamesList = new List<KeyValuePair<string, string>>(ModKeys.ModNames);
            for (int i = 0; i < modKeyNamesList.Count; i++)
            {
                var pair = modKeyNamesList[i];
                if (customProps.TryGetValue(pair.Key, out var value) && value != null)
                    detectedMods.Add(pair.Value);
            }

            Registry.AddAttribute(
                player,
                detectedMods.Count > 0
                    ? string.Join(", ", detectedMods)
                    : "<color=red>No Detected Mods</color>"
            );
        }
    }
}

// if there is a way to improve the code, please do so or i will figure out how to improve it later