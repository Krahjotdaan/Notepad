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
using System.Windows.Shapes;

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

		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();	
		}

		private void SearchForm_Closed(object sender, EventArgs e)
		{
			MainWindow.Set_isSearchWindow(false);
		}

        private void SearchForm_Loaded(object sender, RoutedEventArgs e)
        {
			MainWindow.Set_isSearchWindow(true);
		}
    }
}
