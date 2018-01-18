using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Util.Cache;
using ReKatarina.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ReKatarina.Base.Other
{
    internal static class Dagger
    {
        public static List<GameObject> AllDagers => GameObjects.AllGameObjects.Where(e => e.IsOnScreen && e.IsValid && e.Name.Contains("HiddenMinion") && !e.IsDead).ToList();

    /*
        public static Vector3 GetClosestDagger()
        {
            if (AllDagers != null && AllDagers.Count >= 1)
            {
                return AllDagers.OrderByDescending(e => ReKatarina.Player.Position.Distance(e.Position)).First().ServerPosition;
            }

            return Vector3.Zero;
        }
    */

        public static Vector3 GetBestJumpPoint(Vector3 dagger, Obj_AI_Base target)
        {
            return target.Position.IsInRange(dagger, 150) ? dagger : dagger.Extend(target.Position, 150);
        }

        public static Color GetDaggerColor(GameObject dagger)
        {
            // ReSharper disable once InvertIf
            if (dagger.IsValid)
            {
                if (dagger.IsUnderEnemyTurret() || GameObjects.EnemyHeroes.Any(e => e.IsValid && !e.IsDead && !e.IsInvulnerable && e.Position.IsInRange(dagger.Position, SpellManager.W.Range * 1.5f)) || 
                    GameObjects.EnemyMinions.Any(e => e.IsValid && e.Team != GameObjectTeam.Order && !e.IsDead && !e.IsPlant() && (e.IsMinion || e.IsMonster()) && e.Position.IsInRange(dagger.Position, SpellManager.W.Range * 1.5f)))
                {
                    return Color.Green;
                }
            }

            return Color.Red;
        }

        // TODO
        // ReSharper disable once UnusedParameter.Global
        public static bool IsPositionSafe(Vector3 position, float range = 50f)
        {
            // Check is there Caitlyn trap, Veigar trap, Nidalee trap, Teemo mushroom, Shaco box, Jhin trap

            return true;
        }
    }
}
