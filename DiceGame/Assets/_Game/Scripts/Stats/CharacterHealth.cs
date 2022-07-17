using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] public Health health;
    //[SerializeField] private PlayerData playerData;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioSource hurtAudioSource;

    private void OnEnable()
    {
        health.damageTakenEvent.AddListener(OnDamageTaken);
        health.deathEvent.AddListener(OnDeath);
    }

    private void OnDisable()
    {
        health.damageTakenEvent.RemoveListener(OnDamageTaken);
        health.deathEvent.RemoveListener(OnDeath);
    }

    private void OnDamageTaken(DamageData damageData)
    {
        if(damageData != null && damageData.amount > 0f)
        {
            PlayHurtSound();
        }
    }

    private void PlayHurtSound()
    {
        var clip = hurtSounds.RandomItem();
        hurtAudioSource.clip = clip;
        hurtAudioSource.Play();
    }

    private void OnDeath(DamageData damageData)
    {
        
    }

    public void Heal(float amount)
    {
        health.Heal(amount);
    }
}

