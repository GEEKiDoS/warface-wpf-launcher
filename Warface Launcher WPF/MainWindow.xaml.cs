using System;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace Warface_Launcher_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "c30b2cb5-6265-4d02-8d31-ea693ad6f5e1";
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("launcherconfig.cfg"))
            {
                StreamReader sr = new StreamReader("launcherconfig.cfg");
                string str = sr.ReadToEnd();
                string[] config = str.Split(':');
                textBox.Text = config[0];
                textBox1.Text = config[1];
                textBox2.Text = config[2];
                sr.Close();
            }
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("launcherconfig.cfg"))
                File.Delete("launcherconfig.cfg");
            StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\launcherconfig.cfg");
            sw.Write(textBox.Text + ":");
            sw.Write(textBox1.Text + ":");
            sw.Write(textBox2.Text);
            sw.Close();
            Process wf = new Process();
            if (File.Exists("game.exe"))
                wf.StartInfo.FileName = "game.exe";
            else if (File.Exists(Environment.CurrentDirectory + "\\Bin32Release\\game.exe"))
                wf.StartInfo.FileName = Environment.CurrentDirectory + "\\Bin32Release\\game.exe";
            else
            {
                MessageBox.Show("Game exe not found,please sure you put this to you game directory!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
                wf.StartInfo.Arguments = "+online_use_tls " + Convert.ToInt32(checkBox.IsChecked).ToString() + " +online_use_protect " + Convert.ToInt32(checkBox1.IsChecked).ToString() + " -uid " + textBox.Text + " -token " + textBox1.Text + " " + textBox3.Text;
            else
                wf.StartInfo.Arguments = "+online_use_tls " + Convert.ToInt32(checkBox.IsChecked).ToString() + " +online_use_protect " + Convert.ToInt32(checkBox1.IsChecked).ToString() + " +online_server" + textBox2.Text + " -uid " + textBox.Text + " -token " + textBox1.Text + " " + textBox3.Text;
            wf.Start();
                button.IsEnabled = false;
                button.Content = "STARTED";
            wf.Exited += reset;
        }

        private void reset(object sender, EventArgs e)
        {
            button.IsEnabled = true;
            button.Content = "PLAY";
        }
    }
}

