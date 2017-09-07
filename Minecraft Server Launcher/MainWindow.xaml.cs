using System;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

namespace Minecraft_Server_Launcher
{
	/// <summary>
	/// Lenny Industries
	/// Minecraft Server Manager
	/// Licence: GNU AFFERO GENERAL PUBLIC v3.0
	/// </summary>
	public partial class MainWindow
	{
		private readonly ServerManager _serverManager;

		public MainWindow()
		{
			InitializeComponent();

			_serverManager = new ServerManager();
			_serverManager.LoadOptions();
			_serverManager.LoadServerList();

			noWarningOnExitMenuItem.IsChecked = _serverManager._warningStatus;
			noWarningOnExitMenuItem.Checked += OptionsChanged;
			noWarningOnExitMenuItem.Unchecked += OptionsChanged;

			SetServerList();
		}

		private void SetServerList()
		{
			serverListBox.Items.Clear();

			foreach (string server in _serverManager._serverList)
			{
				AddServerToList(server);
			}

			SortServerList();
		}

		private void AddServerToList(string server)
		{
			ListBoxItem serverListBoxItem = new ListBoxItem();
			serverListBoxItem.Content = server;
			serverListBox.Items.Add(serverListBoxItem);
		}

		private void SortServerList()
		{
			serverListBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Content", System.ComponentModel.ListSortDirection.Ascending));
		}

		// Menu

		private void ImportServerList_OnClick(object sender, RoutedEventArgs e)
		{
			_serverManager.ImportServerList();
		}

		private void ExportServerList_OnClick(object sender, RoutedEventArgs e)
		{
			_serverManager.ExportServerList();
		}

		private void OptionsChanged(object sender, RoutedEventArgs e)
		{
			_serverManager._warningStatus = noWarningOnExitMenuItem.IsChecked;
			_serverManager.UpdateOptions();
		}

		private void ChangeServerListLocation_OnClick(object sender, RoutedEventArgs e)
		{
			_serverManager.ChangeServerListLocation();
		}

		private void Exit_OnClick(object sender, RoutedEventArgs e) // For menu and button
		{
			if (noWarningOnExitMenuItem.IsChecked == false)
			{
				MessageBoxResult result = MessageBox.Show("Exit?", "EXIT", MessageBoxButton.YesNo);

				if (result == MessageBoxResult.Yes)
				{
					Close();
				}
			}
			else
			{
				Close();
			}
		}

		// Buttons

		private void LaunchServerButton_OnClick(object sender, RoutedEventArgs e)
		{
			_serverManager.StartServer(serverListBox.SelectedIndex);
		}

		private void ManageServerButton_OnClick(object sender, RoutedEventArgs e)
		{

		}

		private void AddServerButton_OnClick(object sender, RoutedEventArgs e)
		{
			_serverManager.SaveServerList();

			Window addServerWindow = new AddServer(_serverManager._serverListFile);
			addServerWindow.Show();

			addServerWindow.Closed += Window_Closed;
		}

		private void RemoveServerButton_OnClick(object sender, RoutedEventArgs e)
		{
			int index;
			string server;

			index = serverListBox.SelectedIndex;
			server = serverListBox.SelectedItem.ToString();
			server = server.Substring(server.IndexOf(":", StringComparison.Ordinal) + 1);
			server = server.Substring(0, server.IndexOf(":", StringComparison.Ordinal));

			MessageBoxResult result = MessageBox.Show("Remove server: " + server, "Remove Server", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				_serverManager.RemoveServerAt(index);
			}

			SetServerList();
			_serverManager.SaveServerList();
		}

		private void ClearServerListButton_OnClick(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Clear server list?", "Clear Server List", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				_serverManager.ClearServerList();
				_serverManager.LoadServerList();
				SetServerList();
			}
		}

		private void SortServerListButton_OnClick(object sender, RoutedEventArgs e)
		{
			SortServerList();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			_serverManager.LoadServerList();
			SetServerList();
		}
	}
}
