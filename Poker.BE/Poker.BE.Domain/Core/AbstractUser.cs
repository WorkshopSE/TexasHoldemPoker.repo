
namespace Poker.BE.Domain.Core
{
	public abstract class AbstractUser
	{

		#region Properties
		public string UserName { get; set; }
		public string Password { get; set; }
		public Bank UserBank { get; set; }
		public bool IsConnected { get; set; }

		public byte[] Avatar { get; set; }
		#endregion
	}
}
