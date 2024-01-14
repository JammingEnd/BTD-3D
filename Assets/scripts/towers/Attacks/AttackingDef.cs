using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.scripts.towers.visuals;

[RequireComponent(typeof(TowerStatusEffectHandler))]
public class AttackingDef : MonoBehaviour
{
    public List<UsedAttackDef> AttackTypes = new List<UsedAttackDef>();

    public Transform FirePosition;

    private SphereCollider _rangeCollider;
    public float CurrentRange = 0;
    private Dictionary<UsedAttackDef, bool> _canAttackStorage = new Dictionary<UsedAttackDef, bool>();

    private List<PathingDef> _currentTargets = new List<PathingDef>();
    private Transform _target;

    public BaseAttackSCDef baseAttack;
    public void AddAttacks(UsedAttackDef attack)
    {
        AttackTypes.Add(attack);
        _canAttackStorage.Add(attack, true);
        StartCoroutine(AttackCooldown(attack));
        
    }

    private void Start()
    {
        AddAttacks(SetStartAttack());
        if(this.TryGetComponent(out SphereCollider sphere))
        {
            _rangeCollider = sphere;
            SetRange();
        }
    }

    public void SetRange()
    {
        CurrentRange = AttackTypes[0].Range;
        _rangeCollider.radius = CurrentRange;
    }
    private void Update()
    {
       
    }
    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PathingDef pathing)) {
            _currentTargets.Add(pathing);
           
            _currentTargets.RemoveAll(x => !x);
           
        }
        
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PathingDef pathing))
        {
            _currentTargets.Remove(pathing);
            _currentTargets.RemoveAll(x => !x);
            

        }
    }
    #endregion

    #region Attack related
    private UsedAttackDef SetStartAttack()
    {
        var newAttack = new UsedAttackDef();
        newAttack.SetBaseStats(baseAttack);
        
        return newAttack;
    }

    private IEnumerator AttackCooldown(UsedAttackDef attack)
    {
   
        while (true)
        {
           
            _currentTargets.RemoveAll(x => !x);
            if (_currentTargets.Count > 0)
            {
                if (_currentTargets.Count != 0)
                {
                    _target = SortTargetList();
                    this.transform.LookAt(_target);
                }
                var projectileSpawn = Instantiate(attack.ProjectilePrefab, FirePosition.position, FirePosition.rotation);
               if(attack.NumOfShots > 1)
                {
                    SpawnMultipleWithSpread(attack);
                }
                else
                {
                    SpawnSingleProjectile(attack);
                }
                yield return new WaitForSeconds(attack.AttackSpeed);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
            
        }

    }
    void SpawnSingleProjectile(UsedAttackDef attack)
    {
        var projectileSpawn = Instantiate(attack.ProjectilePrefab, FirePosition.position, FirePosition.rotation);
        if (projectileSpawn.TryGetComponent(out ProjectileDef projectile))
        {
            
            projectile.SetStats(ProjectileSpawnInitializer.ProjectileInitialize(attack));
            TriggerSpecialOnStart(AttackTypes.IndexOf(attack), projectileSpawn);
        }

    }
    void SpawnMultipleWithSpread(UsedAttackDef attack)
    {
        for (int i = 0; i < attack.NumOfShots; i++)
        {
            Quaternion newRotation = Quaternion.Euler(Vector3.zero);
            if (attack.DirectionType == 0)
            {
               newRotation = calculateSpread(FirePosition.rotation.eulerAngles, i, attack.Spread);   
            }
            else if (attack.DirectionType == 1)
            {
               newRotation = calculateSpreadForPingPong(FirePosition.rotation.eulerAngles, i, attack.Spread);
            }
            
            var projectileSpawn = Instantiate(attack.ProjectilePrefab, FirePosition.position, newRotation);
            if (projectileSpawn.TryGetComponent(out ProjectileDef projectile))
            {
                projectile.SetStats(ProjectileSpawnInitializer.ProjectileInitialize(attack));
                    
                TriggerSpecialOnStart(AttackTypes.IndexOf(attack), projectileSpawn);
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
    private Quaternion calculateSpreadForPingPong(Vector3 baseRotation, int index, float spreadAmount)
    {
        if(index % 2 == 0) {
            Vector3 newRot = new Vector3(
               0,
               baseRotation.y + (spreadAmount * index),
               0
               );
            return Quaternion.Euler(newRot);
        }
        else
        {
            Vector3 newRot = new Vector3(
               0,
               baseRotation.y - ( (spreadAmount * index) * 2) ,
               0
               );
            return Quaternion.Euler(newRot);
        }
       
       
    }
    public void ResetAttackCoroutine()
    {
        this.StopAllCoroutines();
        for (int i = 0; i < AttackTypes.Count; i++)
        {
            StartCoroutine(AttackCooldown(AttackTypes[i]));
        }
    }

    #endregion

    #region Upgrade related

    public void Upgrade(List<UsedAttackDef> attacks)
    {
        foreach (var item in attacks)
        {
            if (!AttackTypes.Contains(item))
            {
                AttackTypes.Add(item);
            }
            else
            {

            }

        }
    }



    #endregion

    #region target aquisition

    private Transform SortTargetList()
    {
        //_currentTargets.Where(x => x.OverallProgress == _currentTargets.Max(y => y.OverallProgress));
        if(_currentTargets.Count > 0)
        {
            var singleTarget = _currentTargets.Where(x => x.OverallProgress == _currentTargets.Max(y => y.OverallProgress)).FirstOrDefault();
            return singleTarget.transform;
        }
        else { return null; }
    }

    #endregion
    void TriggerSpecialOnStart(int attackIndex, GameObject attack)
    {
        /*
        if (AttackTypes[attackIndex].ProjectileAugmentHandler != null)
        {
            if (AttackTypes[attackIndex].ProjectileAugmentHandler.TryGetComponent(out MonoBehaviour special))
            {
                Debug.LogWarning(special.GetType());
                attack.gameObject.AddComponent(special.GetType());
                if(attack.TryGetComponent(out ISpecialTriggerAble specialTrigger))
                {
                    specialTrigger.Trigger(attack);
                }
            }
        }
*/
    }
}

