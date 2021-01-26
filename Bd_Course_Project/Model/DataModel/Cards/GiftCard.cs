using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class GiftCard : DataBaseEntity
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
                    throw new ArgumentOutOfRangeException("Регистрационный номер должен быть больше нуля");
                }
            }
        }

        private int _faceValue;
        public int FaceValue
        {
            get => _faceValue;
            set
            {
                if (value >= 0)
                {
                    _faceValue = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Номинал карты должен быть больше либо равен нулю");
                }
            }
        }

        public GiftCard(int registrationNumber, int faceValue)
        {
            RegistrationNumber = registrationNumber;
            FaceValue = faceValue;
        }

        public GiftCard(long? id, IDataAdapter dataAdapter, int registrationNumber, int faceValue) : base(id, dataAdapter)
        {
            RegistrationNumber = registrationNumber;
            FaceValue = faceValue;
        }

        public GiftCard()
        {
        }

        public GiftCard(int id)
        {
            List<GiftCard> giftCards = DataModel.GiftCards.FindAll(s => s.Id == id);
            GiftCard giftCard = new GiftCard();
            foreach (GiftCard gift in giftCards)
            {
                giftCard = new GiftCard(gift.Id, DataAdapter, gift.RegistrationNumber,
                gift.FaceValue);
            }

            Id = giftCard.Id;
            DataAdapter = giftCard.DataAdapter;
            RegistrationNumber = giftCard.RegistrationNumber;
            FaceValue = giftCard.FaceValue;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Gift_Card", new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "FaceValue", FaceValue } });
            DataModel.GiftCards.Add(new GiftCard(id, DataModel.DataAdapter, RegistrationNumber, FaceValue));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Gift_Card", Id.Value);
            DataModel.GiftCards.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Gift_Card", Id.Value, new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "FaceValue", FaceValue } });
        }
    }
}
