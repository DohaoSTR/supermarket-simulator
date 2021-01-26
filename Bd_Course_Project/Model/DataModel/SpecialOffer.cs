using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class SpecialOffer : DataBaseEntity
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 45)
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Название специального предложения должно быть заполнено!");
                }
            }
        }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public SpecialOffer(string name, DateTime dateStart, DateTime dateEnd)
        {
            Name = name;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public SpecialOffer(long? id, IDataAdapter dataAdapter, string name, DateTime dateStart, DateTime dateEnd) : base(id, dataAdapter)
        {
            Name = name;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public SpecialOffer()
        {

        }

        public SpecialOffer(int id)
        {
            List<SpecialOffer> specialOffers = DataModel.SpecialOffers.FindAll(s => s.Id == id);
            SpecialOffer specialOffer = new SpecialOffer();
            foreach (SpecialOffer spec in specialOffers)
            {
                specialOffer = new SpecialOffer(spec.Id, DataAdapter, spec.Name,
                spec.DateStart, spec.DateEnd);
            }

            Id = specialOffer.Id;
            DataAdapter = specialOffer.DataAdapter;
            Name = specialOffer.Name;
            DateStart = specialOffer.DateStart;
            DateEnd = specialOffer.DateEnd;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Special_Offer", new Dictionary<string, object>()
            { { "Name", Name }, { "DateStart", DateStart.ToString("yyyy-MM-dd") }, { "DateEnd", DateEnd.ToString("yyyy-MM-dd") } });
            DataModel.SpecialOffers.Add(new SpecialOffer(id, DataModel.DataAdapter, Name, DateStart, DateEnd));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Special_Offer", Id.Value);
            DataModel.SpecialOffers.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Special_Offer", Id.Value, new Dictionary<string, object>()
            { { "Name", Name }, { "DateStart", DateStart.ToString("yyyy-MM-dd") }, { "DateEnd", DateEnd.ToString("yyyy-MM-dd") } });
        }
    }
}
