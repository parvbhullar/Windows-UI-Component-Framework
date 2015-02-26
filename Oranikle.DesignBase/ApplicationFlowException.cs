using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class ApplicationFlowException : System.Exception
    {

        private static readonly log4net.ILog log;

        private ApplicationFlowException(string msg)
            : base(msg)
        {
            Oranikle.Studio.Controls.ApplicationFlowException.log.Fatal("=========== ApplicationFlowException Start ===============");
            Oranikle.Studio.Controls.ApplicationFlowException.log.Fatal(msg);
            Oranikle.Studio.Controls.ApplicationFlowException.log.Fatal("=========== ApplicationFlowException End ===============");
        }

        static ApplicationFlowException()
        {
            Oranikle.Studio.Controls.ApplicationFlowException.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static void ThrowNewApplicationFlowException(string msg)
        {
            Oranikle.Studio.Controls.ApplicationFlowException.log.Fatal("=========== ApplicationFlowException Start ===============");
            Oranikle.Studio.Controls.ApplicationFlowException.log.Fatal(msg);
            Oranikle.Studio.Controls.ApplicationFlowException.log.Fatal("=========== ApplicationFlowException End ===============");
        }

    }
}
