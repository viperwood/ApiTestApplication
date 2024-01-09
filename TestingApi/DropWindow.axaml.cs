using System.Net.Http;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace TestingApi;

public partial class DropWindow : Window
{
    public DropWindow()
    {
        InitializeComponent();
    }

    private async void DropUser(object? sender, RoutedEventArgs e)
    {
        if (TextBoxNameUser.Text != null && TextBoxNameUser.Text != "")
        {

            using (var client = new HttpClient())
            {
                string user = TextBoxNameUser.Text;
                HttpResponseMessage responseMessage = await client.GetAsync($"http://localhost:5110/api/Controller2/GetDrop?login={user}");
            }
        }
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        MenuWindow menuWindow = new MenuWindow();
        menuWindow.Show();
        Close();
    }
}