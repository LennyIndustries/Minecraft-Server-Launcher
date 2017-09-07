using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Minecraft_Server_Launcher
{
	/// <summary>
	/// Interaction logic for AddServer.xaml
	/// </summary>
	public partial class AddServer
	{
		private string _serverListFile;
		private string _serverName, _path;
		private List<string> _serverList;

		public AddServer(string serverListFile)
		{
			InitializeComponent();

			_serverListFile = serverListFile;
		}

		private void WriteChanges()
		{
			StreamWriter sw = null;
			StreamReader sr = null;

			try
			{
				sr = new StreamReader(_serverListFile);
				string line;

				_serverList = new List<string>();
				_serverList.Clear();

				while ((line = sr.ReadLine()) != null)
				{
					_serverList.Add(line);
				}
			}
			catch (IOException exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}
			catch (UnauthorizedAccessException exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}
			catch (Exception exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}
			finally
			{
				if (sr != null) sr.Close();
			}

			try
			{
				sw = new StreamWriter(_serverListFile);

				sw.WriteLine(_serverName + ":" + _path);

				foreach (string serverInfo in _serverList)
				{
					sw.WriteLine(serverInfo);
				}
			}
			catch (IOException exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}
			catch (UnauthorizedAccessException exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}
			catch (Exception exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}
			finally
			{
				if (sw != null) sw.Close();
			}

			Close();
		}

		private void BrowseButton_OnClick(object sender, RoutedEventArgs e)
		{
			_path = null;

			try
			{
				var dialog = new OpenFileDialog();
				dialog.Filter = Properties.Resources.AddServer_BrowseButton_OnClick_Filter;
				dialog.Multiselect = false;
				dialog.ShowDialog();
				_path = dialog.FileName;
				Console.WriteLine(_path); // Debug
			}
			catch (IOException exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}
			catch (UnauthorizedAccessException exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}
			catch (Exception exception)
			{
				MessageBox.Show($"ERROR\n\n{exception.Message}\n\n{exception.Source}\n\n{exception.StackTrace}", "ERROR",
					MessageBoxButton.OK, MessageBoxImage.Error);
				Console.WriteLine(exception);
			}

			if (_path != null)
			{
				serverLocationTextBox.Text = _path;
			}
		}

		private void AddServerButton_OnClick(object sender, RoutedEventArgs e)
		{
			_serverName = serverNameTextBox.Text;
			_path = serverLocationTextBox.Text;

			WriteChanges();
		}
	}
}
