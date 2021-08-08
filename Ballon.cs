using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Ballon : MonoBehaviourPun
{
    private Animator animator;
    private Rigidbody2D rb;
    private int health;
    private float moveSpeed;
    private bool invincible = false;
    [SerializeField] SpriteRenderer render;
    [SerializeField] GameObject demon;
    [SerializeField] Sprite[] ballonSprite;

    public void InitBalloon(int type) 
    {
      photonView.RPC("SetBallon", RpcTarget.All, type);

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            rb.MovePosition(rb.position + Vector2.up * moveSpeed * Time.fixedDeltaTime);
            demon.transform.localPosition = new Vector3(0, -1, 0);

        }

    }

    public void GetHit() 
    {
        health--;
        if (health <= 0) 
        {
            photonView.RPC("DestroyBalloon", RpcTarget.All);
        }
        else 
        {
            photonView.RPC("SetBallon", RpcTarget.All,0);
        }
    }


    [PunRPC]
    private void SetBallon(int type)
    {
        render.sprite = ballonSprite[type];
        switch (type) 
        {
            case 0:
                health = 1;
                moveSpeed = 1;
                break;

            case 1:
                health = 2;
                moveSpeed = 1;
                break;

            case 2:
                health = 1;
                moveSpeed = 2;
                break;

            case 3:
                health = 1;
                moveSpeed = 1;
                break;
        }
    }

    [PunRPC]
    private void DestroyBalloon()
    {
        demon.transform.SetParent(null);
        demon.GetComponent<Animator>().SetTrigger("GetHit");
        demon.GetComponent<Rigidbody2D>().gravityScale = 1;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            GetHit();
            animator.SetTrigger("GetHit");
            int ownerId = collision.GetComponent<Arrow>().OwnerId;
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { $"Player{ownerId}", ScoreManager.i.playerScore[ownerId] + 1 } });
        }
    }


}
