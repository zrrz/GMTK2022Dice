using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData
{
    public float amount;
    public Vector3 position;
    public Vector3 direction;
    public GameObject source;

    public DamageData(GameObject source, float amount, Vector3 position, Vector3 direction)
    {
        this.amount = amount;
        this.position = position;
        this.direction = direction;
        this.source = source;
    }
}
