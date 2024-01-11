using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using System.IO;
using System.Linq;
using System.Net.Http;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using TestingApi.Models;
using Path = System.IO.Path;

namespace TestingApi;

public partial class OpenImageWindow : Window
{
    private string _pathDirectory;
    private string _pathImageName;
    public OpenImageWindow()
    {
        InitializeComponent();
        PathCombine();
        ListImage();
    }

    
    private async void Open(object? sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filters.Add(new FileDialogFilter() {Name = "jpeg", Extensions = {"jpeg"}});
        openFileDialog.Filters.Add(new FileDialogFilter() {Name = "jpg", Extensions = {"jpg"}});
        var path = await openFileDialog.ShowAsync(this);
        if (path != null)
        {
            _pathImageName = "";
            _pathImageName = Path.GetFileName(path[0]);
            string fulPathImage = Path.Combine(_pathDirectory,_pathImageName);
            File.Copy(path[0],fulPathImage,true);
            ImageFromAdd.Source = new Bitmap(new FileStream(path[0],FileMode.Open));
        }
    }

    private void PathCombine()
    { 
        _pathDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images\\");
        _pathDirectory = _pathDirectory.Replace("bin\\Debug\\net8.0\\", "");
    }


    private async Task SaveImage()
    {
        using (var content = new HttpClient())
        {
            HttpResponseMessage responseMessage = await content.GetAsync($"http://localhost:5110/api/Controller2/GetSaveImmage?immageName={_pathImageName}");
            ComplitText.Text = "Запрос сделан";
        }
    }

    private async Task ListImage()
    {
        using (var content = new HttpClient())
        {
            HttpResponseMessage responseMessage = await content.GetAsync("http://localhost:5110/api/Controller2/GetImmages");
            if (responseMessage.IsSuccessStatusCode)
            {
                string imageContent = await responseMessage.Content.ReadAsStringAsync();
                List<ImageInfo> listImage = JsonConvert.DeserializeObject<List<ImageInfo>>(imageContent)!.ToList();
                List<String> a = new List<string>();
                foreach (var element in listImage)
                {
                    string c = Path.Combine(_pathDirectory, element.ImageName);
                    a.Add(c);
                }
                
                ListBoxImage.ItemsSource = a.Select(x => new
                {
                    savesImage = new Bitmap(new FileStream(x, FileMode.Open))
                }).ToList();
                ComplitText.Text += "\n Картинка добавлена";
            }
        }
    }

    private void AddImage(object? sender, RoutedEventArgs e)
    {
        SaveImage();
        ListImage();
    }
}