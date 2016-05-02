using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone_Deckbuilder.Interface.HearthstoneAPI.Controller
{
    public class HearthstoneAPIController
    {
        private string _attackValue;
        private string _collectibleValue;
        private string _costValue;
        private string _durabilityValue;
        private string _healthValue;
        private string _localeValue;

        protected string CollectibleValue
        {
            set { _collectibleValue = "1"; }
        }

        protected string LocaleValue
        {
            set { _localeValue = "enUs"; }
        }

        public string AttackValue
        {
            get { return _attackValue; }
            set { _attackValue = value; }
        }

        public string CostValue
        {
            get { return _costValue; }
            set { _costValue = value; }
        }

        public string DurabilityValue
        {
            get { return _durabilityValue; }
            set { _durabilityValue = value; }
        }

        public string HealthValue
        {
            get { return _healthValue; }
            set { _healthValue = value; }
        }
    }
}
