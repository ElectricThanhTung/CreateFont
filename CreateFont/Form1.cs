using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace CreateFont {
    public partial class Form1 : Form {
        private string defaultList = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[?]^_`abcdefghijklmnopqrstuvwxyz{|}~?ÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬĐÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴàáảãạăằắẳẵặâầấẩẫậđèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵ";
        private Thread coding;

        public Form1() {
            InitializeComponent();
            int indexDefault = 0;
            for (int i = 0; i < FontFamily.Families.Length; i++) {
                fontComboBox1.Items.Add(FontFamily.Families[i].Name);
                FontStyle fontStyle = fontComboBox1.Font.Style;
                if (!FontFamily.Families[i].IsStyleAvailable(fontComboBox1.Font.Style)){
                    if (FontFamily.Families[i].IsStyleAvailable(FontStyle.Regular))
                        fontStyle = FontStyle.Regular;
                    else if (FontFamily.Families[i].IsStyleAvailable(FontStyle.Italic))
                        fontStyle = FontStyle.Italic;
                    else if (FontFamily.Families[i].IsStyleAvailable(FontStyle.Bold))
                        fontStyle = FontStyle.Bold;
                    else if (FontFamily.Families[i].IsStyleAvailable(FontStyle.Bold | FontStyle.Italic))
                        fontStyle = FontStyle.Bold | FontStyle.Italic;
                }
                Font font = new Font(FontFamily.Families[i].Name, fontComboBox1.Font.Size, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                fontComboBox1.Fonts.Add(font);
                font.Dispose();
                if (FontFamily.Families[i].Name.CompareTo("Times New Roman") == 0)
                    indexDefault = i;
            }
            fontComboBox1.SelectedIndex = indexDefault;
            UpdateFontStyleList();

            fontComboBox2.SelectedIndex = 0;
            textBox1.Text = fontComboBox1.Text;
        }

        private void UpdateFontStyleList() {
            fontComboBox2.SelectedIndex = fontComboBox2.SelectedIndex;
            string str = fontComboBox2.Text;
            int index = 0;
            List<FontStyle> fontStyles = GetFontStyleList(FontFamily.Families[fontComboBox1.SelectedIndex]);
            fontComboBox2.Items.Clear();
            for (int i = 0; i < fontStyles.Count; i++) {
                fontComboBox2.Items.Add(fontStyles[i]);
                fontComboBox2.Fonts.Add(new Font(FontFamily.Families[fontComboBox1.SelectedIndex].Name, fontComboBox2.Font.Size, fontStyles[i], GraphicsUnit.Point, ((byte)(0))));
                if (fontStyles[i].ToString().CompareTo(str) == 0)
                    index = i;
            }
            fontComboBox2.SelectedIndex = index;
        }

        private List<FontStyle> GetFontStyleList(FontFamily fontFamily) {
            List<FontStyle> fontStyles = new List<FontStyle>();
            if (fontFamily.IsStyleAvailable(FontStyle.Regular))
                fontStyles.Add(FontStyle.Regular);
            if (fontFamily.IsStyleAvailable(FontStyle.Italic))
                fontStyles.Add(FontStyle.Italic);
            if (fontFamily.IsStyleAvailable(FontStyle.Bold))
                fontStyles.Add(FontStyle.Bold);
            if (fontFamily.IsStyleAvailable(FontStyle.Bold | FontStyle.Italic))
                fontStyles.Add(FontStyle.Bold | FontStyle.Italic);
            return fontStyles;
        }

        private Font GetFont() {
            List<FontStyle> styles = GetFontStyleList(FontFamily.Families[fontComboBox1.SelectedIndex]);
            Font font = new Font(fontComboBox1.Text, Convert.ToInt32(comboBox1.Text), styles[fontComboBox2.SelectedIndex], GraphicsUnit.Point, ((byte)(0)));
            return font;
        }

        private FontData CreateDataFont(char c, Font font) {
            Bitmap bitmap = new Bitmap(200, 200);
            Graphics graphics = Graphics.FromImage(bitmap);
            if(c == ' ') {
                int space_w = (int)graphics.MeasureString(" ", font).Width;
                graphics.Dispose();
                bitmap.Dispose();

                return new FontData((int)c, space_w, 1, 0, new byte[space_w]);
            }

            SolidBrush solidBrush = new SolidBrush(Color.Black);
            graphics.Clear(Color.FromArgb(255, 255, 255, 255));
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            SizeF size = graphics.MeasureString(c.ToString(), font);
            Size s = new Size((int)size.Width, (int)size.Height);
            graphics.DrawString(c.ToString(), font, solidBrush, new PointF(0, 0));

            int x_start = 0, y_start = 0;
            for(int x = 0; x < (s.Width + 1); x++) {
                for(int y = 0; y < (s.Height + 1); y++) {
                    if(bitmap.GetPixel(x, y) != Color.FromArgb(255, 255, 255, 255)) {
                        x_start = x + 1;
                        break;
                    }
                }
                if(x_start > 0)
                    break;
            }
            for(int y = 0; y < (s.Height + 1); y++) {
                for(int x = 0; x < (s.Width + 1); x++) {
                    if(bitmap.GetPixel(x, y).R < 128) {
                        y_start = y + 1;
                        break;
                    }
                }
                if(y_start > 0)
                    break;
            }
            x_start--;
            y_start--;

            int x_end = 0, y_end = 0;
            for(int x = s.Width; x >= 0; x--) {
                for(int y = s.Height; y >= 0; y--) {
                    if(bitmap.GetPixel(x, y).R < 128) {
                        x_end = x + 1;
                        break;
                    }
                }
                if(x_end > 0)
                    break;
            }
            for(int y = s.Height; y >= 0; y--) {
                for(int x = s.Width; x >= 0; x--) {
                    if(bitmap.GetPixel(x, y).R < 128) {
                        y_end = y + 1;
                        break;
                    }
                }
                if(y_end > 0)
                    break;
            }
            solidBrush.Dispose();
            graphics.Dispose();

            int line = (y_end - y_start) / 8;
            if(((y_end - y_start) % 8) > 0)
                line++;

            int w = (x_end - x_start);
            byte[] data = new byte[w * line];
            for(int x = x_start; x < x_end; x++) {
                for(int y = y_start; y < y_end; y++) {
                    if(bitmap.GetPixel(x, y).R < 128)
                        data[(x - x_start) + ((y - y_start) / 8) * w] |= (byte)(0x80 >> ((y - y_start) % 8));
                }
            }
            bitmap.Dispose();

            return new FontData((int)c, w, line, y_start, data);
        }

        private List<FontData> CreateFontList(char[] list, Font font) {
            List<FontData> fontList = new List<FontData>();
            FontData A = CreateDataFont('A', font);
            for(int i = 0; i < list.Length; i++) {
                FontData data;
                try {
                    data = CreateDataFont(list[i], font);
                }
                catch {
                    data = CreateDataFont('?', font);
                }
                data.Offset -= A.Offset;
                fontList.Add(data);
            }
            return fontList;
        }

        private string ByteToHex(int value) {
            StringBuilder builder = new StringBuilder();
            builder.Append("0x");
            builder.Append(((byte)value).ToString("X2"));
            return builder.ToString();
        }

        private string IntToArray(int value) {
            StringBuilder builder = new StringBuilder();
            if(littleEndian.Checked == true) {
                builder.Append(ByteToHex(value >> 0));
                builder.Append(", ");
                builder.Append(ByteToHex(value >> 8));
                builder.Append(", ");
                builder.Append(ByteToHex(value >> 16));
                builder.Append(", ");
                builder.Append(ByteToHex(value >> 24));
            }
            else {
                builder.Append(ByteToHex(value >> 24));
                builder.Append(", ");
                builder.Append(ByteToHex(value >> 16));
                builder.Append(", ");
                builder.Append(ByteToHex(value >> 8));
                builder.Append(", ");
                builder.Append(ByteToHex(value >> 0));
            }
            return builder.ToString();
        }

        private void SortArray(char[] arr) {
            for(int i = 0; i < arr.Length; i++) {
                for(int j = i + 1; j < arr.Length; j++) {
                    if(arr[i] > arr[j]) {
                        char temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }

        private string CreateCode(string name, char[] list, Font font, out int dataSize) {
            SortArray(list);

            List<FontData> fontList = CreateFontList(list, font);
            StringBuilder fontIndexList = new StringBuilder();
            StringBuilder fontRawData = new StringBuilder();
            int size = 0;

            for(int i = 0; i < fontList.Count; i++) {
                fontIndexList.Append("    ");
                fontIndexList.Append(IntToArray(size + (fontList.Count + 1) * 4));
                fontIndexList.Append(", // '");
                fontIndexList.Append(list[i].ToString());
                fontIndexList.AppendLine("'");

                fontRawData.Append("    ");
                fontRawData.Append(IntToArray(fontList[i].Unicode));
                fontRawData.Append(", ");
                fontRawData.Append(ByteToHex(fontList[i].Width));
                fontRawData.Append(", ");
                fontRawData.Append(ByteToHex(fontList[i].Height));
                fontRawData.Append(", ");
                fontRawData.Append(ByteToHex(fontList[i].Offset));
                foreach(byte data in fontList[i].Data) {
                    fontRawData.Append(", ");
                    fontRawData.Append(ByteToHex(data));
                }
                fontRawData.Append(", // '");
                fontRawData.Append(list[i].ToString());
                fontRawData.Append("'");
                fontRawData.AppendLine(" raw data");

                size += fontList[i].Data.Length + 7;
            }

            StringBuilder ret = new StringBuilder();
            ret.Append("const uint8_t ");
            ret.Append(name.Replace(" ", ""));
            ret.AppendLine("[] = {");
            ret.Append("    ");
            ret.Append(IntToArray(fontList.Count));
            ret.AppendLine(", // font count value");
            ret.Append(fontIndexList);
            ret.Append(fontRawData);
            ret.Append("};");

            dataSize = size;
            return ret.ToString();
        }

        private Bitmap DrawString(string str, Font font, Rectangle rectangle) {
            if (rectangle.Width <= 0 || rectangle.Height <= 0)
                return null;
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            SolidBrush solidBrush = new SolidBrush(Color.Black);
            graphics.Clear(Color.FromArgb(255, 255, 255, 255));
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            graphics.DrawString(str, font, solidBrush, rectangle);
            graphics.Dispose();
            return bitmap;
        }

        private void fontComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (fontComboBox1.SelectedIndex >= 0 && fontComboBox2.SelectedIndex >= 0 && comboBox1.SelectedIndex >= 0) {
                Font font = GetFont();
                pictureBox1.Image = DrawString("!\"#$%&'()*+,-. 0123456789 ABCDEF abcdef\r\nChào Bạn\r\nこんにちは\r\n你好\r\n여보세요\r\nПривет\r\nहैलो\r\nสวัสดี\r\nសួស្តី", font, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
                panel3.Visible = true;
                if (coding != null && coding.IsAlive)
                    coding.Abort();
                string arr_name = textBox1.Text.Replace(" ", "_");
                if (arr_name == "")
                    arr_name = fontComboBox1.Text.Replace(" ", "_");

                string list = textBox2.Text;
                list = Regex.Replace(list, "(\r|\n)", "");
                if (list == "") 
                    list = defaultList;

                coding = new Thread(delegate () {
                    int data_size;
                    string str = CreateCode(arr_name, list.ToCharArray(), font, out data_size);
                    BeginInvoke((MethodInvoker)delegate () {
                        richTextBox1.Text = str;
                        panel3.Visible = false;
                        label7.Text = data_size + " Byte";
                    });
                });
                coding.IsBackground = true;
                coding.Start();
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e) {
            if (fontComboBox1.SelectedIndex >= 0 && fontComboBox2.SelectedIndex >= 0 && comboBox1.SelectedIndex >= 0)
                pictureBox1.Image = DrawString("!\"#$%&'()*+,-. 0123456789 ABCDEF abcdef\r\nChào Bạn\r\nこんにちは\r\n你好\r\n여보세요\r\nПривет\r\nहैलो\r\nสวัสดี\r\nសួស្តី", GetFont(), new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            int index = textBox1.SelectionStart;
            string str = textBox1.Text.Replace(" ", "_");
            while (str != "" && int.TryParse(str[0] + "", out _))
                str = str.Substring(1);
            textBox1.Text = str;
            textBox1.SelectionStart = index;
            if (str == "")
                str = fontComboBox1.Text.Replace(" ", "_");
            label3.Text = "const Font " + str + "[];";
            fontComboBox1_SelectedIndexChanged(sender, e);
        }

        private void fontComboBox1_SelectedIndexChanged_1(object sender, EventArgs e) {
            textBox1.Text = fontComboBox1.Text;
            UpdateFontStyleList();
            fontComboBox1_SelectedIndexChanged(sender, e);
        }

        private void about_menu_Click(object sender, EventArgs e) {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }

        private void exit_menu_Click(object sender, EventArgs e) {
            Confirm confirm = new Confirm("Are you sure you want to exit?");
            confirm.ShowDialog();
            if (confirm.reply)
                Application.Exit();
            confirm.Dispose();
        }

        private string CreateFontHeaderFileExample(string fontName) {
            StringBuilder builder = new StringBuilder();
            builder.Append("#ifndef __");
            builder.Append(fontName.ToUpper());
            builder.Append("_H\r\n#define __");
            builder.Append(fontName.ToUpper());
            builder.AppendLine("_H\r\n");
            builder.AppendLine("typedef struct {\r\n    uint8_t width;\r\n    uint8_t height;\r\n    int8_t offset;\r\n    uint8_t data[];\r\n} Font_TypeDef;\r\n");
            builder.Append("extern const uint8_t ");
            builder.Append(fontName);
            builder.AppendLine("[];\r\n");
            builder.Append("#endif // __");
            builder.Append(fontName.ToUpper());
            builder.AppendLine("_H");
            return builder.ToString();
        }

        private void create_c_Click(object sender, EventArgs e) {
            if (coding != null && !coding.IsAlive) {
                FolderSelectDialog folderSelectDialog = new FolderSelectDialog();
                folderSelectDialog.Title = "Select Folder";
                if (folderSelectDialog.ShowDialog(this.Handle) == true) {
                    string folder = folderSelectDialog.FileName;
                    StreamWriter streamWriter = new StreamWriter(folder + "\\" + textBox1.Text.ToLower() + ".c");
                    streamWriter.Write("\r\n#include \"" + textBox1.Text.ToLower() + ".h\"\r\n\r\n" + richTextBox1.Text + "\r\n");
                    streamWriter.Close();

                    streamWriter = new StreamWriter(folder + "\\" + textBox1.Text.ToLower() + ".h");
                    streamWriter.Write(CreateFontHeaderFileExample(textBox1.Text));
                    streamWriter.Close();

                    streamWriter = new StreamWriter(folder + "\\font.c");
                    streamWriter.Write(Properties.Resources.font_c);
                    streamWriter.Close();

                    streamWriter = new StreamWriter(folder + "\\font.h");
                    streamWriter.Write(Properties.Resources.font_h);
                    streamWriter.Close();

                    Notification notification = new Notification("Created C source file");
                    notification.ShowDialog();
                    notification.Dispose();
                }
            }
            else {
                Notification notification = new Notification("The process is busy!");
                notification.ShowDialog();
                notification.Dispose();
            }
        }
    }
}
