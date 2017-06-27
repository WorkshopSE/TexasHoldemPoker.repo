[System.Serializable]
public class LeaveRoomRequest : IRequest
{

    /// <summary>
    /// player hash code
    /// </summary>
    public int Player;

    /// <summary>
    /// Room hash code - ? no need.
    /// </summary>
    public int Room;
}
