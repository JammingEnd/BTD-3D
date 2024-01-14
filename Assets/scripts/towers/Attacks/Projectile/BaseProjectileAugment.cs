
[System.Serializable]
public class BaseProjectileAugment
{
    public virtual ProjectileDef ProjectileDef { get; set; }
    public virtual void ActivateOnHit(ProjectileDef projectile)
    {
        ProjectileDef = projectile;
    }
}