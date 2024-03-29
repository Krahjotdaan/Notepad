﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Tekstoviy_redaktor
{
	public partial class MainWindow : Window
	{
		string Filename = null;
		double scalex = 1;
		double scaley = 1;
		bool isShift = false;
		bool isCtrl = false;
		static bool isSearchWindow = false;
		static bool isReplaceWindow = false;
		readonly Dictionary<string, string> langs = new Dictionary<string, string>()
		{
			{".txt", "Текстовый файл"},
			{".bat", "Batch"},
			{".c" , "C"},
			{".cpp" , "C++"},
			{".h" , "Заголовочный файл C"},
			{".hpp" , "Заголовочный файл C++"},
			{".cs" , "C#"},
			{".cmake" , "CMake"},
			{".css" , "CSS"},
			{".diff" , "Diff"},
			{".dockerfile" , "Docker"},
			{".fs" , "F#"},
			{".go" , "Golang"},
			{".groovy" , "Groovy"},
			{".html" , "HTML"},
			{".ini" , "Ini"},
			{".java" , "Java"},
			{".js" , "JavaScript"},
			{".json" , "JSON"},
			{".log" , "Log"},
			{".lua" , "Lua"},
			{".mak" , "Makefile"},
			{".m" , "Objective-C"},
			{".mm" , "Objective-C++"},
			{".pl" , "Perl"},
			{".php" , "PHP"},
			{".ps1" , "PowerShell"},
			{".conf" , "Properties"},
			{".py" , "Python"},
			{".r" , "R"},
			{".rb" , "Ruby"},
			{".rs" , "Rust"},
			{".sh" , "Shell Script"},
			{".sql" , "SQL"},
			{".swift" , "Swift"},
			{".toml" , "Toml"},
			{".ts" , "TypeScript"},
			{".vb" , "Visial Basic"},
			{".xml" , "XML"},
			{".yml" , "YAML"}
		};
		public MainWindow()
		{
			InitializeComponent();
			digit_bar.Text = "1";
			textbox.SelectionStart = 0;
		}

		/// <summary>
		/// sets the activity status of the search window
		/// </summary>
		/// <param name="new_flag"> new activity status </param>
		public void Set_isSearchWindow(bool new_flag)
		{
			isSearchWindow = new_flag;
		}

		/// <summary>
		/// sets the activity status of the replace window
		/// </summary>
		/// <param name="new_flag"> new activity status </param>
		public void Set_isReplaceWindow(bool new_flag)
		{
			isReplaceWindow = new_flag;
		}

		/// <summary>
		/// function of text selection by regular expression 
		/// </summary>
		/// <param name="serTxt"> search text </param>
		/// <param name="position"> number of match </param>
		/// <param name="withRegister"> case sensitive </param>
		public void Find_Text(string serTxt, int position, bool withRegister)
		{
			MatchCollection contains;
			if (!withRegister)
			{
				contains = Regex.Matches(textbox.Text, Regex.Escape(serTxt), RegexOptions.IgnoreCase);
			}
			else
			{
				contains = Regex.Matches(textbox.Text, Regex.Escape(serTxt));
			}

			if (contains.Count > 0)
			{
				int i = 0;
				foreach (Match match in contains)
				{
					if (i == position)
					{
						textbox.Select(match.Index, match.Length);
						break;
					}
					else
					{
						i++;
					}
				}
			}
			else
			{
				MessageBox.Show($"Не удаётся найти '{serTxt}'");
			}
		}

		/// <summary>
		/// creating a new document
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void New_document_Click(object sender, RoutedEventArgs e)
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
				saveFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
				"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
				"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak|Objective-C |*.m" +
				"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
				"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";

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

		/// <summary>
		/// opening new main window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void New_window_Click(object sender, RoutedEventArgs e)
		{
			MainWindow window = new MainWindow();
			window.Show();
		}

		/// <summary>
		/// calling dialog window of file opening
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Open_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();	
			openFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
				"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
				"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak|Objective-C |*.m" +
				"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
				"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";
			openFileDialog.FilterIndex = openFileDialog.Filter.Length - 1;
			
			if (openFileDialog.ShowDialog() == true)
			{
				Filename = openFileDialog.FileName;
				textbox.Text = File.ReadAllText(openFileDialog.FileName);
				window.Title = Filename + " - Notepad";
				try
				{
					languages.Text = langs[Path.GetExtension(Filename)];
				}
				catch (KeyNotFoundException)
				{
					if (Path.GetExtension(Filename) == ".cmd")
					{
						languages.Text = langs[".bat"];
					}
					if (Path.GetExtension(Filename) == ".jenkinsfile") {
						languages.Text = langs[".groovy"];
					}
					if (Path.GetExtension(Filename) == ".properties")
					{
						languages.Text = langs[".conf"];
					}
					if (Path.GetExtension(Filename) == ".bash")
					{
						languages.Text = langs[".sh"];
					}
					if (Path.GetExtension(Filename) == ".yaml")
					{
						languages.Text = langs[".yml"];
					}
				}		
			}
		}

		/// <summary>
		/// calling dialog window of file saving
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Save_Click(object sender, RoutedEventArgs e)
		{
			if (Filename != null)
			{
				TextWriter textWriter = new StreamWriter(Filename);
				textWriter.Write(textbox.Text);
				textWriter.Close();
			}
			else
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.CreatePrompt = true;
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
				"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
				"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak|Objective-C |*.m" +
				"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
				"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";
				saveFileDialog.FilterIndex = saveFileDialog.Filter.Length - 1;

				int selected_lang = languages.Items.IndexOf(languages.SelectedItem);
				saveFileDialog.FilterIndex = selected_lang + 1;

				if (saveFileDialog.ShowDialog() == true)
				{
					Filename = saveFileDialog.FileName;
					TextWriter textWriter = new StreamWriter(Filename);
					textWriter.Write(textbox.Text);
					textWriter.Close();
					window.Title = Filename + " - Notepad";
				}
			}
		}

		/// <summary>
		/// calling dialog window of file saving with extentions selection
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Save_as_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.CreatePrompt = true;
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
				"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
				"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak, *.mk|Objective-C |*.m" +
				"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
				"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";
			saveFileDialog.FilterIndex = saveFileDialog.Filter.Length - 1;

			int selected_lang = languages.Items.IndexOf(languages.SelectedItem);
			saveFileDialog.FilterIndex = selected_lang + 1;

			if (saveFileDialog.ShowDialog() == true)
			{
				Filename = saveFileDialog.FileName;
				TextWriter textWriter = new StreamWriter(Filename);
				textWriter.Write(textbox.Text);
				textWriter.Close();
				window.Title = Filename + " - Notepad";
			}
		}

		/// <summary>
		/// setting a page parameters for printing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_settings_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.PageSetupDialog pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
			pageSetupDialog.PageSettings = new PageSettings();
			pageSetupDialog.ShowNetwork = false;
			pageSetupDialog.ShowDialog();
		}

		/// <summary>
		/// calling dialog window of printing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Printing_Click(object sender, RoutedEventArgs e)
		{
			PrintDocument printDocument = new PrintDocument();
			printDocument.PrintPage += PrintHandler;
			PrintDialog printDialog = new PrintDialog();
			if (printDialog.ShowDialog() == true)
			{
				printDocument.Print();
			}
		}

		/// <summary>
		/// closing main window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			if (Filename != null)
			{
				TextWriter textWriter = new StreamWriter(Filename);
				textWriter.Write(textbox.Text);
				textWriter.Close();
				Close();
			}
			else
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.CreatePrompt = true;
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.FilterIndex = saveFileDialog.Filter.Length - 1;
				saveFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
				"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
				"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak|Objective-C |*.m" +
				"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
				"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";

				int selected_lang = languages.Items.IndexOf(languages.SelectedItem);
				saveFileDialog.FilterIndex = selected_lang + 1;

				if (saveFileDialog.ShowDialog() == true)
				{
					Filename = saveFileDialog.FileName;
					TextWriter textWriter = new StreamWriter(saveFileDialog.FileName);
					textWriter.Write(textbox.Text);
					textWriter.Close();
				}
				Close();
			}
		}

		/// <summary>
		/// undo last action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			textbox.Undo();
		}

		/// <summary>
		/// redo last action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Redo_Click(object sender, RoutedEventArgs e)
		{
			textbox.Redo();
		}

		/// <summary>
		/// cutting selected text and move it to clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Cut_Click(object sender, RoutedEventArgs e)
		{
			textbox.Cut();
		}

		/// <summary>
		/// copying selected text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Copy_Click(object sender, RoutedEventArgs e)
		{
			textbox.Copy();
		}

		/// <summary>
		/// pasting text on clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Insert_Click(object sender, RoutedEventArgs e)
		{
			textbox.Paste();
		}

		/// <summary>
		/// deleting selected text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			textbox.SelectedText = "";
		}

		/// <summary>
		/// calling dialog window of searching
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Find_Click(object sender, RoutedEventArgs e)
		{
			if (isSearchWindow == false)
			{
				SearchWindow searchWindow = new SearchWindow();
				searchWindow.Owner = this;
				searchWindow.Show();
			}		
		}

		/// <summary>
		/// calling dialog window of replacing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Replace_Click(object sender, RoutedEventArgs e)
		{
			if (isReplaceWindow == false)
			{
				ReplaceWindow replaceWindow = new ReplaceWindow();
				replaceWindow.Owner = this;
				replaceWindow.Show();
			}
		}

		/// <summary>
		/// selection of all text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Highlight_all_Click(object sender, RoutedEventArgs e)
		{
			textbox.SelectAll();
		}

		/// <summary>
		/// pasting datetime
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Datetime_Click(object sender, RoutedEventArgs e)
		{
			string tmp = Clipboard.GetText();
			DateTime dateTime = DateTime.Now;
			Clipboard.SetText(dateTime.ToLocalTime().ToString());
			textbox.Paste();
			Clipboard.SetText(tmp);
		}

		/// <summary>
		/// setting fontfamily, fontstyle and fontsize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Font_style_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
			//fontDialog.Font = new System.Drawing.Font(textbox.FontFamily.ToString(), (float)textbox.FontSize);
			fontDialog.ShowEffects = false;
			fontDialog.MaxSize = 48;
			
			//textbox.Text += "\n" + textbox.FontFamily.ToString() + "\n" + fontDialog.Font.FontFamily.ToString();

			if (fontDialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
			{
				string fontfamily = fontDialog.Font.FontFamily.ToString();
				textbox.FontFamily = new System.Windows.Media.FontFamily(fontfamily);	
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

		/// <summary>
		/// increase scale
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Increase_scale_Click(object sender, RoutedEventArgs e)
		{
			if (scalex < 3 && scaley < 3)
			{
				scalex += 0.25;
				scaley += 0.25;
				mainview.LayoutTransform = new ScaleTransform(scalex, scaley);
				scale1.Content = (scalex * 100).ToString() + '%';
			}
		}

		/// <summary>
		/// decrease scale
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Decrease_scale_Click(object sender, RoutedEventArgs e)
		{
			if (scalex > 0.25 && scaley > 0.25)
			{
				scalex -= 0.25;
				scaley -= 0.25;
				mainview.LayoutTransform = new ScaleTransform(scalex, scaley);
				scale1.Content = (scalex * 100).ToString() + '%';
			}
		}

		/// <summary>
		/// default scale
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Default_scale_Click(object sender, RoutedEventArgs e)
		{
			scalex = 1;
			scaley = 1;
			mainview.LayoutTransform = new ScaleTransform(scalex, scaley);
			scale1.Content = (scalex * 100).ToString() + '%';
		}

		/// <summary>
		/// calling dialog AboutProgram window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void About_program_Click(object sender, RoutedEventArgs e)
		{
			Window1 about_program = new Window1();
			if (about_program.ShowDialog() == true)
			{
				about_program.Show();
			}
		}

		/// <summary>
		/// handler of text changing, cursor positioning 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
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
			int row = 1;
			int tmp = -1;

			for (int i = 0; i <= textbox.SelectionStart; i++)
			{
				try
				{
					if (textbox.Text[i] == '\n')
					{
						row++;
						tmp = i;
					}
				}
				catch (IndexOutOfRangeException) {
					break;
				}
			}
			int col = textbox.SelectionStart - tmp;
			curs_position.Content = $"Стр: {row}; Стлб: {col}; Поз: {textbox.SelectionStart + 1}";
		}

		/// <summary>
		/// page scrolling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Textbox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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
		/// <summary>
		/// page scrolling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Textbox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			int row = 1;
			int tmp = -1;

			for (int i = 0; i <= textbox.SelectionStart; i++)
			{
				try
				{
					if (textbox.Text[i] == '\n')
					{
						row++;
						tmp = i;
					}
				}
				catch (IndexOutOfRangeException)
				{
					break;
				}
			}
			int col = textbox.SelectionStart - tmp;
			curs_position.Content = $"Стр: {row}; Стлб: {col}; Поз: {textbox.SelectionStart + 1}";
		}

		/// <summary>
		/// handler of hotkeys
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Textbox_PreviewKeyDown(object sender, KeyEventArgs e)
		{ 
			if (e.Key == Key.Left || e.Key == Key.Right)
			{
				int row = 1;
				int tmp = -1;

				for (int i = 0; i <= textbox.SelectionStart; i++)
				{
					try
					{
						if (textbox.Text[i] == '\n')
						{
							row++;
							tmp = i;
						}
					}
					catch (IndexOutOfRangeException)
					{
						break;
					}
				}
				int col = textbox.SelectionStart - tmp;
				curs_position.Content = $"Стр: {row}; Стлб: {col}; Поз: {textbox.SelectionStart + 1}";
			}
			if (e.Key == Key.F5)
			{
				string tmp = Clipboard.GetText();
				DateTime dateTime = DateTime.Now;
				Clipboard.SetText(dateTime.ToLocalTime().ToString());
				textbox.Paste();
				Clipboard.SetText(tmp);
			}
			if (isCtrl)
			{
				textbox.IsEnabled = false;
				if (e.Key == Key.N && isShift == false)
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
						saveFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
						"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
						"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak|Objective-C |*.m" +
						"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
						"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";

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
				if (e.Key == Key.S && isShift == false)
				{
					if (Filename != null)
					{
						TextWriter textWriter = new StreamWriter(Filename);
						textWriter.Write(textbox.Text);
						textWriter.Close();
					}
					else
					{
						SaveFileDialog saveFileDialog = new SaveFileDialog();
						saveFileDialog.CreatePrompt = true;
						saveFileDialog.OverwritePrompt = true;
						saveFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
						"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
						"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak|Objective-C |*.m" +
						"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
						"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";
						saveFileDialog.FilterIndex = saveFileDialog.Filter.Length - 1;

						int selected_lang = languages.Items.IndexOf(languages.SelectedItem);
						saveFileDialog.FilterIndex = selected_lang + 1;

						if (saveFileDialog.ShowDialog() == true)
						{
							Filename = saveFileDialog.FileName;
							TextWriter textWriter = new StreamWriter(Filename);
							textWriter.Write(textbox.Text);
							textWriter.Close();
							window.Title = Filename + " - Notepad";
						}
					}			
				}
				if (e.Key == Key.O && isShift == false)
				{
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
						"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
						"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak|Objective-C |*.m" +
						"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
						"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";
					openFileDialog.FilterIndex = openFileDialog.Filter.Length - 1;

					if (openFileDialog.ShowDialog() == true)
					{
						Filename = openFileDialog.FileName;
						textbox.Text = File.ReadAllText(openFileDialog.FileName);
						window.Title = Filename + " - Notepad";
						try
						{
							languages.Text = langs[Path.GetExtension(Filename)];
						}
						catch (KeyNotFoundException)
						{
							if (Path.GetExtension(Filename) == ".cmd")
							{
								languages.Text = langs[".bat"];
							}
							if (Path.GetExtension(Filename) == ".jenkinsfile")
							{
								languages.Text = langs[".groovy"];
							}
							if (Path.GetExtension(Filename) == ".properties")
							{
								languages.Text = langs[".conf"];
							}
							if (Path.GetExtension(Filename) == ".bash")
							{
								languages.Text = langs[".sh"];
							}
							if (Path.GetExtension(Filename) == ".yaml")
							{
								languages.Text = langs[".yml"];
							}
						}
					}
				}
				if (e.Key == Key.P && isShift == false)
				{

				}
				if (e.Key == Key.OemPlus && isShift == false)
				{
					if (scalex < 3 && scaley < 3)
					{
						scalex += 0.25;
						scaley += 0.25;
						mainview.LayoutTransform = new ScaleTransform(scalex, scaley);
						scale1.Content = (scalex * 100).ToString() + '%';
					}
				}
				if (e.Key == Key.OemMinus && isShift == false)
				{
					if (scalex > 0.25 && scaley > 0.25)
					{
						scalex -= 0.25;
						scaley -= 0.25;
						mainview.LayoutTransform = new ScaleTransform(scalex, scaley);
						scale1.Content = (scalex * 100).ToString() + '%';
					}
				}
				if (e.Key == Key.D0 && isShift == false)
				{
					scalex = 1;
					scaley = 1;
					mainview.LayoutTransform = new ScaleTransform(scalex, scaley);
					scale1.Content = (scalex * 100).ToString() + '%';
				}
				if (isShift)
				{
					if (e.Key == Key.N)
					{
						MainWindow window = new MainWindow();
						window.Show();
					}
					if (e.Key == Key.S)
					{
						SaveFileDialog saveFileDialog = new SaveFileDialog();
						saveFileDialog.CreatePrompt = true;
						saveFileDialog.OverwritePrompt = true;
						saveFileDialog.Filter = "Текстовые файлы |*.txt|Batch |*.bat, *.cmd|C |*.c|C++ |*.cpp|Заголовочный файл C |*.h|Заголовочный файл C++| *.hpp" +
							"|C# |*.cs|CMake| *.cmake|CSS |*.css|Diff |*.diff|Docker| *.docker|F# |*.fs|Golang |*.go|Groovy |*.groovy, *.jenkinsfile|HTML |*.html" +
							"|Ini |*.ini|Java |*.java|JavaScript |*.js|JSON |*.json|Log |*.log|Lua |*.lua|Makefile |.mak, *.mk|Objective-C |*.m" +
							"|Objective-C++ |*.mm|Perl |*.pl|PHP |*.php|PowerShell |*.ps1|Properties |*.conf, *.properties|Python |*.py|R |*.r|Ruby |*.rb" +
							"|Rust |*.rs|Shell Script |*.sh, *.bash|SQL |*.sql|Swift |*.swift|Toml |*.toml|TypeScript |*.ts|Visual Basic |*.vb|XML |*.xml|YAML |*.yml, *.yaml|Все файлы |*.*";
						saveFileDialog.FilterIndex = saveFileDialog.Filter.Length - 1;

						int selected_lang = languages.Items.IndexOf(languages.SelectedItem);
						saveFileDialog.FilterIndex = selected_lang + 1;

						if (saveFileDialog.ShowDialog() == true)
						{
							Filename = saveFileDialog.FileName;
							TextWriter textWriter = new StreamWriter(Filename);
							textWriter.Write(textbox.Text);
							textWriter.Close();
							window.Title = Filename + " - Notepad";
						}
					}
				}
			}
			textbox.IsEnabled = true;	
		}

		/// <summary>
		/// handler of pressing CONTROL
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Textbox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.LeftCtrl)
			{
				isCtrl = true;
			}
			if (e.Key == Key.LeftShift)
			{
				isShift = true;
			}
		}

		/// <summary>
		/// handler of releasing CONTROL
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Textbox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.LeftCtrl)
			{
				isCtrl = false;
			}
			if (e.Key == Key.LeftShift)
			{
				isShift = false;
			}
		}

		/// <summary>
		/// calling dialog window with information about hotkeys
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Info_Click(object sender, RoutedEventArgs e)
		{
			Info info = new Info();
			if (info.ShowDialog() == true)
			{
				info.Show();
			}
		}

		/// <summary>
		/// handler of text drawing before printing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PrintHandler(object sender, PrintPageEventArgs e)
		{
			string fontfam = textbox.FontFamily.Source;
			float fonts = (float)textbox.FontSize;
			e.Graphics.DrawString(textbox.Text, new Font(fontfam, fonts), System.Drawing.Brushes.Black, 0, 0);
		}
	}
}
