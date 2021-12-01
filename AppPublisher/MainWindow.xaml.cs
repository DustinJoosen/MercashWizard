using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace AppPublisher
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			var version = new Version
			{
				VersionNumber = Convert.ToDecimal(txt_version.Text),
				FilesLocation = txt_location.Text,
				ReleasedAt = (DateTime)dp_releasedate.SelectedDate,
				Description = txt_desc.Text
			};

			var client = new HttpClient
			{
				BaseAddress = new Uri("https://localhost:44341")
			};

			var json = JsonSerializer.Serialize(version);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var result = await client.PostAsync("/version", content);
			if (result.IsSuccessStatusCode)
			{
				MessageBox.Show("new version successfully uploaded", "Success");
			}
			else
			{
				MessageBox.Show("something went wrong. HTTP Status code:" + result.StatusCode);
			}
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//var version = await Version.Load();

			//txt_version.Text = version.VersionNumber.ToString();
			//txt_desc.Text = version.Description.ToString();
			//txt_location.Text = version.FilesLocation.ToString();
			//dp_releasedate.SelectedDate = version.ReleasedAt;
		}
	}
}
