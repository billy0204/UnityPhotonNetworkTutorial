using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text[] scoreTexts;
    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    public string[] playerNames;
    public int[] playerScore;
    public string Winnername;

    public static ScoreManager i;

    private void Awake()
    {
        if(i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        players = PhotonNetwork.CurrentRoom.Players;
        playerNames = new string[players.Count + 1];
        playerScore = new int[PhotonNetwork.CurrentRoom.PlayerCount + 1];
        foreach (KeyValuePair<int,Player> player in players)
        {
            
            playerNames[player.Key] = player.Value.NickName;
        }
        SetScoretext();
    }

    public void SetScoretext()
    {
        int maxscore = 0;
        for (int i = 1; i < playerNames.Length; i++)
        {
            if (maxscore <= playerScore[i])
            {
                maxscore = playerScore[i];
                Winnername = playerNames[i];
            }
            scoreTexts[i].text = playerNames[i] + ": " + playerScore[i];
        }
    }


    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        SetChange();
    }

    void SetChange()
    {

        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            playerScore[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[$"Player{i}"];
        }

        ScoreManager.i.SetScoretext();
    }

    public void ResetScoreManager()
    {
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { $"Player{i}", 0 } });
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "PooyanCount", 0 } });

    }
}
