using Microsoft.Win32;
using System.Buffers.Text;
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

        private Ascii85 ascii85;
        public MainWindow()
        {
            ascii85 = new Ascii85();
            InitializeComponent();
            encodeB64Button(null,null);
            encodeA85Button(null, null);
        }

        public Boolean copyToClipboard(String data)
        {
            return false;
        }

        public String generateSaveString()
        {
            //Gather current strings from the text boxes here


            return null;
        }
        private void saveEncodes(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Save Messages");

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; 
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                MessageBox.Show(dialog.FileName);

                //TODO Continue from here

            }
            else
            {
                MessageBox.Show("There was a problem selecting a file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public String encodeBase64(String input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public String decodeBase64(String input)
        {
            if (Base64.IsValid(input))
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(input));
            }
            else
            {
                return "Invalid Base64";
            }
            
        }

        public String encodeASCII85(String input)
        {
            return ascii85.Encode(Encoding.UTF8.GetBytes(input));
        }

        public String decodeASCII85(String input){
            return Encoding.UTF8.GetString(ascii85.Decode(input));
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Next Button Clicked");
            Window1 temp = new Window1();
            temp.Show();
            this.Close();
        }

        private void encodeB64Button(object sender, RoutedEventArgs e)
        {
            b64right.Text = encodeBase64(b64left.Text);
        }

        private void decodeB64Button(object sender, RoutedEventArgs e)
        {
            b64left.Text = decodeBase64(b64right.Text);
        }

        private void encodeA85Button(object sender, RoutedEventArgs e)
        {
            a85right.Text = encodeASCII85(a85left.Text);
        }

        private void decodeA85Button(object sender, RoutedEventArgs e)
        {
            if(decodeASCII85(a85right.Text) == null)
            {
                a85left.Text = "Invalid ASCII85";
            }
            a85left.Text = decodeASCII85(a85right.Text);
        }
    }
}