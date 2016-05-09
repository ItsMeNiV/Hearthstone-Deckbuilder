using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone_Deckbuilder.Interface.HearthstoneAPI.Controller
{
    class HearthstoneAPIControllerBO
    {
        public int Attack { get; set; }

        public int Cost { get; set; }

        public int Health { get; set; }

        public int CardId { get; set; }

        public string Name { get; set; }

        public string Quality { get; set; }

        public string Type { get; set; }

        public string Set { get; set; }

        public string Class { get; set; }
    }
}
