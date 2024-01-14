using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(VFX_OnHit))]
[RequireComponent (typeof(ProjectileAugmentHandler))]
public class ProjectileDef : MonoBehaviour
{
    private Rigidbody rb;
    private protected VFX_OnHit _popVfx;
    private ProjectileAugmentHandler _projectileAugmentHandler;
    private protected ProjectileStats _stats;
    public LayerMask _hitLayers;

    private protected float _currentProjectileSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _popVfx = GetComponent<VFX_OnHit>();
        
        _stats = new ProjectileStats();
    }

    public void SetStats(ProjectileProperties properties)
    {
        _stats.Initialize(properties);
        _currentProjectileSpeed = _stats.ProjectileSpeed;
        _projectileAugmentHandler = GetComponent<ProjectileAugmentHandler>();
        _projectileAugmentHandler._projectile = this;
    }

    private void Start()
    {
        Destroy(gameObject, _stats.LifeTime);
    }

    private void Update()
    {
        ProjectileMover.Move(transform, rb, _currentProjectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }

    private void Hit(Collider target)
    {
        if (target.CompareTag("Tower"))
        {
            Debug.Log($"hit Tower");
            return;
        }
            

        Debug.Log($"Hit {target}");

        if (_stats.AoePierce > 0)
        {
            bool piercingProj = false;
            if(_stats.Pierce > 1) { 
                piercingProj = true;
            }
            ProjectileExplosion.Explode(transform.position, _stats, _popVfx, _hitLayers, piercingProj);
        }
        else
        {
            DamageTarget(target);
        }
        OnHit();
        if(_projectileAugmentHandler != null)
        {
           // _projectileAugmentHandler.ActivateOnHit();
        }
      
        _stats.Pierce--;

        if (_stats.Pierce <= 0)
        {
            if (_stats.AoePierce > 0)
            {
                Destroy(this);
                Debug.Log($"i die");
            }
            else
            {
                Destroy(this.gameObject);
                Debug.Log($"i die");
            }
            
        }
    }
    public virtual void OnHit()
    {

    }
    private void DamageTarget(Collider target)
    {
        if (target.TryGetComponent<IDamageAble>(out var damageable))
        {
            damageable.Damage(_stats.Damage);
        }
    }

}

