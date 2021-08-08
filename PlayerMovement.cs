using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using TMPro;
public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] TMP_Text nameText;
    private Rigidbody2D rb;
    private Animator animator;
    Vector2 movement;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Collider2D>());
            nameText.text = photonView.Owner.NickName;
        }
        else
        {
            CinemachineVirtualCamera vcam = GameObject.Find("CMcam").GetComponent<CinemachineVirtualCamera>();
            if (vcam != null) vcam.Follow = transform;
            nameText.text = "YOU";
            nameText.color = Color.red;

        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (animator != null)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
        }


    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }


    }


}
