using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LevelTrigger : MonoBehaviour
{

    [SerializeField] string levelText;
    private bool isActive = false;


    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }



    private void Update()
    {
        switch (levelText)
        {
            case "Surf":
                if(PunManager.i.SurfCount > 0)
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
                }
                break;

            case "Pooyan":
                if (PunManager.i.PooyanCount > 0)
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
                }
                break;
        }


        if (isActive) 
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (levelText)
            {
                case "Surf":
                    PunManager.i.infoText.text = $"{PunManager.i.SurfCount}/{PhotonNetwork.CurrentRoom.PlayerCount}, waiting for other player";
                    break;

                case "Pooyan":
                    PunManager.i.infoText.text = $"{PunManager.i.PooyanCount}/{PhotonNetwork.CurrentRoom.PlayerCount}, waiting for other player";
                    break;
            }

        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            SetProperties(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetProperties(-1);
            PunManager.i.infoText.text = "";
        }


    }


    private void SetProperties(int n)
    {
        switch (levelText)
        {
            case "Surf":
                PunManager.i.SurfCount += n;
                PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { $"{levelText}Count", PunManager.i.SurfCount } });
                break;

            case "Pooyan":
                PunManager.i.PooyanCount += n;
                PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { $"{levelText}Count", PunManager.i.PooyanCount } });
                break;
        }

    }
}
