using BepInEx.Configuration;

namespace GNCItemSuite
{
    class GNCItemSuiteConfig
    {
        internal static ConfigEntry<float> RitualDaggerProcChance;

        internal static void Load()
        {
            RitualDaggerProcChance = GNCItemSuitePlugin.Instance.Config.Bind<float>("RitualDagger Settings", "Proc Chance",
                12.5f, "Controls the proc chance of rockets firing.");
        }
    }
}