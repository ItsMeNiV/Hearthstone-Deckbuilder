using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Hearthstone_Deckbuilder.Database.CryptoHandler;

namespace Unit_Tests.Database.CryptoHandler
{
    [TestFixture]
    public class CryptoHandlerTest
    {

        [Test]
        public void TestHashPassword()
        {
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
            string hash = Hearthstone_Deckbuilder.Database.CryptoHandler.CryptoHandler.hashPassword("testpassword", salt);

            Assert.AreEqual("AQIDBAUGBwgJCgsMDQ4PEDMExIlNeHf5GUIXz8A7DU8NRnzl", hash);
        }

        [Test]
        public void TestGenerateSaltIsReallyRandom()
        {
            byte[] salt = Hearthstone_Deckbuilder.Database.CryptoHandler.CryptoHandler.generatePasswordSalt();
            Assert.AreNotEqual(salt, Hearthstone_Deckbuilder.Database.CryptoHandler.CryptoHandler.generatePasswordSalt());
        }

    }
}
