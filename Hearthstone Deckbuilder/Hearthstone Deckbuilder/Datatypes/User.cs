namespace Hearthstone_Deckbuilder.Datatypes
{
    public class User
    {

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

    }
}
