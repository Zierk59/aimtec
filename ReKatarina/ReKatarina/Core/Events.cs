using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using ReKatarina.Base;
using ReKatarina.Extensions;
using Spell = Aimtec.SDK.Spell;

namespace ReKatarina.Core
{
    internal static class Events
    {
        internal static void Game_OnUpdate()
        {
            // Don't exec if player is dead
            if (ReKatarina.Player.IsDead || ReKatarina.Player.IsRecalling())
            {
                return;
            }

            // Call main `Update` method
            Controller.Update();
        }

        internal static void Render_OnPresent()
        {
            // Don't exec if player is dead
            if (ReKatarina.Player.IsDead)
            {
                return;
            }

            // Call main `Draw` method
            Controller.Draw();

            #region Draw spells
            foreach (Spell s in SpellManager.AllSpells)
            {
                if (Config.Drawing.Menu.GetMenuBool($"Drawing.{s.Slot.ToString()}.Enabled"))
                {
                    // Render circle
                    Render.Circle(ReKatarina.Player.Position, s.Range, 90, s.GetColor());
                }
            }
            #endregion
            #region Draw daggers
            if (Config.Drawing.Menu.GetMenuBool("Drawing.Daggers"))
            {
                foreach (var d in Base.Other.Dagger.AllDagers)
                {
                    Render.Circle(d.Position, 150, 90, Base.Other.Dagger.GetDaggerColor(d));
                }
            }
            #endregion
        }

        internal static void BuffManager_OnRemoveBuff(Obj_AI_Base sender, Buff buff)
        {
            if (sender.IsMe)
            {
                if (buff.Name.Contains("katarinarsound"))
                {
                    Stuff.UnfreezePlayer();
                    Logger.Print("Buff R removed");
                    Stuff.ForcedRCancel = false;
                }
            }
        }

        internal static void Orbwalker_OnNonKillableMinion(object sender, NonKillableMinionEventArgs e)
        {
            if (ReKatarina.Orbwalker.Mode.HasFlag(OrbwalkingMode.Lasthit) && SpellManager.Q.Ready)
            {
                // Check spell is ready and option is enabled
                if (e.Target.Position.IsInRange(ReKatarina.Player, SpellManager.Q.Range) && Config.Farm.Menu.GetMenuBool("Farm.Q.Unkillable"))
                {
                    SpellManager.Q.Cast((Obj_AI_Base) e.Target);
                }
            }
        }

        /// <summary>
        ///     No more `R cancel`
        /// </summary>
        internal static void Orbwalker_PreMove(object sender, PreMoveEventArgs e)
        {
            if (ReKatarina.HasRBuff && !Stuff.ForcedRCancel)
            {
                Logger.Print($"PreMove prevent! {Stuff.ForcedRCancel}");
                Stuff.ForcedRCancel = false;

                e.Cancel = true;
                return;
            }

            Logger.Print("PreMove success");
        }

        /// <summary>
        ///     Done to prevent spell spam
        /// </summary>
        internal static void SpellBook_OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs e)
        {
            if (sender.IsMe)
            {
                if (e.Slot == SpellSlot.R)
                {
                    Stuff.FreezePlayer();
                    return;
                }

                if (!ShouldSpellGetCasted())
                {
                    e.Process = false;
                }
            }
        }

        private static bool ShouldSpellGetCasted()
        {
            return !ReKatarina.HasRBuff || Stuff.ForcedRCancel;

            // Temponary disabled
            // if (LastCast.ContainsKey(s))
            // {
            //     int t = LastCast[s];
            //     LastCast[s] = Game.TickCount;
            //
            //     return (Game.TickCount - t > 150);
            // }

            // LastCast.Add(s, Game.TickCount);
        }
    }
}
