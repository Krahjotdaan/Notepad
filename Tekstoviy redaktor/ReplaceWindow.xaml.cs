using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Tekstoviy_redaktor
{
    /// <summary>
    /// Логика взаимодействия для ReplaceWindow.xaml
    /// </summary>
    public partial class ReplaceWindow : Window
    {
        int position = -1;
        int count;
        public ReplaceWindow()
        {
            InitializeComponent();
        }

        private void ReplaceForm_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;
            mainWindow.Activate();
            mainWindow.Set_isReplaceWindow(true);
        }

        private void ReplaceForm_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;
            mainWindow.Activate();
            mainWindow.Set_isReplaceWindow(false);
        }

        private void SearchNextButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;
            mainWindow.Activate();
            if (IsRegister.IsChecked == false)
            {
                count = Regex.Matches(mainWindow.textbox.Text, Regex.Escape(ReplacedText.Text), RegexOptions.IgnoreCase).Count;
            }
            else
            {
                count = Regex.Matches(mainWindow.textbox.Text, Regex.Escape(ReplacedText.Text)).Count;
            }
            if (position + 1 < count)
            {
                position++;
            }
            matches.Content = $"Совпадение: {position + 1}/{count}";
            mainWindow.Find_Text(ReplacedText.Text, position, (bool)IsRegister.IsChecked);
        }

        private void SearchPrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (position > 0)
            {
                position--;
            }
            MainWindow mainWindow = (MainWindow)Owner;
            mainWindow.Activate();
            matches.Content = $"Совпадение: {position + 1}/{count}";
            mainWindow.Find_Text(ReplacedText.Text, position, (bool)IsRegister.IsChecked);
        }

        private void ReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;
            mainWindow.Activate();
            if (mainWindow.textbox.SelectedText.Length > 0)
            {
                mainWindow.textbox.SelectedText = NewText.Text;
                position--;
            }
            else
            {
                MessageBox.Show($"Не удаётся заменить {ReplacedText.Text} на {NewText.Text}");
            }
        }

        private void ReplaceAllButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;
            mainWindow.Activate();
            if (IsRegister.IsChecked == false)
            {
                mainWindow.textbox.Text = Regex.Replace(mainWindow.textbox.Text, ReplacedText.Text, NewText.Text, RegexOptions.IgnoreCase);
            }
            else
            {
                mainWindow.textbox.Text = Regex.Replace(mainWindow.textbox.Text, ReplacedText.Text, NewText.Text);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
