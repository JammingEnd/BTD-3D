public static class ProjectileSpawnInitializer
{
    public static ProjectileProperties ProjectileInitialize(UsedAttackDef attack)
    {
        ProjectileProperties props = new ProjectileProperties();
        props.Damage = attack.Damage;
        props.Pierce = attack.Pierce;
        props.ProjectileSpeed = attack.ProjectileSpeed;
        props.LifeTime = attack.LifeTime;
        props.IsInstant = attack.IsInstant;
        props.AoeRadius = attack.AoeRadius;
        props.AoePierce = attack.AoePP;
        props.AoeDamage = attack.AoeDamage;

        return props;
    }
}