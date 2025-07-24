using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using ScoreboardAttributes;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace MonkeView.Behaviours
{
    public class MonkeView : MonoBehaviourPunCallbacks
    {
        #region Constants
        static readonly Dictionary<string, string> ModKeyNames = ModKeys.ModNames;
        static readonly string NoModsText = "<color=red>No Detected Mods</color>";
        static readonly List<string> buffer = new List<string>(ModKeys.ModNames.Count);
        #endregion
        #region Callbacks
        public override void OnJoinedRoom() => PhotonNetwork.PlayerList.ForEach(p => StartCoroutine(WaitForProps(p)));
        public override void OnPlayerEnteredRoom(Player p) => StartCoroutine(WaitForProps(p));
        public override void OnPlayerPropertiesUpdate(Player p, Hashtable _) => StartCoroutine(WaitForProps(p));
        #endregion
        #region Actual Functions
        IEnumerator WaitForProps(Player p)
        {
            // Steve wrote this, not elliot.
            // Please yell at SteveTheAnimator on github if you have any issues with this code. (which, you will)
            yield return new WaitForSeconds(0.25f); // wait for the players to actually, exist
            for (float t = 0; t < 12f && (p.CustomProperties == null || !HasProps(p)); t += 0.1f) yield return new WaitForSeconds(0.1f); // wait for props to exist
            if (p.CustomProperties == null) yield break; // if the props are null, break.
            buffer.Clear(); // clear the buffer
            foreach (var kv in ModKeyNames) if (p.CustomProperties.TryGetValue(kv.Key, out var v) && v != null) buffer.Add(kv.Value); // if the player has a prop, add it to the buffer
            VRRig vrRig = null; // boom big the VRRig to null (idk what making a new object called lmao)
            for (float t = 0; t < 8f && (vrRig = GorillaGameManager.instance?.FindPlayerVRRig(p)) == null; t += 0.1f) yield return new WaitForSeconds(0.1f); // wait for the VRRig not to be null, yes you can scream at me for this.
            if (vrRig?.OwningNetPlayer != null) Registry.AddAttribute(vrRig.OwningNetPlayer, buffer.Count == 0 ? NoModsText : string.Join(", ", buffer)); // we add the attribute
        }

        static bool HasProps(Player p) => p.CustomProperties?.ContainsKey("didTutorial") == true;
        #endregion
    }
}
