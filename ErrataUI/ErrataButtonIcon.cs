using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI;

public class ErrataButtonIcon : Button
{
    private bool _ignoreRoles = true;
    [Browsable(true)]
    [Category("Theme Manager")]
    [Description("If true, role updates will be ignored, allowing individual theme application.")]
    public bool IgnoreRoles
    {
        get => _ignoreRoles;
        set
        {
            _ignoreRoles = value;
        }
    }

    private bool _ignoreTheme;
    [Browsable(true)]
    [Category("Theme Manager")]
    [Description("If true, color updates will be ignored, allowing manual color selection.")]
    public bool IgnoreTheme
    {
        get => _ignoreTheme;
        set
        {
            _ignoreTheme = value;
            UpdateColor();  // Update color immediately if the property changes
        }
    }


    private Color _defaultBackColor;
    private Color _defaultForeColor;
    private Color _entryForeColor;

    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public new FlatStyle FlatStyle
    //{
    //    get => base.FlatStyle;
    //    set
    //    {
    //        // Always force FlatStyle to Flat
    //        if (value != FlatStyle.Flat)
    //            throw new InvalidOperationException("FlatStyle cannot be modified. It is fixed to Flat.");
    //        base.FlatStyle = FlatStyle.Flat;
    //    }
    //}

    [Browsable(true)]
    [Category("Misc")]
    [Description("Gets or sets whether the control is selectable.")]
    public bool Selectable
    {
        get => (GetStyle(ControlStyles.Selectable));
        set
        {
            SetStyle(ControlStyles.Selectable, value);
            Invalidate(); // Force the control to redraw if needed
        }
    }


    private ButtonType _buttonType;
    [Browsable(true)]
    [Category("Misc")]
    [Description("Specifies the type of button.")]
    public ButtonType Type
    {
        get => _buttonType;
        set
        {
            _buttonType = value;
            UpdateText();
        }
    }


    [Category("Misc")]
    [Description("The background color of the button.")]
    public Color BackColor
    {
        get => base.BackColor;
        set
        {
            base.BackColor = value;
            _defaultBackColor = value;
        }
    }


    [Category("Misc")]
    [Description("The background color of the button when the mouse hovers over it.")]
    public Color MouseOverBackColor
    {
        get => FlatAppearance.MouseOverBackColor;
        set => FlatAppearance.MouseOverBackColor = value;
    }


    [Category("Misc")]
    [Description("The background color of the button when the mouse is pressed.")]
    public Color MouseDownBackColor
    {
        get => FlatAppearance.MouseDownBackColor;
        set => FlatAppearance.MouseDownBackColor = value;
    }


    private bool _ignoreMouseBackColor = false;
    [Category("Misc")]
    public bool IgnoreMouseBackColor
    {
        get => _ignoreMouseBackColor;
        set
        {
            _ignoreMouseBackColor = value;
        }
    }



    #region MOUSEOVERFORE
    //MOUSEOVERFORE COLOR
    private UIRole _mouseOverForeRole = UIRole.BodyTextL1;
    private ThemeColorShade _mouseOverForeTheme = ThemeColorShade.Neutral_400;
    private Color _mouseOverForeColor = Color.FromArgb(200, 200, 200);
    [Category("UIRole")]
    [Description("Implements role type.")]
    public UIRole MouseOverForeRole { get => _mouseOverForeRole; set { _mouseOverForeRole = value; } }
    [Category("Theme Manager")]
    [Description("Theme shade.")]
    public ThemeColorShade MouseOverForeTheme
    {
        get => _mouseOverForeTheme; set
        {
            _mouseOverForeTheme = value;
            if (!_ignoreTheme) { MouseOverForeColor = ThemeManager.Instance.GetThemeColorShade(_mouseOverForeTheme); }
        }
    }
    [Category("Misc")]
    [Description("Color.")]
    public Color MouseOverForeColor { get => _mouseOverForeColor; set { _mouseOverForeColor = value; Invalidate(); } }
    #endregion
    #region MOUSEDOWNFORE
    //MOUSEDOWNFORE COLOR
    private UIRole _mouseDownForeRole = UIRole.BodyTextL1;
    private ThemeColorShade _mouseDownForeTheme = ThemeColorShade.Neutral_200;
    private Color _mouseDownForeColor = Color.FromArgb(200, 200, 200);
    [Category("UIRole")]
    [Description("Implements role type.")]
    public UIRole MouseDownForeRole { get => _mouseDownForeRole; set { _mouseDownForeRole = value; } }
    [Category("Theme Manager")]
    [Description("Theme shade.")]
    public ThemeColorShade MouseDownForeTheme
    {
        get => _mouseDownForeTheme; set
        {
            _mouseDownForeTheme = value;
            if (!_ignoreTheme) { MouseDownForeColor = ThemeManager.Instance.GetThemeColorShade(_mouseDownForeTheme); }
        }
    }
    [Category("Misc")]
    [Description("Color.")]
    public Color MouseDownForeColor { get => _mouseDownForeColor; set { _mouseDownForeColor = value; Invalidate(); } }
    #endregion
    private bool _ignoreMouseForeColor = true;
    [Category("Misc")]
    public bool IgnoreMouseForeColor
    {
        get => _ignoreMouseForeColor;
        set
        {
            _ignoreMouseForeColor = value;
        }
    }

