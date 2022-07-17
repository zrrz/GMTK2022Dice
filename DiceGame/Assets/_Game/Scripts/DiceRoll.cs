using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class DiceRoll : MonoBehaviour
{
    public GameObject dice;
    public int diceNumber;

    public float rolling;

    public MMFeedbacks rollCube1;
    public MMFeedbacks rollCube2;
    public MMFeedbacks rollCube3;
    public MMFeedbacks rollCube4;
    public MMFeedbacks rollCube5;
    public MMFeedbacks rollCube6;

    public MMFeedbacks pauseWorld;
    // Start is called before the first frame update
    void Start()
    {

    }
    IEnumerator RollDice()
    {
        PauseWorldRoll();
        yield return new WaitForSeconds(rolling);
        Roll();
    }

    public void RollRange()
    {
        diceNumber = Random.Range(1, 7);
        StartCoroutine(RollDice());

        Debug.Log(diceNumber);
    }

    void Roll()
    {
        switch (diceNumber)
        {
            case 1:
                RollDiceFeels1();
                print("1y");
                break;
            case 2:
                RollDiceFeels2();
                print("2y");
                break;
            case 3:
                RollDiceFeels3();
                print("3y");
                break;
            case 4:
                RollDiceFeels4();
                print("4y");
                break;
            case 5:
                print("5y");
                RollDiceFeels5();
                break;
            case 6:
                print("6y");
                RollDiceFeels6();
                break;
        }
    }


    public void PauseWorldRoll()
    {
        pauseWorld.PlayFeedbacks();
    }
    public void RollDiceFeels6()
    {      
        rollCube6?.PlayFeedbacks();
    }

    public void RollDiceFeels5()
    {
        rollCube5?.PlayFeedbacks();
    }

    public void RollDiceFeels4()
    {
        rollCube4?.PlayFeedbacks();
    }
    public void RollDiceFeels3()
    {
        rollCube3?.PlayFeedbacks();
    }

    public void RollDiceFeels2()
    {
        rollCube2?.PlayFeedbacks();
    }

    public void RollDiceFeels1()
    {
        rollCube1?.PlayFeedbacks();
    }




}
