using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class StickyManager
    {

        private bool isShowingStickies;

        public bool IsShowingStickies
        {
            get
            {
                return isShowingStickies;
            }
            set
            {
                if (value != isShowingStickies)
                {
                    if (value)
                    {
                        ShowAllStickies();
                        return;
                    }
                    HideAllStickies();
                }
            }
        }

        public StickyManager()
        {
        }

        public void DeleteAllStickies(System.Windows.Forms.Control parentControl, bool suspendAndResumeLayout)
        {
            //Oranikle.Studio.Controls.CtrlStickyNote[] ctrlStickyNoteArr1 = ToArray();
            //Oranikle.Studio.Controls.CtrlStickyNote[] ctrlStickyNoteArr2 = ctrlStickyNoteArr1;
            //for (int i = 0; i < ctrlStickyNoteArr2.Length; i++)
            //{
            //    Oranikle.Studio.Controls.CtrlStickyNote ctrlStickyNote = ctrlStickyNoteArr2[i];
            //    DeleteSticky(ctrlStickyNote, parentControl, suspendAndResumeLayout);
            //}
        }

        public void DeleteSticky(Oranikle.Studio.Controls.CtrlStickyNote toDelete, System.Windows.Forms.Control parentControl)
        {
            DeleteSticky(toDelete, parentControl, true);
        }

        public void DeleteSticky(Oranikle.Studio.Controls.CtrlStickyNote toDelete, System.Windows.Forms.Control parentControl, bool suspendAndResumeLayout)
        {
            //Remove(toDelete);
            if (suspendAndResumeLayout)
                parentControl.SuspendLayout();
            IsShowingStickies = true;
            parentControl.Controls.Remove(toDelete);
            toDelete.Dispose();
            if (suspendAndResumeLayout)
                parentControl.ResumeLayout();
        }

        public void DisableAllStickies()
        {
            //System.Collections.Generic.List<Oranikle.Studio.Controls.CtrlStickyNote>.Enumerator enumerator = GetEnumerator();
            //try
            //{
            //    while (enumerator.MoveNext())
            //    {
            //        Oranikle.Studio.Controls.CtrlStickyNote ctrlStickyNote = enumerator.Current;
            //        ctrlStickyNote.Enabled = false;
            //    }
            //}
            //finally
            //{
            //    enumerator.Dispose();
            //}
        }

        private void HideAllStickies()
        {
            //System.Collections.Generic.List<Oranikle.Studio.Controls.CtrlStickyNote>.Enumerator enumerator = GetEnumerator();
            //try
            //{
            //    while (enumerator.MoveNext())
            //    {
            //        Oranikle.Studio.Controls.CtrlStickyNote ctrlStickyNote = enumerator.Current;
            //        ctrlStickyNote.Visible = false;
            //    }
            //}
            //finally
            //{
            //    enumerator.Dispose();
            //}
            isShowingStickies = false;
        }

        public Oranikle.Studio.Controls.CtrlStickyNote InsertSticky(System.Windows.Forms.Control parentControl)
        {
            return InsertSticky(parentControl, true, new System.Drawing.Point(0, 0), 0, 0, Oranikle.Studio.Controls.CtrlStickyNote.Colour.Yellow, "");
        }

        public Oranikle.Studio.Controls.CtrlStickyNote InsertSticky(System.Windows.Forms.Control parentControl, bool showSticky, System.Drawing.Point location, int width, int height, Oranikle.Studio.Controls.CtrlStickyNote.Colour colour, string text)
        {
            Oranikle.Studio.Controls.CtrlStickyNote ctrlStickyNote = new Oranikle.Studio.Controls.CtrlStickyNote();
            ctrlStickyNote.Location = location;
            if (width > 0)
                ctrlStickyNote.Width = width;
            if (height > 0)
                ctrlStickyNote.Height = height;
            ctrlStickyNote.CurrentColour = colour;
            ctrlStickyNote.Text = text;
            //Add(ctrlStickyNote);
            if (showSticky)
                parentControl.SuspendLayout();
            IsShowingStickies = true;
            parentControl.Controls.Add(ctrlStickyNote);
            parentControl.Controls.SetChildIndex(ctrlStickyNote, 0);
            if (showSticky)
            {
                ctrlStickyNote.Show();
                parentControl.ResumeLayout();
            }
            return ctrlStickyNote;
        }

        private void ShowAllStickies()
        {
            //System.Collections.Generic.List<Oranikle.Studio.Controls.CtrlStickyNote>.Enumerator enumerator = GetEnumerator();
            //try
            //{
            //    while (enumerator.MoveNext())
            //    {
            //        Oranikle.Studio.Controls.CtrlStickyNote ctrlStickyNote = enumerator.Current;
            //        if (!ctrlStickyNote.IsDisposed)
            //            ctrlStickyNote.Visible = true;
            //    }
            //}
            //finally
            //{
            //    enumerator.Dispose();
            //}
            //isShowingStickies = true;
        }

    }
}
