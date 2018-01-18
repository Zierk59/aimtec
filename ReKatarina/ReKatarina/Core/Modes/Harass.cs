using Aimtec;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.TargetSelector;
using ReKatarina.Base;
using ReKatarina.Base.Interfaces;
using ReKatarina.Extensions;

namespace ReKatarina.Core.Modes
{
    internal class Harass : IMode
    {
        public void Draw()
        {
            
        }

        public void Execute()
        {
            // I have no idea what more I can do with Harass mode. W + Q + E would be useless.
            if (!SpellManager.Q.Ready || !Config.Harass.Menu.GetMenuBool("Harass.Q.Enabled"))
            {
                return;
            }

            // Get default target
            Obj_AI_Hero target = TargetSelector.Implementation.GetTarget(SpellManager.Q.Range);

            if (target != null && target.IsValid)
            {
                SpellManager.Q.CastOnUnit(target);
            }
        }

        public void Initialize()
        {
            
        }

        public bool ShouldGetExecuted()
        {
            return ReKatarina.Orbwalker.Mode == OrbwalkingMode.Mixed;
        }
    }
}
