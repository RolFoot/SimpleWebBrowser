using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebKit;
using WebKit.Interop;

namespace SimpleWebBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateNewTab();

        }

        int tabCount = 1;

        private void CreateNewTab()
        {
            tabC.TabPages.Add("New Tab");

            //create web browser object
            //WebBrowser web = new WebBrowser();
            WebKitBrowser web = new WebKitBrowser();
            web.Dock = DockStyle.Fill;
            //web.ProgressChanged += Web_ProgressChanged;
            web.DocumentTitleChanged += Web_DocumentTitleChanged;
            web.Navigated += Web_Navigated;
            web.Navigating += Web_Navigating;
            web.DocumentCompleted += Web_DocumentCompleted;
            //web.ScriptErrorsSuppressed = true;  

            web.Navigate("https://www.google.com");

            if(tabCount == 1)
            {
                tabC.SelectedTab.Controls.Add(web);
                tabCount++;
            }
            else if(tabCount > 1)
            {
                tabC.SelectTab(tabCount - 1);
                tabC.SelectedTab.Controls.Add(web);
                tabCount++;
            }
            
        }

        private void Web_DocumentTitleChanged(object sender, EventArgs e)
        {
            string txt = (sender as WebKitBrowser).DocumentTitle;
            if (txt.Length > 20)
                tabC.SelectedTab.Text = txt.Substring(0, 16) + "...";
            else
                tabC.SelectedTab.Text = txt;
        }

        private void Web_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            
        }

        private void Web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            txtURL.Text = e.Url.ToString();
        }

        private void Web_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            progBar.Maximum = (int)e.MaximumProgress;
            if (e.CurrentProgress == -1) return;

            if(e.CurrentProgress < e.MaximumProgress)
            {
                progBar.Value = (int)e.CurrentProgress;
            }
        }

        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string txt = (sender as WebKitBrowser).DocumentTitle;
            if (txt.Length > 20)
                tabC.SelectedTab.Text = txt.Substring(0, 16) + "...";
            else
                tabC.SelectedTab.Text = txt;
            txtURL.Text = e.Url.ToString();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTab();
        }

        private void CloseTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int indx = tabC.SelectedIndex;
            if(tabC.SelectedIndex!=-1)
            {

                tabC.TabPages.RemoveAt(tabC.SelectedIndex);
                tabCount--;
                if (indx != 0)
                    tabC.SelectTab(indx - 1);
                if (tabCount == 1)
                    CreateNewTab();
            }
                
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            (tabC.SelectedTab.Controls[0] as WebKitBrowser).GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            (tabC.SelectedTab.Controls[0] as WebKitBrowser).GoForward();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            (tabC.SelectedTab.Controls[0] as WebKitBrowser).Refresh();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            (tabC.SelectedTab.Controls[0] as WebKitBrowser).Stop();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            (tabC.SelectedTab.Controls[0] as WebKitBrowser).Navigate(txtURL.Text);
        }

        private void closeAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage item in tabC.TabPages)
            {
                tabC.TabPages.Remove(item);
            }
            tabCount = 1;
            NewTabToolStripMenuItem.PerformClick();
        }

        private void restartToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //this.Close();//if only one form
        }
    }
}
