
[System.Serializable]
public class EditProfileRequest
{
    public string oldUserName;
    public string newUserName;
    public string newPassword;
    public byte[] newAvatar;

    public EditProfileRequest() { }
}
