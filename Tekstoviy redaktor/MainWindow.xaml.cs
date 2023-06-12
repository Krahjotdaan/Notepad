using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Tekstoviy_redaktor
{
	public partial class MainWindow : Window
	{
		string Filename = null;

		public MainWindow()
		{
			InitializeComponent();
			digit_bar.Text = "1";
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
			fontDialog.ShowEffects = false;

			if (fontDialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
				string fontfamily = fontDialog.Font.FontFamily.ToString();
                textbox.FontFamily = new FontFamily(fontfamily);	
				textbox.FontSize = fontDialog.Font.Size;
				digit_bar.FontSize = fontDialog.Font.Size;
				
				if (fontDialog.Font.Bold)
                {
					if (fontDialog.Font.Italic)
                    {
						textbox.FontWeight = FontWeights.Bold;
						textbox.FontStyle = FontStyles.Italic;
					}
					else
                    {
						textbox.FontStyle = FontStyles.Normal;
						textbox.FontWeight = FontWeights.Bold;
                    }	
                } 
				else if (fontDialog.Font.Italic)
                {
					textbox.FontStyle = FontStyles.Italic;
					textbox.FontWeight = FontWeights.Normal;
				}
				else if (!fontDialog.Font.Bold & !fontDialog.Font.Italic)
                {
					textbox.FontWeight = FontWeights.Normal;
					textbox.FontStyle = FontStyles.Normal;
				}
				else
                {
					textbox.FontWeight = FontWeights.Bold;
					textbox.FontStyle = FontStyles.Italic;
				}
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

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
			int strs = textbox.LineCount - 1;
			int s = 2;
			digit_bar.Text = "1";
			for (int i = 0; i < strs; i++)
            {
				digit_bar.Text += "\n" + s.ToString();
				s++;
            }
			digit_bar.ScrollToEnd();
        }

        private void textbox_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
			if (e.Delta > 0)
            {
				digit_bar.ScrollToVerticalOffset(digit_bar.VerticalOffset - 54);
				textbox.ScrollToVerticalOffset(textbox.VerticalOffset - 54);
            }	
			else if (e.Delta < 0)
            {
				digit_bar.ScrollToVerticalOffset(digit_bar.VerticalOffset + 54);
				textbox.ScrollToVerticalOffset(textbox.VerticalOffset + 54);
			}
        }
    }
}
