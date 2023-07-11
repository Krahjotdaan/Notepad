using System;
using System.Windows;

namespace Tekstoviy_redaktor
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
	{
		public SearchWindow()
		{
			InitializeComponent();
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = (MainWindow)Owner;
			mainWindow.Activate();
			mainWindow.Find_Text(SearchedWord.Text);
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();	
		}

		private void SearchForm_Closed(object sender, EventArgs e)
		{
			MainWindow mainWindow = (MainWindow)Owner;
			mainWindow.Set_isSearchWindow(false);
		}

        private void SearchForm_Loaded(object sender, RoutedEventArgs e)
        {
			MainWindow mainWindow = (MainWindow)Owner;
			mainWindow.Set_isSearchWindow(true);
		}
    }
}
