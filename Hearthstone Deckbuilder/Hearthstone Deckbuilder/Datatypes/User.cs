namespace Hearthstone_Deckbuilder.NSDatatypes
{
    public class User
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
