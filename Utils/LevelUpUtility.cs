using Il2Cpp;

namespace BuffBar.Utils;

public static class LevelUpUtility
{
    public static void AddLevels(int levels)
    {
        for (var i = 0; i < levels; i++)
        {
            var exp = GameManagerUtil.SurvivorsGameManager.PlayerEntity._experience;
            // exp.Stats.Reference.Stats.ExperienceModifier; // can do something with this maybe?
            exp.AddExperience(exp.RequiredExperience);
        }
    }

    public static void AddRerolls(int rerolls)
    {
        GameManagerUtil.SurvivorsGameManager.PlayerEntity._inventory.InventoryData.Rerolls += rerolls;
    }
}