using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using TestingApi.Models;

namespace TestingApi;

public partial class SearchWindow : Window
{
    public SearchWindow()
    {
        InitializeComponent();
        InformationFromList();
    }

    private List<UserInfo>? _userInfos;
    private async void InformationFromList()
    {
        using (var client = new HttpClient())
        {
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5110/api/Controller2/GetUserInfo");
            if (responseMessage.IsSuccessStatusCode)
            {
                string information = await responseMessage.Content.ReadAsStringAsync();
                _userInfos = JsonConvert.DeserializeObject<List<UserInfo>>(information)!.ToList();
                ListInfo.ItemsSource = _userInfos;
            }
        }
    }
    private List<UserInfo> searchList = new List<UserInfo>();
    private void Serch(object? sender, KeyEventArgs e)
    {
        searchList.Clear();
        if (!string.IsNullOrEmpty(SearchBox.Text))
        {
            foreach (var information in _userInfos)
            {
                var height = information.Login!.Length+1;
                var width = SearchBox.Text.Length+1;
                var errors = new int[height, width];
                for (int i = 0; i < height; i++)
                {
                    errors[i, 0] = i;
                }
                for (int i = 0; i < width; i++)
                {
                    errors[0, i] = i;
                }
                for (int x = 1; x < height; x++)
                {
                    for (int y = 1; y < width; y++)
                    {
                        var element = information.Login[x-1]==SearchBox.Text[y-1] ? 1:0;
                        errors[x, y] = Minimum
                        (
                            errors[x-1,y]+1,
                            errors[x,y-1]+1,
                            errors[x-1,y-1]+element
                        );
                    }
                }
            }
            ListInfo.ItemsSource = searchList.Select(x => new
            {
                Login = x.Login,
                Fullname = x.Fullname,
                Password = x.Password
            });
        }
        else
        {
            InformationFromList();
        }
    }
    private int Minimum(int a, int b, int c)
    {
        if (a > b)
        {
            a = b;
        }
        if (a > c)
        {
            a = c;
        }
        return a;
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        MenuWindow menuWindow = new MenuWindow();
        menuWindow.Show();
        Close();
    }
}