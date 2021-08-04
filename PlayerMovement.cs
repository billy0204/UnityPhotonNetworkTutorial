using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Collider2D>());
            Destroy(this);
        }
        else
        {
            CinemachineVirtualCamera vcam = GameObject.Find("CMcam").GetComponent<CinemachineVirtualCamera>();
            if (vcam != null) vcam.Follow = transform;
        }

   
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
