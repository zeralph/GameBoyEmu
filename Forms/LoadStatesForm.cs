using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBoyTest.Forms
{
    public partial class LoadStatesForm : Form
    {
        private const string path = "..//..//states//";

        private ImageList m_images;
        private List<FileInfo> m_savefiles;
        private int m_nbFiles;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public LoadStatesForm()
        {
            GameBoy.Cpu.Stop();
            InitializeComponent();
            GetFiles();
            ListFiles();
            this.StateFilsList.View = View.Details;
            this.StateFilsList.HeaderStyle = ColumnHeaderStyle.None;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void GetFiles()
        {
            string gameName = GameBoy.Cartridge.GetTitle();
            m_nbFiles = 0;
            m_savefiles = new List<FileInfo>();
            m_images = new ImageList();
            m_images.ImageSize = new Size(64, 64);
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fi = files[i];
                    if (fi.Extension == ".sta" && fi.Name.Contains(gameName))
                    {
                        m_nbFiles++;
                        m_savefiles.Add(fi);
                    }
                }
                m_savefiles = m_savefiles.OrderBy(x => x.CreationTime).Reverse().ToList();
            }
            catch( Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        protected override void OnClosed(EventArgs e)
        {
            GameBoy.Cpu.Start();
            m_savefiles = null;
            for (int i = 0; i < m_images.Images.Count; i++ )
            {
                m_images.Images[i].Dispose();
            }
            m_images.Images.Clear();
            m_images = null;
            
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void ListFiles()
        {
            for( int i=0; i<m_savefiles.Count; i++ )
            {
                FileInfo f = m_savefiles[i];
                Bitmap b = State.StateSystem.GetStateImage(path + f.Name);
                m_images.Images.Add(b);
                ListViewItem l = new ListViewItem(f.Name);
                l.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                l.Name = f.Name;
                l.ImageIndex = i;
                //this.StateFilsList.Items.Add(l);
                string[] s = new string[] { "", f.CreationTime.ToString() };
                ListViewItem m = new ListViewItem(s, i);
                m.Name = f.Name;
                this.StateFilsList.Items.Add(m);
            }
            this.StateFilsList.LargeImageList  = m_images;
            this.StateFilsList.SmallImageList = m_images;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void StateFilsList_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem l = this.StateFilsList.SelectedItems[0];
            State.StateSystem.loadState(path+l.Name);
            this.Close();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                ListViewItem l = this.StateFilsList.SelectedItems[0];
                State.StateSystem.loadState(path + l.Name);
                this.Close();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
