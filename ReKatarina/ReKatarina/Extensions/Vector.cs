using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Util.Cache;
using System.Linq;

namespace ReKatarina.Extensions
{
    internal static class Vector
    {
        public static bool IsInRange(this Vector3 Base, Vector3 target, float radius) => Base.Distance(target) < radius;
        public static bool IsInRange(this Vector3 Base, Obj_AI_Base target, float radius) => IsInRange(Base, target.Position, radius);
        public static bool IsInRange(this Obj_AI_Base Base, Obj_AI_Base target, float radius) => IsInRange(Base.Position, target.Position, radius);

        public static int CountEnemyHeroesInRange(this Vector3 Base, float radius) => GameObjects.EnemyHeroes.Count(e => !e.IsDead && e.Position.IsInRange(Base, radius));
        public static int CountAllyHeroesInRange(this Vector3 Base, float radius) => GameObjects.AllyHeroes.Count(e => !e.IsDead && e.Position.IsInRange(Base, radius));

        public static int CountEnemyMinionsInRange(this Vector3 Base, float radius) => GameObjects.EnemyMinions.Count(e => e.IsValid && !e.IsDead && !e.IsPlant() && e.Position.IsInRange(Base, radius));
        public static int CountAllyMinionsInRange(this Vector3 Base, float radius) => GameObjects.AllyMinions.Count(e => e.IsValid && !e.IsDead && !e.IsPlant() && e.Position.IsInRange(Base, radius));

        public static bool IsWall(this Vector3 Base) => NavMesh.WorldToCell(Base).Flags.HasFlag(NavCellFlags.Wall) || NavMesh.WorldToCell(Base).Flags.HasFlag(NavCellFlags.Building);
    }
}
