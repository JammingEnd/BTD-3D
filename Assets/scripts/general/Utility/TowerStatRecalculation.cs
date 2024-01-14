public static class TowerStatRecaclulation
{
    public static UsedAttackDef RecalculateStats(this UsedAttackDef attackDef, UpgradePath[] upgradePaths)
    {
        for (int i = 0; i < upgradePaths.Length; i++)
        {
            for (int level = 0; level < upgradePaths[i]._currentLevel; level++)
            {
                attackDef.UpgradeStats(upgradePaths[i].Upgrades[level].ValuePair);
            }
           
        }
        return attackDef;
    }
}