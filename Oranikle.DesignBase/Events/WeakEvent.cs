using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Oranikle.Studio.Controls
{

    public class WeakEvent<T>
        where T : System.EventArgs
    {

        private static readonly log4net.ILog log;

        private System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>> obj;

        public WeakEvent()
        {
        }

        static WeakEvent()
        {
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void AddHandler(System.EventHandler<T> handler)
        {
            AddHandler(handler.Target, handler.Method);
        }

        public void AddHandler(System.EventHandler handler)
        {
            AddHandler(handler.Target, handler.Method);
        }

        public void AddHandler(object o, System.Reflection.MethodInfo method)
        {
            //if (!obj)
            //    obj = new System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>();
            if (obj == null)
                obj = new System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>();
            obj.Add(new Oranikle.Studio.Controls.WeakDelegate<T>(o, method));
        }

        public void AddHandler(object o, System.EventHandler<T> handler)
        {
            AddHandler(o, handler.Method);
        }

        public void Invoke(object sender, T e)
        {
            //if (!obj)
            //    return;
            if (obj == null) return;
            lock (obj)
            {
                System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>> list1 = new System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>();
                System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>.Enumerator enumerator1 = obj.GetEnumerator();
                try
                {
                    while (enumerator1.MoveNext())
                    {
                        Oranikle.Studio.Controls.WeakDelegate<T> weakDelegate1 = enumerator1.Current;
                        if (!weakDelegate1.IsAlive)
                            list1.Add(weakDelegate1);
                    }
                }
                finally
                {
                    enumerator1.Dispose();
                }
                System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>.Enumerator enumerator = list1.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        Oranikle.Studio.Controls.WeakDelegate<T> weakDelegate = enumerator.Current;
                        obj.Remove(weakDelegate);
                    }
                }
                finally
                {
                    enumerator.Dispose();
                }
                System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>> list2 = new System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>();
                System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>.Enumerator enumerator2 = obj.GetEnumerator();
                try
                {
                    while (enumerator2.MoveNext())
                    {
                        Oranikle.Studio.Controls.WeakDelegate<T> weakDelegate3 = enumerator2.Current;
                        list2.Add(weakDelegate3);
                    }
                }
                finally
                {
                    enumerator2.Dispose();
                }
                System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>.Enumerator enumerator3 = list2.GetEnumerator();
                try
                {
                    while (enumerator3.MoveNext())
                    {
                        Oranikle.Studio.Controls.WeakDelegate<T> weakDelegate2 = enumerator3.Current;
                        try
                        {
                            weakDelegate2.Invoke(sender, e);
                        }
                        catch (System.Exception e1)
                        {
                            log.Error(e1);
                        }
                    }
                }
                finally
                {
                    enumerator3.Dispose();
                }
            }
        }

        public void RemoveHandler(System.EventHandler handler)
        {
            RemoveHandler(handler.Target, handler.Method);
        }

        public void RemoveHandler(object o, System.Reflection.MethodInfo method)
        {
            //if (!obj)
            //    return;
            if (obj == null) return;
            System.Collections.Generic.List<Oranikle.Studio.Controls.WeakDelegate<T>>.Enumerator enumerator = obj.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Oranikle.Studio.Controls.WeakDelegate<T> weakDelegate = enumerator.Current;
                    System.WeakReference weakReference = weakDelegate.Obj;
                    object obj1 = weakReference.Target;
                    if ((obj1 == o) && (weakDelegate.Method == method))
                    {
                        obj.Remove(weakDelegate);
                        return;
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }

    } // class WeakEvent

}

