using System;

namespace ReKatarina.Base
{
    internal static class Stuff
    {
        private static readonly Random GetRandom = new Random();
        public static bool ForcedRCancel;

        public static bool GetChance(int chance)
        {
            return (GetRandom.Next(0, 100) >= chance);
        }

        public static void FreezePlayer()
        {
            ForcedRCancel = false;

            ReKatarina.Orbwalker.AttackingEnabled = false;
            ReKatarina.Orbwalker.MovingEnabled = false;
        }

        public static void UnfreezePlayer()
        {
            ForcedRCancel = true;

            ReKatarina.Orbwalker.AttackingEnabled = true;
            ReKatarina.Orbwalker.MovingEnabled = true;
        }
    }
}
