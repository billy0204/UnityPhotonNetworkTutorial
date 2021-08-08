using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    //private TMP_Text text;
    [SerializeField]
    private GameObject matchBtn;

    [SerializeField]
    private GameObject searchPanel;

    [SerializeField]
    private InputField nameField;

    private int localPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        searchPanel.SetActive(false);
        matchBtn.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
        nameField.enabled = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to " + PhotonNetwork.CloudRegion + "Server");
        PhotonNetwork.AutomaticallySyncScene = true;
        nameField.enabled = true;
        matchBtn.SetActive(true);
    }

    public void FindMatch()
    {
        searchPanel.SetActive(true);
        matchBtn.SetActive(false);
        PhotonNetwork.NickName = nameField.text;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("fail to join, make one");
        MakeRoom();
    }

    void MakeRoom()
    {
        int randomRoomName = UnityEngine.Random.Range(0, 5000);
        RoomOptions roomOptions =
            new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 2
            };

        // add properties
        Hashtable RoomCustomProps = new Hashtable();
        RoomCustomProps.Add("SurfCount", 0);
        RoomCustomProps.Add("Player1", 0);
        RoomCustomProps.Add("Player2", 0);
        RoomCustomProps.Add("Player3", 0);
        RoomCustomProps.Add("Player4", 0);


        roomOptions.CustomRoomProperties = RoomCustomProps;

        PhotonNetwork.CreateRoom("RoomName_" + randomRoomName, roomOptions);
        Debug.Log("room created");
    }

    public void StopSeaching()
    {
        searchPanel.SetActive(false);
        matchBtn.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("stopped srach");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 start game");
            PhotonNetwork.LoadLevel(1);
        }
    }


}