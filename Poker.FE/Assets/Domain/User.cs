[System.Serializable]
public class User {

    public int level;
    public string userName;
    internal string password;
    internal byte[] Avatar;
    internal double deposit;

    public User()
    {
    }

    public int SecurityKey { get; internal set; }
}