    #region FORECOLOR
    //FORECOLOR
    private UIRole _foreColorRole = UIRole.BodyTextL1;
    private ThemeColorShade _foreColorTheme = ThemeColorShade.Neutral_800;
    private Color _foreColor = Color.FromArgb(50, 49, 48);
    [Category("UIRole")]
    [Description("Implements role type.")]
    public UIRole ForeColorRole { get => _foreColorRole; set { _foreColorRole = value; } }
    [Category("Theme Manager")]
    [Description("Theme shade.")]
    public ThemeColorShade ForeColorTheme
    {
        get => _foreColorTheme; set
        {
            _foreColorTheme = value;
            if (!_ignoreTheme) { ForeColor = ThemeManager.Instance.GetThemeColorShade(_foreColorTheme); }
        }
    }
    [Category("Misc")]
    [Description("Color.")]
    public Color ForeColor { get => _foreColor; set { _foreColor = value; base.ForeColor = value; Invalidate(); } }

    //ADD TO UPDATECOLOR METHOD
    ////ForeColor = ThemeManager.Instance.GetThemeColorShade(ForeColorTheme);
    #endregion



    [Category("Misc")]
    [Description("The border size of the button.")]
    public int BorderSize
    {
        get => FlatAppearance.BorderSize;
        set => FlatAppearance.BorderSize = value;
    }


    [Category("Misc")]
    [Description("The border color of the button.")]
    public Color BorderColor
    {
        get => FlatAppearance.BorderColor;
        set => FlatAppearance.BorderColor = value;
    }


    private void UpdateColor()
    {
        if (!_ignoreTheme)
        {
            MouseOverForeColor = ThemeManager.Instance.GetThemeColorShade(MouseOverForeTheme);
            MouseDownForeColor = ThemeManager.Instance.GetThemeColorShade(MouseDownForeTheme);
            ForeColor = ThemeManager.Instance.GetThemeColorShade(ForeColorTheme);
        }
    }



    public ErrataButtonIcon()
    {
        ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();

        FlatStyle = FlatStyle.Flat;
        SetStyle(ControlStyles.Selectable, false); //Prevents the buttons from being selectable BUT ALSO stops icon movement during click
        FlatAppearance.BorderSize = 0;
        //BackColor = Color.Transparent;
        //_defaultBackColor = BackColor;
        ForeColor = Color.FromArgb(50, 49, 48);
        _defaultForeColor = ForeColor;
        Size = new Size(32, 25);
        Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
        //BackColor = BackColor;
        AutoSize = false;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Text = "";
        Padding = new Padding(0, 0, 0, 0);

        //MouseEnter += (s, e) => BackColor = HoverBackgroundColor;
        //MouseLeave += (s, e) => BackColor = _defaultBackColor;
        //MouseDown += (s, e) => BackColor = PressedBackgroundColor;
        //MouseUp += (s, e) => BackColor = HoverBackgroundColor;
    }




    public enum ButtonType
    {
        Default,
        Close,
        Maximize,
        Restore,
        Minimize,
        CloseSmall,
        MaximizeSmall,
        RestoreSmall,
        MinimizeSmall,
        Plus,
        Minus,
        Multiply,
        Divide,
        Root,
        Percent,
        PlusMinus,
        Equal,
        Search,
        Zoom,
        ZoomIn,
        ZoomOut,
        Setting,
        Edit,
        Filter,
        GlobalNav,
        AddTo,
        RemoveFrom,
        Error,
        Circle,
        ToggleOff,
        ToggleOn,
        RadioOff,
        RadioOn,
        CheckOff,
        CheckOn,
        CheckFill,
        CheckMix,
        ArrowUp,
        ArrowRight,
        ArrowDown,
        ArrowLeft,
        ChevronUp,
        ChevronRight,
        ChevronDown,
        ChevronLeft,
        Sync,
        Expand,
        None
    }

