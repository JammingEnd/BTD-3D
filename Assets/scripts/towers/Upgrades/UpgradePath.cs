using Assets.scripts.towers.visuals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradePath : MonoBehaviour
{
    public List<UpgradeSCDef> Upgrades = new List<UpgradeSCDef>();

    public int _currentLevel = 0;

    private AttackingDef _attackDef;


    //private SideBarUIHandler _UiHandler;
    private void Start()
    {
        if(this.TryGetComponent(out AttackingDef def))
        {
            _attackDef = def;
        }
    }



    bool NeedsRecalculate = false;
    public void OnUpgrade_OnClick()
    {
        UpgradePath[] paths = this.gameObject.GetComponents<UpgradePath>();
        NeedsRecalculate = false;
        foreach (UpgradePath path in paths) 
        { 

            if(_currentLevel > path._currentLevel)
            {
                NeedsRecalculate = true;
            }
            if(_currentLevel < path._currentLevel)
            {
                NeedsRecalculate = false;
            }
        }
        for (int i = 0; i < Upgrades[_currentLevel].baseAttacks.Count; i++)
        {
            if(_attackDef.baseAttack.ProjectilePrefab != Upgrades[_currentLevel].baseAttacks[i].ProjectilePrefab && NeedsRecalculate)
            {
              
                var newBaseAttack = Upgrades[_currentLevel].baseAttacks[i];
                _attackDef.baseAttack = newBaseAttack;
                UsedAttackDef newAttack = new UsedAttackDef();
                newAttack.SetBaseStats(newBaseAttack);
                

                    Debug.Log(paths.Length);
                    newAttack.RecalculateStats(paths);
                
               
                newAttack.ProjectileAugmentHandler = Upgrades[_currentLevel].ProjectileAugmentHandler;
                _attackDef.AttackTypes[i] = newAttack;
                _attackDef.ResetAttackCoroutine();
                _attackDef.SetRange();
            }
            else
            {
                var upgradedAttack = _attackDef.AttackTypes[i];
                
                upgradedAttack.UpgradeStats(Upgrades[_currentLevel].ValuePair);
                
              
               
                upgradedAttack.ProjectileAugmentHandler = Upgrades[_currentLevel].ProjectileAugmentHandler;
                _attackDef.AttackTypes[i] = upgradedAttack;
                _attackDef.ResetAttackCoroutine();
                _attackDef.SetRange();
            }
           
        }
       

        OnUpgrade();
    }

   

    private void OnUpgrade() 
    {
        _currentLevel++;
        
        
    }
}
