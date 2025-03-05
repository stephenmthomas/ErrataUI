using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using ErrataUI;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{


    public class ErrataForm : Form
    {
        private bool _ignoreRoles = true;
        [Category("Theme Manager"), Description("If true, role updates will be ignored, allowing individual theme application.")]
        public bool IgnoreRoles
        {
            get => _ignoreRoles;
            set
            {
                _ignoreRoles = value;
            }
        }

        private bool _ignoreTheme = false;  // New property to ignore theme updates
        [Category("Theme Manager"), Description("If true, color updates will be ignored, allowing manual color selection.")]
        public bool IgnoreTheme
        {
            get => _ignoreTheme;
            set
            {
                _ignoreTheme = value;
                UpdateColor();  // Update color immediately if the property changes
            }
        }

        private int _borderSize = 1;

        [Category("Layout")]
        public bool Sizable { get; set; }
        private int STATUS_BAR_HEIGHT = 24;
        private int ACTION_BAR_HEIGHT = 40;




        //TITLE BAR BUTTONS
        private ErrataButtonIcon btnClose;
        private ErrataButtonIcon btnMaximize;
        private ErrataButtonIcon btnRestore;
        private ErrataButtonIcon btnMinimize;
        private const int TitleBarButtonArea = 24;
        private const int TitleBarButtonSize = 20;
        private Rectangle _minButtonBounds => new Rectangle(ClientSize.Width - 3 * TitleBarButtonArea, ClientRectangle.Y + 2, 20, 20);
        private Rectangle _maxButtonBounds => new Rectangle(ClientSize.Width - 2 * TitleBarButtonArea, ClientRectangle.Y + 2, 20, 20);
        private Rectangle _xButtonBounds => new Rectangle(ClientSize.Width - TitleBarButtonArea, ClientRectangle.Y + 2, 20, 20);
        private Rectangle _statusBarBounds => new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientSize.Width, STATUS_BAR_HEIGHT);


        //TITLE BAR
        private int _titleBarHeight = 30;
        private string _titleText = "Errata Form";
        private Font _titleFont = new Font("Segoe UI Semibold", 9, FontStyle.Regular);
        private int _titleTop = 5;
        private int _titleRight = 8;
        private int _leftPadding = 8;
        private int _topPadding = 6;
        private float _letterSpacing = -3.5F;

        

        // Fields to support dragging the form
        private bool _isDragging = false;
        private Point _lastLocation;





        private bool _resizable = false;
        [Category("Misc")]
        [Description("Enables form resizing.")]
        public bool Resizable
        {
            get => _resizable;
            set
            {
                _resizable = value;
            }
        }

        


        [Category("Misc")]
        [Description("Sets the size of the border.")]
        public int FormBorderSize
        {
            get => _borderSize;
            set
            {
                _borderSize = value;
                Invalidate(); // Repaint the control when text changes
                this.Padding = new Padding(_borderSize, _titleBarHeight, _borderSize, _borderSize);
                UpdateButtonSizes();
            }
        }



        private bool _titleBarGradientFill = false;
        [Category("Misc")]
        [Description("Fills the caption bar and extension with a gradient.")]
        public bool TitleBarGradientFill
        {
            get => _titleBarGradientFill;
            set
            {
                _titleBarGradientFill = value;
                Invalidate(); // Repaint the control when color changes
            }
        }


        private bool _titleExtensionGradientFill = false;
        [Category("Misc")]
        [Description("Fills the caption bar and extension with a gradient.")]
        public bool TitleExtensionGradientFill
        {
            get => _titleExtensionGradientFill;
            set
            {
                _titleExtensionGradientFill = value;
                Invalidate(); // Repaint the control when color changes
            }
        }


        private float _titleBarGradientAngle = 0F;
        [Category("Misc")]
        [Description("Angle of the title bar gradient.")]
        public float TitleBarGradientAngle
        {
            get => _titleBarGradientAngle;
            set
            {
                _titleBarGradientAngle = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        private float _titleExtensionGradientAngle = 0F;
        [Category("Misc")]
        [Description("Angle of the extension bar gradient.")]
        public float TitleExtensionGradientAngle
        {
            get => _titleExtensionGradientAngle;
            set
            {
                _titleExtensionGradientAngle = value;
                Invalidate(); // Repaint the control when text changes
            }
        }








        [Category("Misc"), Description("Form title.")]
        public string FormTitle
        {
            get => _titleText;
            set
            {
                _titleText = value;
                Text = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        private bool _formTitleAutoShade = true;
        [Category("Misc"), Description("Auto colors the title text based on title bar color.")]
        public bool FormTitleAutoShade
        {
            get => _formTitleAutoShade;
            set
            {
                _formTitleAutoShade = value;
            }
        }


        [Category("Misc"), Description("Sets the height of the title bar.")]
        public int TitleBarHeight
        {
            get => _titleBarHeight;
            set
            {
                _titleBarHeight = value;
                _titleTop = (_titleBarHeight / 2) - 8;
                Invalidate(); // Repaint the control when text changes
                this.Padding = new Padding(_borderSize, _titleBarHeight, _borderSize, _borderSize);
                UpdateButtonSizes();
            }
        }

        private int _titleBarLeftBuffer = 0;
        [Category("Misc"), Description("Pixel offset for the title bar, icon, and text to the left.")]
        public int TitleBarLeftBuffer
        {
            get => _titleBarLeftBuffer;
            set
            {
                _titleBarLeftBuffer = value;
                Invalidate();
            }
        }

        private bool _titleExtension = false;
        [Category("Misc"), Description("Extends the title bar down the left side.")]
        public bool TitleExtension
        {
            get => _titleExtension;
            set
            {
                _titleExtension = value;
                if (_titleExtension == true) { TitleBarLeftBuffer = TitleExtensionWidth; Invalidate(); }
                else {  TitleBarLeftBuffer = 0; }
            }
        }

        private int _titleExtensionWidth = 50;
        [Category("Misc"), Description("Width of the title bar extension.")]
        public int TitleExtensionWidth
        {
            get => _titleExtensionWidth;
            set
            {
                _titleExtensionWidth = value;
                if (_titleExtension == true) { TitleBarLeftBuffer = TitleExtensionWidth; Invalidate(); }
            }
        }




        #region TITLE BAR
        //TITLE BAR COLOR
        private UIRole _titleBarRole = UIRole.TitleBar;
        private ThemeColorShade _titleBarTheme = ThemeColorShade.Primary_500;
        private Color _titleBarColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);
        [Category("UIRole")][Description("Implements role type.")]
        public UIRole TitleBarRole { get => _titleBarRole; set{_titleBarRole = value;}}
        [Category("Theme Manager")][Description("Theme shade.")]
        public ThemeColorShade TitleBarTheme{get => _titleBarTheme;set{_titleBarTheme = value;
                if (!_ignoreTheme) { TitleBarColor = ThemeManager.Instance.GetThemeColorShade(_titleBarTheme); UpdateTitleText(); }}}
        [Category("Misc")][Description("Color.")]
        public Color TitleBarColor{get => _titleBarColor;set{_titleBarColor = value;Invalidate();CheckTitleViz(); }}
        #endregion



        #region TITLEBARGRADIENTCOLOR
        //TITLEBARGRADIENTCOLOR
        private UIRole _titleBarColorGradientRole = UIRole.GeneralBorders;
        private ThemeColorShade _titleBarColorGradientTheme = ThemeColorShade.Primary_700;
        private Color _titleBarColorGradient = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);
        [Category("UIRole"), Description("Implements role type.")]
        public UIRole TitleBarColorGradientRole { get => _titleBarColorGradientRole; set { _titleBarColorGradientRole = value; } }
        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade TitleBarColorGradientTheme
        {
            get => _titleBarColorGradientTheme; set
            {
                _titleBarColorGradientTheme = value;
                if (!_ignoreTheme) { TitleBarColorGradient = ThemeManager.Instance.GetThemeColorShade(_titleBarColorGradientTheme); }
            }
        }

        [Category("Misc"), Description("Sets title gradient.")]
        public Color TitleBarColorGradient { get => _titleBarColorGradient; set { _titleBarColorGradient = value; Invalidate(); } }
        #endregion

        #region TITLEEXTENSIONCOLOR
        //TITLEEXTENSIONCOLOR
        private UIRole _titleExtensionColorRole = UIRole.GeneralBorders;
        private ThemeColorShade _titleExtensionColorTheme = ThemeColorShade.Primary_500;
        private Color _titleExtensionColor = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole TitleExtensionColorRole { get => _titleExtensionColorRole; set { _titleExtensionColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade TitleExtensionColorTheme
        {
            get => _titleExtensionColorTheme; set
            {
                _titleExtensionColorTheme = value;
                if (!_ignoreTheme) { TitleExtensionColor = ThemeManager.Instance.GetThemeColorShade(_titleExtensionColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color TitleExtensionColor { get => _titleExtensionColor; set { _titleExtensionColor = value; Invalidate(); } }
        #endregion
        #region TITLEEXTENSIONGRADIENTCOLOR
        //TITLEEXTENSIONGRADIENTCOLOR
        private UIRole _titleExtensionColorGradientRole = UIRole.GeneralBorders;
        private ThemeColorShade _titleExtensionColorGradientTheme = ThemeColorShade.Primary_700;
        private Color _titleExtensionColorGradient = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole TitleExtensionColorGradientRole { get => _titleExtensionColorGradientRole; set { _titleExtensionColorGradientRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade TitleExtensionColorGradientTheme
        {
            get => _titleExtensionColorGradientTheme; set
            {
                _titleExtensionColorGradientTheme = value;
                if (!_ignoreTheme) { TitleExtensionColorGradient = ThemeManager.Instance.GetThemeColorShade(_titleExtensionColorGradientTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color TitleExtensionColorGradient { get => _titleExtensionColorGradient; set { _titleExtensionColorGradient = value; Invalidate(); } }
        #endregion





        #region BORDER COLOR
        //BORDER COLOR
        private UIRole _borderRole = UIRole.GeneralBorders;
        private ThemeColorShade _borderTheme = ThemeColorShade.Neutral_500;
        private Color _borderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole BorderRole { get => _borderRole; set { _borderRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade BorderTheme
        {
            get => _borderTheme; set
            {
                _borderTheme = value;
                if (!_ignoreTheme) { BorderColor = ThemeManager.Instance.GetThemeColorShade(_borderTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }
        #endregion
        #region TITLETEXT
        //TITLETEXT COLOR
        private UIRole _titleTextRole = UIRole.TitleBarText;
        private ThemeColorShade _titleTextTheme = ThemeColorShade.Neutral_100;
        private Color _titleTextColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole TitleTextRole { get => _titleTextRole; set { _titleTextRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade TitleTextTheme
        {
            get => _titleTextTheme; set
            {
                _titleTextTheme = value;
                if (!_ignoreTheme) { TitleTextColor = ThemeManager.Instance.GetThemeColorShade(_titleTextTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color TitleTextColor { get => _titleTextColor; set { _titleTextColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////TitleTextColor = ThemeManager.Instance.GetThemeColorShade(TitleTextTheme);
        #endregion







        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.MainBackground;
        private ThemeColorShade _backColorTheme = ThemeColorShade.Neutral_100;
        private Color _backColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BackColorRole { get => _backColorRole; set { _backColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BackColorTheme
        {
            get => _backColorTheme; set
            {
                _backColorTheme = value;
                if (!_ignoreTheme) { BackColor = ThemeManager.Instance.GetThemeColorShade(_backColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public override Color BackColor { get => _backColor; set { _backColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion








        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                if (ThemeManager.Instance.IsDarkMode)
                {
                    TitleBarColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackColorTheme, 1);
                    BackColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackColorTheme);
                    BorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(BorderTheme, 4);
                    TitleTextColor = ThemeManager.Instance.GetThemeColorShadeOffset(TitleTextTheme, -10);
                    UpdateTitleText();
                    UpdateControlButtonColors();
                    return;
                }


                TitleBarColor = ThemeManager.Instance.GetThemeColorShade(TitleBarTheme);
                TitleBarColorGradient = ThemeManager.Instance.GetThemeColorShade(TitleBarColorGradientTheme);
                TitleExtensionColor = ThemeManager.Instance.GetThemeColorShade(TitleExtensionColorTheme);
                TitleExtensionColorGradient = ThemeManager.Instance.GetThemeColorShade(TitleExtensionColorGradientTheme);
                TitleTextColor = ThemeManager.Instance.GetThemeColorShade(TitleTextTheme);
                BackColor = ThemeManager.Instance.GetThemeColorShade(BackColorTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderTheme);
                

                

                UpdateTitleText();
                UpdateControlButtonColors();

            }
        }

        private void UpdateControlButtonColors()
        {
            //Set BackColor
            Color controlButtonMouse = ThemeManager.Instance.GetThemeColorShade(TitleBarTheme);

            btnClose.BackColor = Color.Transparent;
            btnClose.MouseOverBackColor = Color.FromArgb(255, 46, 42);
            btnClose.MouseDownBackColor = Color.FromArgb(255, 96, 92);
            btnClose.ForeColorTheme = CheckTitleVizTCS();

            btnMaximize.BackColor = Color.Transparent; //0, 128, 200
            btnMaximize.MouseOverBackColor = ControlPaint.Light(controlButtonMouse, 0.35f);
            btnMaximize.MouseDownBackColor = ControlPaint.Light(controlButtonMouse, 0.15f);
            btnMaximize.ForeColorTheme = CheckTitleVizTCS();

            btnRestore.BackColor = Color.Transparent;
            btnRestore.MouseOverBackColor = ControlPaint.Light(controlButtonMouse, 0.35f);
            btnRestore.MouseDownBackColor = ControlPaint.Light(controlButtonMouse, 0.15f);
            btnRestore.ForeColorTheme = CheckTitleVizTCS();

            btnMinimize.BackColor = Color.Transparent;
            btnMinimize.MouseOverBackColor = ControlPaint.Light(controlButtonMouse, 0.35f);
            btnMinimize.MouseDownBackColor = ControlPaint.Light(controlButtonMouse, 0.15f);
            btnMinimize.ForeColorTheme = CheckTitleVizTCS();
        }

        private void UpdateTitleText()
        {        
            if (FormTitleAutoShade)
            {
                TitleTextColor = CheckTitleViz();
                btnClose.ForeColor = TitleTextColor;
                btnMaximize.ForeColor = TitleTextColor;
                btnMinimize.ForeColor = TitleTextColor;
                btnClose.ForeColor = TitleTextColor;
            }
            else
            {
                TitleTextColor = ThemeManager.Instance.GetThemeColorShade(TitleTextTheme);
            }

        }

        private Color CheckTitleViz()
        {
            Color newColor = ThemeManager.IsColorDark(_titleBarColor) ? 
                ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100) :
                ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

            return newColor;
        }

        private ThemeColorShade CheckTitleVizTCS()
        {
            ThemeColorShade newTCS = ThemeManager.IsColorDark(_titleBarColor) ? ThemeColorShade.Neutral_100 : ThemeColorShade.Neutral_800;

            return newTCS;
        }



        public new bool MaximizeBox
        {
            get => base.MaximizeBox;
            set
            {
                base.MaximizeBox = value;
                UpdateButtonSizes();
                Invalidate();
            }
        }

        public new bool MinimizeBox
        {
            get => base.MinimizeBox;
            set
            {
                base.MinimizeBox = value;
                UpdateButtonSizes();
                Invalidate();
            }
        }


        private void InitializeComponent()
        {


            SuspendLayout();
            // 
            // ErrataForm
            // 
            ClientSize = new Size(284, 261);
            Name = "ErrataForm";
            Load += ErrataForm_Load;
            ResumeLayout(false);
            
        }







        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            btnMaximize.Visible = false;
            btnRestore.Visible = true;
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnRestore.Visible = false;
            btnMaximize.Visible = true;
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void UpdateButtonSizes()
        {

            UpdateTitleText();

            // Adjust button sizes
            foreach (Control control in Controls)
            {
                if (control is ErrataButtonIcon button)
                {
                    button.Size = new Size(32, TitleBarHeight);
                    //button.ForeColor = btnTextColor;
                }
            }
            DrawFormButtons();
        }

        private void DrawFormButtons()
        {
            

            int bWidth = 32;
            int btnClose_x = 0;
            int btnMax_x;
            int btnMin_x;

            if (ControlBox == false)
            {
                btnClose.Visible = false; btnMaximize.Visible = false; btnRestore.Visible = false; btnMinimize.Visible = false;
                btnMin_x = -64;
                btnClose_x = -64;
                btnMax_x = -64;
            }
            else
            {
                btnClose.Visible = true; btnMaximize.Visible = true; btnRestore.Visible = false; btnMinimize.Visible = true;
                btnClose_x = 0;
            }

            if (MaximizeBox == true) { btnMax_x = 64; btnMin_x = 96; } else { btnMin_x = 64; btnMax_x = -64; btnMaximize.Visible = false; btnRestore.Visible = false; }
            if (MinimizeBox == false) { btnMin_x = -64; btnMinimize.Visible = false; }

            // Set button positions
            btnClose.Location = new Point(ClientSize.Width - bWidth + btnClose_x - FormBorderSize, FormBorderSize);
            btnMaximize.Location = new Point(ClientSize.Width - btnMax_x - FormBorderSize, FormBorderSize);
            btnRestore.Location = new Point(ClientSize.Width - btnMax_x - FormBorderSize, FormBorderSize); // Initially hidden
            btnMinimize.Location = new Point(ClientSize.Width - btnMin_x - FormBorderSize, FormBorderSize);
            Invalidate();
        }


        // Constructor for the custom form
        public ErrataForm()
        {
            ThemeManager.Instance.RefreshTheme();
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.FormBorderStyle = FormBorderStyle.None;



            

            // Initialize buttons
            btnClose = new ErrataButtonIcon { Type = ErrataButtonIcon.ButtonType.CloseSmall };
            btnMaximize = new ErrataButtonIcon { Type = ErrataButtonIcon.ButtonType.MaximizeSmall };
            btnRestore = new ErrataButtonIcon { Type = ErrataButtonIcon.ButtonType.RestoreSmall };
            btnMinimize = new ErrataButtonIcon { Type = ErrataButtonIcon.ButtonType.MinimizeSmall };


            //Set BackColor
            Color controlButtonMouse = ThemeManager.Instance.Primary;

            btnClose.BackColor = Color.Transparent;
            btnClose.MouseOverBackColor = Color.FromArgb(255, 46, 42);
            btnClose.MouseDownBackColor = Color.FromArgb(255, 96, 92);

            btnMaximize.BackColor =  Color.Transparent; //0, 128, 200
            btnMaximize.MouseOverBackColor = ControlPaint.Light(controlButtonMouse, 0.35f);
            btnMaximize.MouseDownBackColor = ControlPaint.Light(controlButtonMouse, 0.15f);

            btnRestore.BackColor =  Color.Transparent;
            btnRestore.MouseOverBackColor = ControlPaint.Light(controlButtonMouse, 0.35f);
            btnRestore.MouseDownBackColor = ControlPaint.Light(controlButtonMouse, 0.15f);

            btnMinimize.BackColor =  Color.Transparent;
            btnMinimize.MouseOverBackColor = ControlPaint.Light(controlButtonMouse, 0.35f);
            btnMinimize.MouseDownBackColor = ControlPaint.Light(controlButtonMouse, 0.15f);

            // Add buttons to form
            Controls.Add(btnClose);
            Controls.Add(btnMaximize);
            Controls.Add(btnRestore);
            Controls.Add(btnMinimize);
            // Hook up events
            btnClose.Click += BtnClose_Click;
            btnMaximize.Click += BtnMaximize_Click;
            btnRestore.Click += BtnRestore_Click;
            btnMinimize.Click += BtnMinimize_Click;

            UpdateButtonSizes();

            // Set default properties for the form
            FormBorderStyle = FormBorderStyle.None;
            Sizable = true;
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Padding = new Padding(1, _titleBarHeight, 1, 1);
            Size = new Size(800, 600);               // Set default size
            _titleText = Text;


            // Enable mouse dragging
            this.MouseDown += ModernFlatForm_MouseDown;
            this.MouseMove += ModernFlatForm_MouseMove;
            this.MouseUp += ModernFlatForm_MouseUp;

            // Set styles to make the form look modern and flat
            this.UpdateStyles();
            UpdateButtonSizes();
        }




        // Event handler for mouse down event to start dragging
        private void ModernFlatForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _lastLocation = e.Location;
            }
        }

        // Event handler for mouse move event to drag the form
        private void ModernFlatForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                var offset = new Point(e.Location.X - _lastLocation.X, e.Location.Y - _lastLocation.Y);
                this.Location = new Point(this.Location.X + offset.X, this.Location.Y + offset.Y);
            }
        }

        // Event handler for mouse up event to stop dragging
        private void ModernFlatForm_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateButtonSizes();


        }


        #region PAINT METHODS
        // Draw the custom header and body for the form
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (BackgroundImage == null)
            {
                e.Graphics.Clear(BackColor);
            }

            //e.Graphics.Clear(BackColor);
            
            PaintBody(e.Graphics);
            DrawTitleBar(e.Graphics);
            DrawFormBorder(e.Graphics);
            DrawFormIcon(e.Graphics);
            DrawTitleText(e.Graphics);
        }

        private void DrawTitleText(Graphics g)
        {
            
            // Define the bounds for the text
            Rectangle textBounds = new Rectangle(_titleRight + _titleBarLeftBuffer, _titleTop, Width - _leftPadding - _topPadding, _titleBarHeight - _topPadding);

            // Set text formatting options
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near; // Align text to the left
                sf.LineAlignment = StringAlignment.Center; // Vertically center the text
                sf.Trimming = StringTrimming.EllipsisCharacter; // Handle overflow with ellipsis

                // Draw the title text
                using (Brush textBrush = new SolidBrush(_titleTextColor))
                {

                    float letterSpacing = _letterSpacing; // Adjust this value for desired spacing
                    float x = _titleRight + FormBorderSize + _titleBarLeftBuffer; // Starting X position
                    float y = _titleTop + _borderSize;  // Starting Y position
                    if (ShowIcon == true) { x += 18; }

                    string sTitleText = _titleText.Replace(" ", "         ");

                    foreach (char c in sTitleText)
                    {
                        string character = c.ToString();
                        SizeF charSize = g.MeasureString(character, _titleFont);
                        g.DrawString(character, _titleFont, textBrush, new PointF(x, y));

                        // Adjust X position based on character width + spacing
                        x += charSize.Width + letterSpacing;

                        //g.DrawString(_groupBoxText, _groupBoxFont, textBrush, textRect);
                    }
                }
            }

            
        }

        private void DrawFormIcon(Graphics g)
        {
            if (ShowIcon == false) { return; }
            if (this.Icon != null)
            {
                // Define the icon bounds
                Rectangle iconBounds = new Rectangle(4 + _borderSize + _titleBarLeftBuffer, _borderSize + (_titleBarHeight - 15) / 2, 16, 16); // Icon is 16x16 pixels

                // Draw the icon
                g.DrawIcon(this.Icon, iconBounds);
            }
        }

        private void DrawTitleBar(Graphics g)
        {
            Rectangle headerRect = new Rectangle(_borderSize + _titleBarLeftBuffer, _borderSize, this.Width - _borderSize, _titleBarHeight);
            Rectangle extensionRect = new Rectangle(_borderSize, _borderSize, _titleExtensionWidth , this.Height);



            if (_titleBarGradientFill)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(headerRect, _titleBarColor, _titleBarColorGradient, _titleBarGradientAngle))
                {
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillRectangle(brush, headerRect);
                }
            }
            else
            {
                using (Brush headerBrush = new SolidBrush(_titleBarColor))
                {
                    g.FillRectangle(headerBrush, headerRect);
                }
            }

            if (TitleExtension == true) 
            {
                if (_titleExtensionGradientFill)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(extensionRect, _titleExtensionColor, _titleExtensionColorGradient, _titleExtensionGradientAngle))
                    {
                        //g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.FillRectangle(brush, extensionRect);
                    }
                }
                else
                {
                    using (Brush headerBrush = new SolidBrush(_titleExtensionColor))
                    {
                        g.FillRectangle(headerBrush, extensionRect);
                    }
                }
            }
            
        }
        private void DrawFormBorder(Graphics g)
        {
            using (Pen borderPen = new Pen(_borderColor, _borderSize))
            {
                g.DrawRectangle(borderPen, _borderSize / 2, _borderSize / 2, this.Width - _borderSize, this.Height - _borderSize);
            }
        }

        private void PaintBody(Graphics g)
        {

            if (BackgroundImage == null)
            {
                // Draw the body area
                Rectangle bodyRect = new Rectangle(_borderSize, _borderSize + _titleBarHeight, this.Width - _borderSize, this.Height - _titleBarHeight);
                using (Brush bodyBrush = new SolidBrush(BackColor))
                {
                    g.FillRectangle(bodyBrush, bodyRect);
                }
            }
            else
            {
                
            }

            // Draw the body area
            //Rectangle bodyRect = new Rectangle(_borderSize, _borderSize + _titleBarHeight, this.Width - _borderSize, this.Height - _titleBarHeight);
            //using (Brush bodyBrush = new SolidBrush(BackColor))
            //{
            //    g.FillRectangle(bodyBrush, bodyRect);
            //}

        }
        #endregion


        private void ErrataForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
