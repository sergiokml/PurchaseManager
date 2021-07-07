
namespace PurchaseDesktop.Formularios
{
    partial class FDeliverys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDeliverys));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.LabelPanel = new System.Windows.Forms.Label();
            this.BtnCerrar = new Bunifu.UI.WinForms.BunifuImageButton();
            this.FDock = new Bunifu.UI.WinForms.BunifuFormDock();
            this.bunifuSeparator1 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.BtnNewNote = new Bunifu.UI.WinForms.BunifuImageButton();
            this.bunifuSeparator2 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.Grid = new TenTec.Windows.iGridLib.iGrid();
            this.iGrid1DefaultCellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.iGrid1DefaultColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.iGrid1RowTextColCellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.TxtTitle = new Bunifu.UI.WinForms.BunifuTextBox();
            this.DatePicker = new Bunifu.UI.WinForms.BunifuDatePicker();
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
            this.LabelPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPanel.ForeColor = System.Drawing.Color.White;
            this.LabelPanel.Location = new System.Drawing.Point(0, 0);
            this.LabelPanel.Name = "LabelPanel";
            this.LabelPanel.Size = new System.Drawing.Size(99, 25);
            this.LabelPanel.TabIndex = 28;
            this.LabelPanel.Text = "Delivery Dates";
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
            this.bunifuSeparator1.Location = new System.Drawing.Point(0, 201);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Orientation = Bunifu.UI.WinForms.BunifuSeparator.LineOrientation.Horizontal;
            this.bunifuSeparator1.Size = new System.Drawing.Size(379, 1);
            this.bunifuSeparator1.TabIndex = 10;
            // 
            // BtnNewNote
            // 
            this.BtnNewNote.ActiveImage = ((System.Drawing.Image)(resources.GetObject("BtnNewNote.ActiveImage")));
            this.BtnNewNote.AllowAnimations = true;
            this.BtnNewNote.AllowBuffering = false;
            this.BtnNewNote.AllowToggling = false;
            this.BtnNewNote.AllowZooming = true;
            this.BtnNewNote.AllowZoomingOnFocus = false;
            this.BtnNewNote.BackColor = System.Drawing.Color.Transparent;
            this.BtnNewNote.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnNewNote.ErrorImage = ((System.Drawing.Image)(resources.GetObject("BtnNewNote.ErrorImage")));
            this.BtnNewNote.FadeWhenInactive = false;
            this.BtnNewNote.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.BtnNewNote.Image = ((System.Drawing.Image)(resources.GetObject("BtnNewNote.Image")));
            this.BtnNewNote.ImageActive = ((System.Drawing.Image)(resources.GetObject("BtnNewNote.ImageActive")));
            this.BtnNewNote.ImageLocation = null;
            this.BtnNewNote.ImageMargin = 20;
            this.BtnNewNote.ImageSize = new System.Drawing.Size(35, 35);
            this.BtnNewNote.ImageZoomSize = new System.Drawing.Size(55, 55);
            this.BtnNewNote.InitialImage = ((System.Drawing.Image)(resources.GetObject("BtnNewNote.InitialImage")));
            this.BtnNewNote.Location = new System.Drawing.Point(321, 24);
            this.BtnNewNote.Name = "BtnNewNote";
            this.BtnNewNote.Rotation = 0;
            this.BtnNewNote.ShowActiveImage = true;
            this.BtnNewNote.ShowCursorChanges = true;
            this.BtnNewNote.ShowImageBorders = true;
            this.BtnNewNote.ShowSizeMarkers = false;
            this.BtnNewNote.Size = new System.Drawing.Size(55, 55);
            this.BtnNewNote.TabIndex = 27;
            this.BtnNewNote.TabStop = false;
            this.BtnNewNote.ToolTipText = "Save File";
            this.BtnNewNote.WaitOnLoad = false;
            this.BtnNewNote.Zoom = 20;
            this.BtnNewNote.ZoomSpeed = 10;
            this.BtnNewNote.Click += new System.EventHandler(this.BtnNewNote_Click);
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
            this.Grid.Location = new System.Drawing.Point(0, 81);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(379, 120);
            this.Grid.TabIndex = 29;
            this.Grid.ColDividerDoubleClick += new TenTec.Windows.iGridLib.iGColDividerDoubleClickEventHandler(this.Grid_ColDividerDoubleClick);
            // 
            // TxtTitle
            // 
            this.TxtTitle.AcceptsReturn = false;
            this.TxtTitle.AcceptsTab = false;
            this.TxtTitle.AnimationSpeed = 200;
            this.TxtTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.TxtTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.TxtTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            this.TxtTitle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TxtTitle.BackgroundImage")));
            this.TxtTitle.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.TxtTitle.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.TxtTitle.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.TxtTitle.BorderColorIdle = System.Drawing.Color.Silver;
            this.TxtTitle.BorderRadius = 1;
            this.TxtTitle.BorderThickness = 1;
            this.TxtTitle.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TxtTitle.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtTitle.DefaultFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.TxtTitle.DefaultText = "";
            this.TxtTitle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            this.TxtTitle.HideSelection = true;
            this.TxtTitle.IconLeft = null;
            this.TxtTitle.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtTitle.IconPadding = 10;
            this.TxtTitle.IconRight = null;
            this.TxtTitle.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtTitle.Lines = new string[0];
            this.TxtTitle.Location = new System.Drawing.Point(12, 39);
            this.TxtTitle.MaxLength = 50;
            this.TxtTitle.MinimumSize = new System.Drawing.Size(1, 1);
            this.TxtTitle.Modified = false;
            this.TxtTitle.Multiline = false;
            this.TxtTitle.Name = "TxtTitle";
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtTitle.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.TxtTitle.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtTitle.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.TxtTitle.OnIdleState = stateProperties8;
            this.TxtTitle.Padding = new System.Windows.Forms.Padding(3);
            this.TxtTitle.PasswordChar = '\0';
            this.TxtTitle.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.TxtTitle.PlaceholderText = "";
            this.TxtTitle.ReadOnly = false;
            this.TxtTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtTitle.SelectedText = "";
            this.TxtTitle.SelectionLength = 0;
            this.TxtTitle.SelectionStart = 0;
            this.TxtTitle.ShortcutsEnabled = true;
            this.TxtTitle.Size = new System.Drawing.Size(187, 25);
            this.TxtTitle.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Material;
            this.TxtTitle.TabIndex = 31;
            this.TxtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtTitle.TextMarginBottom = 0;
            this.TxtTitle.TextMarginLeft = 1;
            this.TxtTitle.TextMarginTop = 0;
            this.TxtTitle.TextPlaceholder = "";
            this.TxtTitle.UseSystemPasswordChar = false;
            this.TxtTitle.WordWrap = true;
            // 
            // DatePicker
            // 
            this.DatePicker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(106)))), ((int)(((byte)(112)))));
            this.DatePicker.BorderRadius = 1;
            this.DatePicker.Color = System.Drawing.Color.Silver;
            this.DatePicker.CustomFormat = "dd-MM-yyyy";
            this.DatePicker.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thin;
            this.DatePicker.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Left;
            this.DatePicker.DisabledColor = System.Drawing.Color.Gray;
            this.DatePicker.DisplayWeekNumbers = false;
            this.DatePicker.DPHeight = 0;
            this.DatePicker.FillDatePicker = false;
            this.DatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DatePicker.ForeColor = System.Drawing.Color.Black;
            this.DatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker.Icon = ((System.Drawing.Image)(resources.GetObject("DatePicker.Icon")));
            this.DatePicker.IconColor = System.Drawing.Color.Gray;
            this.DatePicker.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.Right;
            this.DatePicker.LeftTextMargin = 5;
            this.DatePicker.Location = new System.Drawing.Point(205, 35);
            this.DatePicker.MinimumSize = new System.Drawing.Size(0, 32);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(113, 32);
            this.DatePicker.TabIndex = 32;
            this.DatePicker.Value = new System.DateTime(2021, 7, 6, 18, 46, 0, 0);
            // 
            // FDeliverys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(379, 202);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.TxtTitle);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.bunifuSeparator2);
            this.Controls.Add(this.BtnNewNote);
            this.Controls.Add(this.bunifuSeparator1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FDeliverys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FAttachment";
            this.Load += new System.EventHandler(this.FAttachment_Load);
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelHeader;
        private Bunifu.UI.WinForms.BunifuFormDock FDock;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator1;
        private Bunifu.UI.WinForms.BunifuImageButton BtnNewNote;
        private Bunifu.UI.WinForms.BunifuImageButton BtnCerrar;
        private System.Windows.Forms.Label LabelPanel;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator2;
        private TenTec.Windows.iGridLib.iGrid Grid;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1DefaultCellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid1DefaultColHdrStyle;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid1RowTextColCellStyle;
        private Bunifu.UI.WinForms.BunifuTextBox TxtTitle;
        private Bunifu.UI.WinForms.BunifuDatePicker DatePicker;
    }
}