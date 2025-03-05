using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataToggleSquare : Control
    {
        private bool isChecked = false;

        public event EventHandler Toggled;

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    Toggled?.Invoke(this, EventArgs.Empty);
                    Invalidate(); // Redraw the control
                }
            }
        }

        private Color trackColorOn = Color.FromArgb(0, 128, 200);
        private bool trackFillOn = true;

        private Color thumbColorOn = Color.FromArgb(224, 224, 224);
        private bool thumbFillOn = true;

        private Color trackColorOff = Color.FromArgb(60, 60, 60);
        private bool trackFillOff = false;
        
        private Color thumbColorOff = Color.FromArgb(60, 60, 60);
        private bool thumbFillOff = true;


        private int paddingThumbB = 0;
        private int paddingThumbR = 0;
        private int paddingThumbT = 0;
        private int paddingThumbL = 0;
        private int divisorThumb = 2;
        private int thumbWeight = 1;
        private int ratioThumb = 8;


        private int paddingTrackL = 1;
        private int paddingTrackT = 1;
        private int paddingTrackR = 1;
        private int paddingTrackB = 1;
        private int paddingDecomp = 2;
        private int trackWeight = 1;





        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingLeftTrack
        {
            get => paddingTrackL;
            set { paddingTrackL = value; Invalidate(); }
        }
        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingTopTrack
        {
            get => paddingTrackT;
            set { paddingTrackT = value; Invalidate(); }
        }
        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingRightTrack
        {
            get => paddingTrackR;
            set { paddingTrackR = value; Invalidate(); }
        }
        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingBottomTrack
        {
            get => paddingTrackB;
            set { paddingTrackB = value; Invalidate(); }
        }


        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingDecompensator
        {
            get => paddingDecomp;
            set { paddingDecomp = value; Invalidate(); }
        }
        [Category("Track")]
        [Description("Track Setting")]
        public int TrackWeight
        {
            get => trackWeight;
            set { trackWeight = value; Invalidate(); }
        }




        [Category("Thumb")]
        [Description("Thumb Setting")]
        
        public int ThumbHeightRatio
        {
            get => ratioThumb;
            set { ratioThumb = value; Invalidate(); }
        }
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int ThumbWeight
        {
            get => thumbWeight;
            set { thumbWeight = value; Invalidate(); }
        }
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int ThumbHeightDivisor
        {
            get => divisorThumb;
            set { divisorThumb = value; if (divisorThumb == 0) { divisorThumb = -1; } Invalidate(); }
        }
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int PaddingLeftThumb
        {
            get => paddingThumbL;
            set { paddingThumbL = value; Invalidate(); }
        }
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int PaddingTopThumb
        {
            get => paddingThumbT;
            set { paddingThumbT = value; Invalidate(); }
        }
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int PaddingRightThumb
        {
            get => paddingThumbR;
            set { paddingThumbR = value; Invalidate(); }
        }
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int PaddingBottomThumb
        {
            get => paddingThumbB;
            set { paddingThumbB = value; Invalidate(); }
        }





        [Category("Turned On")]
        [Description("Track fill mode")]
        public bool TrackFillOn
        {
            get => trackFillOn;
            set { trackFillOn = value; Invalidate(); }
        }
        [Category("Turned Off")]
        [Description("Track fill mode")]
        public bool TrackFillOff
        {
            get => trackFillOff;
            set { trackFillOff = value; Invalidate(); }
        }
        [Category("Turned On")]
        [Description("Thumb fill mode")]
        public bool ThumbFillOn
        {
            get => thumbFillOn;
            set { thumbFillOn = value; Invalidate(); }
        }
        [Category("Turned Off")]
        [Description("Thumb fill mode when off")]
        public bool ThumbFillOff
        {
            get => thumbFillOff;
            set { thumbFillOff = value; Invalidate(); }
        }








        [Category("Turned On")]
        [Description("Track color")]
        public Color TrackColorOn
        {
            get => trackColorOn;
            set { trackColorOn = value; Invalidate(); }
        }

        [Category("Turned Off")]
        [Description("Track Color")]
        public Color TrackColorOff
        {
            get => trackColorOff;
            set { trackColorOff = value; Invalidate(); }
        }
        [Category("Turned On")]
        [Description("Thumb color")]
        public Color ThumbColorOn
        {
            get => thumbColorOn;
            set { thumbColorOn = value; Invalidate(); }
        }
       
        [Category("Turned Off")]
        [Description("Thumb color")]
        public Color ThumbColorOff
        {
            get => thumbColorOff;
            set { thumbColorOff = value; Invalidate(); }
        }

        






        public ErrataToggleSquare()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Size = new Size(60, 30); // Default size
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Skip base.OnPaint due to checkbox inheritance
            base.OnPaint(e);

            var g = e.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;

            // Define the track rectangle (elongated oval)
            

            var trackRect = new Rectangle(paddingTrackL, paddingTrackT, Width - paddingTrackR * paddingDecomp, Height - paddingTrackB * paddingDecomp);

            // Draw the track
                using (Brush trackBrush = new SolidBrush(IsChecked ? TrackColorOn : TrackColorOff))
                using (Pen borderPen = new Pen(IsChecked ? TrackColorOn : TrackColorOff, trackWeight))
                {

                    if (IsChecked)
                    {
                        if (trackFillOn) { e.Graphics.FillRectangle(trackBrush, trackRect); }
                        else { e.Graphics.DrawRectangle(borderPen, trackRect); }
                    }
                    else
                    {
                        if (trackFillOff) { e.Graphics.FillRectangle(trackBrush, trackRect); }
                        else { e.Graphics.DrawRectangle(borderPen, trackRect); }
                    }    
                    


                }
            




            // Draw the thumb
            int tHalf = ThumbHeightRatio / divisorThumb;
            int thumbSize = Height - ThumbHeightRatio;
            int thumbX = IsChecked ? Width - thumbSize - tHalf : tHalf;
            var thumbRect = new Rectangle(thumbX + paddingThumbL, tHalf + paddingThumbT, thumbSize + paddingThumbR, thumbSize + paddingThumbB);
            using (Brush thumbBrush = new SolidBrush(IsChecked ? ThumbColorOn : ThumbColorOff))
            using (Pen borderPen = new Pen(IsChecked ? ThumbColorOn : ThumbColorOff, thumbWeight))
            {
                
                if (IsChecked)
                {
                    if (thumbFillOn) { e.Graphics.FillRectangle(thumbBrush, thumbRect); }
                    else { e.Graphics.DrawRectangle(borderPen, thumbRect); }
                }
                else
                {
                    if (thumbFillOff) { e.Graphics.FillRectangle(thumbBrush, thumbRect); }
                    else { e.Graphics.DrawRectangle(borderPen, thumbRect); }
                }
                

                

            }

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                IsChecked = !IsChecked; // Toggle state
            }
        }
    }
}
