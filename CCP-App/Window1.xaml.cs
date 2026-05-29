using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Collections;
using System.Diagnostics;

namespace CCP_App
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private String currentdir;
        private ArrayList info;
        private String report;
        public Window1()
        {
            currentdir = Directory.GetCurrentDirectory();
            //MessageBox.Show(currentdir);
            info = new ArrayList();
            
            InitializeComponent();

            report = currentdir + "\\ccptempreport.txt";
            reportText.Text = reportText.Text + "\nGenerating report to: " + report;

            Task.Run(() => generateReport());
            //displayReport();



            //reportText.Text = temp2;
        }

        private void generateReport()
        {

            //Process.Start("C:\\WINDOWS\\system32\\msinfo32.exe");

            

            Process temp = new Process();
            temp.StartInfo.FileName = "C:\\WINDOWS\\system32\\msinfo32.exe";
            temp.StartInfo.Arguments = "/report ccptempreport.txt";
            temp.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            temp.Start();
            temp.WaitForExit();

            if (!File.Exists(report))
            {
                MessageBox.Show("There was a problem generating the temporary report.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //reportText.Text += "\nThere was a problem generating the temporary report.";
                return;
            }

            foreach (String s in File.ReadLines(report))
            {
                info.Add(s);
            }

            File.Delete(report);

            if (File.Exists(report))
            {
                MessageBox.Show("There was a problem deleting the temporary report.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //reportText.Text += "\nThere was a problem deleting the temporary report.";
                Process.Start("C:\\WWINDOWS\\explorer.exe", "\"" + currentdir + "\"");
            }

            String temp2 = "";

            foreach (String s in info)
            {
                temp2 = temp2 + s + "\n";
            }
        }

        public void displayReport()
        {
            //Scrolling/fast changing text in a text box somewhere

            

        }

    }
}
