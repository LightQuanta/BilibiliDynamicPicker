using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace DynPicker
{
    public partial class FrmMain : Form
    {
        private static Dictionary<string, string> RepliedUser;
        private static Dictionary<string, string> RepostedUser;
        private static string DynId;
        private int seed = 0;
        
        public FrmMain()
        {
            InitializeComponent();
            RepliedUser = new Dictionary<string, string>();

            //是的动态id是写死在代码里的（
            DynId = "430814326783767162";
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            //多线程处理避免卡住窗体
            Thread t=new Thread(new ThreadStart(GetUsers));
            t.Start();
        }

        private void GetUsers()
        {
            btnGet.Text = "正在获取...";
            btnGet.Enabled = false;
            lstUsers.Items.Clear();
            RepostedUser = BilibiliAPI.GetRepostedUsers(DynId);
            RepliedUser = BilibiliAPI.GetCommentedUsers(DynId);
            foreach (KeyValuePair<string,string> k in RepostedUser)
            {
                if (RepliedUser.ContainsKey(k.Key))
                {
                    lstUsers.Items.Add(k.Key);
                }
            }
            label1.Text = "转发+评论用户数量: " + lstUsers.Items.Count.ToString();
            btnGet.Text = "获取成功！";
            btnGet.Enabled = true;
            btnSelect.Enabled = true;
        }

        private void btnGet_MouseDown(object sender, MouseEventArgs e)
        {
            //不知道为什么代码臭了起来（
            seed = e.X * 114 * e.Y * 514;
            seed -= 1919810;
            seed += DateTime.Now.Second * 7355608;
            seed -= DateTime.Now.Millisecond * -893893;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstUsers.Items.Count >= 5)
            {
                Thread t = new Thread(new ThreadStart(Sel));
                t.Start();
            } else
            {
                //5个人都不够还想抽奖？你在想peach
                MessageBox.Show("请先获取用户！");
            }
        }

        private void Sel()
        {
            btnSelect.Enabled = false;
            btnGet.Enabled = false;

            txtB.Text = "恭喜这5个B\r\n站用户";
            string[] s = new string[4];

            //别问我为啥不用for，问就是懒
            Thread.Sleep(300);
            Random r = new Random(++seed);
            lstUsers.SelectedIndex = r.Next(0, lstUsers.Items.Count - 1);
            txtB.Text += "\r\n";
            s[0] = lstUsers.SelectedItem.ToString();
            for (int i=0;i< lstUsers.SelectedItem.ToString().Length;i++)
            {
                txtB.Text += lstUsers.SelectedItem.ToString()[i];
                Thread.Sleep(50);
            }

            Thread.Sleep(300);
            lstUsers.Items.RemoveAt(lstUsers.SelectedIndex);
            seed *= 2;
            seed += 114;
            r = new Random(seed);
            lstUsers.SelectedIndex = r.Next(0, lstUsers.Items.Count - 1);
            txtB.Text += "\r\n";
            s[1] = lstUsers.SelectedItem.ToString();
            for (int i = 0; i < lstUsers.SelectedItem.ToString().Length; i++)
            {
                txtB.Text += lstUsers.SelectedItem.ToString()[i];
                Thread.Sleep(50);
            }

            Thread.Sleep(300);
            lstUsers.Items.RemoveAt(lstUsers.SelectedIndex);
            seed += 514;
            seed /= 114;
            r = new Random(seed);
            lstUsers.SelectedIndex = r.Next(0, lstUsers.Items.Count - 1);
            txtB.Text += "\r\n";
            s[2] = lstUsers.SelectedItem.ToString();
            for (int i = 0; i < lstUsers.SelectedItem.ToString().Length; i++)
            {
                txtB.Text += lstUsers.SelectedItem.ToString()[i];
                Thread.Sleep(50);
            }

            Thread.Sleep(300);
            lstUsers.Items.RemoveAt(lstUsers.SelectedIndex);
            seed *= DateTime.Now.Second;
            seed += 114514;
            r = new Random(seed);
            lstUsers.SelectedIndex = r.Next(0, lstUsers.Items.Count - 1);
            txtB.Text += "\r\n";
            s[3] = lstUsers.SelectedItem.ToString();
            for (int i = 0; i < lstUsers.SelectedItem.ToString().Length; i++)
            {
                txtB.Text += lstUsers.SelectedItem.ToString()[i];
                Thread.Sleep(50);
            }

            Thread.Sleep(300);
            lstUsers.Items.RemoveAt(lstUsers.SelectedIndex);
            seed += 1919810;
            seed -= DateTime.Now.Millisecond * 233;
            r = new Random(seed);
            lstUsers.SelectedIndex = r.Next(0, lstUsers.Items.Count - 1);
            txtB.Text += "\r\n";
            for (int i = 0; i < lstUsers.SelectedItem.ToString().Length; i++)
            {
                txtB.Text += lstUsers.SelectedItem.ToString()[i];
                Thread.Sleep(50);
            }
            lstUsers.Items.Add(s[0]);
            lstUsers.Items.Add(s[1]);
            lstUsers.Items.Add(s[2]);
            lstUsers.Items.Add(s[3]);

            btnSelect.Enabled = true;
            btnGet.Enabled = true;
        }
    }
}
