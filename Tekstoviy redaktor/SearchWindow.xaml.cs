using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Tekstoviy_redaktor
{
	/// <summary>
	/// Логика взаимодействия для SearchWindow.xaml
	/// </summary>
	public partial class SearchWindow : Window
	{
		int position = -1;
		int count;
		public SearchWindow()
		{
			InitializeComponent();
		}

		private void SearchNextButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = (MainWindow)Owner;
			mainWindow.Activate();
			if (IsRegister.IsChecked == false)
            {
				count = Regex.Matches(mainWindow.textbox.Text, Regex.Escape(SearchedWord.Text), RegexOptions.IgnoreCase).Count;
			}
			else
            {
				count = Regex.Matches(mainWindow.textbox.Text, Regex.Escape(SearchedWord.Text)).Count;
			}
			if (position + 1 < count)
            {
				position++;
            }
			matches.Content = $"Совпадение: {position + 1}/{count}";
			mainWindow.Find_Text(SearchedWord.Text, position, (bool)IsRegister.IsChecked);
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
			mainWindow.Find_Text(SearchedWord.Text, position, (bool)IsRegister.IsChecked);
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
