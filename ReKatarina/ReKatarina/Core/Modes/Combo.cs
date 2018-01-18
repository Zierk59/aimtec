using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.TargetSelector;
using Aimtec.SDK.Util;
using ReKatarina.Base;
using ReKatarina.Extensions;
using System.Linq;
using ReKatarina.Base.Interfaces;

namespace ReKatarina.Core.Modes
{
    internal class Combo : IMode
    {
        public void Draw()
        {
            
        }

        public void Execute()
        {
            // Select target
            Obj_AI_Hero target = TargetSelector.Implementation.GetTarget(SpellManager.E.Range);

            // Check target is valid
            if (target != null && target.IsValid)
            {
                // Spell `Q` logic
                if (!ReKatarina.HasRBuff && SpellManager.Q.Ready && Config.Combo.Menu.GetMenuBool("Combo.Q.Enabled"))
                {
                    if (target.IsInRange(ReKatarina.Player, SpellManager.Q.Range))
                    {
                        SpellManager.Q.CastOnUnit(target);
                    }
                }

                // Spell `W` logic
                if (!ReKatarina.HasRBuff && SpellManager.W.Ready && Config.Combo.Menu.GetMenuBool("Combo.W.Enabled"))
                {
                    if (target.IsInRange(ReKatarina.Player, SpellManager.W.Range))
                    {
                        SpellManager.W.Cast();
                    }
                }

                // Spell `E` logic
                if (SpellManager.E.Ready && Config.Combo.Menu.GetMenuBool("Combo.E.Enabled"))
                {
                    if (target.IsInRange(ReKatarina.Player, SpellManager.E.Range))
                    {
                        // Get all daggers
                        GameObject[] daggers = Base.Other.Dagger.AllDagers.Count > 0 ? Base.Other.Dagger.AllDagers.ToArray() : new GameObject[] { };

                        if (ReKatarina.HasRBuff)
                        {
                            // First scenario: daggers damage can be greather than R damage. It should cancel R and jump into best daggers.
                            // Second scenario: target would like leave R range. It should cancel R and jump to target or closes dagger (or will hit).

                            // Check daggers damage is greater than R damage
                            float m = ReKatarina.Player.HasBuff("katarinasound") ? ((ReKatarina.Player.GetBuff("katarinasound").EndTime - Game.ClockTime) / 2.5f) : 1;
                            float d = DamageManager.GetDamage(target, false, SpellSlot.R) * m;

                            if (d < DamageManager.GetPassiveDamage(target) && daggers.Any())
                            {
                                // Select best dagger
                                GameObject best = daggers.OrderBy(e => e.Distance(target.Position)).First(); // <-- It's closest dagger, default `best` choice

                                foreach (GameObject o in daggers.Where(e => e.Position.IsInRange(target, SpellManager.W.Range)))
                                {
                                    if (daggers.Any(e => e != o && e.Distance(o) <= 100f && e.Position.IsInRange(target, SpellManager.W.Range * 1.5f)))
                                    {
                                        best = o;
                                        break;
                                    }
                                }

                                // Cast `E` to best dagger
                                Stuff.UnfreezePlayer();
                                SpellManager.E.Cast(best.Position);

                                Logger.Print("Used logic 1", Logger.LogType.Warning);
                            }

                            // Our target wanna leave death lotus. Jump to him!
                            if (target.Distance(ReKatarina.Player) >= 0.8f * SpellManager.R.Range)
                            {
                                // We should check chances to kill right now, it's risky so I added this check
                                if (ReKatarina.Player.HealthPercent() >= 10 || target.Health + target.AllShield < DamageManager.GetDamage(target, true, SpellSlot.Q, SpellSlot.E))
                                {
                                    Vector3 best = target.Position;

                                    // Search for dagger that will hit target
                                    foreach (GameObject i in daggers)
                                    {
                                        if (i.Position.IsInRange(ReKatarina.Player, SpellManager.W.Range))
                                        {
                                            best = i.Position;
                                            break;
                                        }
                                    }

                                    // Cast `E` to best searched position
                                    Stuff.UnfreezePlayer();
                                    SpellManager.E.Cast(best);

                                    Logger.Print("Used logic 2", Logger.LogType.Warning);
                                }
                            }
                        }
                        else
                        {
                            // Start from farthest hitting dagger
                            var best = daggers.Where(e => e.Position.IsInRange(target, SpellManager.W.Range * 1.25f)).OrderBy(e => e.Distance(target.Position));

                            // ReSharper disable once PossibleMultipleEnumeration
                            if (best.Any())
                            {
                                // Cast `E` to best dagger and extend position to target
                                // ReSharper disable once PossibleMultipleEnumeration
                                SpellManager.E.Cast(best.Last().Position.Extend(target.Position, SpellManager.W.Range * 0.25f));
                            }
                        }
                    }
                }

                // Spell `R` logic
                if (!ReKatarina.HasRBuff && SpellManager.R.Ready && Config.Combo.Menu.GetMenuBool("Combo.R.Enabled"))
                {
                    // I decided to add this line. Sometimes it can be useless but overall it should improve R usage.
                    if (target.Health + target.AllShield < target.MaxHealth * 0.1f || (SpellManager.E.Ready && DamageManager.GetPassiveDamage(target) > target.Health + target.AllShield))
                    {
                        return;
                    }

                    // Prevent R with Q or W ready
                    if ((SpellManager.Q.Ready && Config.Combo.Menu.GetMenuBool("Combo.Q.Enabled")) || (SpellManager.W.Ready && Config.Combo.Menu.GetMenuBool("Combo.W.Enabled")))
                    {
                        return;
                    }

                    // Check there is required enemies count
                    if (ReKatarina.Player.CountEnemyHeroesInRange(SpellManager.R.Range) >= Config.Combo.Menu.GetMenuSlider("Combo.R.Enemies"))
                    {
                        // Just respect user settings and add special check here
                        if (ReKatarina.Player.CountEnemyHeroesInRange(Config.Combo.Menu.GetMenuSlider("Combo.R.Range")) >= 1)
                        {
                            Stuff.FreezePlayer();
                            SpellManager.R.Cast();

                            DelayAction.Queue(2500, Stuff.UnfreezePlayer);
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
            return ReKatarina.Orbwalker.Mode == OrbwalkingMode.Combo;
        }
    }
}
