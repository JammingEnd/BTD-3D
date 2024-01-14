using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ProjectileAugmentHandler : MonoBehaviour
{
    public ProjectileDef _projectile;
    public ProjectileAugmentHandler(ProjectileDef def)
    {
        _projectile = def;
    }

    public List<BaseProjectileAugment> baseProjectileAugments = new List<BaseProjectileAugment>();


    public void ActivateOnHit()
    {
        if(baseProjectileAugments.Count > 0)
        {
            foreach (var item in baseProjectileAugments)
            {
                item.ActivateOnHit(_projectile);
            }
        }
       
    }
}

