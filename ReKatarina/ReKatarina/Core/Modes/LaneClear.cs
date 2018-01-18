using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util.Cache;
using ReKatarina.Base;
using ReKatarina.Extensions;
using System.Linq;
using ReKatarina.Base.Interfaces;

namespace ReKatarina.Core.Modes
{
    internal class LaneClear : IMode
    {
        public void Draw()
        {
            
        }

        public void Execute()
        {
            var m = GameObjects.EnemyMinions.Where(e => e.IsValid && !e.IsPlant() && !e.IsDead && e.IsMinion && e.IsInRange(ReKatarina.Player, SpellManager.Q.Range)).OrderBy(h => h.Health);

            if (m.Any())
            {
                // Check spell: Q
                if (SpellManager.Q.Ready && Config.Farm.Menu.GetMenuBool("Farm.Q.Enabled"))
                {
                    // Check minions count
                    if (m.Count() >= Config.Farm.Menu.GetMenuSlider("Farm.Q.HitCount"))
                    {
                        SpellManager.Q.CastOnUnit(m.Last());
                    }
                }

                // Check spell: W
                if (SpellManager.W.Ready && Config.Farm.Menu.GetMenuBool("Farm.W.Enabled"))
                {
                    // Check minions count
                    if (m.Count(e => e.IsInRange(ReKatarina.Player, SpellManager.W.Range / 1.5f)) >= 3)
                    {
                        SpellManager.W.Cast();
                    }
                }

                // Check spell: E
                if (SpellManager.E.Ready && Config.Farm.Menu.GetMenuBool("Farm.E.Enabled"))
                {
                    // Old logic, REMOVE it later
                    // Vector3 d = Base.Other.Dagger.GetClosestDagger();
                    //
                    // if (d.IsInRange(ReKatarina.Player, SpellManager.E.Range))
                    // {
                    //     if (!d.IsUnderEnemyTurret() && d.CountEnemyHeroesInRange(SpellManager.E.Range) <= 1 && ReKatarina.Player.HealthPercent() >= 50)
                    //     {
                    //         Vector3 c = Base.Other.Dagger.GetBestJumpPoint(d, m.Last());
                    //
                    //         if (c != null && c.CountEnemyMinionsInRange(SpellManager.W.Range) >= 1)
                    //         {
                    //             SpellManager.E.Cast(c);
                    //         }
                    //      }
                    // }

                    // New logic, check all daggers for better performance
                    foreach (GameObject d in Base.Other.Dagger.AllDagers)
                    {
                        if (d.Position.IsInRange(ReKatarina.Player, SpellManager.E.Range))
                        {
                            if (d.Position.CountEnemyMinionsInRange(SpellManager.W.Range * 1.25f) >= 1 && !d.IsUnderEnemyTurret() && Base.Other.Dagger.IsPositionSafe(d.Position))
                            {
                                // Check player is not in `this` dagger
                                if (ReKatarina.Player.Position.Distance(d) < SpellManager.W.Range)
                                {
                                    continue;
                                }

                                // Cast E to `this` dagger, get the best point by searching nearest to dagger minion
                                SpellManager.E.Cast(Base.Other.Dagger.GetBestJumpPoint(d.Position, m.OrderBy(e => e.Distance(d)).First()));
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void Initialize()
        {
            
        }

        public bool ShouldGetExecuted()
        {
            return ReKatarina.Orbwalker.Mode == OrbwalkingMode.Laneclear;
        }
    }
}
