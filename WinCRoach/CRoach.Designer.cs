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
            this.panelRoach = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelRoach
            // 
            this.panelRoach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRoach.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelRoach.BackColor = System.Drawing.Color.Transparent;
            this.panelRoach.BackgroundImage = global::WinCRoach.Properties.Resources.CRoach;
            this.panelRoach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelRoach.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.panelRoach.Location = new System.Drawing.Point(0, 0);
            this.panelRoach.Margin = new System.Windows.Forms.Padding(0);
            this.panelRoach.Name = "panelRoach";
            this.panelRoach.Size = new System.Drawing.Size(64, 64);
            this.panelRoach.TabIndex = 0;
            // 
            // CRoach
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MaximumSize = new System.Drawing.Size(64, 64);
            this.ClientSize = new System.Drawing.Size(64, 64);
            this.ControlBox = false;
            this.Controls.Add(this.panelRoach);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "rch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panelRoach;
    }
}

