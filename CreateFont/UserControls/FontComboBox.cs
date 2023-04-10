using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CreateFont.UserControls {
    public class FontComboBox : ComboBox {
        public List<Font> Fonts = new List<Font>();
        public FontComboBox() {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnDrawItem(DrawItemEventArgs e) {
            e.DrawBackground();
            e.DrawFocusRectangle();
            if (e.Index >= 0 && Items.Count > 0) {
                DropDownItem item = new DropDownItem(Items[e.Index].ToString());
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                if (e.Index < Fonts.Count) {
                    if (Fonts[e.Index] != null) {
                        Font font = new Font(Fonts[e.Index].Name, e.Font.Size, Fonts[e.Index].Style, GraphicsUnit.Point, ((byte)(0)));
                        e.Graphics.DrawString(item.Value, font, new SolidBrush(e.ForeColor), e.Bounds.Left , e.Bounds.Top + 2);
                    }
                    else
                        e.Graphics.DrawString(item.Value, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top + 2);
                }
                else
                    e.Graphics.DrawString(item.Value, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top + 2);
            }
            base.OnDrawItem(e);
        }

        private class DropDownItem {
            public string Value {
                get { return value; }
                set { this.value = value; }
            }
            private string value;
            public Image Image {
                get { return img; }
                set { img = value; }
            }
            private Image img;
            public DropDownItem() : this("") { }
            public DropDownItem(string val) {
                value = val;
                this.img = new Bitmap(16, 16);
                Graphics g = Graphics.FromImage(img);
                Brush b = new SolidBrush(Color.FromName(val));
                g.DrawRectangle(Pens.White, 0, 0, img.Width, img.Height);
                g.FillRectangle(b, 1, 1, img.Width - 1, img.Height - 1);
            }
            public override string ToString() {
                return value;
            }
        }
    }
}
