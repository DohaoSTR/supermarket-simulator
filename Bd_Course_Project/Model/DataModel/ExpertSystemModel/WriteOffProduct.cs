using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class WriteOffProduct : DataBaseEntity
    {
        public Product Product { get; set; }

        public DateTime DateArrival { get; set; }

        public DateTime DateWriteOff { get; set; }

        private int _numberOfProducts;
        public int NumberOfProducts
        {
            get => _numberOfProducts;
            set
            {
                if (value > 0)
                {
                    _numberOfProducts = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Кол-во товаров должно быть больше нуля!");
                }
            }
        }

        public WriteOffProduct(Product product, DateTime dateArrival, int numberOfProducts, DateTime dateWriteOff)
        {
            Product = product;
            DateArrival = dateArrival;
            NumberOfProducts = numberOfProducts;
            DateWriteOff = dateWriteOff;
        }

        public WriteOffProduct(long? id, IDataAdapter dataAdapter, Product product, DateTime dateArrival, int numberOfProducts, DateTime dateWriteOff) : base(id, dataAdapter)
        {
            Product = product;
            DateArrival = dateArrival;
            NumberOfProducts = numberOfProducts;
            DateWriteOff = dateWriteOff;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Write_Off_Products",  new Dictionary<string, object>()
            { { "Id_Product", Product.Id }, { "NumberOfProducts", NumberOfProducts}, { "DateArrival", DateArrival.ToString("yyyy-MM-dd") }, { "DateWriteOff", DateWriteOff.ToString("yyyy-MM-dd") } });

            DataModel.WriteOffProducts.Add(new WriteOffProduct(id, DataModel.DataAdapter, Product, DateArrival, NumberOfProducts, DateWriteOff));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Write_Off_Products", Id.Value);
            DataModel.WriteOffProducts.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Write_Off_Products", Id.Value, new Dictionary<string, object>()
            { { "Id_Product", Product.Id }, { "NumberOfProducts", NumberOfProducts}, { "DateArrival", DateArrival.ToString("yyyy-MM-dd") }, { "DateWriteOff", DateWriteOff.ToString("yyyy-MM-dd") } });
        }
    }
}
