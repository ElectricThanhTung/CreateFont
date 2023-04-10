using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CreateFont {
    public partial class Confirm : Form {
        public bool reply = false;
        public Confirm(string confirm) {
            InitializeComponent();
            label1.Text = confirm;
            label1.Location = new Point((this.Width - label1.Width - 6) / 2, label1.Location.Y);
        }

        private void Button1_Click(object sender, EventArgs e) {
            reply = false;
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e) {
            reply = true;
            this.Close();
        }

        private void Confirm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Escape) {
                reply = false;
                this.Close();
            }
        }
    }
}
