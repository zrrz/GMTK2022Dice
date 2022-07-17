using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Cogs/DamageEffect")]
public class DamageEffectData : ScriptableObject
{
    [SerializeField] public Color damageColor = Color.red;
    [SerializeField] public AnimationCurve strengthCurve
        = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));
    [SerializeField] public int flashCount = 3;
    [SerializeField] public ParticleSystem hitParticle;
}
