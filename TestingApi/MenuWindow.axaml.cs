using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using TestingApi.Models;

namespace TestingApi;

public partial class MenuWindow : Window
{
    public MenuWindow()
    {
        InitializeComponent();
        ListBoxInformation();
    }

    private async Task ListBoxInformation()
    {
        using (var client = new HttpClient())
        {
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5110/api/Controller2/GetUserInfo");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                List<UserInfo> information = JsonConvert.DeserializeObject<List<UserInfo>>(content)!.ToList();
                ListBoxUsers.ItemsSource = information;
            }
        }
    }

    private void AddUser(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void DropUser(object? sender, RoutedEventArgs e)
    {
        DropWindow dropWindow = new DropWindow();
        dropWindow.Show();
        Close();
    }

    private void EditingUser(object? sender, RoutedEventArgs e)
    {
        EditingWindow editingWindow = new EditingWindow();
        editingWindow.Show();
        Close();
    }

    private void SearchUser(object? sender, RoutedEventArgs e)
    {
        SearchWindow searchWindow = new SearchWindow();
        searchWindow.Show();
        Close();
    }

    private void OpenImage(object? sender, RoutedEventArgs e)
    {
        
    }
}