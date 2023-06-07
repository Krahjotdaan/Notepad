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
using System.Reflection;

namespace Tekstoviy_redaktor
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string Filename = null;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void new_document_Click(object sender, RoutedEventArgs e)
		{
			if (Filename != null)
			{
				TextWriter textWriter = new StreamWriter(@Filename);
				textWriter.Write(textbox.Text);
				textWriter.Close();
				textbox.Clear();
				Filename = null;
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
					textbox.Clear();
					Filename = null;
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
			if (Filename != null)
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
			pageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();

			pageSetupDialog.ShowNetwork = false;

			System.Windows.Forms.DialogResult result = pageSetupDialog.ShowDialog();
		}

		private void printing_Click(object sender, RoutedEventArgs e)
		{
			PrintDialog printDialog = new PrintDialog();
			printDialog.ShowDialog();
		}

		private void exit_Click(object sender, RoutedEventArgs e)
		{
			if (Filename != null)
			{
				TextWriter textWriter = new StreamWriter(@Filename);
				textWriter.Write(textbox.Text);
				textWriter.Close();
				Close();
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
				Close();
			}
		}

		private void cancel_Click(object sender, RoutedEventArgs e)
		{
			textbox.Undo();
		}

		private void redo_Click(object sender, RoutedEventArgs e)
		{
			textbox.Redo();
		}

		private void cut_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.Clear();
			textbox.Cut();
		}

		private void copy_Click(object sender, RoutedEventArgs e)
		{
			textbox.Copy();
		}

		private void insert_Click(object sender, RoutedEventArgs e)
		{
			textbox.Paste();
		}

		private void delete_Click(object sender, RoutedEventArgs e)
		{
			textbox.SelectedText = "";
		}

		private void find_Click(object sender, RoutedEventArgs e)
		{

		}

		private void replace_Click(object sender, RoutedEventArgs e)
		{

		}

		private void go_to_Click(object sender, RoutedEventArgs e)
		{

		}

		private void highlight_all_Click(object sender, RoutedEventArgs e)
		{
			textbox.SelectAll();
		}

		private void datetime_Click(object sender, RoutedEventArgs e)
		{
			string tmp = Clipboard.GetText();
			DateTime dateTime = DateTime.Now;
			Clipboard.SetText(dateTime.ToLocalTime().ToString());
			textbox.Paste();
			Clipboard.SetText(tmp);
		}

		private void font_style_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();

			if (fontDialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                string fontfamily = fontDialog.Font.FontFamily.ToString();
				string fontstyle = fontDialog.Font.Style.ToString();
                textbox.FontFamily = new FontFamily(fontfamily);
				textbox.FontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), fontstyle, true);
				
				textbox.FontSize = fontDialog.Font.Size;
            }
		}

		private void increase_scale_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void decrease_scale_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void default_scale_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void about_program_Click(object sender, RoutedEventArgs e)
		{
			Window1 about_program = new Window1();
			about_program.Show();
		}
	}
}
