using System.Drawing;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Util;
using ReKatarina.Core.Config;
using ReKatarina.Extensions;

namespace ReKatarina.Base.Other
{
    internal class JumpPoint
    {
        public Vector3 Start { get; }
        public Vector3 End { get; }

        public JumpPoint(Vector2 start, Vector2 end)
        {
            Start = start.To3D();
            End = end.To3D();
        }

        /// <summary>
        ///     Draw `this` point
        /// </summary>
        public void Draw()
        {
            int d = GetDifficulty();

            // Check difficulty first
            if (d > WallJump.Menu.GetMenuList("Walljump.Difficulty"))
            {
                return;
            }

            // Get spot color
            Color c = d > 1 ? Color.Red : d < 1 ? Color.LawnGreen : Color.Orange;

            // Draw point
            if (ReKatarina.Player.Distance(Start) < ReKatarina.Player.Distance(End))
            {
                Render.Circle(Start, 125f, 90, c);
                return;
            }

            Render.Circle(End, 125f, 90, c);
        }

        /// <summary>
        ///     Jump to `this` point
        /// </summary>
        public void Jump()
        {
            // Check difficulty
            if (GetDifficulty() > WallJump.Menu.GetMenuList("Walljump.Difficulty"))
            {
                return;
            }

            Vector3 p = ReKatarina.Player.Distance(Start) > ReKatarina.Player.Distance(End) ? Start : End;

            // Check is in range
            if (p.IsInRange(ReKatarina.Player, 330f))
            {
                SpellManager.W.Cast();

                // Cast jump with delay
                DelayAction.Queue(250, () => SpellManager.E.Cast(ReKatarina.Player.Position.Extend(p, 180)));
            }
        }

        /// <summary>
        ///     Get jump difficulty based on distance
        /// </summary>
        /// <returns></returns>
        private int GetDifficulty()
        {
            if (Start.Distance(End) <= 250)
            {
                return 0;
            }

            return Start.Distance(End) <= 325 ? 1 : 2;
        }
    }
}
