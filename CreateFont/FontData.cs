namespace CreateFont {
    internal class FontData {
        private int unicode;
        private int width;
        private int height;
        private int offset;
        private byte[] data;

        public FontData(int unicode, int width, int height, int offset, byte[] data) {
            this.unicode = unicode;
            this.width = width;
            this.height = height;
            this.offset = offset;
            this.data = data;
        }

        public int Unicode {
            set {
                unicode = value;
            }
            get {
                return unicode;
            }
        }

        public int Width {
            set {
                width = value;
            }
            get {
                return width;
            }
        }

        public int Height {
            set {
                height = value;
            }
            get {
                return height;
            }
        }

        public int Offset {
            set {
                offset = value;
            }
            get {
                return offset;
            }
        }

        public byte[] Data {
            set {
                data = value;
            }
            get {
                return data;
            }
        }
    }
}
