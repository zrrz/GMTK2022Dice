using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //public PlayerAnimator PlayerAnimator { get; private set; }
    //public PlayerPickup PlayerPickup { get; private set; }
    public CharacterHealth CharacterHealth;
    public WeaponAnimator WeaponAnimator;

    public static PlayerData Instance;

    private void Awake()
    {
        //PlayerAnimator = GetComponent<PlayerAnimator>();
        //PlayerPickup = GetComponent<PlayerPickup>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(PlayerData)} already exists");
        }
    }

    public Vector3 GetCenterPosition()
    {
        return transform.position + Vector3.up;
    }

    public static bool InputLocked => inputLockedCount > 0;

    private static int inputLockedCount = 0;

    public static void AddInputLock()
    {
        inputLockedCount++;
        //print("add: " + inputLockedCount);
    }

    public static void ReleaseInputLock()
    {
        inputLockedCount = Mathf.Max(0, inputLockedCount - 1);
        //print("release: " + inputLockedCount);
    }

    public static void ResetInputLock()
    {
        inputLockedCount = 0;
    }  
}
