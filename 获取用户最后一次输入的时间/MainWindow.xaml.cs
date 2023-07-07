using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 获取用户最后一次输入的时间
{
    // 保持后台超过N分钟，退出管理员身份
    // 超过N分钟在应用是前台的情况下，退出管理员身份
    // 锁屏了退出管理员身份

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TypeInfo genericTypeInfo = typeof(bool?).GetTypeInfo();

            // DelegateCommand allows object or Nullable<>.  
            // note: Nullable<> is a struct so we cannot use a class constraint.
            if (genericTypeInfo.IsValueType)
            {
                if ((!genericTypeInfo.IsGenericType) || (!typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo())))
                {
                    throw new InvalidCastException();
                }
            }

            var ret = (bool?)false;

            // 无减法，只有加法；减去一个非负数等价于加上一个非正数。
            // 加法，符号相异不会溢出，符号相同才可能会溢出。
            // 正数相加溢出得到负数，负数相加溢出得到正数
            // 正数相加得到负数是溢出，负数相加得到正数是溢出
            // 溢出值是什么? 两正和 - 类型最大值 + 类型最小值 + 1； 两负和 + 类型最大值 - 类型最小值 + 1
            // 溢出值不可能大于类型最大值 * 2
            // 有符号数和无符号数结果不可控
            // 不想溢出的话，要保证运算数是同一个类型，且放宽类型再运算，结果一定不会溢出。
            // 整数 + 1,沿着数轴循环，相减
            // 大范围变小范围，直接是截断
            byte b1 =Byte.MaxValue;
            byte b2 =Byte.MaxValue;



            int a = int.MinValue + 1;
            int b = -3;
            Console.WriteLine(a + b);
            Console.WriteLine((long)a + b + int.MaxValue - int.MinValue + 1);



            //int a = int.MaxValue -1;
            //int b = 3;
            //Console.WriteLine(a + b);
            //Console.WriteLine((long)a + b - int.MaxValue + int.MinValue + 1);




            Application.Current.Deactivated += (sender, args) =>
            {
                Console.WriteLine("Deactivated");
            };

            Application.Current.Activated += (sender, args) =>
            {
                Console.WriteLine("Activated");
            };


            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine(GetUserInputIdleMillisecondTime());
                    Thread.Sleep(1000);
                }
            });
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // 后台超过2分钟 锁屏
            // 前台但是2分钟无键盘鼠标输入了也锁屏

        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        internal static extern bool GetLastInputInfo(ref LASTINPUTINFO LASTINPUTINFO);

        public static int GetUserInputIdleMillisecondTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;
            if (GetLastInputInfo(ref lastInputInfo))
            {
                return Environment.TickCount - (int)lastInputInfo.dwTime;
            }

            throw new ApplicationException("GetLastInputInfo() failed in user32.dll ");
        }
    }


    class UserInputEventManager
    {
        private Timer _timer;
        private Stopwatch _stopwatch = new Stopwatch();

        private UserInputEventManager()
        {
            Application.Current.Activated += Current_Activated;
            Application.Current.Deactivated += CurrentOnDeactivated;
            _timer = new Timer((state) =>
            {
                if (_stopwatch.ElapsedTicks > IdleTimeout.Ticks)
                {
                    IdleTimeoutEvent?.Invoke();
                    return;
                }

                if (MainWindow.GetUserInputIdleMillisecondTime() > IdleTimeout.Ticks)
                {
                    IdleTimeoutEvent?.Invoke();
                    return;
                }
            });
        }

        private void CurrentOnDeactivated(object? sender, EventArgs e)
        {
            _stopwatch.Restart();
        }

        private void Current_Activated(object? sender, EventArgs e)
        {
            _stopwatch.Stop();
            _stopwatch.Reset();
        }

        public static UserInputEventManager Instance { get; } = new UserInputEventManager();

        public event Action IdleTimeoutEvent;

        public TimeSpan IdleTimeout { get; set; } = Timeout.InfiniteTimeSpan;
    }
}
