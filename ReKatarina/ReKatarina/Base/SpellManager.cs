using Aimtec;
using ReKatarina.Extensions;
using System.Collections.Generic;
using System.Drawing;
using Spell = Aimtec.SDK.Spell;

namespace ReKatarina.Base
{
    internal static class SpellManager
    {
        // Spells instances
        public static Spell Q { get; private set; }
        public static Spell W { get; private set; }
        public static Spell E { get; private set; }
        public static Spell R { get; private set; }

        // List with all spells
        public static List<Spell> AllSpells;

        // Const dictionary with color definitions
        private static readonly Dictionary<SpellSlot, Color> AllColors = new Dictionary<SpellSlot, Color>()
        {
            [SpellSlot.Q] = Color.LimeGreen.SetTransparency(150),
            [SpellSlot.W] = Color.CornflowerBlue.SetTransparency(150),
            [SpellSlot.E] = Color.YellowGreen.SetTransparency(150),
            [SpellSlot.R] = Color.OrangeRed.SetTransparency(150),
        };

        public static void Initialize()
        {
            // Create new objects of all spells
            Q = new Spell(SpellSlot.Q, 625);
            W = new Spell(SpellSlot.W, 325);
            E = new Spell(SpellSlot.E, 725);
            R = new Spell(SpellSlot.R, 550);

            // Create ref to spells objects
            AllSpells = new List<Spell>(new[] { Q, W, E, R });

            Logger.Print("SpellManager initialized");
        }

        // Extend `Spell` class
        public static Color GetColor(this Spell s)
        {
            return AllColors.ContainsKey(s.Slot) ? AllColors[s.Slot] : Color.Wheat.SetTransparency(150);
        }
    }
}
