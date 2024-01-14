using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UsedAttackDef
{
    //base Stats
    public int Damage;
    public int Pierce;
    public float AttackSpeed;
    public float Range;
    public float ProjectileSpeed;
    public float LifeTime;
    public bool IsInstant;
    public bool CanSeeCamo;
    public bool CanDamageFrozen;
    public bool CanDamageLead;
    public bool CanDamagePurple;
    public bool CanRehit;

    //Explosive
    public int AoeDamage;
    public float AoeRadius;
    public int AoePP;

    public int NumOfShots;
    public float Spread;
    public int DirectionType;

    public int DamageVsFrozen,
      DamageVsLead,
      DamageVsPurple,
      DamageVsCeramic,
      DamageVsFortified,
      DamageVsMoab,
      DamageVsBoss;

    public bool isUpdated = false;

    public GameObject ProjectilePrefab;
    public ProjectileAugmentHandler ProjectileAugmentHandler;
    public void SetBaseStats(BaseAttackSCDef BaseAttack)
    {
        Damage = BaseAttack.BaseDamage;
        Pierce = BaseAttack.BasePierce;
        DamageVsFrozen = BaseAttack.BaseDamageVsFrozen;
        DamageVsLead = BaseAttack.BaseDamageVsLead;
        DamageVsPurple = BaseAttack.BaseDamageVsPurple;
        DamageVsCeramic = BaseAttack.BaseDamageVsCeramic;
        DamageVsFortified = BaseAttack.BaseDamageVsFortified;
        DamageVsMoab = BaseAttack.BaseDamageVsMoab;
        DamageVsBoss = BaseAttack.BaseDamageVsBoss;
        NumOfShots = BaseAttack.BaseNumOfShots;
        DirectionType = BaseAttack.BaseSpreadType;

        AttackSpeed = BaseAttack.BaseAttackSpeed;
        Range = BaseAttack.BaseRange;
        ProjectileSpeed = BaseAttack.BaseProjectileSpeed;
        LifeTime = BaseAttack.BaseLifeTime;
        Spread = BaseAttack.BaseSpread;

        IsInstant = BaseAttack.IsInstant;
        CanSeeCamo = BaseAttack.CanSeeCamo;
        CanDamageFrozen = BaseAttack.CanDamageFrozen;
        CanDamageLead = BaseAttack.CanDamageLead;
        CanDamagePurple = BaseAttack.CanDamagePurple;
        CanRehit = BaseAttack.CanRehit;


        AoeDamage = BaseAttack.BaseAoeDamage;
        AoePP = BaseAttack.BaseAoePierce;
        AoeRadius = BaseAttack.BaseAoeRadius;

        

        ProjectilePrefab = BaseAttack.ProjectilePrefab;
    }

    public void UpgradeStats(ValuePairWrapper valuePair)
    {
        var types = valuePair.ValuePairs;
        for (int i = 0; i < types.Count; i++)
        {
            StatTypes switchTypes = types[i].StatType;
            var typeValue = types[i].Value;
            Debug.Log($"Type: {switchTypes}, Value; {typeValue}");
            switch (switchTypes)
            {
                case StatTypes.Damage:
                    {
                        Damage += (int)typeValue;
                    }
                    break;
                case StatTypes.Pierce:
                    {
                        Pierce += (int)typeValue;
                    }
                    break;
                case StatTypes.AoeDamage:
                    {
                        AoeDamage += (int)typeValue;
                    }
                    break;
                case StatTypes.AoePP:
                    {
                        AoePP += (int)typeValue;
                    }
                    break;
                case StatTypes.DamageVsFrozen:
                    {
                        DamageVsFrozen += (int)typeValue;
                    }
                    break;
                case StatTypes.DamageVsLead:
                    {
                        DamageVsLead += (int)typeValue;
                    }
                    break;
                case StatTypes.DamageVsPurple:
                    {
                        DamageVsPurple += (int)typeValue;
                    }
                    break;
                case StatTypes.DamageVsCeramic:
                    {
                        DamageVsCeramic += (int)typeValue;
                    }
                    break;
                case StatTypes.DamageVsFortified:
                    {
                        DamageVsFortified += (int)typeValue;
                    }
                    break;
                case StatTypes.DamageVsMoab:
                    {
                        DamageVsMoab += (int)typeValue;
                    }
                    break;
                case StatTypes.NumOfShots:
                    {
                        NumOfShots += (int)typeValue;
                    }
                    break;
                case StatTypes.DirectionType:
                    {
                        DirectionType = (int)typeValue;
                    }
                    break;
                case StatTypes.DamageVsBoss:
                    {
                        DamageVsBoss += (int)typeValue;
                    }
                    break;
                case StatTypes.AttackSpeed:
                    {
                        if (AttackSpeed == 0)
                        {
                            AttackSpeed = typeValue;
                        }
                        else
                        {
                            AttackSpeed = AttackSpeed * (1 - typeValue);
                        }
                    }
                    break;
                case StatTypes.Range:
                    {
                        if (Range == 0)
                        {
                            Range = typeValue;
                        }
                        else
                        {
                            Range = Range * (1 + typeValue);
                        }
                    }
                    break;
                case StatTypes.ProjectileSpeed:
                    {
                        if (ProjectileSpeed == 0)
                        {
                            ProjectileSpeed = typeValue;
                        }
                        else
                        {
                            ProjectileSpeed = ProjectileSpeed * (1 + typeValue);
                        }
                    }
                    break;
                case StatTypes.Spread:
                    {
                        if (Spread == 0)
                        {
                            Spread = typeValue;
                        }
                        else
                        {
                            Spread = Spread * (1 + typeValue);
                        }
                    }
                    break;
                case StatTypes.LifeTime:
                    {
                        if (LifeTime == 0)
                        {
                            LifeTime = typeValue;
                        }
                        else
                        {
                            LifeTime = LifeTime * (1 + typeValue);
                        }
                    }
                    break;
                case StatTypes.AoeRadius:
                    {  
                       
                            if (AoeRadius == 0)
                            {
                                AoeRadius = typeValue;
                            }
                            else 
                            { 
                                AoeRadius = AoeRadius * (1 + typeValue);
                            }
                    
                    }
                    break;
                case StatTypes.IsInstant:
                    {
                        //bools

                        if ((int)typeValue == 1)
                        {
                            IsInstant = true;

                        }
                        else
                        {
                            IsInstant = false;
                        }

                    }
                    break;
                case StatTypes.CanSeeCamo:
                    {
                        //bools

                        if ((int)typeValue == 1)
                        {
                            CanSeeCamo = true;

                        }
                        else
                        {
                            CanSeeCamo = false;
                        }

                    }
                    break;
                case StatTypes.CanDamageFrozen:
                    {
                        //bools

                        if ((int)typeValue == 1)
                        {
                            CanDamageFrozen = true;

                        }
                        else
                        {
                            CanDamageFrozen = false;
                        }

                    }
                    break;
                case StatTypes.CanDamageLead:
                    {
                        //bools

                        if ((int)typeValue == 1)
                        {
                            CanDamageLead = true;

                        }
                        else
                        {
                            CanDamageLead = false;
                        }

                    }
                    break;
                case StatTypes.CanDamagePurple:
                    {
                        //bools

                        if ((int)typeValue == 1)
                        {
                            CanDamagePurple = true;

                        }
                        else
                        {
                            CanDamagePurple = false;
                        }

                    }
                    break;
                case StatTypes.CanRehit:
                    {
                        //bools
                       
                        if ((int)typeValue == 1)
                        {
                             CanRehit = true;

                        }
                        else
                        {
                            CanRehit = false;
                        }
                       
                    }
                    break;
                case StatTypes.ProjectilePrefab:
                    break;
              
               
            }
        }
        // other stuff




    }
}
