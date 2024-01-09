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

public partial class EditingWindow : Window
{
    public EditingWindow()
    {
        InitializeComponent();
        Information();
    }

    private List<UserInfo> inform = new List<UserInfo>();

    private async Task Information()
    {
        using (var client = new HttpClient())
        {
            HttpResponseMessage inf = await client.GetAsync("http://localhost:5110/api/Controller2/GetUserInfo");
            if (inf.IsSuccessStatusCode)
            {
                string content = await inf.Content.ReadAsStringAsync();
                inform = JsonConvert.DeserializeObject<List<UserInfo>>(content)!.ToList();
                ListBoxUsers.ItemsSource = inform;
            }
        }
    }

    private void Button(object? sender, RoutedEventArgs e)
    {
        ReplaseResponseMessage();
    }



    private async Task ReplaseResponseMessage()
    {
        if (ListBoxUsers.SelectedIndex != -1)
        {
            string? roleText = null;
            string? userNameText = null;
            string? passwordText = null;
            string? loginText = null;
            string? userLoginFromReplase = inform[ListBoxUsers.SelectedIndex].Login;
            CheckData checkData = new CheckData();
            checkData.LoginTest = LoginBox.Text;
            if (checkData.LoginTest == "")
            {
                loginText = LoginBox.Text;
            }
            else
            {
                LoginBox.Text = checkData.LoginTest;
            }
            checkData.PasswordTest = PasswordBox.Text;
            if (checkData.PasswordTest == "")
            {
                passwordText = PasswordBox.Text;
            }
            else
            {
                PasswordBox.Text = checkData.PasswordTest;
            }
            checkData.UserNameTest = UserNameBox.Text;
            if (checkData.UserNameTest == "")
            {
                userNameText = UserNameBox.Text;
            }
            else
            {
                UserNameBox.Text = checkData.UserNameTest;
            }
            checkData.RoleTest = RoleBox.Text;
            if (checkData.RoleTest == "")
            {
                roleText = RoleBox.Text;
            }
            else
            {
                RoleBox.Text = checkData.RoleTest;
            }
            if (checkData.RoleTest == "" && checkData.UserNameTest == "" && checkData.PasswordTest == "" && checkData.LoginTest == "")
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage getreplase = await client.GetAsync($"http://localhost:5110/api/Controller2/GetReplase?login={userLoginFromReplase}&newLogin={loginText}&password={passwordText}&userName={userNameText}&role={roleText}");
                }
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