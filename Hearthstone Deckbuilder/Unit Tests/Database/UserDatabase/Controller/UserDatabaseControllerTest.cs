using NUnit.Framework;
using Moq;

namespace Unit_Tests.Database.UserDatabase.Controller
{
    [TestFixture]
    public class UserDatabaseControllerTest
    {

        [Test]
        public void TestCreateNewUser()
        {
            Mock<Hearthstone_Deckbuilder.Database.DatabaseConnector.DatabaseConnectionHandler> connectionHandlerMock = new Mock<Hearthstone_Deckbuilder.Database.DatabaseConnector.DatabaseConnectionHandler>();
            
        }

    }
}
