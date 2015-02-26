using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls.ProgressBar
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
    public class SpinningProgress : System.Windows.Forms.UserControl
    {

        private System.ComponentModel.IContainer components;
        private System.Drawing.Region innerBackgroundRegion;
        private System.Drawing.Color m_ActiveColour;
        private bool m_AutoIncrement;
        private System.Timers.Timer m_AutoRotateTimer;
        private bool m_BehindIsActive;
        private System.Drawing.Color m_InactiveColour;
        private double m_IncrementFrequency;
        private System.Drawing.Color m_TransistionColour;
        private int m_TransitionSegment;
        private System.Drawing.Drawing2D.GraphicsPath[] segmentPaths;

        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "35, 146, 33")]
        public System.Drawing.Color ActiveSegmentColour
        {
            get
            {
                return m_ActiveColour;
            }
            set
            {
                m_ActiveColour = value;
                Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(true)]
        public bool AutoIncrement
        {
            get
            {
                return m_AutoIncrement;
            }
            set
            {
                m_AutoIncrement = value;
                bool flag = value || (m_AutoRotateTimer == null);
                if (!flag)
                {
                    m_AutoRotateTimer.Dispose();
                    m_AutoRotateTimer = null;
                }
                flag = !value || (m_AutoRotateTimer != null);
                if (!flag)
                {
                    m_AutoRotateTimer = new System.Timers.Timer(m_IncrementFrequency);
                    m_AutoRotateTimer.Elapsed += new System.Timers.ElapsedEventHandler(IncrementTransisionSegment);
                    m_AutoRotateTimer.Start();
                }
            }
        }

        [System.ComponentModel.DefaultValue(100)]
        public double AutoIncrementFrequency
        {
            get
            {
                return m_IncrementFrequency;
            }
            set
            {
                m_IncrementFrequency = value;
                bool flag = m_AutoRotateTimer == null;
                if (!flag)
                {
                    AutoIncrement = false;
                    AutoIncrement = true;
                }
            }
        }

        [System.ComponentModel.DefaultValue(true)]
        public bool BehindTransistionSegmentIsActive
        {
            get
            {
                return m_BehindIsActive;
            }
            set
            {
                m_BehindIsActive = value;
                Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "218, 218, 218")]
        public System.Drawing.Color InactiveSegmentColour
        {
            get
            {
                return m_InactiveColour;
            }
            set
            {
                m_InactiveColour = value;
                Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(-1)]
        public int TransistionSegment
        {
            get
            {
                return m_TransitionSegment;
            }
            set
            {
                bool flag = (value <= 11) && (value >= -1);
                if (!flag)
                    throw new System.ArgumentException("TransistionSegment must be between -1 and 11\uFFFD");
                m_TransitionSegment = value;
                Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "129, 242, 121")]
        public System.Drawing.Color TransistionSegmentColour
        {
            get
            {
                return m_TransistionColour;
            }
            set
            {
                m_TransistionColour = value;
                Invalidate();
            }
        }

        public SpinningProgress()
        {
            m_InactiveColour = System.Drawing.Color.FromArgb(218, 218, 218);
            m_ActiveColour = System.Drawing.Color.FromArgb(35, 146, 33);
            m_TransistionColour = System.Drawing.Color.FromArgb(129, 242, 121);
            segmentPaths = new System.Drawing.Drawing2D.GraphicsPath[12];
            m_AutoIncrement = true;
            m_IncrementFrequency = 100.0;
            m_BehindIsActive = true;
            m_TransitionSegment = 0;
            InitializeComponent();
            CalculateSegments();
            m_AutoRotateTimer = new System.Timers.Timer(m_IncrementFrequency);
            m_AutoRotateTimer.Elapsed += new System.Timers.ElapsedEventHandler(IncrementTransisionSegment);
            DoubleBuffered = true;
            m_AutoRotateTimer.Start();
            EnabledChanged += new System.EventHandler(SpinningProgress_EnabledChanged);
            Paint += new System.Windows.Forms.PaintEventHandler(ProgressDisk_Paint);
            Resize += new System.EventHandler(ProgressDisk_Resize);
            SizeChanged += new System.EventHandler(ProgressDisk_SizeChanged);
        }

        private void CalculateSegments()
        {
            bool flag = true;

            System.Drawing.Rectangle rectangle1 = new System.Drawing.Rectangle(0, 0, Width, Height);
            System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(Width * 7 / 30, Height * 7 / 30, Width - (Width * 14 / 30), Height - (Height * 14 / 30));
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = null;
            int i = 0;
            while (flag)
            {
                segmentPaths[i] = new System.Drawing.Drawing2D.GraphicsPath();
                segmentPaths[i].AddPie(rectangle1, (float)((i * 30) - 90), 25.0F);
                i++;
                flag = i < 12;
            }
            graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            graphicsPath.AddPie(rectangle2, 0.0F, 360.0F);
            innerBackgroundRegion = new System.Drawing.Region(graphicsPath);
        }

        private void IncrementTransisionSegment(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool flag = m_TransitionSegment != 11;
            if (!flag)
            {
                m_TransitionSegment = 0;
                m_BehindIsActive = !m_BehindIsActive;
            }
            else
            {
                flag = m_TransitionSegment != -1;
                if (!flag)
                    m_TransitionSegment = 0;
                else
                    m_TransitionSegment++;
            }
            Invalidate();
        }

        [System.Diagnostics.DebuggerStepThrough]
        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Name = "SpinningProgress";
            Size = new System.Drawing.Size(30, 30);
            ResumeLayout(false);
        }

        private void ProgressDisk_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            bool flag = true;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.ExcludeClip(innerBackgroundRegion);
            int i = 0;
            while (flag)
            {
                flag = !Enabled;
                if (!flag)
                {
                    flag = i != m_TransitionSegment;
                    if (!flag)
                    {
                        e.Graphics.FillPath(new System.Drawing.SolidBrush(m_TransistionColour), segmentPaths[i]);
                    }
                    else
                    {
                        flag = i >= m_TransitionSegment;
                        if (!flag)
                        {
                            flag = !m_BehindIsActive;
                            if (!flag)
                                e.Graphics.FillPath(new System.Drawing.SolidBrush(m_ActiveColour), segmentPaths[i]);
                            else
                                e.Graphics.FillPath(new System.Drawing.SolidBrush(m_InactiveColour), segmentPaths[i]);
                        }
                        else
                        {
                            flag = !m_BehindIsActive;
                            if (!flag)
                                e.Graphics.FillPath(new System.Drawing.SolidBrush(m_InactiveColour), segmentPaths[i]);
                            else
                                e.Graphics.FillPath(new System.Drawing.SolidBrush(m_ActiveColour), segmentPaths[i]);
                        }
                    }
                }
                else
                {
                    e.Graphics.FillPath(new System.Drawing.SolidBrush(m_InactiveColour), segmentPaths[i]);
                }
                i++;
                flag = i < 12;
            }
        }

        private void ProgressDisk_Resize(object sender, System.EventArgs e)
        {
            CalculateSegments();
        }

        private void ProgressDisk_SizeChanged(object sender, System.EventArgs e)
        {
            CalculateSegments();
        }

        private void SpinningProgress_EnabledChanged(object sender, System.EventArgs e)
        {
            bool flag = !Enabled;
            if (!flag)
            {
                flag = m_AutoRotateTimer == null;
                if (!flag)
                    m_AutoRotateTimer.Start();
            }
            else
            {
                flag = m_AutoRotateTimer == null;
                if (!flag)
                    m_AutoRotateTimer.Stop();
            }
        }

        [System.Diagnostics.DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            bool flag = !disposing || (components == null);
            if (!flag)
                components.Dispose();
            base.Dispose(disposing);
        }

    } 
}
