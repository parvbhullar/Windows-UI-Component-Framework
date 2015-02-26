using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public class ColorSchemeConstants
    {

        private static Color _Inventory_LabelTitle;
        private static ModuleTheme _InventoryModuleTheme;
        private static Color _Purchasing_LabelTitle;
        private static ModuleTheme _PurchasingModuleTheme;
        private static Color _Sales_LabelTitle;
        private static ModuleTheme _SalesModuleTheme;
        private static Color _Settings_LabelTitle;
        private static ModuleTheme _SettingsModuleTheme;

        public static Color Inventory_LabelTitle
        {
            get
            {
                return ColorSchemeConstants._Inventory_LabelTitle;
            }
        }

        public static ModuleTheme InventoryModuleTheme
        {
            get
            {
                if (ColorSchemeConstants._InventoryModuleTheme == null)
                    ColorSchemeConstants._InventoryModuleTheme = ColorSchemeConstants.GetModuleTheme(EnumModules.Enums.Inventory);
                return ColorSchemeConstants._InventoryModuleTheme;
            }
        }

        public static Color Purchasing_LabelTitle
        {
            get
            {
                return ColorSchemeConstants._Purchasing_LabelTitle;
            }
        }

        public static ModuleTheme PurchasingModuleTheme
        {
            get
            {
                if (ColorSchemeConstants._PurchasingModuleTheme == null)
                    ColorSchemeConstants._PurchasingModuleTheme = ColorSchemeConstants.GetModuleTheme(EnumModules.Enums.Purchasing);
                return ColorSchemeConstants._PurchasingModuleTheme;
            }
        }

        public static Color Sales_LabelTitle
        {
            get
            {
                return ColorSchemeConstants._Sales_LabelTitle;
            }
        }

        public static ModuleTheme SalesModuleTheme
        {
            get
            {
                if (ColorSchemeConstants._SalesModuleTheme == null)
                    ColorSchemeConstants._SalesModuleTheme = ColorSchemeConstants.GetModuleTheme(EnumModules.Enums.Sales);
                return ColorSchemeConstants._SalesModuleTheme;
            }
        }

        public static Color Settings_LabelTitle
        {
            get
            {
                return ColorSchemeConstants._Settings_LabelTitle;
            }
        }

        public static ModuleTheme SettingsModuleTheme
        {
            get
            {
                if (ColorSchemeConstants._SettingsModuleTheme == null)
                    ColorSchemeConstants._SettingsModuleTheme = ColorSchemeConstants.GetModuleTheme(EnumModules.Enums.Settings);
                return ColorSchemeConstants._SettingsModuleTheme;
            }
        }

        public ColorSchemeConstants()
        {
        }

        static ColorSchemeConstants()
        {
            ColorSchemeConstants._Sales_LabelTitle = Color.FromArgb(121, 191, 0);
            ColorSchemeConstants._Purchasing_LabelTitle = Color.FromArgb(255, 192, 0);
            ColorSchemeConstants._Inventory_LabelTitle = Color.FromArgb(80, 197, 255);
            ColorSchemeConstants._Settings_LabelTitle = Color.FromArgb(178, 178, 178);
        }

        public static void ApplyBlackAndWhiteTheme(StyledDataGridView dgv)
        {
            dgv.SuspendLayout();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(90, 90, 90);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgv.AllowUserToResizeRows = false;
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.DefaultCellStyle = dataGridViewCellStyle3;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = Color.White; //Color.FromArgb(209, 209, 209);
            dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgv.RowHeadersWidth = 25;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.ShowGridBorder = true;
            dgv.ResumeLayout();
        }

        private static ModuleTheme GetModuleTheme(EnumModules.Enums module)
        {
            ModuleTheme moduleTheme = new ModuleTheme();
            switch (module)
            {
                case EnumModules.Enums.Sales:
                    moduleTheme.LabelTitleColor = ColorSchemeConstants.Sales_LabelTitle;
                    break;

                case EnumModules.Enums.Purchasing:
                    moduleTheme.LabelTitleColor = ColorSchemeConstants.Purchasing_LabelTitle;
                    break;

                case EnumModules.Enums.Inventory:
                    moduleTheme.LabelTitleColor = ColorSchemeConstants.Inventory_LabelTitle;
                    break;

                case EnumModules.Enums.Settings:
                    moduleTheme.LabelTitleColor = ColorSchemeConstants.Settings_LabelTitle;
                    break;
            }
            return moduleTheme;
        }

    } 
}
