using System;

namespace Oranikle.Studio.Controls
{
    public delegate void UnregisterCallback(System.EventHandler eventHandler);

    public class WeakEventHandler<T> : Oranikle.Studio.Controls.IWeakEventHandler
    {

        private delegate void OpenEventHandler(T This, object sender, System.EventArgs e);

        private System.EventHandler m_Handler;
        private Oranikle.Studio.Controls.WeakEventHandler<T>.OpenEventHandler m_OpenHandler;
        private System.WeakReference m_TargetRef;
        private Oranikle.Studio.Controls.UnregisterCallback m_Unregister;

        public System.EventHandler Handler
        {
            get
            {
                return m_Handler;
            }
        }

        public static implicit operator System.EventHandler(Oranikle.Studio.Controls.WeakEventHandler<T> weh)
        {
            return weh.m_Handler;
        }

        public WeakEventHandler(System.EventHandler eventHandler, Oranikle.Studio.Controls.UnregisterCallback unregister)
        {
            m_TargetRef = new System.WeakReference(eventHandler.Target);
            m_OpenHandler = (Oranikle.Studio.Controls.WeakEventHandler<T>.OpenEventHandler) System.Delegate.CreateDelegate(typeof(Oranikle.Studio.Controls.WeakEventHandler<T>.OpenEventHandler), null, eventHandler.Method);
            m_Handler = new System.EventHandler(Invoke);
            m_Unregister = unregister;
        }

        public void Invoke(object sender, System.EventArgs e)
        {
            T t = (T)m_TargetRef.Target;
            if (t != null)
            {
                m_OpenHandler(t, sender, e);
                return;
            }
            //if (m_Unregister)
            if (m_Unregister!=null)
            {
                m_Unregister(m_Handler);
                m_Unregister = null;
            }
        }

    } 

}

