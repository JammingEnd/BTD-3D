using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerStatusEffectHandler : MonoBehaviour
{
    public AttackingDef AttackingDef;

    private Dictionary<TowerStatusEffectDef, float> OngoingEffects;
    private void Start()
    {
        AttackingDef = GetComponent<AttackingDef>();
    }

    public void DoEffect(TowerStatusEffectDef effect)
    {
        if (OngoingEffects.ContainsKey(effect)) {
            OngoingEffects.Add(effect, effect.Duration);
        }
        else
        {
            OngoingEffects[effect] = effect.Duration;
        }
       
    }

    private IEnumerator StatusEffectCooldown(float time)
    {
        while(true)
        {



            yield return new WaitForSeconds(time);
        }

        
    }
}
