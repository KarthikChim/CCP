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
        public Boolean readystat;
        public Window1()
        {
            //Not going to use this but might as well add this here anyway
            if (!OperatingSystem.IsWindows())
            {
                MessageBox.Show("WARNING\nYour operating system is not Windows and will not work with the following section of this program.", "Unsupported OS", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

            readystat = false;
            currentdir = Directory.GetCurrentDirectory();
            //MessageBox.Show(currentdir);
            info = new ArrayList();

            InitializeComponent();

            report = currentdir + "\\ccptempreport.txt";
            reportText.Text = "There's a good chance this window is going to freeze while generating the system report. That is normal because the text report is being processed in the same thread as this window, and can be safely ignored.\n\n" + "Generating report to: " + report + "\n\n" + reportText.Text + "\n";

            
            //displayReport();
            //Show();
            //generateReport();

            //reportText.Text = temp2

            Task.Run(() => generateReport());
        }

        public ArrayList getList()
        {
            return info;
        }

        public void generateReport()
        {
            //Process.Start("C:\\WINDOWS\\system32\\msinfo32.exe");

            Process temp = new Process();
            temp.StartInfo.FileName = "C:\\WINDOWS\\system32\\msinfo32.exe";
            temp.StartInfo.Arguments = "/report ccptempreport.txt";
            temp.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            temp.Start();

            Task.Run(() => updateLoading());

            temp.WaitForExit();
            readystat = true;

            if (!File.Exists(report))
            {
                readystat = true;
                MessageBox.Show("There was a problem generating the temporary report.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Dispatcher.InvokeAsync(() => {
                    reportText.Text += "\n\nThere was a problem generating the temporary report.";
                });
                Process.Start("C:\\WINDOWS\\explorer.exe", "\"" + currentdir + "\"");
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
                Process.Start("C:\\WINDOWS\\explorer.exe", "\"" + currentdir + "\"");
            }

            String temp2 = "";

            foreach (String s in info)
            {
                temp2 = temp2 + s + "\n";
            }

            readystat = true;

            Application.Current.Dispatcher.InvokeAsync(() => {
                reportText.Text = temp2;
            });

        }

        /*public void displayReport()
        {
            //Scrolling/fast changing text in a text box somewhere

            //reportText.Text += "\nHello there again!";
            
        }
        */

        public void updateLoading()
        {
            while (!readystat)
            {
                Application.Current.Dispatcher.InvokeAsync(() => {

                    reportText.Text += ".";
                });
                Thread.Sleep(1000);
            }
        }

    }
}
