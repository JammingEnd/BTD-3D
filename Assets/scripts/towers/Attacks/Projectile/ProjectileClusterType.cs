using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileClusterType : ProjectileDef
{
    public bool SetCustomSpread;
    public float AngleBetweenShots;
    public int NumOfShots;
    public float RandomSpreadForwardDistance = 3f;
    public bool IsCluster = false;
    public GameObject projectilePrefab;

   
    public override void OnHit()
    {
        projectilePrefab = this.gameObject;
        if (!IsCluster)
        {
          
                SpawnProjectileInCluster(projectilePrefab, this.gameObject.transform, NumOfShots, AngleBetweenShots, RandomSpreadForwardDistance);
            
        }
      
       
    }

     void SpawnProjectileInCluster(GameObject objectToSpawn, Transform spawnPoint, int numOfShots, float angleBetweenShots, float randomDistanceFactor ,bool SetCustomSpread = false)
    {

        if (!SetCustomSpread)
        {
            angleBetweenShots = 360 / numOfShots;
          
        }
        for (int i = 0; i < numOfShots; i++)
        {


            Quaternion newRotation = Quaternion.Euler(Vector3.zero);
            newRotation = calculateSpread(this.transform.rotation.eulerAngles, i, angleBetweenShots);
            

            var projectileSpawn = Instantiate(objectToSpawn, this.transform.position, newRotation) as GameObject;
            float dist = UnityEngine.Random.Range(0, randomDistanceFactor);
            Vector3 newPos = (projectileSpawn.transform.position + projectileSpawn.transform.forward * 2.5f);
            projectileSpawn.transform.position = newPos;
            if (projectileSpawn.TryGetComponent(out Collider collision))
            {
                collision.enabled = false;

                if (projectileSpawn.TryGetComponent(out ProjectileClusterType projectile))
                {
                    
                    projectile._stats = this._stats;
                    projectile._stats.AoeRadius *= 0.87f;
                    projectile._currentProjectileSpeed = 0;
                    projectile._popVfx.RenderSwitch(false);
                    projectile.IsCluster = true;
                    projectile.SetFuse(0.1f);
                    Debug.Log("AssignStats");
                }

                
            }

            
          
        }
       
    }
    private Quaternion calculateSpread(Vector3 baseRotation, int index, float spreadAmount)
    {
        Vector3 newRot = new Vector3(
                0,
                baseRotation.y + (spreadAmount * index),
                0
                );
        return Quaternion.Euler(newRot);
    }
    public void SetFuse(float time)
    {
        StartCoroutine(StarttFuse(time));
    }
    public IEnumerator StarttFuse(float time)
    {
        yield return new WaitForSeconds(time);
        ProjectileExplosion.Explode(transform.position, _stats, _popVfx, _hitLayers);
        StopAllCoroutines();
    }

}