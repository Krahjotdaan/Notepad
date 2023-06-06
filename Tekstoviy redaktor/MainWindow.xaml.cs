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
using System.IO;
using Microsoft.Win32;
using System.Windows.Xps.Packaging;
using System.Drawing.Printing;

namespace Tekstoviy_redaktor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Filename = "";

        public MainWindow()
        {
            InitializeComponent();  
        }

        private void new_document_Click(object sender, RoutedEventArgs e)
        {
            if (Filename != "")
            {
                TextWriter textWriter = new StreamWriter(@Filename);
                textWriter.Write(textbox.Text);
                textWriter.Close();
                textbox.Text = "";
                Filename = "";
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы |*.txt|Все файлы |*.*";

                if (saveFileDialog.ShowDialog() == true)
                {
                    Filename = saveFileDialog.FileName;
                    TextWriter textWriter = new StreamWriter(@saveFileDialog.FileName);
                    textWriter.Write(textbox.Text);
                    textWriter.Close();
                    textbox.Text = "";
                    Filename = "";
                }
            }
        }

        private void new_window_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show(); 
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы |*.txt|Все файлы |*.*";
            
            if (openFileDialog.ShowDialog() == true)
            {
                Filename = openFileDialog.FileName;
                textbox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (Filename != "")
            {
                TextWriter textWriter = new StreamWriter(@Filename);
                textWriter.Write(textbox.Text);
                textWriter.Close();
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы |*.txt|Все файлы |*.*";

                if (saveFileDialog.ShowDialog() == true)
                {
                    Filename = saveFileDialog.FileName;
                    TextWriter textWriter = new StreamWriter(@saveFileDialog.FileName);
                    textWriter.Write(textbox.Text);
                    textWriter.Close();
                }
            }
        }

        private void save_as_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы |*.txt|Все файлы |*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                Filename = saveFileDialog.FileName;
                TextWriter textWriter = new StreamWriter(@saveFileDialog.FileName);
                textWriter.Write(textbox.Text);
                textWriter.Close();
            }
        }

        private void page_settings_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.PageSetupDialog pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            pageSetupDialog.PageSettings = new PageSettings();

            pageSetupDialog.ShowNetwork = false;

            System.Windows.Forms.DialogResult result = pageSetupDialog.ShowDialog();
        }

        private void printing_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();

            try
            {
                XpsDocument xpsDocument = new XpsDocument(Filename, FileAccess.Read);
                FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
                DocumentPaginator docPaginator = fixedDocSeq.DocumentPaginator;
                printDialog.PrintDocument(docPaginator, $"Printing {System.IO.Path.GetFileName(Filename)}");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
