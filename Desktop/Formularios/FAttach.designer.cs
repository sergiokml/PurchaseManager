
namespace PurchaseDesktop.Formularios
{
    partial class FAttach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAttach));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.LabelPanel = new System.Windows.Forms.Label();
            this.BtnCerrar = new Bunifu.UI.WinForms.BunifuImageButton();
            this.TxtName = new Bunifu.UI.WinForms.BunifuTextBox();
            this.FDock = new Bunifu.UI.WinForms.BunifuFormDock();
            this.bunifuSeparator1 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.BtnNewDetail = new Bunifu.UI.WinForms.BunifuImageButton();
            this.bunifuSeparator2 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.Grid = new TenTec.Windows.iGridLib.iGrid();
            this.iGrid1DefaultCellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1DefaultColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1RowTextColCellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.PanelHeader.Controls.Add(this.LabelPanel);
            this.PanelHeader.Controls.Add(this.BtnCerrar);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 1);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(379, 25);
            this.PanelHeader.TabIndex = 1;
            // 
            // LabelPanel
            // 
            this.LabelPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPanel.ForeColor = System.Drawing.Color.White;
            this.LabelPanel.Location = new System.Drawing.Point(0, 0);
            this.LabelPanel.Name = "LabelPanel";
            this.LabelPanel.Size = new System.Drawing.Size(160, 25);
            this.LabelPanel.TabIndex = 28;
            this.LabelPanel.Text = "Attachment ";
            this.LabelPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.BtnCerrar.Location = new System.Drawing.Point(354, 1);
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
            // TxtName
            // 
            this.TxtName.AcceptsReturn = false;
            this.TxtName.AcceptsTab = false;
            this.TxtName.AnimationSpeed = 200;
            this.TxtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.TxtName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.TxtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            this.TxtName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TxtName.BackgroundImage")));
            this.TxtName.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.TxtName.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.TxtName.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.TxtName.BorderColorIdle = System.Drawing.Color.Silver;
            this.TxtName.BorderRadius = 1;
            this.TxtName.BorderThickness = 1;
            this.TxtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtName.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.TxtName.DefaultText = "";
            this.TxtName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            this.TxtName.HideSelection = true;
            this.TxtName.IconLeft = null;
            this.TxtName.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtName.IconPadding = 10;
            this.TxtName.IconRight = null;
            this.TxtName.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtName.Lines = new string[0];
            this.TxtName.Location = new System.Drawing.Point(12, 43);
            this.TxtName.MaxLength = 32767;
            this.TxtName.MinimumSize = new System.Drawing.Size(1, 1);
            this.TxtName.Modified = false;
            this.TxtName.Multiline = false;
            this.TxtName.Name = "TxtName";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtName.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.TxtName.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtName.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtName.OnIdleState = stateProperties4;
            this.TxtName.Padding = new System.Windows.Forms.Padding(3);
            this.TxtName.PasswordChar = '\0';
            this.TxtName.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.TxtName.PlaceholderText = "Enter text";
            this.TxtName.ReadOnly = false;
            this.TxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtName.SelectedText = "";
            this.TxtName.SelectionLength = 0;
            this.TxtName.SelectionStart = 0;
            this.TxtName.ShortcutsEnabled = true;
            this.TxtName.Size = new System.Drawing.Size(247, 30);
            this.TxtName.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Material;
            this.TxtName.TabIndex = 8;
            this.TxtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtName.TextMarginBottom = 0;
            this.TxtName.TextMarginLeft = 3;
            this.TxtName.TextMarginTop = 0;
            this.TxtName.TextPlaceholder = "Enter text";
            this.TxtName.UseSystemPasswordChar = false;
            this.TxtName.WordWrap = true;
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
            this.bunifuSeparator1.Location = new System.Drawing.Point(0, 263);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Orientation = Bunifu.UI.WinForms.BunifuSeparator.LineOrientation.Horizontal;
            this.bunifuSeparator1.Size = new System.Drawing.Size(379, 1);
            this.bunifuSeparator1.TabIndex = 10;
            // 
            // BtnNewDetail
            // 
            this.BtnNewDetail.ActiveImage = ((System.Drawing.Image)(resources.GetObject("BtnNewDetail.ActiveImage")));
            this.BtnNewDetail.AllowAnimations = true;
            this.BtnNewDetail.AllowBuffering = false;
            this.BtnNewDetail.AllowToggling = false;
            this.BtnNewDetail.AllowZooming = true;
            this.BtnNewDetail.AllowZoomingOnFocus = false;
            this.BtnNewDetail.BackColor = System.Drawing.Color.Transparent;
            this.BtnNewDetail.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnNewDetail.ErrorImage = ((System.Drawing.Image)(resources.GetObject("BtnNewDetail.ErrorImage")));
            this.BtnNewDetail.FadeWhenInactive = false;
            this.BtnNewDetail.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.BtnNewDetail.Image = ((System.Drawing.Image)(resources.GetObject("BtnNewDetail.Image")));
            this.BtnNewDetail.ImageActive = ((System.Drawing.Image)(resources.GetObject("BtnNewDetail.ImageActive")));
            this.BtnNewDetail.ImageLocation = null;
            this.BtnNewDetail.ImageMargin = 20;
            this.BtnNewDetail.ImageSize = new System.Drawing.Size(40, 40);
            this.BtnNewDetail.ImageZoomSize = new System.Drawing.Size(60, 60);
            this.BtnNewDetail.InitialImage = ((System.Drawing.Image)(resources.GetObject("BtnNewDetail.InitialImage")));
            this.BtnNewDetail.Location = new System.Drawing.Point(303, 29);
            this.BtnNewDetail.Name = "BtnNewDetail";
            this.BtnNewDetail.Rotation = 0;
            this.BtnNewDetail.ShowActiveImage = true;
            this.BtnNewDetail.ShowCursorChanges = true;
            this.BtnNewDetail.ShowImageBorders = true;
            this.BtnNewDetail.ShowSizeMarkers = false;
            this.BtnNewDetail.Size = new System.Drawing.Size(60, 60);
            this.BtnNewDetail.TabIndex = 27;
            this.BtnNewDetail.TabStop = false;
            this.BtnNewDetail.ToolTipText = "";
            this.BtnNewDetail.WaitOnLoad = false;
            this.BtnNewDetail.Zoom = 20;
            this.BtnNewDetail.ZoomSpeed = 10;
            this.BtnNewDetail.Click += new System.EventHandler(this.BtnNewDetail_Click);
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
            this.bunifuSeparator2.Size = new System.Drawing.Size(379, 1);
            this.bunifuSeparator2.TabIndex = 28;
            // 
            // Grid
            // 
            this.Grid.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
            this.Grid.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Grid.Header.Height = 16;
            this.Grid.Location = new System.Drawing.Point(0, 107);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(379, 156);
            this.Grid.TabIndex = 29;
            this.Grid.ColDividerDoubleClick += new TenTec.Windows.iGridLib.iGColDividerDoubleClickEventHandler(this.Grid_ColDividerDoubleClick);
            this.Grid.CellEllipsisButtonClick += new TenTec.Windows.iGridLib.iGEllipsisButtonClickEventHandler(this.Grid_CellEllipsisButtonClick);
            // 
            // FAttach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(379, 264);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.bunifuSeparator2);
            this.Controls.Add(this.BtnNewDetail);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.TxtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FAttach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FAttachment";
            this.Load += new System.EventHandler(this.FAttachment_Load);
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelHeader;
        private Bunifu.UI.WinForms.BunifuTextBox TxtName;
        private Bunifu.UI.WinForms.BunifuFormDock FDock;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator1;
        private Bunifu.UI.WinForms.BunifuImageButton BtnNewDetail;
        private Bunifu.UI.WinForms.BunifuImageButton BtnCerrar;
        private System.Windows.Forms.Label LabelPanel;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator2;
        private TenTec.Windows.iGridLib.iGrid Grid;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1DefaultCellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1DefaultColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1RowTextColCellStyle;
    }
}