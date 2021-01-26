using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class BillsOfLading : DataBaseEntity
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

        private string _customer;
        public string Customer
        {
            get => _customer;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 45)
                {
                    _customer = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Имя клиента должно быть заполнено!");
                }
            }
        }

        private string _consignor;
        public string Consignor
        {
            get => _consignor;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 45)
                {
                    _consignor = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Грузоотправитель должно быть заполнено!");
                }
            }
        }

        private string _loadingPoint;
        public string LoadingPoint
        {
            get => _loadingPoint;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _loadingPoint = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Место загрузки должно быть заполнено!");
                }
            }
        }

        private string _shippingPoint;
        public string ShippingPoint
        {
            get => _shippingPoint;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _shippingPoint = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Место отгрузки должно быть заполнено!");
                }
            }
        }

        public BillsOfLading(int registrationNumber, string customer, string consignor, string loadingPoint, string shippingPoint)
        {
            RegistrationNumber = registrationNumber;
            Customer = customer;
            Consignor = consignor;
            LoadingPoint = loadingPoint;
            ShippingPoint = shippingPoint;
        }

        public BillsOfLading(long? id, IDataAdapter dataAdapter, int registrationNumber, string customer, string consignor, string loadingPoint, string shippingPoint) : base(id, dataAdapter)
        {
            RegistrationNumber = registrationNumber;
            Customer = customer;
            Consignor = consignor;
            LoadingPoint = loadingPoint;
            ShippingPoint = shippingPoint;
        }

        public BillsOfLading()
        {
        }

        public BillsOfLading(int id)
        {
            List<BillsOfLading> billsOfLadings = DataModel.BillsOfLadings.FindAll(s => s.Id == id);
            BillsOfLading billOfLading = new BillsOfLading();
            foreach (BillsOfLading bill in billsOfLadings)
            {
                billOfLading = new BillsOfLading(bill.Id, DataAdapter, bill.RegistrationNumber, bill.Customer,
                bill.Consignor, bill.LoadingPoint, bill.ShippingPoint);
            }

            Id = billOfLading.Id;
            DataAdapter = billOfLading.DataAdapter;
            RegistrationNumber = billOfLading.RegistrationNumber;
            Customer = billOfLading.Customer;
            Consignor = billOfLading.Consignor;
            LoadingPoint = billOfLading.LoadingPoint;
            ShippingPoint = billOfLading.ShippingPoint;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Bills_Of_Lading", new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "Customer", Customer }, { "Consignor", Consignor }, { "LoadingPoint", LoadingPoint }, { "ShippingPoint", ShippingPoint } });
            DataModel.BillsOfLadings.Add(new BillsOfLading(id, DataModel.DataAdapter, RegistrationNumber, Customer, Consignor, LoadingPoint, ShippingPoint));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Bills_Of_Lading", Id.Value);
            DataModel.BillsOfLadings.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Bills_Of_Lading", Id.Value, new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "Customer", Customer }, { "Consignor", Consignor },
                { "LoadingPoint", LoadingPoint }, { "ShippingPoint", ShippingPoint } });
        }
    }
}
