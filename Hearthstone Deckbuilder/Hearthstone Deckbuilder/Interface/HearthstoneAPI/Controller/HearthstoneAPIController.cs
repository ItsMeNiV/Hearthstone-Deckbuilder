using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone_Deckbuilder.Interface.HearthstoneAPI.Controller
{
    public class HearthstoneAPIController
    {
        private string _cardAttackValue;
        private string _cardCostValue;
        private string _cardHealthValue;
        private string _cardNameValue;
        private string _cardQualityValue;
        private string _cardTypeValue;
        private string _cardSetValue;
        private string _classValue;
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

        public string CardAttackValue
        {
            get { return _cardAttackValue; }
            set { _cardAttackValue = value; }
        }

        public string CardCostValue
        {
            get { return _cardCostValue; }
            set { _cardCostValue = value; }
        }

        public string CardHealthValue
        {
            get { return _cardHealthValue; }
            set { _cardHealthValue = value; }
        }

        public string CardNameValue
        {
            get { return _cardNameValue; }
            set { _cardNameValue = value; }
        }

        public string CardQualityValue
        {
            get { return _cardQualityValue; }
            set { _cardQualityValue = value; }
        }

        public string CardTypeValue
        {
            get { return _cardTypeValue; }
            set { _cardTypeValue = value; }
        }

        public string CardSetValue
        {
            get { return _cardSetValue; }
            set { _cardSetValue = value; }
        }

        public string ClassValue
        {
            get { return _classValue; }
            set { _classValue = value; }
        }
    }
}
