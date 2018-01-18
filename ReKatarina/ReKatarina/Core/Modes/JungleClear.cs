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
    internal class JungleClear : IMode
    {
        public void Draw()
        {
            
        }

        public void Execute()
        {
            // var m = GameObjects.EnemyMinions.Where(e => e.IsValid && !e.IsPlant() && !e.IsDead && e.Team == GameObjectTeam.Neutral && e.IsInRange(ReKatarina.Player, SpellManager.Q.Range)).OrderBy(h => h.Health);
            var m = GameObjects.EnemyMinions.Where(e => e.IsValid && !e.IsDead && e.IsMonster() && e.IsInRange(ReKatarina.Player, SpellManager.Q.Range)).OrderBy(h => h.Health);

            // ReSharper disable once PossibleMultipleEnumeration
            if (m.Any())
            {
                // Check spell: Q
                if (SpellManager.Q.Ready && Config.Farm.Menu.GetMenuBool("Farm.Q.Enabled"))
                {
                    // Check minions count
                    if (Config.Farm.Menu.GetMenuBool("Farm.Q.Ignore") || (!Config.Farm.Menu.GetMenuBool("Farm.Q.Ignore") && m.Count() >= Config.Farm.Menu.GetMenuSlider("Farm.Q.HitCount")))
                    {
                        // ReSharper disable once PossibleMultipleEnumeration
                        SpellManager.Q.CastOnUnit(m.Last());
                    }
                }

                // Check spell: W
                if (SpellManager.W.Ready && Config.Farm.Menu.GetMenuBool("Farm.W.Enabled"))
                {
                    // Check minions count
                    // ReSharper disable once PossibleMultipleEnumeration
                    if (m.Count(e => e.IsInRange(ReKatarina.Player, SpellManager.W.Range / 1.5f)) >= 1)
                    {
                        SpellManager.W.Cast();
                    }
                }

                // Check spell: E
                if (SpellManager.E.Ready && Config.Farm.Menu.GetMenuBool("Farm.E.Enabled"))
                {
                    // Old `logic`, REMOVE it later
                    // Vector3 d = Base.Other.Dagger.GetClosestDagger();
                    //
                    // if (d.IsInRange(ReKatarina.Player, SpellManager.E.Range))
                    // {
                    //     Vector3 c = Base.Other.Dagger.GetBestJumpPoint(d, m.Last());
                    //
                    //     if (c != null)
                    //     {
                    //         SpellManager.E.Cast(c);
                    //     }
                    // }

                    // New logic
                    foreach (GameObject d in Base.Other.Dagger.AllDagers.Where(e => !ReKatarina.Player.IsWallBetweenPlayer(e.Position)))
                    {
                        if (d.Position.IsInRange(ReKatarina.Player, SpellManager.E.Range) && Base.Other.Dagger.IsPositionSafe(d.Position))
                        {
                            // Cast E to `this` dagger, get the best point by searching nearest to dagger minion
                            SpellManager.E.Cast(Base.Other.Dagger.GetBestJumpPoint(d.Position, m.OrderBy(e => e.Distance(d)).First()));

                            return;
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
