using Avalonia.Styling;

namespace TestingApi;

public class CheckData
{
    private string? _login;

    private string? _password;
    
    private string? _role;

    private string? _userName;
    public string? LoginTest
    {
        get { return _login; }
        set
        {
            _login = "";
            if (string.IsNullOrEmpty(value))
            {
                _login = "Значения нет";
            }
        }
    }
    public string? UserNameTest
    {
        get { return _userName;}
        set
        {
            _userName = "";
            if (string.IsNullOrEmpty(value))
            {
                _userName = "Значения нет";
            }
        }
    }
    public string? RoleTest
    {
        get { return _role; }
        set
        {
            _role = "";
            if (string.IsNullOrEmpty(value))
            {
                _role = "Значения нет";
            }
        }
    }
    public string? PasswordTest
    {
        get { return _password;}
        set
        {
            _password = "";
            if (string.IsNullOrEmpty(value))
            {
                _password = "Значения нет";
            }
        }
    }
}