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
using System.IO.Ports;
using PropertyChanged;
using System.Threading;

namespace SerialPortTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [ImplementPropertyChanged]
    public partial class MainWindow : Window
    {
        public string Input { get; set; }
        public string Output { get; set; }
        SerialPort port;
        Thread readThread;
        public MainWindow()
        {
            InitializeComponent();

            port = new SerialPort("COM4");
            port.Open();
            Input = "<RM>";
            port.DataReceived += Port_DataReceived;
           
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Output+= port.ReadExisting();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte[] MyMessage = System.Text.Encoding.UTF8.GetBytes(Input);

            port.Write(MyMessage, 0, MyMessage.Length);

            Output = "";
        }

        public void Read()
        {
            while (true)
            {
                try
                {
                    Output = port.ReadLine();
                }
                catch (TimeoutException) { }
                catch
                {
                    int a = 0;
                }
            }
        }
    }
}
