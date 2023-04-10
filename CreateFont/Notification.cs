using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CreateFont {
    public partial class Notification : Form {
        public Notification(string error) {
            InitializeComponent();
            label1.Text = error;
            label1.Location = new Point((this.Width - label1.Width - 6) / 2, (button1.Location.Y - label1.Height) / 2);
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
