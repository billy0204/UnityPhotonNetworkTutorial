using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LevelTrigger : MonoBehaviour
{

    public int nextLevel;

    bool isStay = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = true;
            PunManager.i.infoText.text = $"{PunManager.i.SurfCount}/{PhotonNetwork.CurrentRoom.PlayerCount}, waiting for other player";

        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PunManager.i.SurfCount++;
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "SurfCount", PunManager.i.SurfCount } });

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PunManager.i.SurfCount--;
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "SurfCount", PunManager.i.SurfCount } });
            PunManager.i.infoText.text = "";
        }


    }
}
