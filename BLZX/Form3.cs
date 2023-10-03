using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BLZX
{
    public partial class Form3 : Form
    {
        string Game = Form1.Game;
        string 字符1 = null;
        string 字符2 = null;
        string 字符3 = null;
        string 字符4 = null;
        string 字符5 = null;

        
        public static long FileSize(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        public static long GetDirectoryLength(string dirPath)
        {
            long len = 0;
            if (!Directory.Exists(dirPath))
            {
                len = 0;
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                foreach (FileInfo fi in di.GetFiles())
                {
                    len += fi.Length;
                }
                DirectoryInfo[] dis = di.GetDirectories();
                if (dis.Length > 0)
                {
                    for (int i = 0; i < dis.Length; i++)
                    {
                        len += GetDirectoryLength(dis[i].FullName);
                    }
                }
            }
            return len;
        }
        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " Byte";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
            return m_strSize;
        }

       

        public void jiemian(string cun, string c)
        {
            pictureBox1.Visible = true;
            label4.Visible = true;
            Label2.Visible = true;
            label4.Visible = true;
            textBox1.Visible = true;
            label5.Visible = true;
            label3.Visible = true;
            button6.Visible = true;
            label7.Visible = true;
            button9.Visible = true;
            button10.Visible = true;

            pictureBox1.ImageLocation = cun + @"\screenshot.jpg";
            Label2.Text = cun;
            label4.Text = c;
            textBox1.Text = JObject.Parse(File.ReadAllText(cun + @"\gameinfo.json"))["changeSet"].ToString();
            label5.Text = "最后修改时间： " + new FileInfo(cun + @"\global-objects.bin").LastWriteTime.ToShortDateString();
            if (Directory.Exists(cun + @"\screenshots")) button10.Text = "打开存档图片文件夹"; else button10.Text = "创建存档图片文件夹";
            label7.Text = "存档缓存：" + CountSize(GetDirectoryLength(Label2.Text) - (GetDirectoryLength(Label2.Text + @"\timecapsules") + GetDirectoryLength(Label2.Text + @"\screenshot") + FileSize(Label2.Text + @"\gameinfo.json") + FileSize(Label2.Text + @"\global-objects.bin") + FileSize(Label2.Text + @"\scene-objects.bin") + FileSize(Label2.Text + @"\screenshot.jpg")));
        }       
        public Form3()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog { Description = "请选择游戏路径" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(dialog.SelectedPath + @"\Subnautica_Data\Managed"))
                {
                    Game = dialog.SelectedPath;
                    label1.Text = Game;
                    //shuaxing
                    字符1 = null;
                    字符2 = null;
                    字符3 = null;
                    字符4 = null;
                    字符5 = null;
                    pictureBox1.Visible = false;
                    label4.Visible = false;
                    Label2.Visible = false;
                    label4.Visible = false;
                    textBox1.Visible = false;
                    label5.Visible = false;
                    label3.Visible = false;
                    button6.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    label7.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;

                    //嘤嘤嘤
                    string lujing = Game + @"\SNAppData\SavedGames\";
                    int i = 0;
                    string cd = string.Empty;
                    if (Directory.Exists(lujing))
                    {
                        foreach (DirectoryInfo cundang in new DirectoryInfo(lujing).GetDirectories())
                        {
                            if (File.Exists(lujing + cundang.Name + @"\gameinfo.json") && File.Exists(lujing + cundang.Name + @"\global-objects.bin") && File.Exists(lujing + cundang.Name + @"\scene-objects.bin") && File.Exists(lujing + cundang.Name + @"\screenshot.jpg"))
                            {
                                i++;
                                if (i == 1)
                                {
                                    字符1 = cundang.Name;
                                    button1.Visible = true;
                                    button1.Text = 字符1;
                                }
                                else if (i == 2)
                                {
                                    字符2 = cundang.Name;
                                    button2.Visible = true;
                                    button2.Text = 字符2;
                                }
                                else if (i == 3)
                                {
                                    字符3 = cundang.Name;
                                    button3.Visible = true;
                                    button3.Text = 字符3;
                                }
                                else if (i == 4)
                                {
                                    字符4 = cundang.Name;
                                    button4.Visible = true;
                                    button4.Text = 字符4;
                                }
                                else if (i == 5)
                                {
                                    字符5 = cundang.Name;
                                    button5.Visible = true;
                                    button5.Text = 字符5;
                                }
                                else
                                {
                                    textBox2.Visible = true;
                                    button7.Visible = true;
                                    listBox1.Visible = true;
                                    listBox1.Items.Add(cundang.Name);
                                }
                            }
                        }
                    }

                    //嘤嘤嘤


                }
                else
                {
                    if (Directory.Exists(dialog.SelectedPath + @"\SubnauticaZero_Data\Managed"))
                    {
                        Game = dialog.SelectedPath;
                        label1.Text = Game;
                        //shuaxing
                        字符1 = null;
                        字符2 = null;
                        字符3 = null;
                        字符4 = null;
                        字符5 = null;
                        pictureBox1.Visible = false;
                        label4.Visible = false;
                        Label2.Visible = false;
                        label4.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        label3.Visible = false;
                        button6.Visible = false;
                        button1.Visible = false;
                        button2.Visible = false;
                        button3.Visible = false;
                        button4.Visible = false;
                        button5.Visible = false;
                        label7.Visible = false;
                        button9.Visible = false;
                        button10.Visible = false;

                        //嘤嘤嘤
                        string lujing = Game + @"\SNAppData\SavedGames\";
                        int i = 0;
                        string cd = string.Empty;
                        if (Directory.Exists(lujing))
                        {
                            foreach (DirectoryInfo cundang in new DirectoryInfo(lujing).GetDirectories())
                            {
                                if (File.Exists(lujing + cundang.Name + @"\gameinfo.json") && File.Exists(lujing + cundang.Name + @"\global-objects.bin") && File.Exists(lujing + cundang.Name + @"\scene-objects.bin") && File.Exists(lujing + cundang.Name + @"\screenshot.jpg"))
                                {
                                    i++;
                                    if (i == 1)
                                    {
                                        字符1 = cundang.Name;
                                        button1.Visible = true;
                                        button1.Text = 字符1;
                                    }
                                    else if (i == 2)
                                    {
                                        字符2 = cundang.Name;
                                        button2.Visible = true;
                                        button2.Text = 字符2;
                                    }
                                    else if (i == 3)
                                    {
                                        字符3 = cundang.Name;
                                        button3.Visible = true;
                                        button3.Text = 字符3;
                                    }
                                    else if (i == 4)
                                    {
                                        字符4 = cundang.Name;
                                        button4.Visible = true;
                                        button4.Text = 字符4;
                                    }
                                    else if (i == 5)
                                    {
                                        字符5 = cundang.Name;
                                        button5.Visible = true;
                                        button5.Text = 字符5;
                                    }
                                    else
                                    {
                                        textBox2.Visible = true;
                                        button7.Visible = true;
                                        listBox1.Visible = true;
                                        listBox1.Items.Add(cundang.Name);
                                    }
                                }
                            }
                        }
                        if (字符1 != null) jiemian(lujing + 字符1, 字符1); else MessageBox.Show("没有检测到存档！");
                        //嘤嘤嘤

                    }
                    else MessageBox.Show(dialog.SelectedPath + "\n\n不是正确的游戏路径。\n\n请选择正确的游戏路径！", "路径错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                dialog.Dispose();
            }
            else
            {
                dialog.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Game + @"\SNAppData\SavedGames\" + 字符1 + @"\screenshot.jpg";
            Label2.Text = Game + @"\SNAppData\SavedGames\" + 字符1;
            label4.Text = 字符1;
            textBox1.Text = JObject.Parse(File.ReadAllText(Game + @"\SNAppData\SavedGames\" + 字符1 + @"\gameinfo.json"))["changeSet"].ToString();
            label5.Text = "最后修改时间： " + new FileInfo(Game + @"\SNAppData\SavedGames\" + 字符1 + @"\global-objects.bin").LastWriteTime.ToShortDateString();
            label7.Text = "存档缓存：" + CountSize(GetDirectoryLength(Label2.Text) - (GetDirectoryLength(Label2.Text + @"\timecapsules") + GetDirectoryLength(Label2.Text + @"\screenshot") + FileSize(Label2.Text + @"\gameinfo.json") + FileSize(Label2.Text + @"\global-objects.bin") + FileSize(Label2.Text + @"\scene-objects.bin") + FileSize(Label2.Text + @"\screenshot.jpg")));
            if (Directory.Exists(Label2.Text + @"\screenshots")) button10.Text = "打开存档图片文件夹"; else button10.Text = "创建存档图片文件夹";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Game + @"\SNAppData\SavedGames\" + 字符2 + @"\screenshot.jpg";
            Label2.Text = Game + @"\SNAppData\SavedGames\" + 字符2;
            label4.Text = 字符2;
            textBox1.Text = JObject.Parse(File.ReadAllText(Game + @"\SNAppData\SavedGames\" + 字符2 + @"\gameinfo.json"))["changeSet"].ToString();
            label5.Text = "最后修改时间： " + new FileInfo(Game + @"\SNAppData\SavedGames\" + 字符2 + @"\global-objects.bin").LastWriteTime.ToShortDateString();
            label7.Text = "存档缓存：" + CountSize(GetDirectoryLength(Label2.Text) - (GetDirectoryLength(Label2.Text + @"\timecapsules") + GetDirectoryLength(Label2.Text + @"\screenshot") + FileSize(Label2.Text + @"\gameinfo.json") + FileSize(Label2.Text + @"\global-objects.bin") + FileSize(Label2.Text + @"\scene-objects.bin") + FileSize(Label2.Text + @"\screenshot.jpg")));
            if (Directory.Exists(Label2.Text + @"\screenshots")) button10.Text = "打开存档图片文件夹"; else button10.Text = "创建存档图片文件夹";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Game + @"\SNAppData\SavedGames\" + 字符3 + @"\screenshot.jpg";
            Label2.Text = Game + @"\SNAppData\SavedGames\" + 字符3;
            label4.Text = 字符3;
            textBox1.Text = JObject.Parse(File.ReadAllText(Game + @"\SNAppData\SavedGames\" + 字符3 + @"\gameinfo.json"))["changeSet"].ToString();
            label5.Text = "最后修改时间： " + new FileInfo(Game + @"\SNAppData\SavedGames\" + 字符3 + @"\global-objects.bin").LastWriteTime.ToShortDateString();
            label7.Text = "存档缓存：" + CountSize(GetDirectoryLength(Label2.Text) - (GetDirectoryLength(Label2.Text + @"\timecapsules") + GetDirectoryLength(Label2.Text + @"\screenshot") + FileSize(Label2.Text + @"\gameinfo.json") + FileSize(Label2.Text + @"\global-objects.bin") + FileSize(Label2.Text + @"\scene-objects.bin") + FileSize(Label2.Text + @"\screenshot.jpg")));
            if (Directory.Exists(Label2.Text + @"\screenshots")) button10.Text = "打开存档图片文件夹"; else button10.Text = "创建存档图片文件夹";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Game + @"\SNAppData\SavedGames\" + 字符4 + @"\screenshot.jpg";
            Label2.Text = Game + @"\SNAppData\SavedGames\" + 字符4;
            label4.Text = 字符4;
            textBox1.Text = JObject.Parse(File.ReadAllText(Game + @"\SNAppData\SavedGames\" + 字符4 + @"\gameinfo.json"))["changeSet"].ToString();
            label5.Text = "最后修改时间： " + new FileInfo(Game + @"\SNAppData\SavedGames\" + 字符4 + @"\global-objects.bin").LastWriteTime.ToShortDateString();
            label7.Text = "存档缓存：" + CountSize(GetDirectoryLength(Label2.Text) - (GetDirectoryLength(Label2.Text + @"\timecapsules") + GetDirectoryLength(Label2.Text + @"\screenshot") + FileSize(Label2.Text + @"\gameinfo.json") + FileSize(Label2.Text + @"\global-objects.bin") + FileSize(Label2.Text + @"\scene-objects.bin") + FileSize(Label2.Text + @"\screenshot.jpg")));
            if (Directory.Exists(Label2.Text + @"\screenshots")) button10.Text = "打开存档图片文件夹"; else button10.Text = "创建存档图片文件夹";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Game + @"\SNAppData\SavedGames\" + 字符5 + @"\screenshot.jpg";
            Label2.Text = Game + @"\SNAppData\SavedGames\" + 字符5;
            label4.Text = 字符5;
            textBox1.Text = JObject.Parse(File.ReadAllText(Game + @"\SNAppData\SavedGames\" + 字符5 + @"\gameinfo.json"))["changeSet"].ToString();
            label5.Text = "最后修改时间： " + new FileInfo(Game + @"\SNAppData\SavedGames\" + 字符5 + @"\global-objects.bin").LastWriteTime.ToShortDateString();
            label7.Text = "存档缓存：" + CountSize(GetDirectoryLength(Label2.Text) - (GetDirectoryLength(Label2.Text + @"\timecapsules") + GetDirectoryLength(Label2.Text + @"\screenshot") + FileSize(Label2.Text + @"\gameinfo.json") + FileSize(Label2.Text + @"\global-objects.bin") + FileSize(Label2.Text + @"\scene-objects.bin") + FileSize(Label2.Text + @"\screenshot.jpg")));
            if (Directory.Exists(Label2.Text + @"\screenshots")) button10.Text = "打开存档图片文件夹"; else button10.Text = "创建存档图片文件夹";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Game + @"\SNAppData\SavedGames\" + textBox2.Text))
            {
                pictureBox1.ImageLocation = Game + @"\SNAppData\SavedGames\" + textBox2.Text + @"\screenshot.jpg";
                Label2.Text = Game + @"\SNAppData\SavedGames\" + textBox2.Text;
                label4.Text = textBox2.Text;
                textBox1.Text = JObject.Parse(File.ReadAllText(Game + @"\SNAppData\SavedGames\" + textBox2.Text + @"\gameinfo.json"))["changeSet"].ToString();
                label5.Text = "最后修改时间： " + new FileInfo(Game + @"\SNAppData\SavedGames\" + textBox2.Text + @"\global-objects.bin").LastWriteTime.ToShortDateString();
                label7.Text = "存档缓存：" + CountSize(GetDirectoryLength(Label2.Text) - (GetDirectoryLength(Label2.Text + @"\timecapsules") + GetDirectoryLength(Label2.Text + @"\screenshot") + FileSize(Label2.Text + @"\gameinfo.json") + FileSize(Label2.Text + @"\global-objects.bin") + FileSize(Label2.Text + @"\scene-objects.bin") + FileSize(Label2.Text + @"\screenshot.jpg")));
                if (Directory.Exists(Label2.Text + @"\screenshots")) button10.Text = "打开存档图片文件夹"; else button10.Text = "创建存档图片文件夹";

            }
            else
            {
                MessageBox.Show("请输入正确的存档名");
                textBox2.Text = null;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {
                textBox1.Text = JObject.Parse(File.ReadAllText(Label2.Text + @"\gameinfo.json"))["changeSet"].ToString();
                MessageBox.Show("请输入版本号！");
            }
            else
            {
                if (int.TryParse(textBox1.Text, out int i))
                {
                    JObject jo = JObject.Parse(File.ReadAllText(Label2.Text + @"\gameinfo.json"));
                    jo["changeSet"] = i;
                    File.WriteAllText(Path.Combine(Label2.Text + @"\gameinfo.json"), jo.ToString());
                    MessageBox.Show("版本锁定修改为：" + textBox1.Text);
                }
                else MessageBox.Show("请输入版本号！");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Label2.Text + @"\screenshots"))
            {
                Directory.CreateDirectory(Label2.Text + @"\screenshots");
                System.Diagnostics.Process.Start("explorer.exe", Label2.Text + @"\screenshots");
                button10.Text = "打开存档图片文件夹";
            }
            else System.Diagnostics.Process.Start("explorer.exe", Label2.Text + @"\screenshots");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            File.Move(Label2.Text + @"\gameinfo.json", Game + @"\SNAppData\SavedGames\1");
            File.Move(Label2.Text + @"\scene-objects.bin", Game + @"\SNAppData\SavedGames\2");
            File.Move(Label2.Text + @"\global-objects.bin", Game + @"\SNAppData\SavedGames\3");
            File.Move(Label2.Text + @"\screenshot.jpg", Game + @"\SNAppData\SavedGames\4");
            try
            {
                Directory.Move(Label2.Text + @"\timecapsules", Game + @"\SNAppData\SavedGames\tc");
            }
            catch { }
            try
            {
                Directory.Move(Label2.Text + @"\screenshot", Game + @"\SNAppData\SavedGames\ss");
            }
            catch { }
            Directory.Delete(Label2.Text, true);
            DirectoryInfo dir = new DirectoryInfo(Game + @"\SNAppData\SavedGames");
            dir.CreateSubdirectory(label4.Text);
            File.Move(Game + @"\SNAppData\SavedGames\1", Label2.Text + @"\gameinfo.json");
            File.Move(Game + @"\SNAppData\SavedGames\2", Label2.Text + @"\scene-objects.bin");
            File.Move(Game + @"\SNAppData\SavedGames\3", Label2.Text + @"\global-objects.bin");
            File.Move(Game + @"\SNAppData\SavedGames\4", Label2.Text + @"\screenshot.jpg");
            try
            {
                Directory.Move(Game + @"\SNAppData\SavedGames\tc", Label2.Text + @"\timecapsules");
            }
            catch { }
            try
            {
                Directory.Move(Game + @"\SNAppData\SavedGames\ss", Label2.Text + @"\screenshot");
            }
            catch { }
            label7.Text = "存档缓存：" + CountSize(GetDirectoryLength(Label2.Text) - (GetDirectoryLength(Label2.Text + @"\timecapsules") + GetDirectoryLength(Label2.Text + @"\screenshot") + FileSize(Label2.Text + @"\gameinfo.json") + FileSize(Label2.Text + @"\global-objects.bin") + FileSize(Label2.Text + @"\scene-objects.bin") + FileSize(Label2.Text + @"\screenshot.jpg")));
            MessageBox.Show("清理完成");

        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            if (Form1.Game != null) Game = Form1.Game;
            if (Game == null)
            {
                if (Directory.Exists(@"SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                {
                    Game = Directory.GetCurrentDirectory();
                }
                else if (Form1.Yxlj(@"SOFTWARE\BLZX", "InstallLocation", out string Key1))
                {
                    Game = Key1;
                }
                else if (Form1.Yxlj(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 264710", "InstallLocation", out string Key2))
                {
                    Game = Key2;
                }
                else if (Form1.Yxlj(@"Software\Valve\Steam", "SteamPath", out string Key3))
                {
                    Game = Key3;
                }
                else if (Form1.Yxlj(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\深海迷航", "InstallSource",out string Key4))
                {
                    Game = Key4;
                }
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
                        if (Directory.Exists(fulist[i] + @"Program Files\Steam\steamapps\common\Subnautica\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                        {
                            Game = fulist[i] + @"Program Files\Steam\steamapps\common\Subnautica";
                            break;
                        }
                        else if (Directory.Exists(fulist[i] + @"Steam\steamapps\common\Subnautica\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                        {
                            Game = fulist[i] + @"Steam\steamapps\common\Subnautica";
                            break;
                        }
                        else if (Directory.Exists(fulist[i] + @"SteamLibrary\steamapps\common\Subnautica\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                        {
                            Game = fulist[i] + @"SteamLibrary\steamapps\common\Subnautica";
                            break;
                        }
                        else if (Directory.Exists(fulist[i] + @"WeGame\rail_apps\深海迷航(2000174)\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                        {
                            Game = fulist[i] + @"WeGame\rail_apps\深海迷航(2000174)";
                            break;
                        }
                        else if (Directory.Exists(fulist[i] + @":PGP\games\Subnautica\SubnauticaZero_Data\StreamingAssets\SNUnmanagedData\LanguageFiles"))
                        {
                            Game = fulist[i] + @"PGP\games\Subnautica";
                            break;
                        }
                    }
                }
            }
            if (Game != null)
            {
                label1.Text = Game;
                //嘤嘤嘤
                string lujing = Game + @"\SNAppData\SavedGames\";
                int i = 0;
                string cd = string.Empty;
                if (Directory.Exists(lujing))
                {
                    foreach (DirectoryInfo cundang in new DirectoryInfo(lujing).GetDirectories())
                    {
                        if (File.Exists(lujing + cundang.Name + @"\gameinfo.json") && File.Exists(lujing + cundang.Name + @"\global-objects.bin") && File.Exists(lujing + cundang.Name + @"\scene-objects.bin") && File.Exists(lujing + cundang.Name + @"\screenshot.jpg"))
                        {
                            i++;
                            if (i == 1)
                            {
                                字符1 = cundang.Name;
                                button1.Visible = true;
                                button1.Text = 字符1;
                            }
                            else if (i == 2)
                            {
                                字符2 = cundang.Name;
                                button2.Visible = true;
                                button2.Text = 字符2;
                            }
                            else if (i == 3)
                            {
                                字符3 = cundang.Name;
                                button3.Visible = true;
                                button3.Text = 字符3;
                            }
                            else if (i == 4)
                            {
                                字符4 = cundang.Name;
                                button4.Visible = true;
                                button4.Text = 字符4;
                            }
                            else if (i == 5)
                            {
                                字符5 = cundang.Name;
                                button5.Visible = true;
                                button5.Text = 字符5;
                            }
                            else
                            {
                                textBox2.Visible = true;
                                button7.Visible = true;
                                listBox1.Visible = true;
                                listBox1.Items.Add(cundang.Name);
                            }
                        }
                    }
                }
                if (字符1 != null) jiemian(lujing + 字符1, 字符1); else MessageBox.Show("没有检测到存档！");
                //嘤嘤嘤
            }
            else
            {
                label1.Text = "请选择游戏路径";
            }

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

