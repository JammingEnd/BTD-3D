public class ProjectileStats
{
    public int Damage { get; private set; }
    public int Pierce { get; set; }
    public float ProjectileSpeed { get; private set; }
    public float LifeTime { get; private set; }
    public bool IsInstant { get; private set; }
    public int AoeDamage { get; private set; }
    public float AoeRadius { get; set; }
    public int AoePierce { get; private set; }

    public bool IsSeeking { get; private set; }
    public float SeekingArc {  get; private set; }

    public void Initialize(ProjectileProperties properties)
    {
        Damage = properties.Damage;
        Pierce = properties.Pierce;
        ProjectileSpeed = properties.ProjectileSpeed * 10;
        LifeTime = properties.LifeTime;
        IsInstant = properties.IsInstant;
        AoeDamage = properties.AoeDamage;
        AoeRadius = properties.AoeRadius;
        AoePierce = properties.AoePierce;

        IsSeeking = properties.IsSeeking;
        SeekingArc = properties.SeekingArc;
    }
}

public struct ProjectileProperties
{
    public int Damage, Pierce;
    public float ProjectileSpeed, LifeTime;
    public bool IsInstant;
    public int AoeDamage;
    public float AoeRadius;
    public int AoePierce;

    public bool IsSeeking;
    public float SeekingArc;
}