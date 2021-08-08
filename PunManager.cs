using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class PunManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject[] SpawnPoints;
    [SerializeField]
    Text pingtext;
    public Text infoText;
    public static PunManager i;
    private bool startMiniGame = false;
    public int SurfCount = 0;
    public int PooyanCount = 0;
    void Awake()
    {
        if (i == null)
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
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }


    void SpawnPlayer()
    {
        int player = 0;
        if (!PhotonNetwork.IsMasterClient)
        {
            player = 1;
        }

        GameObject Player = PhotonNetwork.Instantiate("Player", SpawnPoints[player].transform.position, Quaternion.identity);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        SetChange();
    }


    void SetChange()
    {
        SurfCount = (int)PhotonNetwork.CurrentRoom.CustomProperties["SurfCount"];
        PooyanCount = (int)PhotonNetwork.CurrentRoom.CustomProperties["PooyanCount"];

    }


    private void Update()
    {
        if (SurfCount == PhotonNetwork.CurrentRoom.PlayerCount && PhotonNetwork.IsMasterClient && !startMiniGame)
        {
            startMiniGame = true;
            PhotonNetwork.LoadLevel(2);
        }
        else if (PooyanCount == PhotonNetwork.CurrentRoom.PlayerCount && PhotonNetwork.IsMasterClient && !startMiniGame)
        {
            startMiniGame = true;
            PhotonNetwork.LoadLevel(3);
        }

            pingtext.text = PhotonNetwork.GetPing().ToString();


    }
}
