using System.ComponentModel;
using System.Windows.Forms;

namespace BLZX
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.webBrowser1.ScriptErrorsSuppressed = true;
        }
        
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            Text = "打开下载链接";
            this.webBrowser1.Url = new System.Uri("https://pan.baidu.com/s/1-Um5PKEuStfImBpRs_IFHQ", System.UriKind.Absolute);
        }
        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.webBrowser1.Url = new System.Uri(this.webBrowser1.Document.ActiveElement.GetAttribute("href"), System.UriKind.Absolute);
        }
    }
}
