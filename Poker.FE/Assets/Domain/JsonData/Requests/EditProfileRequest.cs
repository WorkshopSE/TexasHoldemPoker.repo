
[System.Serializable]
public class EditProfileRequest : IRequest
{
    public string Password;
    public string NewUserName;
    public string NewPassword;
    public byte[] NewAvatar;
}
