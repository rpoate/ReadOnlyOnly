using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ReadOnlyOnly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.htmlEditControl1.DocumentLoadComplete += HtmlEditControl1_DocumentLoadComplete;
            this.htmlEditControl1.MouseUp += HtmlEditControl1_MouseUp;
            this.htmlEditControl1.ZoomLevelChanged += HtmlEditControl1_ZoomLevelChanged;
            this.htmlEditControl1.KeyDown += HtmlEditControl1_KeyDown;

            this.htmlEditControl1.DocumentHTML = "<h2>The simplest and most extensible Winforms HTML Editor.</h2><img src='https://zoople.tech/img/logo.png?1' /><p>" +
                "Caters for all levels of end-user, from the most basic to people with advanced HTML knowledge.</p>" +
                "<p>Complete HTML DOM manipulation is achievable, simply and without the need for COM references<a href='https://google.com'>Google Link</a></p>" +
                "<table width=\"100%\" style=\"border: 3px dotted green\"><tr><td>test1</td></tr><td>test1</td></tr><td>test1</td></tr></table><br />" +
                "<table width=\"100%\"><tr><td>test1</td><td>test1</td><td>test1</td></tr><td>test1</td><td>test1</td><td>test1</td></tr><td>test1</td><td>test1</td><td>test1</td></tr></table>";

            this.htmlEditControl1.CSSText = "* {caret-color: transparent;} body {font-family: Calibri; background-image: url('https://www.w3schools.com/cssref/paper.gif'); background-size: cover;} table, th, tr, td {border: none !important;}";
            this.htmlEditControl1.ZoomLevel = 100;

        }

        private void HtmlEditControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false)
            { 
                e.SuppressKeyPress = true;
                return;
            }

            if (e.KeyCode == Keys.V || e.KeyCode == Keys.X) e.SuppressKeyPress = true;

            if (e.KeyCode == Keys.Oemplus) toolStripButton1_Click(null, null);

            if (e.KeyCode == Keys.OemMinus) toolStripButton2_Click(null, null);

            if (e.KeyCode == Keys.F) this.htmlEditControl1.ShowFindTextDialog();

        }

        private void HtmlEditControl1_ZoomLevelChanged(object sender, EventArgs e)
        {
            this.toolStripLabel1.Text = "Zoom: " + this.htmlEditControl1.ZoomLevel + "%";
        }

        private void HtmlEditControl1_MouseUp(object sender, MouseEventArgs e)
        {
            var oEle = this.htmlEditControl1.FindParentElementOfType("a");

            if (oEle != null)
            {
                Process browserProc = new Process();
                browserProc.StartInfo.UseShellExecute = true;
                browserProc.StartInfo.FileName = oEle.GetAttribute("href");
                browserProc.Start();
            }
        }

        private void HtmlEditControl1_DocumentLoadComplete(object sender, EventArgs e)
        {
            this.htmlEditControl1.Document.Body.SetAttribute("contenteditable", "false");

            this.htmlEditControl1.ContextMenuWYSIWYG.Items.Remove(this.htmlEditControl1.ContextMenuWYSIWYG.Items["ContextWYSIWYGCut"]);
            this.htmlEditControl1.ContextMenuWYSIWYG.Items.Remove(this.htmlEditControl1.ContextMenuWYSIWYG.Items["ContextWYSIWYGPaste"]);
            this.htmlEditControl1.ContextMenuWYSIWYG.Items.Remove(this.htmlEditControl1.ContextMenuWYSIWYG.Items["TablePropertiesToolStripMenuItem"]);
            this.htmlEditControl1.ContextMenuWYSIWYG.Items.Remove(this.htmlEditControl1.ContextMenuWYSIWYG.Items["ImagePropetiesToolStripMenuItem"]);
            this.htmlEditControl1.ContextMenuWYSIWYG.Items.Remove(this.htmlEditControl1.ContextMenuWYSIWYG.Items["LinkPropertiesToolStripMenuItem"]);
            this.htmlEditControl1.ContextMenuWYSIWYG.Items.Remove(this.htmlEditControl1.ContextMenuWYSIWYG.Items["ToolStripMenuItem1"]);

        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            this.htmlEditControl1.copy_document();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            this.htmlEditControl1.print_Preview();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.htmlEditControl1.ZoomLevel > 275) return;

            this.htmlEditControl1.ZoomLevel += 25;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.htmlEditControl1.ZoomLevel < 50) return;

            this.htmlEditControl1.ZoomLevel -= 25;
        }
    }
}
