using System.Collections.Generic;
using UnityEngine;

public class ProjectileClusterLogic 
{
  
  
    public static void SpawnProjectileInCluster(List<GameObject> objectToSpawn, Transform spawnPoint, int numOfShots, float angleBetweenShots, bool SetCustomSpread = false)
    {
        
        if (SetCustomSpread)
        {
            angleBetweenShots = 360 / numOfShots;
        }
        for (int i = 0; i < numOfShots; i++)
        {
            Quaternion newRotation = Quaternion.Euler(Vector3.zero);
            newRotation = calculateSpread(spawnPoint.rotation.eulerAngles, i, angleBetweenShots);
            
          

            var projectileSpawn = objectToSpawn[i];
            if (projectileSpawn.TryGetComponent(out ProjectileDef projectile))
            {
              
            }
        }
    }
    private static Quaternion calculateSpread(Vector3 baseRotation, int index, float spreadAmount)
    {
        Vector3 newRot = new Vector3(
                0,
                baseRotation.y + (spreadAmount * index),
                0
                );
        return Quaternion.Euler(newRot);
    }
}