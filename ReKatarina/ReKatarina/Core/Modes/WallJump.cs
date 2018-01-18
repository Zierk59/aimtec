using System.Collections.Generic;
using System.Linq;
using ReKatarina.Extensions;
using ReKatarina.Base.Other;
using Aimtec;
using Aimtec.SDK.Orbwalking;
using ReKatarina.Base;
using ReKatarina.Base.Interfaces;

namespace ReKatarina.Core.Modes
{
    internal class WallJump : IMode
    {
        private readonly Dictionary<string, OrbwalkingMode> _modes = new Dictionary<string, OrbwalkingMode>()
        {
            ["Walljump.Combo"] = OrbwalkingMode.Combo,
            ["Walljump.Harass"] = OrbwalkingMode.Mixed,
            ["Walljump.Farm"] = OrbwalkingMode.Laneclear,
            ["Walljump.Lasthit"] = OrbwalkingMode.Lasthit
        };

        private List<JumpPoint> _allPoints;

        public void Draw()
        {
            // Check mode is enabled
            if (!IsEnabled())
            {
                return;
            }

            // Draw all valid points
            foreach (JumpPoint p in _allPoints.Where(e => e.Start.IsInRange(ReKatarina.Player, 1000f) || e.End.IsInRange(ReKatarina.Player, 1000f)))
            {
                p.Draw();
            }
        }

        public void Execute()
        {
            if (!SpellManager.W.Ready || !SpellManager.E.Ready)
            {
                return;
            }

            foreach (JumpPoint p in _allPoints.Where(e => e.Start.IsInRange(ReKatarina.Player, 100f) || e.End.IsInRange(ReKatarina.Player, 100f)))
            {
                p.Jump();
                return;
            }
        }

        public void Initialize()
        {
            #region Initialize all points
            _allPoints = new List<JumpPoint>()
            {
                new JumpPoint(new Vector2(6524, 8856), new Vector2(6656, 9030)),
                new JumpPoint(new Vector2(3732, 7425), new Vector2(3722, 7661)),
                new JumpPoint(new Vector2(2171, 9227), new Vector2(2043, 9426)),
                new JumpPoint(new Vector2(2636, 9541), new Vector2(2776, 9574)),
                new JumpPoint(new Vector2(3160, 9938), new Vector2(3342, 10084)),
                new JumpPoint(new Vector2(3293, 9498), new Vector2(3443, 9476)),
                new JumpPoint(new Vector2(2831, 6781), new Vector2(3014, 6742)),
                new JumpPoint(new Vector2(3082, 6085), new Vector2(3223, 6225)),
                new JumpPoint(new Vector2(4199, 6248), new Vector2(4019, 6329)),
                new JumpPoint(new Vector2(5734, 10692), new Vector2(5514, 10605)),
                new JumpPoint(new Vector2(4448, 10383), new Vector2(4309, 10246)),
                new JumpPoint(new Vector2(5047, 9722), new Vector2(5068, 9975)),
                new JumpPoint(new Vector2(4941, 11905), new Vector2(5005, 12141)),
                new JumpPoint(new Vector2(6576, 11728), new Vector2(6547, 11948)),
                new JumpPoint(new Vector2(8116, 9756), new Vector2(8347, 9767)),
                new JumpPoint(new Vector2(7664, 9262), new Vector2(7507, 9025)),
                new JumpPoint(new Vector2(7117, 8676), new Vector2(6941, 8534)),
                new JumpPoint(new Vector2(4632, 5836), new Vector2(4720, 5631)),
                new JumpPoint(new Vector2(7176, 5591), new Vector2(7399, 5844)),
                new JumpPoint(new Vector2(7721, 6098), new Vector2(7942, 6241)),
                new JumpPoint(new Vector2(8243, 5806), new Vector2(8348, 5978)),
                new JumpPoint(new Vector2(7991, 3581), new Vector2(7912, 3772)),
                new JumpPoint(new Vector2(8443, 3220), new Vector2(8490, 2931)),
                new JumpPoint(new Vector2(9730, 2793), new Vector2(9813, 3032)),
                new JumpPoint(new Vector2(10385, 4414), new Vector2(10596, 4447)),
                new JumpPoint(new Vector2(9800, 4934), new Vector2(9802, 5145)),
                new JumpPoint(new Vector2(9077, 4641), new Vector2(9264, 4581)),
                new JumpPoint(new Vector2(11039, 7247), new Vector2(10987, 7467)),
                new JumpPoint(new Vector2(1961, 8520), new Vector2(1683, 8536)),
                new JumpPoint(new Vector2(11306, 7941), new Vector2(11149, 8179)),
                new JumpPoint(new Vector2(11604, 8691), new Vector2(11772, 8895)),
                new JumpPoint(new Vector2(11800, 8123), new Vector2(12041, 8107)),
                new JumpPoint(new Vector2(10678, 8707), new Vector2(10787, 8532)),
                new JumpPoint(new Vector2(10209, 9116), new Vector2(10061, 9269)),
                new JumpPoint(new Vector2(9970, 8519), new Vector2(9860, 8729)),
                new JumpPoint(new Vector2(10705, 6900), new Vector2(10442, 6855)),
                new JumpPoint(new Vector2(4113, 7974), new Vector2(4341, 8134)),
                new JumpPoint(new Vector2(7434, 10717), new Vector2(7695, 10761)),
                new JumpPoint(new Vector2(9904, 12116), new Vector2(10131, 12188)),
                new JumpPoint(new Vector2(12118, 10292), new Vector2(12075, 10067)),
                new JumpPoint(new Vector2(10244, 11716), new Vector2(10064, 11587)),
                new JumpPoint(new Vector2(11644, 10164), new Vector2(11676, 10414)),
                new JumpPoint(new Vector2(12271, 5468), new Vector2(12062, 5418)),
                new JumpPoint(new Vector2(11745, 4995), new Vector2(11540, 4963)),
                new JumpPoint(new Vector2(11555, 5355), new Vector2(11355, 5467)),
                new JumpPoint(new Vector2(12044, 4594), new Vector2(11860, 4454)),
                new JumpPoint(new Vector2(12823, 6168), new Vector2(13097, 6144)),
                new JumpPoint(new Vector2(4768, 2221), new Vector2(4966, 2307)),
                new JumpPoint(new Vector2(3194, 4552), new Vector2(3327, 4748)),
                new JumpPoint(new Vector2(6755, 5100), new Vector2(6485, 5112))
            };
            #endregion
        }

        public bool ShouldGetExecuted()
        {
            return IsEnabled();
        }

        private bool IsEnabled()
        {
            if (Config.WallJump.Menu.GetMenuList("Walljump.Mode") != 1)
            {
                // Check mode is enabled, diff menu type for another modes
                if (Config.WallJump.Menu.GetMenuList("Walljump.Mode") <= 0 ? Config.WallJump.Menu.GetMenuBool("Walljump.Status") : Config.WallJump.Menu.GetMenuKeyBind("Walljump.Status"))
                {
                    // Check orbwalker
                    foreach (var e in _modes)
                    {
                        if (!Config.WallJump.Menu.GetMenuBool(e.Key) && ReKatarina.Orbwalker.Mode == e.Value)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
            else
            {
                return Config.WallJump.Menu.GetMenuKeyBind("Walljump.Status");
            }

            return false;
        }
    }
}
