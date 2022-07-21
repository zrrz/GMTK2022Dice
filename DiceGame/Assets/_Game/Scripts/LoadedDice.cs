using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class LoadedDice : MonoBehaviour
{
    [SerializeField] private DiceRoll diceRoll;

    [SerializeField] private GameObject visuals;

    public GameObject extraDice;
    public GameObject diceHolder;
    public float rolling;
    public int diceNumberExtra;

    public bool diceActive;

    public MMFeedbacks rollExtraCube1;
    public MMFeedbacks rollExtraCube2;
    public MMFeedbacks rollExtraCube3;
    public MMFeedbacks rollExtraCube4;
    public MMFeedbacks rollExtraCube5;
    public MMFeedbacks rollExtraCube6;
    // Start is called before the first frame update
    void Start()
    {
        SetDiceActive(false);
    }

    public void SetDiceActive(bool shouldBeActive)//tells it
    {
        diceActive = shouldBeActive; //setting the value to the value
        //listen

        visuals.SetActive(shouldBeActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {           
            RollRangeExtra();
            SetDiceActive(true);
           // diceRoll.RollRange();
           // Combine();
        }
    }
    IEnumerator RollDice()
    {
      
        yield return new WaitForSeconds(rolling);
        Roll();
    }
    public void RollRangeExtra()
    {
        diceNumberExtra = Random.Range(1, 7);
        StartCoroutine(RollDice());

        //Debug.Log(diceNumberExtra);
    }

    public void Combine()
    {
        diceNumberExtra += diceRoll.diceNumber;
        //Debug.Log(diceNumberExtra);
    }
    void Roll()
    {
        switch (diceNumberExtra)
        {
            case 1:
                RollDiceFeels1();
                print(diceNumberExtra);
                break;
            case 2:
                RollDiceFeels2();
                break;
            case 3:
                RollDiceFeels3();               
                break;
            case 4:
                RollDiceFeels4();
                break;
            case 5:
                RollDiceFeels5();
                break;
            case 6:
                RollDiceFeels6();
                print(diceNumberExtra);
                break;
        }
    }

    public void RollDiceFeels6()
    {
        rollExtraCube6?.PlayFeedbacks();
    }

    public void RollDiceFeels5()
    {
        rollExtraCube5?.PlayFeedbacks();
    }

    public void RollDiceFeels4()
    {
        rollExtraCube4?.PlayFeedbacks();
    }
    public void RollDiceFeels3()
    {
        rollExtraCube3?.PlayFeedbacks();
    }

    public void RollDiceFeels2()
    {
        rollExtraCube2?.PlayFeedbacks();
    }

    public void RollDiceFeels1()
    {
        rollExtraCube1?.PlayFeedbacks();
    }
}
