
[System.Serializable]
public class GetProfileResult : IResult
{
    public string UserName;
    public string Password;
    public byte[] Avatar;

    public GetProfileResult() { }
}
