using System.Reflection;
using R2API;
using RoR2;
using UnityEngine;

namespace GNCItemSuite
{
    class GNCItemSuite
    {
        internal static ItemIndex PoisonOnHitItemIndex;
        internal static ItemIndex BlightOnHitItemIndex;
        internal static ItemIndex RitualDaggerItemIndex;
        
        
        internal static GameObject PoisonOnHitPrefab;
        internal static GameObject BlightOnHitPrefab;
        internal static GameObject RitualDaggerPrefab;
        
        private const string ModPrefix = "@GNCItemSuite:";
        private const string PrefabPath = ModPrefix + "Assets/Import/belt/";
        private const string IconPath = ModPrefix + "Assets/Import/belt_icon/dagger.png";

        internal static void Init()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GNCItemSuite.exampleitemmod")) 
            {
                Debug.Log(Assembly.GetExecutingAssembly().GetManifestResourceNames()[0]);
                var bundle = AssetBundle.LoadFromStream(stream);
                var provider = new AssetBundleResourcesProvider(ModPrefix.TrimEnd(':'), bundle);
                ResourcesAPI.AddProvider(provider);

                
                PoisonOnHitPrefab = bundle.LoadAsset<GameObject>("Assets/Import/belt/Poison.prefab");
                BlightOnHitPrefab = bundle.LoadAsset<GameObject>("Assets/Import/belt/Blight.prefab");
                RitualDaggerPrefab = bundle.LoadAsset<GameObject>("Assets/Import/belt/dagger.prefab");
                
            }
            
            AddTokens();
            AddPoisonOnHit();
            AddBlightOnHit();
            AddRitualDagger();
        }

        private static void AddPoisonOnHit()
        {
            var PoisonOnHitItemDef = new ItemDef
            {
                name = "PoisonOnHit", // its the internal name, no spaces, apostrophes and stuff like that
                tier = ItemTier.Lunar,
                pickupModelPath = PrefabPath+"Poison.prefab",
                pickupIconPath = IconPath,
                nameToken = "PoisonOnHit_NAME", // stylised name
                pickupToken = "PoisonOnHit_PICKUP",
                descriptionToken = "PoisonOnHit_DESC",
                loreToken = "PoisonOnHit_LORE",
                tags = new[]
                {
                    ItemTag.Utility,
                    ItemTag.Damage
                }
            };

            ItemDisplayRule[] itemDisplayRules = null; // keep this null if you don't want the item to show up on the survivor 3d model. You can also have multiple rules !

            var PoisonOnHit = new R2API.CustomItem(PoisonOnHitItemDef, itemDisplayRules);

            PoisonOnHitItemIndex = ItemAPI.Add(PoisonOnHit); // ItemAPI sends back the ItemIndex of your item
        }  
        
        private static void AddBlightOnHit()
        {
            var BlightOnHitItemDef = new ItemDef
            {
                name = "BlightOnHit", // its the internal name, no spaces, apostrophes and stuff like that
                tier = ItemTier.Tier2,
                pickupModelPath = PrefabPath+"Blight.prefab",
                pickupIconPath = IconPath,
                nameToken = "BlightOnHit_NAME", // stylised name
                pickupToken = "BlightOnHit_PICKUP",
                descriptionToken = "BlightOnHit_DESC",
                loreToken = "BlightOnHit_LORE",
                tags = new[]
                {
                    ItemTag.Utility,
                    ItemTag.Damage
                }
            };

            ItemDisplayRule[] itemDisplayRules = null; // keep this null if you don't want the item to show up on the survivor 3d model. You can also have multiple rules !

            var BlightOnHit
              = new R2API.CustomItem(BlightOnHitItemDef, itemDisplayRules);

            BlightOnHitItemIndex = ItemAPI.Add(BlightOnHit); // ItemAPI sends back the ItemIndex of your item
        }
        
