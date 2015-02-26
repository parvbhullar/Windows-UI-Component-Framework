using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    internal class CustomColors
    {

        internal static System.Drawing.Color ButtonBorder;
        internal static System.Drawing.Color ButtonHoverDark;
        internal static System.Drawing.Color ButtonHoverLight;
        internal static System.Drawing.Color Col11;
        internal static System.Drawing.Color Col12;
        internal static System.Drawing.Color Col13;
        internal static System.Drawing.Color Col14;
        internal static System.Drawing.Color Col15;
        internal static System.Drawing.Color Col21;
        internal static System.Drawing.Color Col22;
        internal static System.Drawing.Color Col23;
        internal static System.Drawing.Color Col24;
        internal static System.Drawing.Color Col25;
        internal static System.Drawing.Color Col31;
        internal static System.Drawing.Color Col32;
        internal static System.Drawing.Color Col33;
        internal static System.Drawing.Color Col34;
        internal static System.Drawing.Color Col35;
        internal static System.Drawing.Color Col41;
        internal static System.Drawing.Color Col42;
        internal static System.Drawing.Color Col43;
        internal static System.Drawing.Color Col44;
        internal static System.Drawing.Color Col45;
        internal static System.Drawing.Color Col51;
        internal static System.Drawing.Color Col52;
        internal static System.Drawing.Color Col53;
        internal static System.Drawing.Color Col54;
        internal static System.Drawing.Color Col55;
        internal static System.Drawing.Color Col61;
        internal static System.Drawing.Color Col62;
        internal static System.Drawing.Color Col63;
        internal static System.Drawing.Color Col64;
        internal static System.Drawing.Color Col65;
        internal static System.Drawing.Color Col71;
        internal static System.Drawing.Color Col72;
        internal static System.Drawing.Color Col73;
        internal static System.Drawing.Color Col74;
        internal static System.Drawing.Color Col75;
        internal static System.Drawing.Color Col81;
        internal static System.Drawing.Color Col82;
        internal static System.Drawing.Color Col83;
        internal static System.Drawing.Color Col84;
        internal static System.Drawing.Color Col85;
        internal static System.Drawing.Color ColorPickerBackgroundDocked;
        internal static System.Drawing.Color FocusDashedBorder;
        private static System.Drawing.Imaging.ImageAttributes grayScaleAttributes;
        internal static System.Drawing.Color[] SelectableColors;
        internal static string[] SelectableColorsNames;
        internal static System.Drawing.Color SelectedAndHover;
        internal static System.Drawing.Color SelectedBorder;

        public static System.Drawing.Imaging.ImageAttributes GrayScaleAttributes
        {
            get
            {
                if (Oranikle.Studio.Controls.CustomColors.grayScaleAttributes == null)
                {
                    System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix();
                    colorMatrix.Matrix00 = 0.3333333F;
                    colorMatrix.Matrix01 = 0.3333333F;
                    colorMatrix.Matrix02 = 0.3333333F;
                    colorMatrix.Matrix10 = 0.3333333F;
                    colorMatrix.Matrix11 = 0.3333333F;
                    colorMatrix.Matrix12 = 0.3333333F;
                    colorMatrix.Matrix20 = 0.3333333F;
                    colorMatrix.Matrix21 = 0.3333333F;
                    colorMatrix.Matrix22 = 0.3333333F;
                    Oranikle.Studio.Controls.CustomColors.grayScaleAttributes = new System.Drawing.Imaging.ImageAttributes();
                    Oranikle.Studio.Controls.CustomColors.grayScaleAttributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
                }
                return Oranikle.Studio.Controls.CustomColors.grayScaleAttributes;
            }
        }

        public CustomColors()
        {
        }

        static CustomColors()
        {
           // Oranikle.Studio.Controls.CustomColors.ButtonHoverLight = System.Drawing.Color.FromArgb(255, 240, 207);
            Oranikle.Studio.Controls.CustomColors.ButtonHoverLight = System.Drawing.Color.Gold;// FromArgb(249, 179, 48);
            Oranikle.Studio.Controls.CustomColors.ButtonHoverDark = System.Drawing.Color.FromArgb(249, 179, 48);
            Oranikle.Studio.Controls.CustomColors.SelectedAndHover = System.Drawing.Color.FromArgb(254, 128, 62);
            Oranikle.Studio.Controls.CustomColors.ButtonBorder = System.Drawing.Color.FromArgb(172, 168, 153);
            Oranikle.Studio.Controls.CustomColors.FocusDashedBorder = System.Drawing.Color.FromArgb(83, 87, 102);
            Oranikle.Studio.Controls.CustomColors.SelectedBorder = System.Drawing.Color.FromArgb(0, 0, 128);
            Oranikle.Studio.Controls.CustomColors.ColorPickerBackgroundDocked = System.Drawing.Color.FromArgb(248, 248, 248);
            Oranikle.Studio.Controls.CustomColors.Col11 = System.Drawing.Color.FromArgb(255, 130, 0);
            Oranikle.Studio.Controls.CustomColors.Col12 = System.Drawing.Color.FromArgb(255, 151, 0);
            Oranikle.Studio.Controls.CustomColors.Col13 = System.Drawing.Color.FromArgb(255, 175, 46);
            Oranikle.Studio.Controls.CustomColors.Col14 = System.Drawing.Color.FromArgb(255, 184, 59);
            Oranikle.Studio.Controls.CustomColors.Col15 = System.Drawing.Color.FromArgb(255, 213, 153);
            Oranikle.Studio.Controls.CustomColors.Col21 = System.Drawing.Color.FromArgb(121, 191, 0);
            Oranikle.Studio.Controls.CustomColors.Col22 = System.Drawing.Color.FromArgb(166, 216, 57);
            Oranikle.Studio.Controls.CustomColors.Col23 = System.Drawing.Color.FromArgb(188, 229, 74);
            Oranikle.Studio.Controls.CustomColors.Col24 = System.Drawing.Color.FromArgb(212, 249, 98);
            Oranikle.Studio.Controls.CustomColors.Col25 = System.Drawing.Color.FromArgb(244, 255, 182);
            Oranikle.Studio.Controls.CustomColors.Col31 = System.Drawing.Color.FromArgb(255, 192, 0);
            Oranikle.Studio.Controls.CustomColors.Col32 = System.Drawing.Color.FromArgb(255, 200, 0);
            Oranikle.Studio.Controls.CustomColors.Col33 = System.Drawing.Color.FromArgb(255, 224, 41);
            Oranikle.Studio.Controls.CustomColors.Col34 = System.Drawing.Color.FromArgb(255, 238, 136);
            Oranikle.Studio.Controls.CustomColors.Col35 = System.Drawing.Color.FromArgb(255, 245, 176);
            Oranikle.Studio.Controls.CustomColors.Col41 = System.Drawing.Color.FromArgb(3, 116, 229);
            Oranikle.Studio.Controls.CustomColors.Col42 = System.Drawing.Color.FromArgb(0, 168, 252);
            Oranikle.Studio.Controls.CustomColors.Col43 = System.Drawing.Color.FromArgb(80, 197, 255);
            Oranikle.Studio.Controls.CustomColors.Col44 = System.Drawing.Color.FromArgb(184, 228, 255);
            Oranikle.Studio.Controls.CustomColors.Col45 = System.Drawing.Color.FromArgb(217, 244, 255);
            Oranikle.Studio.Controls.CustomColors.Col51 = System.Drawing.Color.FromArgb(141, 69, 255);
            Oranikle.Studio.Controls.CustomColors.Col52 = System.Drawing.Color.FromArgb(145, 115, 255);
            Oranikle.Studio.Controls.CustomColors.Col53 = System.Drawing.Color.FromArgb(159, 136, 255);
            Oranikle.Studio.Controls.CustomColors.Col54 = System.Drawing.Color.FromArgb(185, 179, 255);
            Oranikle.Studio.Controls.CustomColors.Col55 = System.Drawing.Color.FromArgb(217, 217, 255);
            Oranikle.Studio.Controls.CustomColors.Col61 = System.Drawing.Color.FromArgb(20, 196, 221);
            Oranikle.Studio.Controls.CustomColors.Col62 = System.Drawing.Color.FromArgb(3, 242, 235);
            Oranikle.Studio.Controls.CustomColors.Col63 = System.Drawing.Color.FromArgb(52, 247, 232);
            Oranikle.Studio.Controls.CustomColors.Col64 = System.Drawing.Color.FromArgb(138, 255, 241);
            Oranikle.Studio.Controls.CustomColors.Col65 = System.Drawing.Color.FromArgb(215, 255, 248);
            Oranikle.Studio.Controls.CustomColors.Col71 = System.Drawing.Color.FromArgb(209, 0, 185);
            Oranikle.Studio.Controls.CustomColors.Col72 = System.Drawing.Color.FromArgb(226, 19, 211);
            Oranikle.Studio.Controls.CustomColors.Col73 = System.Drawing.Color.FromArgb(239, 65, 206);
            Oranikle.Studio.Controls.CustomColors.Col74 = System.Drawing.Color.FromArgb(239, 137, 230);
            Oranikle.Studio.Controls.CustomColors.Col75 = System.Drawing.Color.FromArgb(255, 220, 253);
            Oranikle.Studio.Controls.CustomColors.Col81 = System.Drawing.Color.FromArgb(0, 0, 0);
            Oranikle.Studio.Controls.CustomColors.Col82 = System.Drawing.Color.FromArgb(64, 64, 64);
            Oranikle.Studio.Controls.CustomColors.Col83 = System.Drawing.Color.FromArgb(128, 128, 128);
            Oranikle.Studio.Controls.CustomColors.Col84 = System.Drawing.Color.FromArgb(192, 192, 192);
            Oranikle.Studio.Controls.CustomColors.Col85 = System.Drawing.Color.FromArgb(255, 255, 255);
            System.Drawing.Color[] colorArr = new System.Drawing.Color[40];
            colorArr[0] = Oranikle.Studio.Controls.CustomColors.Col11;
            colorArr[1] = Oranikle.Studio.Controls.CustomColors.Col21;
            colorArr[2] = Oranikle.Studio.Controls.CustomColors.Col31;
            colorArr[3] = Oranikle.Studio.Controls.CustomColors.Col41;
            colorArr[4] = Oranikle.Studio.Controls.CustomColors.Col51;
            colorArr[5] = Oranikle.Studio.Controls.CustomColors.Col61;
            colorArr[6] = Oranikle.Studio.Controls.CustomColors.Col71;
            colorArr[7] = Oranikle.Studio.Controls.CustomColors.Col81;
            colorArr[8] = Oranikle.Studio.Controls.CustomColors.Col12;
            colorArr[9] = Oranikle.Studio.Controls.CustomColors.Col22;
            colorArr[10] = Oranikle.Studio.Controls.CustomColors.Col32;
            colorArr[11] = Oranikle.Studio.Controls.CustomColors.Col42;
            colorArr[12] = Oranikle.Studio.Controls.CustomColors.Col52;
            colorArr[13] = Oranikle.Studio.Controls.CustomColors.Col62;
            colorArr[14] = Oranikle.Studio.Controls.CustomColors.Col72;
            colorArr[15] = Oranikle.Studio.Controls.CustomColors.Col82;
            colorArr[16] = Oranikle.Studio.Controls.CustomColors.Col13;
            colorArr[17] = Oranikle.Studio.Controls.CustomColors.Col23;
            colorArr[18] = Oranikle.Studio.Controls.CustomColors.Col33;
            colorArr[19] = Oranikle.Studio.Controls.CustomColors.Col43;
            colorArr[20] = Oranikle.Studio.Controls.CustomColors.Col53;
            colorArr[21] = Oranikle.Studio.Controls.CustomColors.Col63;
            colorArr[22] = Oranikle.Studio.Controls.CustomColors.Col73;
            colorArr[23] = Oranikle.Studio.Controls.CustomColors.Col83;
            colorArr[24] = Oranikle.Studio.Controls.CustomColors.Col14;
            colorArr[25] = Oranikle.Studio.Controls.CustomColors.Col24;
            colorArr[26] = Oranikle.Studio.Controls.CustomColors.Col34;
            colorArr[27] = Oranikle.Studio.Controls.CustomColors.Col44;
            colorArr[28] = Oranikle.Studio.Controls.CustomColors.Col54;
            colorArr[29] = Oranikle.Studio.Controls.CustomColors.Col64;
            colorArr[30] = Oranikle.Studio.Controls.CustomColors.Col74;
            colorArr[31] = Oranikle.Studio.Controls.CustomColors.Col84;
            colorArr[32] = Oranikle.Studio.Controls.CustomColors.Col15;
            colorArr[33] = Oranikle.Studio.Controls.CustomColors.Col25;
            colorArr[34] = Oranikle.Studio.Controls.CustomColors.Col35;
            colorArr[35] = Oranikle.Studio.Controls.CustomColors.Col45;
            colorArr[36] = Oranikle.Studio.Controls.CustomColors.Col55;
            colorArr[37] = Oranikle.Studio.Controls.CustomColors.Col65;
            colorArr[38] = Oranikle.Studio.Controls.CustomColors.Col75;
            colorArr[39] = Oranikle.Studio.Controls.CustomColors.Col85;
            Oranikle.Studio.Controls.CustomColors.SelectableColors = colorArr;
            string[] sArr = new string[] {
                                           "Darker Orange", 
                                           "Darker Green", 
                                           "Darker Yellow", 
                                           "Darker Blue", 
                                           "Darker Purple", 
                                           "Darker Teal", 
                                           "Darker Pink", 
                                           "Black", 
                                           "Dark Orange", 
                                           "Dark Green", 
                                           "Dark Yellow", 
                                           "Dark Blue", 
                                           "Dark Purple", 
                                           "Dark Teal", 
                                           "Dark Pink", 
                                           "75% Gray", 
                                           "Mild Orange", 
                                           "Mild Green", 
                                           "Mild Yellow", 
                                           "Mild Blue", 
                                           "Mild Purple", 
                                           "Mild Teal", 
                                           "Mild Pink", 
                                           "50% Gray", 
                                           "Light Orange", 
                                           "Light Green", 
                                           "Light Yellow", 
                                           "Light Blue", 
                                           "Light Purple", 
                                           "Light Teal", 
                                           "Light Pink", 
                                           "25% Gray", 
                                           "Ligher Orange", 
                                           "Ligher Green", 
                                           "Ligher Yellow", 
                                           "Ligher Blue", 
                                           "Ligher Purple", 
                                           "Ligher Teal", 
                                           "Ligher Pink", 
                                           "White", 
                                           "More Colors" };
            Oranikle.Studio.Controls.CustomColors.SelectableColorsNames = sArr;
        }

        internal static bool ColorEquals(System.Drawing.Color color1, System.Drawing.Color color2)
        {
            if ((color1.R == color2.R) && (color1.G == color2.G))
                return color1.B == color2.B;
            return false;
        }

    } 
}
