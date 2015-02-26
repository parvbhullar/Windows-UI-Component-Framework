using System;
using System.Reflection;

namespace Oranikle.Studio.Controls
{

    public class WeakDelegate<T>
        where T : System.EventArgs
    {

        private System.Reflection.MethodInfo _Method;
        private System.WeakReference _Obj;

        public bool IsAlive
        {
            get
            {
                if (!_Method.IsStatic)
                {
                    if (_Obj != null)
                        return _Obj.IsAlive;
                    return false;
                }
                return true;
            }
        }

        public System.Reflection.MethodInfo Method
        {
            get
            {
                return _Method;
            }
        }

        public System.WeakReference Obj
        {
            get
            {
                return _Obj;
            }
        }

        public WeakDelegate(object target, System.Reflection.MethodInfo method)
        {
            _Obj = new System.WeakReference(target);
            _Method = method;
        }

        public void Invoke(object sender, T e)
        {
            object obj = _Obj.Target;
            if (_Method.IsStatic || (obj != null))
            {
                object[] objArr = new object[] {
                                                 sender, 
                                                 e };
                _Method.Invoke(obj, objArr);
            }
        }

    } // class WeakDelegate

}

