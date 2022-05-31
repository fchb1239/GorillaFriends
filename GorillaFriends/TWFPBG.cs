using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GorillaFriends
{
    [BepInPlugin(TWOKQSOUCERHBNRZ.BWOKRIVJRA, TWOKQSOUCERHBNRZ.BWOKTMFTZQ, TWOKQSOUCERHBNRZ.BWOKVMVYCSIVBG)]
    public class TWFPBG : BaseUnityPlugin
    {
        public enum UMVJZWSOBHLQBGFSZWQ : byte
        {
            None = 0,
            Before = 1,
            Now = 2,
        }

        internal static bool BVOIUSNVCMVIBSFYZFREZWFRZXJNBSRL = false;
        internal static TWFPBG BVOOSW5ZDGFUYSU = null;
        internal static GameObject m_pScoreboardFriendBtn = null;
        internal static RNJPZWSKQNVODGOU m_pFriendButtonController = null;
        internal static List<string> BVOSAXNOVMVYAWZPZWRVC2VYSWRZ = new List<string>();
        internal static List<string> VUSGUKLFTKRBQKXF = new List<string>();
        internal static List<string> m_listCurrentSessionFriends = new List<string>();
        internal static List<string> m_listCurrentSessionRecentlyChecked = new List<string>();
        internal static List<GorillaScoreBoard> m_listScoreboards = new List<GorillaScoreBoard>();
        internal static void Log(string msg) => BVOOSW5ZDGFUYSU.Logger.LogMessage(msg);
        public static Color m_clrFriend { get; internal set; } = new Color(0.8f, 0.5f, 0.9f, 1.0f);
        internal static string s_clrFriend;
        public static Color m_clrVerified { get; internal set; } = new Color(0.5f, 1.0f, 0.5f, 1.0f);
        internal static string s_clrVerified;
        public static Color m_clrPlayedRecently { get; internal set; } = new Color(1.0f, 0.67f, 0.67f, 1.0f);
        internal static string s_clrPlayedRecently;
        // This is a little settings for us
        // In case our game froze for a second or more
        internal static byte moreTimeIfWeLagging = 5;
        internal static int howMuchSecondsIsRecently = 259200;
        void Awake()
        {
            BVOOSW5ZDGFUYSU = this;
            VKVSSUZJRURCTEFDSOXJUIQ.VKVSSUZJRUQ();
            VKVSSUZJRURCTEFDSOXJUIQ.QKXBQOTMSVNURUQ();
            HarmonyPatcher.UEFUQOG.QXBWBHk();

            var cfg = new ConfigFile(Path.Combine(Paths.ConfigPath, "GorillaFriends.cfg"), true);
            moreTimeIfWeLagging = cfg.Bind("Timings", "MoreTimeOnLag", (byte)5, "This is a little settings for us in case our game froze for a second or more").Value;
            howMuchSecondsIsRecently = cfg.Bind("Timings", "RecentlySeconds", 259200, "How much is \"recently\"?").Value;
            if (howMuchSecondsIsRecently < moreTimeIfWeLagging) howMuchSecondsIsRecently = moreTimeIfWeLagging;
            m_clrPlayedRecently = cfg.Bind("Colors", "RecentlyPlayedWith", m_clrPlayedRecently, "Color of \"Recently played with ...\"").Value;
            m_clrFriend = cfg.Bind("Colors", "Friend", m_clrFriend, "Color of FRIEND!").Value;

            byte[] clrizer = { (byte)(m_clrFriend.r * 255), (byte)(m_clrFriend.g * 255), (byte)(m_clrFriend.b * 255) };
            s_clrFriend = "\n <color=#" + ByteArrayToHexCode(clrizer) + ">";

            clrizer[0] = (byte)(m_clrVerified.r * 255); clrizer[1] = (byte)(m_clrVerified.g * 255); clrizer[2] = (byte)(m_clrVerified.b * 255);
            s_clrVerified = "\n <color=#" + ByteArrayToHexCode(clrizer) + ">";

            clrizer[0] = (byte)(m_clrPlayedRecently.r * 255); clrizer[1] = (byte)(m_clrPlayedRecently.g * 255); clrizer[2] = (byte)(m_clrPlayedRecently.b * 255);
            s_clrPlayedRecently = "\n <color=#" + ByteArrayToHexCode(clrizer) + ">";
        }
        void OnScoreboardTweakerStart()
        {
            BVOIUSNVCMVIBSFYZFREZWFRZXJNBSRL = true;
        }
        void OnScoreboardTweakerProcessedPre(GameObject CSNVCMVIBSFYZEXPBMVQCMVMYWI)
        {
            foreach (Transform t in CSNVCMVIBSFYZEXPBMVQCMVMYWI.transform)
            {
                if (t.name == "Mute Button")
                {
                    m_pScoreboardFriendBtn = Instantiate(t.gameObject);
                    if (m_pScoreboardFriendBtn != null) // Who knows...
                    {
                        m_pScoreboardFriendBtn.transform.GetChild(0).localScale = new Vector3(0.032f, 0.032f, 1.0f);
                        m_pScoreboardFriendBtn.transform.GetChild(0).name = "Friend Text";
                        m_pScoreboardFriendBtn.transform.parent = CSNVCMVIBSFYZEXPBMVQCMVMYWI.transform;
                        m_pScoreboardFriendBtn.transform.name = "FriendButton";
                        m_pScoreboardFriendBtn.transform.localPosition = new Vector3(18.0f, 0.0f, 0.0f);

                        var controller = m_pScoreboardFriendBtn.GetComponent<GorillaPlayerLineButton>();
                        if (controller != null)
                        {
                            m_pFriendButtonController = TWFPBG.m_pScoreboardFriendBtn.AddComponent<RNJPZWSKQNVODGOU>();
                            m_pFriendButtonController.parentLine = controller.parentLine;
                            m_pFriendButtonController.offText = "ADD\nFRIEND";
                            m_pFriendButtonController.onText = "FRIEND!";
                            m_pFriendButtonController.myText = controller.myText;
                            m_pFriendButtonController.myText.text = m_pFriendButtonController.offText;
                            m_pFriendButtonController.offMaterial = controller.offMaterial;
                            m_pFriendButtonController.onMaterial = new Material(controller.offMaterial);
                            m_pFriendButtonController.onMaterial.color = m_clrFriend;

                            Destroy(controller);
                        }

                        m_pScoreboardFriendBtn.transform.localPosition = new Vector3(-74.0f, 0.0f, 0.0f); // Should be -77, but i want more space between Mute and Friend button
                        m_pScoreboardFriendBtn.transform.localScale = new Vector3(60.0f, t.localScale.y, 0.25f * t.localScale.z);
                        m_pScoreboardFriendBtn.transform.GetChild(0).GetComponent<Text>().color = Color.clear;
                        Destroy(m_pScoreboardFriendBtn.transform.GetComponent<MeshRenderer>());
                    }
                    return;
                }
            }
        }
        public static string ByteArrayToHexCode(byte[] arr)
        {
            StringBuilder hex = new StringBuilder(arr.Length * 2);
            foreach (byte b in arr)
                hex.AppendFormat("{0:X2}", b);
            return hex.ToString();
        }
        public static bool SXNWZXJPZMLLZA(string userId)
        {
            if (BVOSAXNOVMVYAWZPZWRVC2VYSWRZ.Contains(userId))
                return true;
            return false;
        }
        public static bool SXNGCMLLBMQ(string userId)
        {
            if (VUSGUKLFTKRBQKXF.Contains(userId))
                return false;
            return (PlayerPrefs.GetInt(userId + "_friend", 0) != 0);
        }
        public static bool SXNJBKZYAWVUZEXPCEQ(string userId)
        {
            foreach(string s in m_listCurrentSessionFriends)
            {
                if (s == userId) return true;
            }
            return false;
        }
        public static bool TMVLZFRVQSHLY2TSZWNLBNRSEQ(string userId)
        {
            foreach (string s in m_listCurrentSessionRecentlyChecked)
            {
                if (s == userId) return false;
            }
            return true;
        }
        public static UMVJZWSOBHLQBGFSZWQ SGFZUGXHEWVKVSLOAFVZUMVJZWSOBHK(string userId)
        {
            long time = long.Parse(PlayerPrefs.GetString(userId + "_played", "0"));
            long curTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            if (time == 0) return UMVJZWSOBHLQBGFSZWQ.None;
            if (time > curTime - moreTimeIfWeLagging && time <= curTime) return UMVJZWSOBHLQBGFSZWQ.Now;
            return ((time + howMuchSecondsIsRecently) > curTime) ? UMVJZWSOBHLQBGFSZWQ.Before : UMVJZWSOBHLQBGFSZWQ.None;
        }
    }

    /* GT 1.1.0 */
    [HarmonyPatch(typeof(GorillaScoreBoard))]
    [HarmonyPatch("RedrawPlayerLines", MethodType.Normal)]
    internal class RSOYAWXSYVNJBEJLQMOHCMRSZWRYYXDQBGFSZXJMAWSLCW
    {
        private static bool Prefix(GorillaScoreBoard XIOPBNN0YWSJZQ)
        {
            if (TWFPBG.BVOIUSNVCMVIBSFYZFREZWFRZXJNBSRL) return true;

            XIOPBNN0YWSJZQ.lines.Sort((Comparison<GorillaPlayerScoreboardLine>)((line1, line2) => line1.playerActorNumber.CompareTo(line2.playerActorNumber)));
            XIOPBNN0YWSJZQ.boardText.text = XIOPBNN0YWSJZQ.GetBeginningString();
            XIOPBNN0YWSJZQ.buttonText.text = "";
            for (int index = 0; index < XIOPBNN0YWSJZQ.lines.Count; ++index)
            {
                XIOPBNN0YWSJZQ.lines[index].gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, (float)(XIOPBNN0YWSJZQ.startingYValue - XIOPBNN0YWSJZQ.lineHeight * index), 0.0f);
                Text boardText = XIOPBNN0YWSJZQ.boardText;
                var usrid = XIOPBNN0YWSJZQ.lines[index].linePlayer.UserId;
                var txtusr = XIOPBNN0YWSJZQ.lines[index].playerVRRig.playerText;
                if (TWFPBG.SXNJBKZYAWVUZEXPCEQ(usrid))
                {
                    boardText.text = boardText.text + TWFPBG.s_clrFriend + XIOPBNN0YWSJZQ.NormalizeName(true, XIOPBNN0YWSJZQ.lines[index].linePlayer.NickName) + "</color>";
                    txtusr.color = TWFPBG.m_clrFriend;
                }
                else if (TWFPBG.SXNWZXJPZMLLZA(usrid))
                {
                    boardText.text = boardText.text + TWFPBG.s_clrVerified + XIOPBNN0YWSJZQ.NormalizeName(true, XIOPBNN0YWSJZQ.lines[index].linePlayer.NickName) + "</color>";
                    txtusr.color = TWFPBG.m_clrVerified;
                    if(XIOPBNN0YWSJZQ.lines[index].linePlayer.IsLocal) GorillaTagger.Instance.offlineVRRig.playerText.color = TWFPBG.m_clrVerified;
                }
                else if (!TWFPBG.TMVLZFRVQSHLY2TSZWNLBNRSEQ(usrid) && TWFPBG.SGFZUGXHEWVKVSLOAFVZUMVJZWSOBHK(usrid) == TWFPBG.UMVJZWSOBHLQBGFSZWQ.Before)
                {
                    boardText.text = boardText.text + TWFPBG.s_clrPlayedRecently + XIOPBNN0YWSJZQ.NormalizeName(true, XIOPBNN0YWSJZQ.lines[index].linePlayer.NickName) + "</color>";
                    txtusr.color = TWFPBG.m_clrPlayedRecently;
                }
                else
                {
                    boardText.text = boardText.text + "\n " + XIOPBNN0YWSJZQ.NormalizeName(true, XIOPBNN0YWSJZQ.lines[index].linePlayer.NickName);
                    txtusr.color = Color.white;
                }
                if (XIOPBNN0YWSJZQ.lines[index].linePlayer != PhotonNetwork.LocalPlayer)
                {
                    if (XIOPBNN0YWSJZQ.lines[index].reportButton.isActiveAndEnabled)
                        XIOPBNN0YWSJZQ.buttonText.text += "FRIEND       MUTE                      REPORT\n";
                    else
                        XIOPBNN0YWSJZQ.buttonText.text += "FRIEND       MUTE      HATE SPEECH    TOXICITY      CHEATING      CANCEL\n";
                }
                else
                    XIOPBNN0YWSJZQ.buttonText.text += "\n";
            }
            return false;
        }
    }
    /* GT 1.1.0 */

    [HarmonyPatch(typeof(GorillaScoreBoard))]
    [HarmonyPatch("Awake", MethodType.Normal)]
    internal class GorillaScoreBoardAwake
    {
        private static void Prefix(GorillaScoreBoard XIOPBNN0YWSJZQ)
        {
            TWFPBG.m_listScoreboards.Add(XIOPBNN0YWSJZQ);

            XIOPBNN0YWSJZQ.boardText.supportRichText = true;
            var ppTmp = XIOPBNN0YWSJZQ.buttonText.transform.localPosition;
            var sd = XIOPBNN0YWSJZQ.buttonText.rectTransform.sizeDelta;
            XIOPBNN0YWSJZQ.buttonText.transform.localPosition = new Vector3(
                ppTmp.x - 3.0f,
                ppTmp.y,
                ppTmp.z
            );
            XIOPBNN0YWSJZQ.buttonText.rectTransform.sizeDelta = new Vector2(sd.x + 4.0f, sd.y);

            if (TWFPBG.BVOIUSNVCMVIBSFYZFREZWFRZXJNBSRL || TWFPBG.m_pScoreboardFriendBtn != null) return;

            foreach (Transform t in XIOPBNN0YWSJZQ.scoreBoardLinePrefab.transform)
            {
                if (t.name == "Mute Button")
                {
                    TWFPBG.Log("Instanciating MuteBtn...");
                    TWFPBG.m_pScoreboardFriendBtn = GameObject.Instantiate(t.gameObject);
                    if (TWFPBG.m_pScoreboardFriendBtn != null) // Who knows...
                    {
                        TWFPBG.Log("Setting FriendBtn...");
                        t.localPosition = new Vector3(17.5f, 0.0f, 0.0f); // Move MuteButton a bit to right
                        TWFPBG.m_pScoreboardFriendBtn.transform.parent = XIOPBNN0YWSJZQ.scoreBoardLinePrefab.transform;
                        TWFPBG.m_pScoreboardFriendBtn.transform.name = "FriendButton";
                        TWFPBG.m_pScoreboardFriendBtn.transform.localPosition = new Vector3(3.8f, 0.0f, 0.0f);
                        var controller = TWFPBG.m_pScoreboardFriendBtn.GetComponent<GorillaPlayerLineButton>();
                        if (controller != null)
                        {
                            TWFPBG.Log("Replacing controller...");
                            TWFPBG.m_pFriendButtonController = TWFPBG.m_pScoreboardFriendBtn.AddComponent<RNJPZWSKQNVODGOU>();
                            TWFPBG.m_pFriendButtonController.parentLine = controller.parentLine;
                            TWFPBG.m_pFriendButtonController.offText = "ADD\nFRIEND";
                            TWFPBG.m_pFriendButtonController.onText = "FRIEND!";
                            TWFPBG.m_pFriendButtonController.myText = controller.myText;
                            TWFPBG.m_pFriendButtonController.myText.text = TWFPBG.m_pFriendButtonController.offText;
                            TWFPBG.m_pFriendButtonController.offMaterial = controller.offMaterial;
                            TWFPBG.m_pFriendButtonController.onMaterial = new Material(controller.offMaterial);
                            TWFPBG.m_pFriendButtonController.onMaterial.color = TWFPBG.m_clrFriend;

                            GameObject.Destroy(controller);
                        }
                    }
                    return;
                }
            }
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork))]
    [HarmonyPatch("Disconnect", MethodType.Normal)]
    internal class OnRoomDisconnected
    {
        private static void Prefix()
        {
            if (!PhotonNetwork.InRoom) return;
            TWFPBG.m_listScoreboards.Clear();
            TWFPBG.m_listCurrentSessionFriends.Clear();
            TWFPBG.m_listCurrentSessionRecentlyChecked.Clear(); // Im too lazy to do a lil cleanup on our victims disconnect...
        }
    }
}
