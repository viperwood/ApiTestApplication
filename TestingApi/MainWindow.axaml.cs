using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TestingApi.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace TestingApi;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Test();
    }
    
    private async Task Test()
    {
        using (var client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:5110/api/Controller2/GetUserInfo");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<UserInfo> information = JsonConvert.DeserializeObject<List<UserInfo>>(content)!.ToList();
                TestList.ItemsSource = information;
            }
        }
    }
    private async void CreateUser(object? sender, RoutedEventArgs e)
    {
        using (var client = new HttpClient())
        {
            string username = TextName.Text!;
            string loginuser = TextLogin.Text!;
            string role = TextRole.Text!;
            string passworduser = TextPassword.Text!;
            if (username != null && loginuser != null && passworduser != null)
            {
                HttpResponseMessage responseMessage = await client.GetAsync($"http://localhost:5110/api/Controller2/GetSave?nameUser={username}&login={loginuser}&password={passworduser}&role={role}");
            }
        }
    }

    private void Update(object? sender, RoutedEventArgs e)
    {
        Test();
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        MenuWindow menuWindow = new MenuWindow();
        menuWindow.Show();
        Close();
    }
}