    private void UpdateText()
    {
        // Update the Text property based on the ButtonType
        switch (_buttonType)
        {
            case ButtonType.Default:
                Text = ""; // MDL deprecated symbol
                Font = new Font("Segoe MDL2 Assets", 11, FontStyle.Regular);
                Padding = new Padding(0, 0, 0, 0);
                break;
            case ButtonType.Close:
                Text = ""; // MDL2 close symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 0, 0);
                break;
            case ButtonType.Maximize:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Restore:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Minimize:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 2, 1, 0);
                break;
            case ButtonType.CloseSmall:
                Text = ""; // MDL2 close symbol
                Font = new Font("Segoe MDL2 Assets", 9, FontStyle.Regular);
                Padding = new Padding(1, 0, 0, 0);
                break;
            case ButtonType.MaximizeSmall:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 9, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.RestoreSmall:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 9, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.MinimizeSmall:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 9, FontStyle.Regular);
                Padding = new Padding(1, 2, 1, 0);
                break;
            case ButtonType.Plus:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Minus:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 1, 1, 0);
                break;
            case ButtonType.Multiply:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Divide:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Root:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Percent:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.PlusMinus:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Equal:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 1, 1, 0);
                break;
            case ButtonType.Search:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Zoom:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ZoomIn:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ZoomOut:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Setting:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 13, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Edit:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Filter:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.GlobalNav:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.AddTo:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 13, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.RemoveFrom:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 13, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Error:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Circle:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ToggleOff:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ToggleOn:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.RadioOff:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.RadioOn:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.CheckOff:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.CheckOn:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.CheckFill:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.CheckMix:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ArrowUp:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ArrowRight:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ArrowDown:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ArrowLeft:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ChevronUp:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ChevronRight:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ChevronDown:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.ChevronLeft:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Sync:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.Expand:
                Text = ""; // MDL2 symbol
                Font = new Font("Segoe MDL2 Assets", 12, FontStyle.Regular);
                Padding = new Padding(1, 0, 1, 0);
                break;
            case ButtonType.None:
                break;
                // - circling arrows
        }
    }


    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);

        
    }

    protected override void OnMouseDown(MouseEventArgs mevent)
    {
        base.OnMouseDown(mevent);

        if (IgnoreMouseBackColor == false)
        {
            if (FlatStyle == FlatStyle.Flat)
            {
                // Change the background color when the button is pressed
                if (FlatAppearance.MouseDownBackColor == Color.Empty)
                {
                    FlatAppearance.MouseDownBackColor = ControlPaint.Dark(_defaultBackColor, 0.01f); // Darken the color for click

                }
            }
        }
        else if (IgnoreMouseBackColor == true)
        {
            FlatAppearance.MouseDownBackColor = BackColor;
        }



        if (IgnoreMouseForeColor == false)
        {
            if (_mouseDownForeColor != Color.Empty) 
            {
                ForeColor = _mouseDownForeColor;
            }
        }
        
        Invalidate(); // Force repaint without visual offset
    }
    protected override void OnMouseEnter(EventArgs e)
    {
        //the base flat style button mouse over event with no color set = Control.FlatAppearance.MouseOverBackColor
        base.OnMouseEnter(e);

        _entryForeColor = ForeColor;

        if (IgnoreMouseBackColor == false)
        {
            if (FlatAppearance.MouseOverBackColor == Color.Empty)
            {
                using (Graphics graphics = CreateGraphics())
                {
                    Color nearestColor = graphics.GetNearestColor(_defaultBackColor);
                    BackColor = nearestColor;
                }
            }
        }
        else if (IgnoreMouseBackColor == true)
        {
            FlatAppearance.MouseOverBackColor = BackColor;
        }



        if (IgnoreMouseForeColor == false)
        {
            if (_mouseOverForeColor != Color.Empty)
            {
                ForeColor = _mouseOverForeColor;
            }
        }

        Invalidate();
    }



    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        ForeColor = _entryForeColor;//ThemeManager.Instance.GetThemeColorShade(ForeColorTheme);
        Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        base.OnMouseUp(mevent);
        ForeColor = ThemeManager.Instance.GetThemeColorShade(MouseOverForeTheme);
        Invalidate();
    }
}