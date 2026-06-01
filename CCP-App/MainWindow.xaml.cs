using Microsoft.Win32;
using System.Buffers.Text;
using System.Collections;
using System.IO;
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
            encodeB64Button(null, null);
            encodeA85Button(null, null);
        }

        public String generateSaveString(String time)
        {
            //Gather current strings from the text boxes here

            encodeB64Button(null,null);
            encodeA85Button(null, null);

            String s = "[" + time + "]\n";
            s += "---Base64---\n";
            s += b64left.Text + " <=> " + b64right.Text + "\n";
            s += "---ASCII85---\n";
            s += a85left.Text + " <=> " + a85right.Text + "\n";

            return s;
        }
        private void saveEncodes(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Save Messages");
            
            String time = DateTime.Now.ToString("MMddyyyy-HHmmss");
            //MessageBox.Show(time);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            dialog.FileName = "ccpdata" + time + ".txt";
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; 
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                //MessageBox.Show(dialog.FileName);

                File.WriteAllText(dialog.FileName, generateSaveString(time));
                MessageBox.Show("File saved to\n" + dialog.FileName, "Save Successful", MessageBoxButton.OK, MessageBoxImage.Information);
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
            this.Close();
            temp.Show();
            //temp.Show();
            //object value = await temp.generateReport();
            //Task.Run(() => temp.generateReport());
            //Task.Run(() => temp.updateLoading

            /*
            while (!temp.readystat)
            {

            }

            ArrayList list = temp.getList();
            temp.reportText.Text += "" + "\n";
            */

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

        private void b64left_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;

                encodeB64Button(null, null);
            }
        }
        private void b64right_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;

                decodeB64Button(null,null);
            }
        }

        private void a85left_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;

                encodeA85Button(null, null);
            }
        }

        private void encAlgButton(object sender, RoutedEventArgs e)
        {

            Window2 temp = new Window2();
            temp.Show();

        }
    }
}