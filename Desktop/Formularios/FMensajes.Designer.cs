
namespace PurchaseDesktop.Formularios
{
    partial class FMensajes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMensajes));
            this.LblMensaje = new Bunifu.UI.WinForms.BunifuLabel();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.bunifuSeparator1 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.BtnCerrar = new Bunifu.UI.WinForms.BunifuImageButton();
            this.bunifuSeparator2 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.FDock = new Bunifu.UI.WinForms.BunifuFormDock();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblMensaje
            // 
            this.LblMensaje.AllowParentOverrides = false;
            this.LblMensaje.AutoEllipsis = false;
            this.LblMensaje.AutoSize = false;
            this.LblMensaje.Cursor = System.Windows.Forms.Cursors.Default;
            this.LblMensaje.CursorType = System.Windows.Forms.Cursors.Default;
            this.LblMensaje.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.Color.LightGray;
            this.LblMensaje.Location = new System.Drawing.Point(12, 41);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblMensaje.Size = new System.Drawing.Size(277, 59);
            this.LblMensaje.TabIndex = 0;
            this.LblMensaje.Text = "...";
            this.LblMensaje.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblMensaje.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(45, 106);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "Ok";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(180, 106);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.bunifuSeparator1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuSeparator1.BackgroundImage")));
            this.bunifuSeparator1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuSeparator1.DashCap = Bunifu.UI.WinForms.BunifuSeparator.CapStyles.Flat;
            this.bunifuSeparator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(196)))), ((int)(((byte)(85)))));
            this.bunifuSeparator1.LineStyle = Bunifu.UI.WinForms.BunifuSeparator.LineStyles.Solid;
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(0, 148);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Orientation = Bunifu.UI.WinForms.BunifuSeparator.LineOrientation.Horizontal;
            this.bunifuSeparator1.Size = new System.Drawing.Size(301, 1);
            this.bunifuSeparator1.TabIndex = 11;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.PanelHeader.Controls.Add(this.BtnCerrar);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 1);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(301, 25);
            this.PanelHeader.TabIndex = 12;
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.ActiveImage = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.ActiveImage")));
            this.BtnCerrar.AllowAnimations = true;
            this.BtnCerrar.AllowBuffering = false;
            this.BtnCerrar.AllowToggling = false;
            this.BtnCerrar.AllowZooming = true;
            this.BtnCerrar.AllowZoomingOnFocus = false;
            this.BtnCerrar.BackColor = System.Drawing.Color.Transparent;
            this.BtnCerrar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnCerrar.ErrorImage = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.ErrorImage")));
            this.BtnCerrar.FadeWhenInactive = false;
            this.BtnCerrar.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.BtnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.Image")));
            this.BtnCerrar.ImageActive = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.ImageActive")));
            this.BtnCerrar.ImageLocation = null;
            this.BtnCerrar.ImageMargin = 5;
            this.BtnCerrar.ImageSize = new System.Drawing.Size(17, 17);
            this.BtnCerrar.ImageZoomSize = new System.Drawing.Size(22, 22);
            this.BtnCerrar.InitialImage = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.InitialImage")));
            this.BtnCerrar.Location = new System.Drawing.Point(276, 0);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Rotation = 0;
            this.BtnCerrar.ShowActiveImage = true;
            this.BtnCerrar.ShowCursorChanges = true;
            this.BtnCerrar.ShowImageBorders = true;
            this.BtnCerrar.ShowSizeMarkers = false;
            this.BtnCerrar.Size = new System.Drawing.Size(22, 22);
            this.BtnCerrar.TabIndex = 27;
            this.BtnCerrar.TabStop = false;
            this.BtnCerrar.ToolTipText = "";
            this.BtnCerrar.WaitOnLoad = false;
            this.BtnCerrar.Zoom = 5;
            this.BtnCerrar.ZoomSpeed = 10;
            this.BtnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // bunifuSeparator2
            // 
            this.bunifuSeparator2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.bunifuSeparator2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuSeparator2.BackgroundImage")));
            this.bunifuSeparator2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuSeparator2.DashCap = Bunifu.UI.WinForms.BunifuSeparator.CapStyles.Flat;
            this.bunifuSeparator2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuSeparator2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(196)))), ((int)(((byte)(85)))));
            this.bunifuSeparator2.LineStyle = Bunifu.UI.WinForms.BunifuSeparator.LineStyles.Solid;
            this.bunifuSeparator2.LineThickness = 1;
            this.bunifuSeparator2.Location = new System.Drawing.Point(0, 0);
            this.bunifuSeparator2.Name = "bunifuSeparator2";
            this.bunifuSeparator2.Orientation = Bunifu.UI.WinForms.BunifuSeparator.LineOrientation.Horizontal;
            this.bunifuSeparator2.Size = new System.Drawing.Size(301, 1);
            this.bunifuSeparator2.TabIndex = 13;
            // 
            // FDock
            // 
            this.FDock.AllowFormDragging = true;
            this.FDock.AllowFormDropShadow = true;
            this.FDock.AllowFormResizing = true;
            this.FDock.AllowHidingBottomRegion = true;
            this.FDock.AllowOpacityChangesWhileDragging = true;
            this.FDock.BorderOptions.BottomBorder.BorderColor = System.Drawing.Color.Silver;
            this.FDock.BorderOptions.BottomBorder.BorderThickness = 1;
            this.FDock.BorderOptions.BottomBorder.ShowBorder = true;
            this.FDock.BorderOptions.LeftBorder.BorderColor = System.Drawing.Color.Silver;
            this.FDock.BorderOptions.LeftBorder.BorderThickness = 1;
            this.FDock.BorderOptions.LeftBorder.ShowBorder = true;
            this.FDock.BorderOptions.RightBorder.BorderColor = System.Drawing.Color.Silver;
            this.FDock.BorderOptions.RightBorder.BorderThickness = 1;
            this.FDock.BorderOptions.RightBorder.ShowBorder = true;
            this.FDock.BorderOptions.TopBorder.BorderColor = System.Drawing.Color.Silver;
            this.FDock.BorderOptions.TopBorder.BorderThickness = 1;
            this.FDock.BorderOptions.TopBorder.ShowBorder = true;
            this.FDock.ContainerControl = this;
            this.FDock.DockingIndicatorsColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.FDock.DockingIndicatorsOpacity = 0.5D;
            this.FDock.DockingOptions.DockAll = true;
            this.FDock.DockingOptions.DockBottomLeft = true;
            this.FDock.DockingOptions.DockBottomRight = true;
            this.FDock.DockingOptions.DockFullScreen = true;
            this.FDock.DockingOptions.DockLeft = true;
            this.FDock.DockingOptions.DockRight = true;
            this.FDock.DockingOptions.DockTopLeft = true;
            this.FDock.DockingOptions.DockTopRight = true;
            this.FDock.FormDraggingOpacity = 0.9D;
            this.FDock.ParentForm = this;
            this.FDock.ShowCursorChanges = true;
            this.FDock.ShowDockingIndicators = true;
            this.FDock.TitleBarOptions.AllowFormDragging = true;
            this.FDock.TitleBarOptions.BunifuFormDock = this.FDock;
            this.FDock.TitleBarOptions.DoubleClickToExpandWindow = false;
            this.FDock.TitleBarOptions.TitleBarControl = this.PanelHeader;
            this.FDock.TitleBarOptions.UseBackColorOnDockingIndicators = false;
            // 
            // FMensajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(301, 149);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.bunifuSeparator2);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.LblMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FMensajes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FMensajes";
            this.Load += new System.EventHandler(this.FMensajes_Load);
            this.PanelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuLabel LblMensaje;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator1;
        private System.Windows.Forms.Panel PanelHeader;
        private Bunifu.UI.WinForms.BunifuImageButton BtnCerrar;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator2;
        private Bunifu.UI.WinForms.BunifuFormDock FDock;
    }
}