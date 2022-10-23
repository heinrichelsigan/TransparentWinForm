using WinCRoach.Properties;

namespace WinCRoach
{
    partial class CRoach
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxRoach = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoach)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRoach
            // 
            this.pictureBoxRoach.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxRoach.Image = global::WinCRoach.Properties.Resources.CRoach;
            this.pictureBoxRoach.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxRoach.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxRoach.Name = "pictureBoxRoach";
            this.pictureBoxRoach.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxRoach.TabIndex = 0;
            this.pictureBoxRoach.TabStop = false;
            // 
            // CRoach
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Size = new System.Drawing.Size(64, 64);
            this.ClientSize = new System.Drawing.Size(64, 64);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxRoach);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::WinCRoach.Properties.Resources.WinCRoach;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CRoach";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxRoach;
    }
}

