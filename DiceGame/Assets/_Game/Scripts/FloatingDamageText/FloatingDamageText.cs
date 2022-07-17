using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    private static FloatingDamageText instance = null;

    [SerializeField]
    public FloatingDamageTextInstance floatingDamageTextPrefab;

    public enum DamageType
    {
        Normal, Critical
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(FloatingDamageText)} already exists");
        }
    }

    public static void ShowDamageText(string text, Vector3 position, DamageType damageType)
    {
        if(instance == null)
        {
            Debug.LogError($"No instance of {nameof(FloatingDamageText)}");
            return;
        }
        //TODO implement critical hit text color and size 
        var newFloatingText = Instantiate(instance.floatingDamageTextPrefab);
        newFloatingText.textMesh.text = text;
        newFloatingText.transform.position = position;
    }
}
