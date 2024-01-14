using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "AddContent/New Attack")]
public class BaseAttackSCDef : ScriptableObject
{
    [Header("Base Stats")]
    public int BaseDamage;
    public int BasePierce;
    public int BaseDamageVsFrozen;
    public int BaseDamageVsLead;
    public int BaseDamageVsPurple;
    public int BaseDamageVsCeramic;
    public int BaseDamageVsFortified;
    public int BaseDamageVsMoab;
    public int BaseDamageVsBoss;
    public float BaseAttackSpeed;
    public float BaseRange;
    public float BaseProjectileSpeed;
    public float BaseLifeTime;
    public bool IsInstant;
    public bool CanSeeCamo;
    public bool CanDamageFrozen;
    public bool CanDamageLead;
    public bool CanDamagePurple;
    public bool CanRehit;

    [Header("Special Scripts")]
    public GameObject SpecialScript;

    [Header("Explosive Stats")]
    public int BaseAoeDamage;
    public float BaseAoeRadius;
    public int BaseAoePierce;

    [Header("spread")]
    public int BaseNumOfShots;
    public float BaseSpread;

    [Header("0 = clockwise, 1 = pingpong")]
    [Range(0f, 1f)]
    public int BaseSpreadType;

    [Header("PojectilePrefab")]
    public GameObject ProjectilePrefab;
    // [Header("Debuffs")]
    // public List<DebuffDef> Debuffs = new List<DebuffDef>();

    [Header("Misc")]
    public string AttackName;
}