        private static void AddRitualDagger()
        {
            var RitualDaggerItemDef = new ItemDef
            {
                name = "RitualDagger", // its the internal name, no spaces, apostrophes and stuff like that
                tier = ItemTier.Tier1,
                pickupModelPath = PrefabPath+"dagger.prefab",
                pickupIconPath = IconPath,
                nameToken = "RitualDagger_NAME", // stylised name
                pickupToken = "RitualDagger_PICKUP",
                descriptionToken = "RitualDagger_DESC",
                loreToken = "RitualDagger_LORE",
                tags = new[]
                {
                    ItemTag.Utility,
                    ItemTag.Damage
                }
            };

            ItemDisplayRule[] itemDisplayRules = null; // keep this null if you don't want the item to show up on the survivor 3d model. You can also have multiple rules !

            var RitualDagger = new R2API.CustomItem(RitualDaggerItemDef, itemDisplayRules);

            RitualDaggerItemIndex = ItemAPI.Add(RitualDagger); // ItemAPI sends back the ItemIndex of your item
        }

        private static void AddTokens()
        {
            R2API.AssetPlus.Languages.AddToken("PoisonOnHit_NAME", "Deadly Poison");
            R2API.AssetPlus.Languages.AddToken("PoisonOnHit_PICKUP", "Acrid lent me this.");
            R2API.AssetPlus.Languages.AddToken("PoisonOnHit_DESC",
                "Grants <style=cDeath>RAMPAGE</style> on kill. \n<style=cDeath>RAMPAGE</style> : Specifics rewards for reaching kill streaks. \nIncreases <style=cIsUtility>movement speed</style> by <style=cIsUtility>1%</style> <style=cIsDamage>(+1% per item stack)</style> <style=cStack>(+1% every 20 Rampage Stacks)</style>. \nIncreases <style=cIsUtility>damage</style> by <style=cIsUtility>2%</style> <style=cIsDamage>(+2% per item stack)</style> <style=cStack>(+2% every 20 Rampage Stacks)</style>.");
            R2API.AssetPlus.Languages.AddToken("PoisonOnHit_LORE",
                "You were always there, by my side, whether we sat or played. Our friendship was a joyful ride, I wish you could have stayed.");
            
            R2API.AssetPlus.Languages.AddToken("BlightOnHit_NAME", "Deadly Plague");
            R2API.AssetPlus.Languages.AddToken("BlightOnHit_PICKUP", "The plague - but on bullets!");
            R2API.AssetPlus.Languages.AddToken("BlightOnHit_DESC",
                "Grants <style=cDeath>RAMPAGE</style> on kill. \n<style=cDeath>RAMPAGE</style> : Specifics rewards for reaching kill streaks. \nIncreases <style=cIsUtility>movement speed</style> by <style=cIsUtility>1%</style> <style=cIsDamage>(+1% per item stack)</style> <style=cStack>(+1% every 20 Rampage Stacks)</style>. \nIncreases <style=cIsUtility>damage</style> by <style=cIsUtility>2%</style> <style=cIsDamage>(+2% per item stack)</style> <style=cStack>(+2% every 20 Rampage Stacks)</style>.");
            R2API.AssetPlus.Languages.AddToken("BlightOnHit_LORE",
                "You were always there, by my side, whether we sat or played. Our friendship was a joyful ride, I wish you could have stayed.");
            
            R2API.AssetPlus.Languages.AddToken("RitualDagger_NAME", "Ritual Dagger");
            R2API.AssetPlus.Languages.AddToken("RitualDagger_PICKUP", "Two stones from one bird");
            R2API.AssetPlus.Languages.AddToken("RitualDagger_DESC",
                "Grants <style=cDeath>RAMPAGE</style> on kill. \n<style=cDeath>RAMPAGE</style> : Specifics rewards for reaching kill streaks. \nIncreases <style=cIsUtility>movement speed</style> by <style=cIsUtility>1%</style> <style=cIsDamage>(+1% per item stack)</style> <style=cStack>(+1% every 20 Rampage Stacks)</style>. \nIncreases <style=cIsUtility>damage</style> by <style=cIsUtility>2%</style> <style=cIsDamage>(+2% per item stack)</style> <style=cStack>(+2% every 20 Rampage Stacks)</style>.");
            R2API.AssetPlus.Languages.AddToken("RitualDagger_LORE",
                "You were always there, by my side, whether we sat or played. Our friendship was a joyful ride, I wish you could have stayed.");
        }
    }
}
