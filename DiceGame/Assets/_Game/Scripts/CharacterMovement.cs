using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private DiceRoll diceRoll;
    [SerializeField] private AnimationsControl animationControl;
    [SerializeField] private LoadedDice loadedDice;

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
        diceRoll.RollRange();
        moveSpeed = diceRoll.diceNumber;
       //moveSpeed = 2f;
    }

    private void OnGUI()
    {
        GUILayout.Label("R to restart\n" +
            "WASD to move\n" +
            "Esc to quit\n" +
            "Space to jump\n" +
            "Shift to sprint");
        GUILayout.Label("Most actions roll a dice that effect speed and power");
    }

    void Update()
    {
        Debug.Log("move speed it " + moveSpeed);

        if(Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
  
        if (Input.GetKeyDown(KeyCode.A))
        {
            diceRoll.RollRange();
            moveSpeed = diceRoll.diceNumber;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            diceRoll.RollRange();
            moveSpeed = diceRoll.diceNumber;
        }

       

      

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
            animationControl.Run();
            characterArt.transform.localScale = new Vector3(move.x, 1f, 1f);
        }
        else
        {
            animationControl.RunOff();
        }

     // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            animationControl.Jump();
            diceRoll.RollRange();
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
            animationControl.JumpOff();
            OnLanded();
        }
        wasLanded = characterController.isGrounded;
    }
}
