using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace CreateFont {
    public partial class About : Form {
        public About() {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://www.facebook.com/electricthanhtung");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData) {
            if (KeyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void Info_Deactivate(object sender, EventArgs e) {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://github.com/electricthanhtung");
        }
    }
}
