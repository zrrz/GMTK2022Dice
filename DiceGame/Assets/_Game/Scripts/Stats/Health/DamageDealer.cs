using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DamageDealer : MonoBehaviour
{
    public LayerMask layerMask;
    public float damageAmount = 10f;

    public System.Action<Health> OnHit;

    private void OnTriggerEnter(Collider collision)
    {
        if (layerMask.IsInLayerMask(collision.gameObject.layer) == false)
        {
            return;
        }

        Health health = collision.GetComponent<Health>();
        if (health != null)
        {
            if(OnHit != null)
            {
                OnHit.Invoke(health);
            }
            else
            {
                health.Damage(transform.root.gameObject, damageAmount, transform.position, transform.forward);
            }
        }
    }
}
