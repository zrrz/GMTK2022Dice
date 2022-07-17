using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect
{
    private List<Renderer> allRenderers = new List<Renderer>();

    private List<DamageEffectState> effectState = new List<DamageEffectState>();
    private Health healthObject;

    private DamageEffectData effectData;

    public DamageEffect(Health healthObject, List<Renderer> renderers, DamageEffectData effectData)
    {
        this.healthObject = healthObject;
        this.allRenderers = renderers;
        this.effectData = effectData;
        SetupDamageEffect();
    }

    [System.Serializable]
    private class DamageEffectState
    {
        public Material mat;
        public Color defaultColor;
    }

    public void Play(float effectLength, DamageData damageData)
    {
        healthObject.StartCoroutine(ShowDamageEffect(effectLength));
        SpawnDamageParticle(damageData);
    }

    private void SpawnDamageParticle(DamageData damageData)
    {
        var ps = effectData.hitParticle;
        GameObject.Instantiate(ps, damageData.position, Quaternion.LookRotation(damageData.direction));
    }

    private void SetupDamageEffect()
    {
        if (allRenderers.Count == 0)
        {
            Debug.LogWarning("No Renderers", healthObject.gameObject);
        }
        effectState.Clear();
        foreach (Renderer rend in allRenderers)
        {
            effectState.Add(new DamageEffectState() { mat = rend.material, defaultColor = rend.material.color });
        }
    }

    IEnumerator ShowDamageEffect(float effectLength)
    {

        for (int i = 0; i < effectData.flashCount; i++)
        {
            for (float t = 0; t < 1f; t += Time.deltaTime / effectLength * (float)effectData.flashCount)
            {
                float effectStrength = effectData.strengthCurve.Evaluate(t);
                foreach (var effect in effectState)
                {
                    effect.mat.color = Color.Lerp(effect.defaultColor, effectData.damageColor, effectStrength);
                }
                yield return null;
            }
        }

        foreach (var effect in effectState)
        {
            effect.mat.color = effect.defaultColor;
        }
    }
}
