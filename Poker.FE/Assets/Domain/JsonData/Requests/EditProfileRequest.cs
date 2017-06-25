
[System.Serializable]
public class EditProfileRequest
{
    public string UserName;
    public string Password;
    public string newUserName;
    public string newPassword;
    public byte[] newAvatar;

    public EditProfileRequest() { }
}
