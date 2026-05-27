using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;

namespace CCP_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Ascii85 ascii85;
        public MainWindow()
        {
            ascii85 = new Ascii85();
            InitializeComponent();
        }

        public Boolean copyToClipboard(String data)
        {
            return false;
        }

        public Boolean saveToFile(FileStyleUriParser file, String text)
        {
            //
            return false;
        }

        public String encodeBase64(String input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public String decodeBase64(String input)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }

        public String encodeASCII85(String input)
        {
            return ascii85.Encode(Encoding.UTF8.GetBytes(input));
        }

    }
}