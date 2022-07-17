using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    public float MaxHealth { get { return maxHealth; } }

    public float CurrentHealth { get; set; } = default;

    [SerializeField] private float damageCooldown = 0.2f;

    [SerializeField] public bool invulnerable = false;

    /// <summary>
    /// damageTaken, direction
    /// </summary>
    [System.NonSerialized] public UnityEvent<DamageData> damageTakenEvent = new UnityEvent<DamageData>();

    [System.NonSerialized] public UnityEvent<DamageData> deathEvent = new UnityEvent<DamageData>();

    [SerializeField] private List<Renderer> allRenderers = new List<Renderer>();
    [SerializeField] private DamageEffectData damageEffectData;

    private DamageEffect damageEffect;

    private float lastDamageTime = 0f;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        if(damageEffectData)
        {
            damageEffect = new DamageEffect(this, allRenderers, damageEffectData);
        }
    }

    public void Damage(GameObject source, float amount, Vector3 position, Vector3 direction)
    {
        if (lastDamageTime + damageCooldown > Time.time)
        {
            return;
        }
        //Debug.Log($"Damaging {gameObject.name} for {amount}");
        lastDamageTime = Time.time;
        CurrentHealth -= amount;

        DamageData damageData = new DamageData(source, amount, position, direction);
        damageTakenEvent.Invoke(damageData);

        if (CurrentHealth <= 0f)
        {
            deathEvent.Invoke(damageData);
        }

        if(damageEffect != null)
        {
            damageEffect.Play(damageCooldown, damageData);
        }

        FloatingDamageText.ShowDamageText(amount.ToString("F2"), position, FloatingDamageText.DamageType.Normal);
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, MaxHealth);
        DamageData damageData = new DamageData(null, -amount, Vector3.zero, Vector3.zero);
        damageTakenEvent.Invoke(damageData);
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        damageTakenEvent.Invoke(null);
    }
}
