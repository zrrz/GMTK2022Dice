using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private DiceRoll diceRoll;

    public float moveSpeed = 2f;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private bool wasLanded = false;

    [SerializeField] private Transform characterArt;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void OnLanded()
    {
        moveSpeed = 2f;
    }
   
    void Update()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(x, 0f, 0f);
        characterController.Move(move * Time.deltaTime * moveSpeed);

        if (move != Vector3.zero)
        {
            characterArt.transform.localScale = new Vector3(move.x, 1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            diceRoll.rollRange();
            moveSpeed = diceRoll.diceNumber;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 2f;
        }


        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
           
            diceRoll.rollRange();
            jumpHeight = diceRoll.diceNumber;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);



        //if(x < 0f)
        //{
        //    transform.localScale = new Vector3(-1f, 1f, 1f);
        //}
        //else if(x > 0f)
        //{
        //    transform.localScale = new Vector3(1f, 1f, 1f);
        //}
        if(characterController.isGrounded && !wasLanded)
        {
            OnLanded();
        }
        wasLanded = characterController.isGrounded;
    }
}
