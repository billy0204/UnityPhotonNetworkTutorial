using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviourPun
{
    [SerializeField] private float speed = 20f;
    private Rigidbody2D rb;
    public int OwnerId = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Collider2D>());
            Destroy(this);
        }
        OwnerId = PhotonNetwork.LocalPlayer.ActorNumber;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    
}
