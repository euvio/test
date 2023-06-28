using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace 应用程序域DLL
{
    public class TestClass
    {
        public void Test()
        {

            ////建立新的应用程序域对象
            AppDomain newAppDomain = AppDomain.CreateDomain("newAppDomain");
            CrossAppDomainDelegate crossAppDomainDelegate = new CrossAppDomainDelegate(MyCallBack);

            newAppDomain.DoCallBack(crossAppDomainDelegate);

        }

        static public void MyCallBack()
        {
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("C:\\Users\\liliuwei\\Desktop\\应用程序域测试\\NLog2.config");
            LogManager.GetLogger("妈的").Debug("他妈的");
        }
    }
}
