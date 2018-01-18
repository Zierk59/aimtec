using Aimtec;
using Aimtec.SDK.Damage;
using ReKatarina.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ReKatarina.Base
{
    internal static class DamageManager
    {
        private static readonly Dictionary<int, int> Multiplers = new Dictionary<int, int>() { { 1, 55 }, { 6, 70 }, { 11, 85 }, { 16, 100 } };

        /// <summary>
        /// Calculates damage from catching daggers
        /// </summary>
        public static float GetPassiveDamage(Obj_AI_Base target)
        {
            float m = .55f;

            // Calculate multipler
            foreach (var a in Multiplers)
            {
                if (ReKatarina.Player.Level <= a.Key)
                {
                    m = (a.Value / 100f);
                }
            }

            // One line XD
            int s = (int) ReKatarina.Player.CalculateDamage(target, DamageType.Magical, (new[] { 0, 75, 80, 87, 94, 102, 111, 120, 131, 143, 155, 168, 183, 198, 214, 231, 248, 267, 287 }[ReKatarina.Player.Level] + (m * ReKatarina.Player.TotalAbilityDamage) + (ReKatarina.Player.TotalAttackDamage - ReKatarina.Player.BaseAttackDamage)));

            // Get total damage
            int t = 0;

            foreach (var d in Other.Dagger.AllDagers)
            {
                if (target.Position.IsInRange(d.Position, SpellManager.W.Range + 75))
                {
                    t += s; // <-- Total damage += single dagger damage
                }
            }

            return t;
        }

        /// <summary>
        /// Get damage with any spell sequence
        /// </summary>
        public static int GetDamage(Obj_AI_Base target, bool readyCheck, params SpellSlot[] spells)
        {
            int t = 0;

            foreach (SpellSlot spell in spells)
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (spell)
                {
                    case SpellSlot.Q:
                    case SpellSlot.E:
                    case SpellSlot.R:
                        {
                            if (!readyCheck || (SpellManager.AllSpells.First(e => e.Slot == spell).Ready))
                            {
                                t += (int)ReKatarina.Player.GetSpellDamage(target, spell);
                            }

                            break;
                        }
                }
            }

            return t;
        }
    }
}
