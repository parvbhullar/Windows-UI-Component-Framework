using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    public class RichTextBoxEx : System.Windows.Forms.RichTextBox
    {

        private struct CHARFORMAT2_STRUCT
        {

            public byte bAnimation;
            public byte bCharSet;
            public byte bPitchAndFamily;
            public byte bReserved1;
            public byte bRevAuthor;
            public byte bUnderlineType;
            public uint cbSize;
            public int crBackColor;
            public int crTextColor;
            public uint dwEffects;
            public uint dwMask;
            public int dwReserved;
            public int lcid;
            public ushort sSpacing;
            public short sStyle;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szFaceName;
            public short wKerning;
            public ushort wWeight;
            public int yHeight;
            public int yOffset;

        }

        private const uint CFE_AUTOCOLOR = 1073741824U;
        private const uint CFE_BOLD = 1U;
        private const uint CFE_ITALIC = 2U;
        private const uint CFE_LINK = 32U;
        private const uint CFE_PROTECTED = 16U;
        private const uint CFE_STRIKEOUT = 8U;
        private const uint CFE_SUBSCRIPT = 65536U;
        private const uint CFE_SUPERSCRIPT = 131072U;
        private const uint CFE_UNDERLINE = 4U;
        private const int CFM_ALLCAPS = 128;
        private const int CFM_ANIMATION = 262144;
        private const int CFM_BACKCOLOR = 67108864;
        private const uint CFM_BOLD = 1U;
        private const uint CFM_CHARSET = 134217728U;
        private const uint CFM_COLOR = 1073741824U;
        private const int CFM_DISABLED = 8192;
        private const int CFM_EMBOSS = 2048;
        private const uint CFM_FACE = 536870912U;
        private const int CFM_HIDDEN = 256;
        private const int CFM_IMPRINT = 4096;
        private const uint CFM_ITALIC = 2U;
        private const int CFM_KERNING = 1048576;
        private const int CFM_LCID = 33554432;
        private const uint CFM_LINK = 32U;
        private const uint CFM_OFFSET = 268435456U;
        private const int CFM_OUTLINE = 512;
        private const uint CFM_PROTECTED = 16U;
        private const int CFM_REVAUTHOR = 32768;
        private const int CFM_REVISED = 16384;
        private const int CFM_SHADOW = 1024;
        private const uint CFM_SIZE = 2147483648U;
        private const int CFM_SMALLCAPS = 64;
        private const int CFM_SPACING = 2097152;
        private const uint CFM_STRIKEOUT = 8U;
        private const int CFM_STYLE = 524288;
        private const uint CFM_SUBSCRIPT = 196608U;
        private const uint CFM_SUPERSCRIPT = 196608U;
        private const uint CFM_UNDERLINE = 4U;
        private const int CFM_UNDERLINETYPE = 8388608;
        private const int CFM_WEIGHT = 4194304;
        private const byte CFU_UNDERLINE = 1;
        private const byte CFU_UNDERLINEDASH = 5;
        private const byte CFU_UNDERLINEDASHDOT = 6;
        private const byte CFU_UNDERLINEDASHDOTDOT = 7;
        private const byte CFU_UNDERLINEDOTTED = 4;
        private const byte CFU_UNDERLINEDOUBLE = 3;
        private const byte CFU_UNDERLINEHAIRLINE = 10;
        private const byte CFU_UNDERLINENONE = 0;
        private const byte CFU_UNDERLINETHICK = 9;
        private const byte CFU_UNDERLINEWAVE = 8;
        private const byte CFU_UNDERLINEWORD = 2;
        private const int EM_GETCHARFORMAT = 1082;
        private const int EM_SETCHARFORMAT = 1092;
        private const int SCF_ALL = 4;
        private const int SCF_SELECTION = 1;
        private const int SCF_WORD = 2;
        private const int WM_USER = 1024;

        [System.ComponentModel.DefaultValue(false)]
        public new bool DetectUrls
        {
            get
            {
                return base.DetectUrls;
            }
            set
            {
                base.DetectUrls = value;
            }
        }

        public RichTextBoxEx()
        {
            //if (!DesignMode)
            LP.Validate();
            DetectUrls = false;
        }

        public int GetSelectionLink()
        {
            return GetSelectionStyle(32, 32);
        }

        private int GetSelectionStyle(uint mask, uint effect)
        {
            int i;
            Oranikle.Studio.Controls.RichTextBoxEx.CHARFORMAT2_STRUCT charformat2_STRUCT;

            charformat2_STRUCT = new Oranikle.Studio.Controls.RichTextBoxEx.CHARFORMAT2_STRUCT();
            charformat2_STRUCT.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(charformat2_STRUCT);
            charformat2_STRUCT.szFaceName = new char[32];
            System.IntPtr intPtr1 = new System.IntPtr(1);
            System.IntPtr intPtr2 = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Runtime.InteropServices.Marshal.SizeOf(charformat2_STRUCT));
            System.Runtime.InteropServices.Marshal.StructureToPtr(charformat2_STRUCT, intPtr2, false);
            Oranikle.Studio.Controls.RichTextBoxEx.SendMessage(Handle, 1082, intPtr1, intPtr2);
            charformat2_STRUCT = (Oranikle.Studio.Controls.RichTextBoxEx.CHARFORMAT2_STRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(intPtr2, typeof(Oranikle.Studio.Controls.RichTextBoxEx.CHARFORMAT2_STRUCT));
            if ((charformat2_STRUCT.dwMask & mask) == mask)
            {
                if ((charformat2_STRUCT.dwEffects & effect) == effect)
                    i = 1;
                else
                    i = 0;
            }
            else
            {
                i = -1;
            }
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(intPtr2);
            return i;
        }

        public void InsertLink(string text, string hyperlink, int position)
        {
            if ((position < 0) || (position > Text.Length))
                throw new System.ArgumentOutOfRangeException("position");
            SelectionStart = position;
            SelectedRtf = "{\\rtf1\\ansi " + text + "\\v #" + hyperlink + "\\v0}";
            Select(position, text.Length + hyperlink.Length + 1);
            SetSelectionLink(true);
            Select(position + text.Length + hyperlink.Length + 1, 0);
        }

        public void InsertLink(string text, int position)
        {
            if ((position < 0) || (position > Text.Length))
                throw new System.ArgumentOutOfRangeException("position");
            SelectionStart = position;
            SelectedText = text;
            Select(position, text.Length);
            SetSelectionLink(true);
            Select(position + text.Length, 0);
        }

        public void InsertLink(string text)
        {
            InsertLink(text, SelectionStart);
        }

        public void InsertLink(string text, string hyperlink)
        {
            InsertLink(text, hyperlink, SelectionStart);
        }

        public void SetSelectionLink(bool link)
        {
            SetSelectionStyle((uint)32, link ? (uint)32 : (uint)0);
        }

        private void SetSelectionStyle(uint mask, uint effect)
        {
            Oranikle.Studio.Controls.RichTextBoxEx.CHARFORMAT2_STRUCT charformat2_STRUCT;

            charformat2_STRUCT = new Oranikle.Studio.Controls.RichTextBoxEx.CHARFORMAT2_STRUCT();
            charformat2_STRUCT.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(charformat2_STRUCT);
            charformat2_STRUCT.dwMask = mask;
            charformat2_STRUCT.dwEffects = effect;
            System.IntPtr intPtr1 = new System.IntPtr(1);
            System.IntPtr intPtr2 = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Runtime.InteropServices.Marshal.SizeOf(charformat2_STRUCT));
            System.Runtime.InteropServices.Marshal.StructureToPtr(charformat2_STRUCT, intPtr2, false);
            Oranikle.Studio.Controls.RichTextBoxEx.SendMessage(Handle, 1092, intPtr1, intPtr2);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(intPtr2);
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr SendMessage(System.IntPtr hWnd, int msg, System.IntPtr wParam, System.IntPtr lParam);

    } // class RichTextBoxEx

}

