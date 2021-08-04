using System;
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class DataHolder : MonoBehaviourPun
{

    private string winnerName;
    public static DataHolder i;
    private const byte SHOW_WINNER_NAME_CODE = 1;


    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);

        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;

    }


 

    private void NetworkingClient_EventReceived(ExitGames.Client.Photon.EventData obj)
    {
        if(obj.Code == SHOW_WINNER_NAME_CODE) 
        {
            object[] datas = (object[])obj.CustomData;
            string name = (string)datas[0];
            winnerName = name;

            StartCoroutine(ShowWinner());
        }



    }

    public void setWinnerName(string name)
    {
        object[] datas = new object[] {name};

        PhotonNetwork.RaiseEvent(SHOW_WINNER_NAME_CODE, datas,RaiseEventOptions.Default , SendOptions.SendReliable);
        winnerName = name;
        StartCoroutine(ShowWinner());

    }


    IEnumerator ShowWinner()
    {
        
        yield return new WaitForSeconds(1f);

        PunManager.i.infoText.text = $"{winnerName} win the game";
        yield return new WaitForSeconds(3f);

        PunManager.i.infoText.text = "";
        winnerName = "";
    }
}
