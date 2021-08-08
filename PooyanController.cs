using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PooyanController : MonoBehaviourPun
{
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float maxattackSpeed;
    [SerializeField] private TMP_Text nameText;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Collider2D>());
            nameText.text = photonView.Owner.NickName;
        }
        else
        {
            nameText.text = "YOU";
            nameText.color = Color.red;
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return; 
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Vertical", Mathf.Abs(movement.y));

        if (Input.GetKeyDown("x") && attackSpeed<=0)
        {
            attackSpeed = maxattackSpeed;
            photonView.RPC("shootAnimation", RpcTarget.All);
            Shoot();
        }
        else
        {
            attackSpeed -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);


    }


    public void Shoot() 
    {
        PhotonNetwork.Instantiate("Arrow", firepoint.position, Quaternion.Euler(0, 0, -180f));
    }

    [PunRPC]
    private void shootAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
