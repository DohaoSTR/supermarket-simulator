﻿using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class ArrivalProduct : DataBaseEntity
    {
        public Product Product { get; set; }

        public DateTime DateArrival { get; set; }

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

        public ArrivalProduct(Product product, DateTime dateArrival, int numberOfProducts)
        {
            Product = product;
            DateArrival = dateArrival;
            NumberOfProducts = numberOfProducts;
        }

        public ArrivalProduct(long? id, IDataAdapter dataAdapter, Product product, DateTime dateArrival, int numberOfProducts) : base(id, dataAdapter)
        {
            Product = product;
            DateArrival = dateArrival;
            NumberOfProducts = numberOfProducts;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Arrivals_Products", new Dictionary<string, object>()
            { { "Id_Product", Product.Id }, { "NumberOfProducts", NumberOfProducts}, { "DateArrival", DateArrival.ToString("yyyy-MM-dd") } });

            DataModel.ArrivalProducts.Add(new ArrivalProduct(id, DataModel.DataAdapter, Product, DateArrival, NumberOfProducts));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Arrivals_Products", Id.Value);
            DataModel.ArrivalProducts.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Arrivals_Products", Id.Value, new Dictionary<string, object>()
            { { "Id_Product", Product.Id }, { "NumberOfProducts", NumberOfProducts}, { "DateArrival", DateArrival.ToString("yyyy-MM-dd") } });
        }
    }
}
