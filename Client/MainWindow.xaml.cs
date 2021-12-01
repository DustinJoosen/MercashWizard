using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Client
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

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{

			lbl_loading.Visibility = Visibility.Visible;

			//open a client
			using (var client = new HttpClient{ BaseAddress = new Uri("https://localhost:44341") })
			{
				//send a get request to the api
				var response = await client.GetAsync("/version");
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					//get both the new and old version
					var newVersion = JsonConvert.DeserializeObject<Version>(content);
					var oldVersion = Version.Load();

					//if the new version has changed, let the users know
					if (!newVersion.Equals(oldVersion))
					{
						//check if the user wants to update
						var result = MessageBox.Show($"Version {newVersion.VersionNumber.ToString()} has come out! Do you want to update to this new version?", "New version", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.Yes)
						{
							MessageBox.Show("New version is downloading");
							Version.Save(newVersion);
						}

					}
					//when the version is still the same, do nothing
					else
					{
						MessageBox.Show("No new version...");
					}

					//display the current version data
					var version = Version.Load();
					lbl_version.Content = version.VersionNumber.ToString();
					lbl_releasedate.Content = version.ReleasedAt.ToString();
					lbl_location.Content = version.FilesLocation.ToString();
					lbl_desc.Content = version.Description.ToString();
					lbl_loading.Visibility = Visibility.Hidden;

				}
				else
				{
					MessageBox.Show("could not retrieve latest version details");
				}
			}


		}
	}
}
