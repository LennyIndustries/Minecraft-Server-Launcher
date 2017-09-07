using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Minecraft_Server_Launcher.Properties;
using MessageBox = System.Windows.MessageBox;

namespace Minecraft_Server_Launcher
{
	class ServerManager
	{
		private readonly string _optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
										   @"\Lenny Industries\MC Server Manager\Options.txt";

		public ServerManager()
		{
			_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Lenny Industries\MC Server Manager";
			_warningStatus = false;
			_serverList = new List<string>();
			_serverListFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
							  @"\Lenny Industries\MC Server Manager\Server List.txt"; // Default server list path

			Initialize();
		}

		public bool _warningStatus { get; set; }
		public string _path { get; set; }
		public List<string> _serverList { get; }
		public string _serverListFile { get; set; }

		public void Initialize()
		{
			if (!Directory.Exists(_path))
			{
				try
				{
					Directory.CreateDirectory(_path);
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
			}

			if (!File.Exists(_optionsFile))
			{
				try
				{
					File.Create(_optionsFile).Close();
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
			}

			if (!File.Exists(_serverListFile))
			{
				try
				{
					File.Create(_serverListFile).Close();
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
			}
		}

		public void UpdateOptions()
		{
			StreamWriter sw = null;

			try
			{
				sw = new StreamWriter(_optionsFile);
				string option = "NoWarningOnExit:" + _warningStatus;
				string path = "ServerListPath:" + _path;

				sw.WriteLine(option);
				sw.WriteLine(path);

				_serverListFile = _path + @"\Server List.txt";
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
		}

		public void LoadOptions()
		{
			StreamReader sr = null;

			try
			{
				sr = new StreamReader(_optionsFile);
				string option, setting, line;

				while ((line = sr.ReadLine()) != null)
				{
					option = line.Substring(0, line.IndexOf(":", StringComparison.Ordinal));
					Console.WriteLine(option); // Debug
					setting = line.Substring(line.IndexOf(":", StringComparison.Ordinal) + 1);
					Console.WriteLine(setting); // Debug

					switch (option)
					{
						case "NoWarningOnExit":
							if (setting.ToLower() == "true")
							{
								_warningStatus = true;
								Console.WriteLine(Resources.ServerManager_LoadOptions_Debug); // Debug
							}
							break;
						case "ServerListPath":
							_serverListFile = setting + @"\Server List.txt";
							Console.WriteLine(_serverListFile); // Debug
							break;
					}
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
		}

		public void SaveServerList()
		{
			StreamWriter sw = null;

			try
			{
				sw = new StreamWriter(_serverListFile);

				sw.Write("");

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
		}

		public void LoadServerList()
		{
			StreamReader sr = null;

			try
			{
				sr = new StreamReader(_serverListFile);
				string line;

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
		}

		public void ExportServerList()
		{
			StreamReader sr = null;
			StreamWriter sw = null;
			List<string> settingsList = new List<string>();
			string path = _path + @"\Server List.txt";

			try
			{
				var dialog = new FolderBrowserDialog
				{
					Description = Resources.ServerManager_ExportServerList_Description
				};
				dialog.ShowDialog();
				path = dialog.SelectedPath + @"\Server List.txt";
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

			try
			{
				sr = new StreamReader(_serverListFile);
				string line;

				while ((line = sr.ReadLine()) != null)
				{
					settingsList.Add(line);
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
				sw = new StreamWriter(path);

				for (int i = 0; i < settingsList.Count; i++)
				{
					sw.WriteLine(settingsList[i]);
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
		}

		public void ImportServerList()
		{
			StreamReader sr = null;
			StreamWriter sw = null;
			List<string> settingsList = new List<string>();
			string path = null;

			try
			{
				var dialog = new OpenFileDialog();
				dialog.Filter = Resources.ServerManager_ImportServerList_Filter;
				dialog.Multiselect = false;
				dialog.ShowDialog();
				path = dialog.FileName;
				Console.WriteLine(path); // Debug
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

			try
			{
				if (path != null && path.Contains(".txt"))
				{
					sr = new StreamReader(path);

					string line;

					while ((line = sr.ReadLine()) != null)
					{
						settingsList.Add(line);
					}
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
				if (path != null && path.Contains(".txt"))
				{
					sw = new StreamWriter(_serverListFile);

					for (int i = 0; i < settingsList.Count; i++)
					{
						sw.WriteLine(settingsList[i]);
					}
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
		}

		public void ChangeServerListLocation()
		{
			try
			{
				var dialog = new FolderBrowserDialog();
				dialog.Description = Resources.ServerManager_ChangeServerListLocation_Description;
				dialog.ShowDialog();
				_path = dialog.SelectedPath;
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

			UpdateOptions();
		}

		public void AddServer(string serverInfo)
		{
			_serverList.Add(serverInfo);
		}

		public void RemoveServerAt(int index)
		{
			_serverList.RemoveAt(index);
		}

		public void StartServer(int index)
		{
			string serverBatFile = _serverList[index].Substring(_serverList[index].IndexOf(":", StringComparison.Ordinal) + 1);
			string serverDir = _serverList[index].Substring(_serverList[index].IndexOf(":", StringComparison.Ordinal) + 1, _serverList[index].LastIndexOf(@"\", StringComparison.Ordinal) - _serverList[index].IndexOf(":", StringComparison.Ordinal));
			string serverName = _serverList[index].Substring(0, _serverList[index].IndexOf(":", StringComparison.Ordinal));
			Console.WriteLine(serverBatFile); // Debug
			Console.WriteLine(serverDir); // Debug
			Console.WriteLine(serverName); // Debug

			MessageBoxResult result = MessageBox.Show("Start: " + serverName, "Start Server", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				ProcessStartInfo processStartInfo;
				Process process;

				processStartInfo = new ProcessStartInfo(serverBatFile);
				processStartInfo.CreateNoWindow = false;
				processStartInfo.UseShellExecute = true;
				processStartInfo.WorkingDirectory = serverDir;
				processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

				process = Process.Start(processStartInfo);
				if (process != null) process.WaitForExit();
			}
		}

		public void MangageServer()
		{

		}

		public void ClearServerList()
		{
			_serverList.Clear();
			SaveServerList();
		}
	}
}
