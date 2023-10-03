using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BLZX
{
    public partial class Form1 : Form
    {
        public static bool Yxlj (string keyPath ,string keyName,out string lj)
        {
            string bzd=string.Empty;
            RegistryKey Key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
            RegistryKey Value = string.IsNullOrEmpty(keyPath) ? Key : Key.OpenSubKey(keyPath);
            if (Value != null)
            {
                bzd = Convert.ToString(Value.GetValue(keyName));
                if (Directory.Exists(bzd + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles")) { lj = bzd ; return true; }
                else if (Directory.Exists(bzd + @"\steamapps\common\SubnauticaZero\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles")) { lj = bzd + @"\steamapps\common\SubnauticaZero"; return true; }
                else if (Directory.Exists(bzd + @"\steamapps\common\Subnautica\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles")) { lj = bzd + @"\steamapps\common\Subnautica"; return true; }
                else { lj = bzd; return false; }
            }
            else { lj = bzd; return false; }
        }

        private void 路径()
        {
            if (Directory.Exists(@"SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles")) Game = Directory.GetCurrentDirectory();
            else if (Yxlj(@"SOFTWARE\BLZX", "InstallLocation", out string Key1)) Game = Key1;
            else if (Yxlj(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 848450", "InstallLocation", out string Key2)) Game = Key2;   
            else if (Yxlj(@"Software\Valve\Steam", "SteamPath", out string Key3)) Game = Key3;
            else
            {
                List<string> strList = new List<string>();
                var allDrives = DriveInfo.GetDrives();
                foreach (var aDrive in allDrives)
                {
                    strList.Add(aDrive.ToString());
                }
                string[] fulist = strList.ToArray();
                for (int i = 0; i < fulist.Count(); i++)
                {
                    if (Directory.Exists(fulist[i] + @"Program Files\Steam\steamapps\common\SubnauticaZero\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                    {
                        Game = fulist[i] + @"Program Files\Steam\steamapps\common\SubnauticaZero";
                        break;
                    }
                    else if (Directory.Exists(fulist[i] + @"Steam\steamapps\common\SubnauticaZero\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                    {
                        Game = fulist[i] + @"Steam\steamapps\common\SubnauticaZero";
                        break;
                    }
                    else if (Directory.Exists(fulist[i] + @"SteamLibrary\steamapps\common\SubnauticaZero\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                    {
                        Game = fulist[i] + @"SteamLibrary\steamapps\common\SubnauticaZero";
                        break;
                    }
                }
            }
            if (Game != null)
            {
                this.BeginInvoke(new Action(() => { label2.Text = Game; }));
                if (File.Exists(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\碧蓝之星零度之下汉化.json")) { this.BeginInvoke(new Action(() => { button2.Text = "更新"; button10.Visible = true; })); }
            }
            else this.BeginInvoke(new Action(() => { label2.Text = "未搜索到路径,请选择游戏路径"; }));
        }
        private void 更新()
        {
            this.BeginInvoke(new Action(() =>
            {
                button9.Text = "正在检查更新";
            }));

            string nm = null;
            WebBrowser webBrowser1 = new WebBrowser();
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://pan.baidu.com/s/1-Um5PKEuStfImBpRs_IFHQ");
            while (webBrowser1.ReadyState < WebBrowserReadyState.Complete) Application.DoEvents();
            HtmlElementCollection ht = webBrowser1.Document.GetElementsByTagName("div");
            foreach (HtmlElement h in ht)
            {
                if (h.GetAttribute("className").Equals("file-name"))
                {
                    nm = h.InnerText;
                    break;
                }
            }
            if (nm != null)
            {
                string s = new Regex(@"(?i)(?<=\[)(.*)(?=\])").Match(nm).Value;
                版本 = "最新版本："+ s ;
                this.BeginInvoke(new Action(() =>

                {
                    button9.Text = 版本;
                }));
            }

            if (版本 != null)
            {
                if (版本 != "最新版本：V2.2")
                {
                    if (MessageBox.Show(版本 + "\n\n点击“确定”打开下载链接。", "检测到更新", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    {
                        this.BeginInvoke(new Action(() =>

                        {
                            fr4.Show();
                        }));
                    }

                }
            }

        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string resourceName = new AssemblyName(args.Name).Name;
            resourceName = Assembly.GetExecutingAssembly().GetManifestResourceNames().First(x => x.Contains(resourceName));
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public static string Game;
        Form fr2 = new Form2();
        Form fr3 = new Form3();
        WenjianjiaDialog dialog = new WenjianjiaDialog();
        private void button1_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog(this)== DialogResult.OK)
            {
                if (Directory.Exists(dialog.DirectoryPath + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                {
                    Game = dialog.DirectoryPath;
                    label2.Text = Game;
                    if (File.Exists(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\碧蓝之星零度之下汉化.json"))
                    {
                        button2.Text = "更新";
                        button10.Visible = true;
                    }
                    else
                    {
                        button2.Text = "安装";
                        button10.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show(dialog.DirectoryPath + "\n\n不是正确的游戏路径。\n\n请选择正确的游戏路径！", "路径错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Game != null)
            {
                File.WriteAllText(Path.Combine(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\碧蓝之星零度之下汉化.json"), Properties.Resources.汉化);
                DateTime riqi = DateTime.Parse("2020-2-14"); File.SetCreationTime(Path.Combine(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\碧蓝之星零度之下汉化.json"), riqi); File.SetLastWriteTime(Path.Combine(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\碧蓝之星零度之下汉化.json"), riqi); File.SetLastAccessTime(Path.Combine(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\碧蓝之星零度之下汉化.json"), riqi);
                if (button2.Text == "更新")
                {
                    MessageBox.Show("已将碧蓝之星汉化版本更新为V2.2\n注意：请进入游戏将语言切换成碧蓝之星汉化食用，碧蓝之星汉化与官方语言文件不冲突!\n\n//已将汉化文件释放到：\n" + Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles", "更新完成");
                }
                else
                {
                    button2.Text = "更新";
                    button10.Visible = true;
                    MessageBox.Show("注意：请进入游戏将语言切换成碧蓝之星汉化食用，碧蓝之星汉化与官方语言文件不冲突!\n\n//已将汉化文件释放到：\n" + Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles", "安装完成");

                }
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey("SOFTWARE", true).CreateSubKey("BLZX"); key.SetValue("InstallLocation", Game); key.SetValue("ReadMe", "碧蓝之星汉化组：688593222");
                MessageBox.Show("初次安装汉化后，游戏内还需进行设置才能使用！\n进入游戏界面之后，点击设置（Options）\n在语言（Language）选项中，向左切换并找到到碧蓝之星汉化即可", "温馨提示");
            }
            else MessageBox.Show("请输入游戏路径!", "路径为空，无法安装!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Game != null)
            {
                if (File.Exists(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\碧蓝之星零度之下汉化.json"))
                {
                    File.Delete(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\碧蓝之星零度之下汉化.json");
                    button2.Text = "安装";
                    button10.Visible = false;
                    MessageBox.Show("已卸载");
                }
                else MessageBox.Show("未安装");
            }
            else MessageBox.Show("游戏路径为空");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://jq.qq.com/?_wv=1027&k=5x6aOmw");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://jq.qq.com/?_wv=1027&k=5NFNSBV");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://jq.qq.com/?_wv=1027&k=5gYw38b");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fr2.Show();
        }       

        private void button8_Click(object sender, EventArgs e)
        {
             fr3.Show();
        }
        string 版本;
        private void Form1_Shown(object sender, EventArgs e)
        {
            Thread xc = new Thread(new ThreadStart(更新));
            xc.SetApartmentState(ApartmentState.STA);
            xc.Start();
            new Thread(new ThreadStart(路径)).Start();
            MessageBox.Show("各位深海玩家们：\n你们好，碧蓝之星汉化组在取得未知世界的深海迷航：冰点之下中文项目的管理权限之后，以我们发布的碧蓝之星冰点之下汉化为基础，用了四天的时间，花费大量的精力重新校对和修改了一遍官方中文（2020/02/13）。并且目前已经登录深海迷航：冰点之下最新的测试版本，正式稳定版本可能会随后更新。因此，可以认为官方中文已经非常完善。\n\n碧蓝之星的深海迷航：冰点之下汉化项目从2019/01/31开始进行，目前已经取得了较大的成果，在上一篇文章[http://blzxteam.com/notice]中，我们已经简单的介绍了汉化组，相信玩家们都有所了解。碧蓝汉化组成员皆由深海迷航玩家组成，对深海迷航有着丰富的游玩经验，经过主线故事深海迷航之后，让我们知道在冰点之下汉化当中，如何对新的生物进行翻译以及命名，在对话等翻译方面有非常本地化的体现。\n不忘初心，始终如一的进行翻译，表现了我们对深海迷航的热爱。翻译只为中文玩家得到更好的游戏体验，我们并没有因此得到任何的实质性报酬。\n在此，感谢主要汉化官雷顿的辛苦付出，感谢碧蓝汉化组所有成员在各方面的努力。\n\n碧蓝之星汉化组\n2020/02/13", "感谢所有支持碧蓝之星的玩家们，下次见");
        }
        Form fr4 = new Form4();
        private void button9_Click(object sender, EventArgs e)
        {
             fr4.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("优化汉化的实质是用碧蓝之星零度之下汉化替换掉官方中文文件，以获得更好的汉化效果。\n\n更新游戏或者验证游戏完整性官方中文都会被替换回来，需要重新优化。\n\n完成后请将语言切换到“碧蓝之星汉化优化版”。\n\n点击“确定”继续。", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                JObject jo = JObject.Parse(Properties.Resources.汉化);
                jo["LanguageChinese (Simplified)"] = "碧蓝之星汉化V2.2优化版";
                jo.Remove("Language碧蓝之星零度之下汉化");
                string json = jo.ToString();
                StringBuilder stb = new StringBuilder("");
                for (int i = 0; i < json.Length; i++)
                {
                    if (Regex.IsMatch(json[i].ToString(), @"[\u4e00-\u9fa5]|[\（\）\《\》\—\；\，\。\“\”\！\？\｛\｝\【\】\、\‘\’]"))
                    {
                        stb.Append("\\u" + ((int)json[i]).ToString("x"));
                    }
                    else
                    {
                        stb.Append(json[i].ToString());
                    }
                }
                stb.Insert(1, "//制作者：碧蓝之星汉化组，下载地址：http://blzxteam.com/blbzcn");
                File.WriteAllText(Path.Combine(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\Chinese (Simplified).json"), stb.ToString());
                DateTime riqi = DateTime.Parse("2020-2-14"); File.SetCreationTime(Path.Combine(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\Chinese (Simplified).json"), riqi); File.SetLastWriteTime(Path.Combine(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\Chinese (Simplified).json"), riqi); File.SetLastAccessTime(Path.Combine(Game + @"\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\Chinese (Simplified).json"), riqi);
                MessageBox.Show("更新游戏或者验证游戏完整性官方中文都会被替换回来，需要重新优化。\n\n请将语言切换到“碧蓝之星汉化优化版”。", "优化完成");
            }
        }
    }
}