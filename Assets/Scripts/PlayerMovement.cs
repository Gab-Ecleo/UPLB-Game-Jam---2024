using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

//[RequireComponent(typeof(CapsuleCollider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private Transform playerTransform;

    [SerializeField] private float speed = 2.0f;

    private float horizontalInput;
    private bool canMove = true;
    private bool isFacingRight = true;

    //Audio
    private EventInstance playerFootsteps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        playerTransform = transform;
    }

    private void Start()
    {
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (canMove )
        {
            MovePlayer();
            //FlipSprite();
            UpdateSound();
        }
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private void FlipSprite()
    {
        if ((horizontalInput < 0 && isFacingRight) || (horizontalInput > 0 && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 rotate = new Vector3(transform.rotation.x, 180f, transform.rotation.y);
            transform.rotation = Quaternion.Euler(rotate);
            isFacingRight = !isFacingRight;
        }
    }

    private void UpdateSound()
    {
        //Start footsteps event if player has x velocity
        if (rb.velocity.x != 0)
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }

    }
}
