using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util.Cache;
using ReKatarina.Base;
using ReKatarina.Extensions;
using System.Linq;
using ReKatarina.Base.Interfaces;

namespace ReKatarina.Core.Modes
{
    internal class LastHit : IMode
    {
        public void Draw()
        {
            
        }

        public void Execute()
        {
            if (SpellManager.Q.Ready && Config.Farm.Menu.GetMenuBool("Farm.Q.LastHit"))
            {
                foreach (Obj_AI_Minion m in GameObjects.EnemyMinions.Where(e => e.IsValid && !e.IsPlant() &&!e.IsDead && e.IsMinion && e.IsInRange(ReKatarina.Player, SpellManager.Q.Range)).OrderBy(h => h.Health))
                {
                    if (m.IsValidTarget(SpellManager.Q.Range) && (m.AllShield + m.Health + 3) <= DamageManager.GetDamage(m, true, SpellSlot.Q) && !m.IsInRange(ReKatarina.Player.AttackRange * 1.5f))
                    {
                        SpellManager.Q.CastOnUnit(m);
                        return;
                    }
                }
            }
        }

        public void Initialize()
        {
            
        }

        public bool ShouldGetExecuted()
        {
            return ReKatarina.Orbwalker.Mode == OrbwalkingMode.Lasthit;
        }
    }
}
