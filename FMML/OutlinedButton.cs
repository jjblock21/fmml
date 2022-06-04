using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JJsGuiLibrary.UI
{
    public class OutlinedButton : Control
    {
        private Brush brush;
        private Brush textBrush;
        private Pen borderPen;

        private byte buttonState = 0;

        private StringFormat stringFormat;

        private int borderThickness = 3;
        private int hoverBorderThickness = 4;

        private Color hoverBackColor = Color.FromArgb(40, 40, 40);
        private Color hoverForeColor = Color.Gainsboro;
        private Color clickedBorderColor = Color.DeepSkyBlue;
        private Color clickedBackColor = Color.FromArgb(60, 60, 60);
        private Color borderColor = Color.FromArgb(70, 70, 70);
        private Color hoverBorderColor = Color.CornflowerBlue;
        private Color disabledBackColor = Color.FromArgb(70, 70, 70);
        private Color disabledBorderColor = Color.FromArgb(80, 80, 80);

        #region Events
        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        protected override void OnMove(EventArgs e)
        {
            Invalidate();
            base.OnMove(e);
        }
        #endregion

        #region ButtonLogic
        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
            {
                SetButtonState(ButtonState.Normal);
                base.OnEnabledChanged(e);
                return;
            }
            SetButtonState(ButtonState.Disbaled);
            base.OnEnabledChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (Enabled)
            {
                SetButtonState(ButtonState.Hovered);
                base.OnMouseEnter(e);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (Enabled)
            {
                SetButtonState(ButtonState.Normal);
                base.OnMouseLeave(e);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Enabled)
            {
                SetButtonState(ButtonState.Clicked);
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Enabled)
            {
                SetButtonState(ButtonState.Hovered);
                base.OnMouseUp(e);
            }
        }
        #endregion

        public OutlinedButton()
        {
            BackColor = Color.FromArgb(40, 40, 40);
            ForeColor = Color.WhiteSmoke;

            brush = new SolidBrush(BackColor);
            borderPen = new Pen(borderColor, borderThickness);
            textBrush = new SolidBrush(ForeColor);

            stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;

            DoubleBuffered = true;
        }

        #region PublicMethods
        public void SetButtonState(ButtonState state)
        {
            buttonState = (byte)state;
            UpdatePens();
            Invalidate();
        }

        public ButtonState State { get { return (ButtonState)buttonState; } }

        #endregion

        private void UpdatePens()
        {
            switch (buttonState)
            {
                case 0:
                    borderPen = new Pen(borderColor, borderThickness);
                    brush = new SolidBrush(BackColor);
                    textBrush = new SolidBrush(ForeColor);
                    break;
                case 1:
                    borderPen = new Pen(hoverBorderColor, hoverBorderThickness);
                    brush = new SolidBrush(hoverBackColor);
                    textBrush = new SolidBrush(hoverForeColor);
                    break;
                case 2:
                    borderPen = new Pen(clickedBorderColor, hoverBorderThickness);
                    brush = new SolidBrush(clickedBackColor);
                    textBrush = new SolidBrush(hoverForeColor);
                    break;
                case 3:
                    borderPen = new Pen(disabledBorderColor, borderThickness);
                    brush = new SolidBrush(disabledBackColor);
                    textBrush = new SolidBrush(ForeColor);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.InterpolationMode = InterpolationMode.Bilinear;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillRectangle(brush, ClientRectangle);
            g.DrawRectangle(borderPen, ClientRectangle);

            g.DrawString(Text, Font, textBrush, ClientRectangle, stringFormat);
        }

        #region OldProperties
        [Obsolete("This Property is only used in the original Panel.")]
        [Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never),
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackgroundImage { get; set; }

        [Obsolete("This Property is only used in the original Panel.")]
        [Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never),
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageLayout BackgroundImageLayout { get; set; }
        #endregion

        #region Border
        [Category("Appearance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance - Hovered")]
        public Color HoverBorderColor
        {
            get { return hoverBorderColor; }
            set
            {
                hoverBorderColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance")]
        public int BorderThickness
        {
            get { return borderThickness; }
            set
            {
                borderThickness = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance - Hovered")]
        public int HoverBorderThickness
        {
            get { return hoverBorderThickness; }
            set
            {
                hoverBorderThickness = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance - Clicked")]
        public Color ClickedBorderColor
        {
            get { return clickedBorderColor; }
            set
            {
                clickedBorderColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        #endregion

        #region OtherColors
        [Category("Appearance - Hovered")]
        public Color HoverBackColor
        {
            get { return hoverBackColor; }
            set
            {
                hoverBackColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance - Hovered")]
        public Color HoverForeColor
        {
            get { return hoverForeColor; }
            set
            {
                hoverForeColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance - Clicked")]
        public Color ClickedBackColor
        {
            get { return clickedBackColor; }
            set
            {
                clickedBackColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        #endregion

        #region Disabled
        [Category("Appearance - Disabled")]
        public Color DisabledBackColor
        {
            get { return disabledBackColor; }
            set
            {
                disabledBackColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        [Category("Appearance - Disabled")]
        public Color DisabledBorderColor
        {
            get { return disabledBorderColor; }
            set
            {
                disabledBorderColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        #endregion
    }
}

public enum ButtonState : byte
{
    Normal = 0,
    Hovered = 1,
    Clicked = 2,
    Disbaled = 3
}