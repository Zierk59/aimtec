#region License

// @author: Kreiz59
// @created: 23-12-2017 16:41
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2018 by Kreiz59

#endregion

using System;
using Aimtec;
using Aimtec.SDK.Events;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using ReKatarina.Base;
using ReKatarina.Core;
using Version = ReKatarina.Base.Other.Version;

namespace ReKatarina
{
    internal static class ReKatarina
    {
        public static IOrbwalker Orbwalker;
        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();
        public static bool HasRBuff => Player != null && Player.HasBuff("katarinarsound");

        public static void Main()
        {
            GameEvents.GameStart += GameEventsOnGameStart;
        }

        private static void GameEventsOnGameStart()
        {
            try
            {
                if (Player.ChampionName != "Katarina") return;

                Version.Check();
                Controller.Initialize();
                SpellManager.Initialize();
                // Base.Activator.Loader.Initialize(); <-- Currently disabled 

                #region Trigger events

                Game.OnUpdate += Events.Game_OnUpdate;
                Render.OnPresent += Events.Render_OnPresent;

                SpellBook.OnCastSpell += Events.SpellBook_OnCastSpell;
                BuffManager.OnRemoveBuff += Events.BuffManager_OnRemoveBuff;

                Orbwalker.PreMove += Events.Orbwalker_PreMove;
                Orbwalker.OnNonKillableMinion += Events.Orbwalker_OnNonKillableMinion;

                #endregion

                Logger.Print("ReKatarina loaded!");
            }
            catch (Exception e)
            {
                Logger.Print(e.Message, Logger.LogType.Error);
            }
        }
    }
}