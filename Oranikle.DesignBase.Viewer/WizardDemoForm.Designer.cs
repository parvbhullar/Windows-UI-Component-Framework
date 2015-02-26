namespace Oranikle.DesignBase.Viewer
{
    partial class WizardDemoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wizard1 = new Oranikle.Studio.Controls.Wizard();
            this.wizardPage2 = new Oranikle.Studio.Controls.WizardPage();
            this.header1 = new Oranikle.Studio.Controls.Header();
            this.wizardPage1 = new Oranikle.Studio.Controls.WizardPage();
            this.wizardPage3 = new Oranikle.Studio.Controls.WizardPage();
            this.wizardPage4 = new Oranikle.Studio.Controls.WizardPage();
            this.wizard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.Controls.Add(this.wizardPage4);
            this.wizard1.Controls.Add(this.wizardPage2);
            this.wizard1.Controls.Add(this.wizardPage1);
            this.wizard1.Controls.Add(this.wizardPage3);
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, 70);
            this.wizard1.Name = "wizard1";
            this.wizard1.Pages.AddRange(new Oranikle.Studio.Controls.WizardPage[] {
            this.wizardPage2,
            this.wizardPage1,
            this.wizardPage3,
            this.wizardPage4});
            this.wizard1.Size = new System.Drawing.Size(621, 378);
            this.wizard1.TabIndex = 0;
            // 
            // wizardPage2
            // 
            this.wizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage2.IsFinishPage = false;
            this.wizardPage2.Location = new System.Drawing.Point(177, 0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.PageTitle = "Welcome";
            this.wizardPage2.Size = new System.Drawing.Size(444, 330);
            this.wizardPage2.TabIndex = 3;
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.SystemColors.Control;
            this.header1.CausesValidation = false;
            this.header1.Description = "Description";
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Image = null;
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(621, 70);
            this.header1.TabIndex = 1;
            this.header1.Title = "Welcome to accy wizard";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(177, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.PageTitle = "Taxation && Accounts";
            this.wizardPage1.Size = new System.Drawing.Size(444, 330);
            this.wizardPage1.TabIndex = 4;
            // 
            // wizardPage3
            // 
            this.wizardPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage3.IsFinishPage = false;
            this.wizardPage3.Location = new System.Drawing.Point(177, 0);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.PageTitle = "Inventory";
            this.wizardPage3.Size = new System.Drawing.Size(444, 330);
            this.wizardPage3.TabIndex = 5;
            // 
            // wizardPage4
            // 
            this.wizardPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage4.IsFinishPage = false;
            this.wizardPage4.Location = new System.Drawing.Point(177, 0);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.PageTitle = "Gerenal Settings";
            this.wizardPage4.Size = new System.Drawing.Size(444, 330);
            this.wizardPage4.TabIndex = 6;
            // 
            // WizardDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(621, 448);
            this.Controls.Add(this.wizard1);
            this.Controls.Add(this.header1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardDemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WizardDemoForm";
            this.wizard1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Oranikle.Studio.Controls.Wizard wizard1;
        private Oranikle.Studio.Controls.Header header1;
        private Oranikle.Studio.Controls.WizardPage wizardPage2;
        private Oranikle.Studio.Controls.WizardPage wizardPage3;
        private Oranikle.Studio.Controls.WizardPage wizardPage1;
        private Oranikle.Studio.Controls.WizardPage wizardPage4;

    }
}