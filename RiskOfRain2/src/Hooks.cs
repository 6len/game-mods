using JetBrains.Annotations;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace GNCItemSuite
{
    public class Hooks
    {
        internal static void Init()
        {
            On.RoR2.GlobalEventManager.OnHitEnemy += PoisonOnHit_OnHit;
            On.RoR2.GlobalEventManager.OnCharacterDeath += RitualDagger_OnKill;

        }


        private static void PoisonOnHit_OnHit(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager gem,
            DamageInfo damageInfo, [NotNull] GameObject victim)
        {
            try
            {
                if (damageInfo.procCoefficient == 0f || damageInfo.rejected)
                {
                    return;
                }
                if (!NetworkServer.active)
                {
                    return;
                }
                CharacterBody component = damageInfo.attacker.GetComponent<CharacterBody>();
                CharacterBody characterBody = victim ? victim.GetComponent<CharacterBody>() : null;
                if (component)
                {
                    CharacterMaster master = component.master;
                    if (master)
                    {
                        Inventory inventory = master.inventory;
                        TeamComponent component2 = component.GetComponent<TeamComponent>();
                        TeamIndex teamIndex = component2 ? component2.teamIndex : TeamIndex.Neutral;
                        Vector3 aimOrigin = component.aimOrigin;
                        int itemCount2 = inventory.GetItemCount(GNCItemSuite.PoisonOnHitItemIndex);
                        int itemCount3 = inventory.GetItemCount(GNCItemSuite.BlightOnHitItemIndex);

                        
                        if ((itemCount2 > 0) && (Util.CheckRoll(5f * (float)itemCount2 * damageInfo.procCoefficient, master)))
                        {
                            ProcChainMask procChainMask2 = damageInfo.procChainMask;
                            procChainMask2.AddProc(ProcType.BleedOnHit);
                            DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Poison, 10f * damageInfo.procCoefficient, 1f);
                            
                        }
                        
                        if ((itemCount3 > 0) && (Util.CheckRoll(10f * (float)itemCount3 * damageInfo.procCoefficient, master)))
                        {
                            ProcChainMask procChainMask2 = damageInfo.procChainMask;
                            procChainMask2.AddProc(ProcType.BleedOnHit);
                            DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Blight, 10f * damageInfo.procCoefficient, 1f);
                            
                        }
                       
                    }
                }
            }
            catch
            {
            }

            orig(gem, damageInfo, victim);
        }
        
        private static void RitualDagger_OnKill(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager gem,
            DamageReport damageReport)
        {
            try
            {
                if (damageReport.damageInfo.attacker)
                {
                    Inventory Inv = damageReport.damageInfo.attacker.GetComponent<CharacterBody>().inventory;
                    int RitualDaggerCount = Inv.GetItemCount(GNCItemSuite.RitualDaggerItemIndex);
                    if (RitualDaggerCount > 0)
                    {
                        CharacterBody component = damageReport.damageInfo.attacker.GetComponent<CharacterBody>();
                        TeamComponent component2 = component.GetComponent<TeamComponent>();
                        TeamIndex teamIndex = component2 ? component2.teamIndex : TeamIndex.Neutral;
                        
                        if (component)
                        {
                            CharacterMaster master = component.master;
                            if (master)
                            {
                                if ((RitualDaggerCount + GNCItemSuiteConfig.RitualDaggerProcChance.Value) >=
                                    UnityEngine.Random.Range(0, 100))
                                {
                                    ProcMissile(RitualDaggerCount, component, master, teamIndex,
                                        damageReport.damageInfo.procChainMask, null, damageReport.damageInfo);
                                    for (var i = 0; i < RitualDaggerCount; i+=2)
                                    {
                                        ProcMissile(RitualDaggerCount, component, master, teamIndex,
                                            damageReport.damageInfo.procChainMask, null, damageReport.damageInfo);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            orig(gem, damageReport);
        }

        private static void ProcMissile(int stack, CharacterBody attackerBody, CharacterMaster attackerMaster,
            TeamIndex attackerTeamIndex, ProcChainMask procChainMask, GameObject victim, DamageInfo damageInfo)
        {
            if (stack > 0)
            {
                GameObject gameObject = attackerBody.gameObject;
                InputBankTest component = gameObject.GetComponent<InputBankTest>();
                Vector3 position = component ? component.aimOrigin : gameObject.transform.position;
                Vector3 vector = component ? component.aimDirection : gameObject.transform.forward;
                Vector3 up = Vector3.up;
                var projectile = GlobalEventManager.instance.daggerPrefab;
        
                if (Util.CheckRoll(10f * GNCItemSuiteConfig.RitualDaggerProcChance.Value, attackerMaster))
                {
                    float damageCoefficient = 3.5f * (float) stack;
                    float damage = Util.OnKillProcDamage(attackerBody.damage, damageCoefficient);
                    ProcChainMask procChainMask2 = procChainMask;
                    procChainMask2.AddProc(ProcType.Missile);
                    FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    {
                        projectilePrefab = projectile,
                        position = position,
                        rotation = Util.QuaternionSafeLookRotation(Vector3.up + Random.insideUnitSphere * 0.1f),
                        procChainMask = procChainMask2,
                        target = victim,
                        owner = gameObject,
                        damage = damage,
                        crit = damageInfo.crit,
                        force = 200f,
                        damageColorIndex = DamageColorIndex.Item
                    };
                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }
            }
        }
    }
    
}
