using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SurfManager : MonoBehaviourPun
{
    public GameObject[] SpawnPoints;
    private Rocks[] rocks;
    [SerializeField] Text pingtext;
    public static SurfManager i;


    private bool gameStart = false;
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

        GameObject Player = PhotonNetwork.Instantiate("SURFBORRD", SpawnPoints[player].transform.position, Quaternion.Euler(0, 0, 25));
    }


    void FixedUpdate()
    {
        foreach(Rocks rock in rocks)
        {
            rock.speed += Time.fixedDeltaTime/5;
        }
        pingtext.text = PhotonNetwork.GetPing().ToString();
    }

    private void Update()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (!gameStart && playerCount == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            gameStart = true;
        }


        if (gameStart)
        {
            if(playerCount <= 1)
            {
                gameStart = false;
                //show text in main world


                string winnerName = GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>().Owner.NickName;
                if (PhotonNetwork.IsMasterClient)
                {
                    DataHolder.i.setWinnerName(winnerName);
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "SurfCount", 0 } });
                    PhotonNetwork.LoadLevel(1);
                }
            }
        }
    }
}
