using BepInEx;
using R2API;
using R2API.AssetPlus;
using R2API.Utils;

namespace GNCItemSuite
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [R2APISubmoduleDependency(nameof(AssetPlus), nameof(ItemAPI), nameof(ItemDropAPI), nameof(ResourcesAPI))]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class GNCItemSuitePlugin : BaseUnityPlugin
    {   
        private const string ModVer = "1.0.0";
        private const string ModName = "PoisonOnHit";
        public const string ModGuid = "com.GlenCloughley.PoisonOnHit";
        internal static GNCItemSuitePlugin Instance;

        public void Awake()
        {
            if (Instance == null) { Instance = this; }
            GNCItemSuiteConfig.Load();
            GNCItemSuite.Init();
            Hooks.Init();
        }
    }
}