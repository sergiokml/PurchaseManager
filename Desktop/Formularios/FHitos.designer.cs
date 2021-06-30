
namespace PurchaseDesktop.Formularios
{
    partial class FHitos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FHitos));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.LabelPanel = new System.Windows.Forms.Label();
            this.BtnCerrar = new Bunifu.UI.WinForms.BunifuImageButton();
            this.FDock = new Bunifu.UI.WinForms.BunifuFormDock();
            this.bunifuSeparator1 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.BtnNewDetail = new Bunifu.UI.WinForms.BunifuImageButton();
            this.bunifuSeparator2 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.Grid = new TenTec.Windows.iGridLib.iGrid();
            this.CboDays = new System.Windows.Forms.ComboBox();
            this.TxtDescription = new Bunifu.UI.WinForms.BunifuTextBox();
            this.LblMensaje = new System.Windows.Forms.Label();
            this.TrackBar = new System.Windows.Forms.TrackBar();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar)).BeginInit();
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
            this.LabelPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPanel.ForeColor = System.Drawing.Color.White;
            this.LabelPanel.Location = new System.Drawing.Point(0, 0);
            this.LabelPanel.Name = "LabelPanel";
            this.LabelPanel.Size = new System.Drawing.Size(87, 25);
            this.LabelPanel.TabIndex = 28;
            this.LabelPanel.Text = "Hitos";
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
            this.BtnNewDetail.ImageSize = new System.Drawing.Size(35, 35);
            this.BtnNewDetail.ImageZoomSize = new System.Drawing.Size(55, 55);
            this.BtnNewDetail.InitialImage = ((System.Drawing.Image)(resources.GetObject("BtnNewDetail.InitialImage")));
            this.BtnNewDetail.Location = new System.Drawing.Point(312, 43);
            this.BtnNewDetail.Name = "BtnNewDetail";
            this.BtnNewDetail.Rotation = 0;
            this.BtnNewDetail.ShowActiveImage = true;
            this.BtnNewDetail.ShowCursorChanges = true;
            this.BtnNewDetail.ShowImageBorders = true;
            this.BtnNewDetail.ShowSizeMarkers = false;
            this.BtnNewDetail.Size = new System.Drawing.Size(55, 55);
            this.BtnNewDetail.TabIndex = 27;
            this.BtnNewDetail.TabStop = false;
            this.BtnNewDetail.ToolTipText = "Save File";
            this.BtnNewDetail.WaitOnLoad = false;
            this.BtnNewDetail.Zoom = 20;
            this.BtnNewDetail.ZoomSpeed = 10;
            this.BtnNewDetail.Click += new System.EventHandler(this.BtnNewHito_Click);
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
            this.Grid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Grid.Header.Height = 16;
            this.Grid.Location = new System.Drawing.Point(0, 122);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(379, 141);
            this.Grid.TabIndex = 29;
            this.Grid.ColDividerDoubleClick += new TenTec.Windows.iGridLib.iGColDividerDoubleClickEventHandler(this.Grid_ColDividerDoubleClick);
            // 
            // CboDays
            // 
            this.CboDays.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CboDays.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CboDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboDays.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.CboDays.FormattingEnabled = true;
            this.CboDays.Location = new System.Drawing.Point(242, 43);
            this.CboDays.Name = "CboDays";
            this.CboDays.Size = new System.Drawing.Size(55, 24);
            this.CboDays.TabIndex = 34;
            // 
            // TxtDescription
            // 
            this.TxtDescription.AcceptsReturn = false;
            this.TxtDescription.AcceptsTab = false;
            this.TxtDescription.AnimationSpeed = 200;
            this.TxtDescription.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.TxtDescription.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.TxtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            this.TxtDescription.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TxtDescription.BackgroundImage")));
            this.TxtDescription.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.TxtDescription.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.TxtDescription.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.TxtDescription.BorderColorIdle = System.Drawing.Color.Silver;
            this.TxtDescription.BorderRadius = 1;
            this.TxtDescription.BorderThickness = 1;
            this.TxtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TxtDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtDescription.DefaultFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.TxtDescription.DefaultText = "";
            this.TxtDescription.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            this.TxtDescription.HideSelection = true;
            this.TxtDescription.IconLeft = null;
            this.TxtDescription.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtDescription.IconPadding = 10;
            this.TxtDescription.IconRight = null;
            this.TxtDescription.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtDescription.Lines = new string[0];
            this.TxtDescription.Location = new System.Drawing.Point(12, 43);
            this.TxtDescription.MaxLength = 20;
            this.TxtDescription.MinimumSize = new System.Drawing.Size(1, 1);
            this.TxtDescription.Modified = false;
            this.TxtDescription.Multiline = false;
            this.TxtDescription.Name = "TxtDescription";
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtDescription.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.TxtDescription.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtDescription.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtDescription.OnIdleState = stateProperties8;
            this.TxtDescription.Padding = new System.Windows.Forms.Padding(3);
            this.TxtDescription.PasswordChar = '\0';
            this.TxtDescription.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.TxtDescription.PlaceholderText = "Enter Description";
            this.TxtDescription.ReadOnly = false;
            this.TxtDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtDescription.SelectedText = "";
            this.TxtDescription.SelectionLength = 0;
            this.TxtDescription.SelectionStart = 0;
            this.TxtDescription.ShortcutsEnabled = true;
            this.TxtDescription.Size = new System.Drawing.Size(212, 25);
            this.TxtDescription.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Material;
            this.TxtDescription.TabIndex = 36;
            this.TxtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtDescription.TextMarginBottom = 0;
            this.TxtDescription.TextMarginLeft = 1;
            this.TxtDescription.TextMarginTop = 0;
            this.TxtDescription.TextPlaceholder = "Enter Description";
            this.TxtDescription.UseSystemPasswordChar = false;
            this.TxtDescription.WordWrap = true;
            // 
            // LblMensaje
            // 
            this.LblMensaje.AutoSize = true;
            this.LblMensaje.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(196)))), ((int)(((byte)(85)))));
            this.LblMensaje.Location = new System.Drawing.Point(248, 86);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(43, 19);
            this.LblMensaje.TabIndex = 38;
            this.LblMensaje.Text = "0 %";
            // 
            // TrackBar
            // 
            this.TrackBar.Location = new System.Drawing.Point(12, 74);
            this.TrackBar.Name = "TrackBar";
            this.TrackBar.Size = new System.Drawing.Size(212, 45);
            this.TrackBar.TabIndex = 39;
            this.TrackBar.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // FHitos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(379, 264);
            this.Controls.Add(this.TrackBar);
            this.Controls.Add(this.LblMensaje);
            this.Controls.Add(this.TxtDescription);
            this.Controls.Add(this.CboDays);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.bunifuSeparator2);
            this.Controls.Add(this.BtnNewDetail);
            this.Controls.Add(this.bunifuSeparator1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FHitos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FAttachment";
            this.Load += new System.EventHandler(this.FAttachment_Load);
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PanelHeader;
        private Bunifu.UI.WinForms.BunifuFormDock FDock;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator1;
        private Bunifu.UI.WinForms.BunifuImageButton BtnNewDetail;
        private Bunifu.UI.WinForms.BunifuImageButton BtnCerrar;
        private System.Windows.Forms.Label LabelPanel;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator2;
        private TenTec.Windows.iGridLib.iGrid Grid;
        private System.Windows.Forms.ComboBox CboDays;
        private Bunifu.UI.WinForms.BunifuTextBox TxtDescription;
        private System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.TrackBar TrackBar;
    }
}