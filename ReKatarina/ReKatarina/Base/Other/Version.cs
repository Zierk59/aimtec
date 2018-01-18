using Aimtec.SDK.Util;
using System;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReKatarina.Base.Other
{
    internal static class Version
    {
        public static System.Version AssVersion => Assembly.GetExecutingAssembly().GetName().Version;
        public static string Status { get; private set; }

        public static void Check()
        {
            Status = "Checking version...";

            Task.Factory.StartNew(() =>
            {
                try
                {
                    string champion = ReKatarina.Player.ChampionName;
                    var result = new Regex(@"\[assembly\: AssemblyVersion\(""(\d+)\.(\d+)\.(\d+).(\d+)""\)\]").Match(new WebClient().DownloadString("https://raw.githubusercontent.com/Zierk59/AimTec/master/Re" + champion + "/Re" + champion + "/Properties/AssemblyInfo.cs"));

                    if (result.Success)
                    {
                        var version = new System.Version($"{result.Groups[1]}.{result.Groups[2]}.{result.Groups[3]}.{result.Groups[4]}");

                        Status = version > AssVersion ? $"New version of Re{champion} is available! Please update." : $"Thank you for using newest version of Re{champion}!";
                    }

                    DelayAction.Queue(45 * 1000, () =>
                    {
                        Status = "";
                    });
                }
                catch (Exception e)
                {
                    Status = e.Message;
                    Logger.Print(e.Message, Logger.LogType.Error);

                    DelayAction.Queue(45 * 1000, () =>
                    {
                        Status = "";
                    });
                }
            });
        }
    }
}
