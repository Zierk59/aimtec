using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.TargetSelector;
using Aimtec.SDK.Util.Cache;
using ReKatarina.Base;
using ReKatarina.Extensions;
using System;
using System.Drawing;
using System.Linq;
using ReKatarina.Base.Interfaces;

namespace ReKatarina.Core.Modes
{
    internal class PermaActive : IMode
    {
        public void Draw()
        {
            #region Top-left bottom text
            string text = Base.Other.Version.Status + Environment.NewLine;

            if (Config.Misc.Menu.GetMenuBool("Misc.Developer"))
            {
                text += "Orbwalker.ModeName : " + ReKatarina.Orbwalker.ModeName + Environment.NewLine;
                text += "Orbwalker.MovingEnabled : " + ReKatarina.Orbwalker.MovingEnabled + Environment.NewLine;
                text += "Orbwalker.AttackingEnabled : " + ReKatarina.Orbwalker.AttackingEnabled + Environment.NewLine;
                text += "AllDagers : " + Base.Other.Dagger.AllDagers.Count + Environment.NewLine;
                text += "HasRBuff : " + ReKatarina.HasRBuff + Environment.NewLine;
                text += "IsWallBetweenPlayer : " + ReKatarina.Player.IsWallBetweenPlayer(Game.CursorPos) + Environment.NewLine;
                text += "Stuff.ForcedRCancel : " + Stuff.ForcedRCancel + Environment.NewLine;
                text += Environment.NewLine;
            }

            Render.Text(text, new Vector2(50, 50), RenderTextFlags.None, Color.Yellow);
            #endregion
        }

        public void Execute()
        {
            #region R Updater
            if (ReKatarina.HasRBuff)
            {
                if (ReKatarina.Player.CountEnemyHeroesInRange(SpellManager.R.Range) <= 0)
                {
                    Stuff.UnfreezePlayer();
                    Stuff.ForcedRCancel = false;

                    return;
                }
                else
                {
                    // Added to prevent R canceling
                    Stuff.FreezePlayer();
                    return;
                }
            }
            #endregion
            #region KillSteal
            foreach (Obj_AI_Hero p in GameObjects.EnemyHeroes.Where(e => !e.IsInvulnerable && !e.IsDead && e.IsInRange(ReKatarina.Player, SpellManager.Q.Range)))
            {
                // Check spell: Q
                if (SpellManager.Q.Ready && Config.Misc.Menu.GetMenuBool("Misc.KillSteal.Q"))
                {
                    // Check damage > health
                    if (p.Health + p.AllShield + 3 < DamageManager.GetDamage(p, true, SpellSlot.Q))
                    {
                        Logger.Print($"Tried to steal {p.ChampionName}");

                        SpellManager.Q.CastOnUnit(p);
                        return;
                    }
                }

                // Check spell: E
                if (SpellManager.E.Ready && Config.Misc.Menu.GetMenuBool("Misc.KillSteal.E"))
                {
                    // Check damage > health
                    if (p.Health + p.AllShield + 3 < DamageManager.GetDamage(p, true, SpellSlot.E) + ReKatarina.Player.GetAutoAttackDamage(p))
                    {
                        Logger.Print($"Tried to steal {p.ChampionName}");

                        SpellManager.E.CastOnUnit(p);
                        return;
                    }
                }
            }
            #endregion
            #region Auto harass [put at the end]
            if (ReKatarina.Orbwalker.Mode.HasFlag(OrbwalkingMode.None) && SpellManager.Q.Ready && Config.Harass.Menu.GetMenuBool("Harass.Q.Auto.Enabled"))
            {
                // Prevent spaming all the time
                if (!Stuff.GetChance(Config.Harass.Menu.GetMenuSlider("Harass.Q.Auto.Chance")))
                {
                    return;
                }

                // Get default target
                Obj_AI_Hero p = TargetSelector.Implementation.GetTarget(SpellManager.Q.Range);

                if (p != null && p.IsValid)
                {
                    // Check whitelist
                    if (!Config.Harass.Menu.GetMenuBool($"Harass.Auto.Whitelist.{p.ChampionName}"))
                    {
                        return;
                    }

                    SpellManager.Q.CastOnUnit(p);
                }
            }
            #endregion
        }

        public void Initialize()
        {
            // Set player skin
            ReKatarina.Player.SetSkinId(Config.Misc.Menu.GetMenuSlider("Misc.Skin.Id"));
        }

        public bool ShouldGetExecuted()
        {
            return true;
        }
    }
}
