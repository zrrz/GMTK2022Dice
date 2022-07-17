using UnityEngine;
using System.Collections;
using System;

public class WeaponAnimator : MonoBehaviour
{
    //[System.Serializable]
    //private class AttackTransformData
    //{
    //    [SerializeField] public Vector3 startPosition = Vector3.left;
    //    [SerializeField] public Vector3 endPosition = Vector3.right;
    //    [SerializeField] public Vector3 startRotation;
    //    [SerializeField] public Vector3 endRotation;

    //    public Vector3 startDirection => Quaternion.Euler(startRotation) * Vector3.forward;
    //    public Vector3 endDirection => Quaternion.Euler(endRotation) * Vector3.forward;
    //}

    [SerializeField] Transform startTransform;
    [SerializeField] Transform endTransform;

    [SerializeField] private DamageDealer swordDamageDealer = default;

    [SerializeField] private AudioClip slashAttackSound = default;

    [SerializeField] private TrailRenderer originalSwordTrail = default;

    private bool attacking = false;

    [SerializeField] public float attackTime = 0.2f;

    [SerializeField] public Transform weaponTransform = null;

    //public Vector3 defaultPosition = Vector3.zero;
    //public Vector3 defaultRotation = Vector3.zero;

    [SerializeField] public float attackCooldownTime = 0.1f;

    //private float lastAttackTime = 0f;

    //[SerializeField] private AttackTransformData attackData = default;

    [SerializeField] private float damageAmount = 10f;

    private Vector3 AttackStartPos => startTransform.localPosition;// attackData.startPosition + defaultPosition;
    private Vector3 AttackEndPos => endTransform.localPosition; //attackData.endPosition + defaultPosition;
    private Vector3 AttackStartDir => startTransform.localRotation * Vector3.forward;// startTransform.forward; //.startDirection;// + defaultRotation;
    private Vector3 AttackEndDir => endTransform.localRotation * Vector3.forward; //attackData.endDirection;// + defaultRotation;

    private void Start()
    {
        originalSwordTrail.emitting = false;
        weaponTransform.gameObject.SetActive(false);
        //swordDamageDealer.enabled = false;
        //defaultPosition = weaponTransform.localPosition;
        //defaultRotation = weaponTransform.localEulerAngles;
    }

    private void Update()
    {
        if (attacking)
            return;

        if(Input.GetButtonDown("Fire1"))
        {
            Attack(OnHit);
        }
    }

    public void OnHit(PlayerData characterData, Health health)
    {
        var node = health.GetComponent<Tree>();
        if (node != null)
        {
            health.Damage(characterData.gameObject, damageAmount, health.transform.position, health.transform.forward);
        }

        health.Damage(characterData.gameObject, damageAmount, health.transform.position, health.transform.forward);
    }

    public void Attack(System.Action<PlayerData, Health> OnHit)
    {
        if(attacking)
        {
            return;
        }
        attacking = true;
        //bool reverse = !lastWasReversed && (Time.time < lastAttackTime + comboTime) && lastAttackDirection == AttackDirection.Up;
        //lastWasReversed = reverse;
        StartCoroutine(DoAttack(OnHit));
    }

    private IEnumerator DoAttack(System.Action<PlayerData, Health> OnHit)
    {
        swordDamageDealer.OnHit += (Health health) => { 
            OnHit.Invoke(GetComponentInParent<PlayerData>(), health); 
        };

        if (slashAttackSound != null)
        {
            AudioSource.PlayClipAtPoint(slashAttackSound, transform.position);
        }

        weaponTransform.gameObject.SetActive(true);

        TrailRenderer swordTrail = Instantiate(originalSwordTrail, originalSwordTrail.transform.parent);

        swordTrail.enabled = true;
        swordTrail.emitting = true;
        for (float t = 0; t < 1f; t += Time.deltaTime / attackTime)
        {
            SetWeaponTransformAtTime(t);
            yield return null;
        }

        //HACK for for if t doesn't hit 1 exactly
        SetWeaponTransformAtTime(1f);

        swordTrail.emitting = false;
        swordTrail.transform.parent = null;
        Destroy(swordTrail.gameObject, 0.05f);


        yield return new WaitForSeconds(attackCooldownTime);
        weaponTransform.gameObject.SetActive(false);
        //lastAttackTime = Time.time;
        attacking = false;
        swordDamageDealer.OnHit = null;
    }

    private void SetWeaponTransformAtTime(float t)
    {
        Vector3 transformedStartPos = transform.TransformPoint(AttackStartPos);
        Vector3 transformedEndPos = transform.TransformPoint(AttackEndPos);
        Quaternion transformedStartRotation = Quaternion.LookRotation(transform.TransformVector(AttackStartDir), Vector3.up);
        Quaternion transformedEndRotation = Quaternion.LookRotation(transform.TransformVector(AttackEndDir), Vector3.up);

        float easeIn = t * t;
        Vector3 position = Vector3.Lerp(transformedStartPos, transformedEndPos, easeIn);
        Quaternion rotation = Quaternion.Lerp(transformedStartRotation, transformedEndRotation, easeIn);
        weaponTransform.position = position;
        weaponTransform.rotation = rotation;
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 startPos = weaponTransform.transform.localPosition + attackData.startPosition;
    //    Vector3 endPos = weaponTransform.transform.localPosition + attackData.endPosition;
    //    Vector3 startDir = weaponTransform.transform.localEulerAngles + attackData.startDirection;
    //    Vector3 endDir = weaponTransform.transform.localEulerAngles + attackData.endDirection;

    //    if (Application.isPlaying)
    //    {
    //        startPos = AttackStartPos;
    //        endPos = AttackEndPos;
    //        startDir = AttackStartDir;
    //        endDir = AttackEndDir;
    //    }

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.TransformPoint(startPos), transform.TransformPoint(endPos));
    //    DrawGizmoArrow(transform.TransformPoint(startPos), transform.TransformVector(startDir), Color.blue);
    //    DrawGizmoArrow(transform.TransformPoint(endPos), transform.TransformVector(endDir), Color.blue);
    //}

    public static void DrawGizmoArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        direction = direction.normalized * 0.5f;
        Gizmos.color = color;
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 up = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back;
        Vector3 down = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back;
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, up * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, down * arrowHeadLength);
    }

    private void OnValidate()
    {
        //attackData.startDirection.Normalize();
        //attackData.endDirection.Normalize();
    }
}

