using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

//[RequireComponent(typeof(CapsuleCollider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;
    public static PlayerMovement Instance => instance;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float speed = 2.0f;

    private float horizontalInput;
    private bool isFacingRight = true;
    public bool canMove = true;

    //Audio
    private EventInstance playerFootsteps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
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
        if (canMove)
        {
            MovePlayer();
            FlipSprite();
            UpdateSound();
        }

        // animation
        if(horizontalInput > 0|| horizontalInput < 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private void FlipSprite()
    {
        if (horizontalInput < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 rotate = new Vector3(transform.rotation.x, 180f, transform.rotation.y);
            transform.rotation = Quaternion.Euler(rotate);
        }
        else if(horizontalInput > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 rotate = new Vector3(transform.rotation.x, 0f, transform.rotation.y);
            transform.rotation = Quaternion.Euler(rotate);
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
