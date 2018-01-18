using Aimtec;
using Aimtec.SDK.Extensions;
using System.Linq;

namespace ReKatarina.Extensions
{
    internal static class Unit
    {
        // All jungle monsters
        private static readonly string[] Monsters =
        {
            "TT_Spiderboss", "TTNGolem", "TTNWolf", "TTNWraith",
            "SRU_Blue", "SRU_Gromp", "SRU_Murkwolf", "SRU_Razorbeak",
            "SRU_Red", "SRU_Krug", "Sru_Crab", "SRU_Baron", "SRU_RiftHerald",
            "SRU_Dragon_Air", "SRU_Dragon_Water", "SRU_Dragon_Fire", "SRU_Dragon_Elder", "SRU_Dragon_Earth"
        };

        public static bool IsMonster(this Obj_AI_Base target) => Monsters.Contains(target.UnitSkinName.Replace("Mini", ""));
        public static bool IsPlant(this Obj_AI_Base target) => target.UnitSkinName.Contains("Plant");

        public static bool IsWallBetweenPlayer(this Obj_AI_Base target, Vector3 position)
        {
            for (float i = 0; i < target.Position.Distance(position); i += 1)
            {
                if (target.Position.Extend(position, i).IsWall())
                {
                    return true;
                }
            }

            return false;
        }


    }
}
