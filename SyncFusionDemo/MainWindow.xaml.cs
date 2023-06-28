using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SyncFusionDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.PropertyGrid.SelectedObject = new HH();




            //char c = 'A';
            //string s = "A";

            //char c = '\r';
            //string s = "\r";

            //char tabc = '  ';
            //char tabc = '\t';

            //string tabs = " ";
            //string tabs = "\t";

            //char stxc = '\u0002';
            //string stxs = "\u0002";
            //char etxc = '\u0003';
            //string etxs = "\u0003";
            //char nullc = '\u0000';
            //string nulls = "\u0000";

            string s = "\u004E\u004F\u0050\u0051";
            // \uD834\uDF06
            string ss = "😀";
            ss = "\ud83d\ude00";
            ss = Encoding.Unicode.GetString(new byte[] { 0x3d, 0xd8, 0x00, 0xde});
            Console.WriteLine(ss.Length);

            //string ccc = '😀';

            char aaaa = '\uFFFF';
           ushort bbbb = (ushort)aaaa;




            Console.WriteLine(ss);

            Console.ReadLine();


            char a = '\u1F33';




        }



    }


    public class HH
    {
        public int Age { get; set; } = 392;

       public decimal JJJJ
       {
           get;
           set;
       }

       public float GGG { get; set; }
    }
}