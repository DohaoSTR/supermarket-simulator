using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class Discount : DataBaseEntity
    {
        private decimal _percent;
        public decimal Percent
        {
            get => _percent;
            set
            {
                if (value <= 100 && value >= 0)
                {
                    _percent = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Процент указан неккоректно");
                }
            }
        }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public Discount(decimal percent, DateTime dateStart, DateTime dateEnd)
        {
            Percent = percent;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public Discount(long? id, IDataAdapter dataAdapter, decimal percent, DateTime dateStart, DateTime dateEnd) : base(id, dataAdapter)
        {
            Percent = percent;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public Discount()
        {
        }

        public Discount(int id)
        {
            List<Discount> discounts = DataModel.Discounts.FindAll(s => s.Id == id);
            Discount discount = new Discount();
            foreach (Discount disc in discounts)
            {
                discount = new Discount(disc.Id, DataAdapter, disc.Percent,
                disc.DateStart, disc.DateEnd);
            }

            Id = discount.Id;
            DataAdapter = discount.DataAdapter;
            Percent = discount.Percent;
            DateStart = discount.DateStart;
            DateEnd = discount.DateEnd;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Discount", new Dictionary<string, object>()
            { { "Percent", Percent }, { "DateStart", DateStart.ToString("yyyy-MM-dd") }, { "DateEnd", DateEnd.ToString("yyyy-MM-dd") } });
            DataModel.Discounts.Add(new Discount(id, DataModel.DataAdapter, Percent, DateStart, DateEnd));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Discount", Id.Value);
            DataModel.Discounts.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Discount", Id.Value, new Dictionary<string, object>()
            { { "Percent", Percent }, { "DateStart", DateStart.ToString("yyyy-MM-dd") }, { "DateEnd", DateEnd.ToString("yyyy-MM-dd") }});
        }
    }
}
