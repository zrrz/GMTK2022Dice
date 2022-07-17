using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamageTextInstance : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;

    [SerializeField] private AnimationCurve sizeOverLifetime = default;

    [SerializeField] private Gradient colorOverLifetime = default;

    [SerializeField] private Vector3 startVelocity = default;

    private float timer = 0f;
    private Vector3 startScale;

    [SerializeField]
    public TMPro.TextMeshProUGUI textMesh = null;

    private void Start()
    {
        startScale = transform.localScale;
        //Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform.position);

        timer += Time.deltaTime;

        float normalizedTime = timer / lifeTime;

        transform.localScale = startScale * sizeOverLifetime.Evaluate(normalizedTime);
        textMesh.color = colorOverLifetime.Evaluate(normalizedTime);
        transform.position += startVelocity * Time.deltaTime;

        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
