using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone_Deckbuilder.Interface.HearthstoneAPI.Controller
{
    public class HearthstoneAPIController
    {
        private string _collectibleValue;
        private string _localeValue;


        protected string CollectibleValue
        {
            set { _collectibleValue = "1"; }
        }

        protected string LocaleValue
        {
            set { _localeValue = "enUs"; }
        }

        public int CardAttackValue { get; set; }

        public int CardCostValue { get; set; }

        public int CardHealthValue { get; set; }

        public int CardIdValue { get; set; }

        public string CardNameValue { get; set; }

        public string CardQualityValue { get; set; }

        public string CardTypeValue { get; set; }

        public string CardSetValue { get; set; }

        public string ClassValue { get; set; }
    }
}
