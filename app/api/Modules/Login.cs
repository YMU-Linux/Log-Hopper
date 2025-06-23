namespace api.Modules;
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class LoginResponse
{
    public string Token { get; set; }
    public string Username { get; set; }
    public int RolePower { get; set; }
}