using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoseTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonNetwork.Destroy(collision.gameObject);
    }
}
