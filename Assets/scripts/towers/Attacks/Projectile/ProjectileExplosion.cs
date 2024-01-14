using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class ProjectileExplosion
{
    public static void Explode(Vector3 position, ProjectileStats stats, VFX_OnHit vfx, LayerMask hitLayers, bool HasDefaultPierce = false)
    {
        vfx.RenderSwitch(true);
        vfx.SetExplosion(50, stats.AoeRadius, 10, HasDefaultPierce); // Adjust these parameters as necessary

        Collider[] hitTargets = Physics.OverlapSphere(position, stats.AoeRadius, hitLayers);
      

        IDictionary<GameObject, float> pierceCalcDict = new Dictionary<GameObject, float>();

        foreach (var target in hitTargets)
        {
            pierceCalcDict.Add(target.gameObject, Vector3.Distance(position, target.transform.position));
           
        }

        var sortedList = pierceCalcDict.OrderBy(d => d.Value).ToList();
        if (sortedList.Count > stats.AoePierce)
        {
            sortedList.RemoveAll(x => x.Value > sortedList[stats.AoePierce - 1].Value);
        }

        foreach (var target in sortedList)
        {
            var collider = target.Key;
            if (collider.TryGetComponent(out IDamageAble damageable))
            {
               
                damageable.Damage(stats.AoeDamage);
            }
        }
    }
}

