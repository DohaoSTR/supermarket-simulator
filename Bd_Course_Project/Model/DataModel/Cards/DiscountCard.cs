using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class DiscountCard : DataBaseEntity
    {
        private int _registrationNumber;
        public int RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                if (value > 0)
                {
                    _registrationNumber = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Регистрационный номер должен быть больше нуля!");
                }
            }
        }

        private int _bonusValue;
        public int BonusValue
        {
            get => _bonusValue;
            set
            {
                if (value >= 0)
                {
                    _bonusValue = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Кол-во бонусов должно быть больше либо равно нулю!");
                }
            }
        }

        public DiscountCard(int registrationNumber, int bonusValue)
        {
            RegistrationNumber = registrationNumber;
            BonusValue = bonusValue;
        }

        public DiscountCard(long? id, IDataAdapter dataAdapter, int registrationNumber, int bonusValue) : base(id, dataAdapter)
        {
            RegistrationNumber = registrationNumber;
            BonusValue = bonusValue;
        }

        public DiscountCard()
        {
        }

        public DiscountCard(int id)
        {
            List<DiscountCard> discountCards = DataModel.DiscountCards.FindAll(s => s.Id == id);
            DiscountCard discountCard = new DiscountCard();
            foreach (DiscountCard disc in discountCards)
            {
                discountCard = new DiscountCard(disc.Id, DataAdapter, disc.RegistrationNumber,
                disc.BonusValue);
            }

            Id = discountCard.Id;
            DataAdapter = discountCard.DataAdapter;
            RegistrationNumber = discountCard.RegistrationNumber;
            BonusValue = discountCard.BonusValue;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Discount_Card", new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "BonusValue", BonusValue } });
            DataModel.DiscountCards.Add(new DiscountCard(id, DataModel.DataAdapter, RegistrationNumber, BonusValue));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Discount_Card", Id.Value);
            DataModel.DiscountCards.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Discount_Card", Id.Value, new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "BonusValue", BonusValue } });
        }
    }
}
