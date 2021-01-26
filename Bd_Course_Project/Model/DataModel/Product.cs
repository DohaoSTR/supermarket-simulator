using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class Product : DataBaseEntity
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

        public DateTime ExpirationDate { get; set; }

        private int _netWeight;
        public int NetWeight
        {
            get => _netWeight;
            set
            {
                if (value > 0)
                {
                    _netWeight = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Масса нетто должна быть больше нуля!");
                }
            }
        }

        private int _grossWeight;
        public int GrossWeight
        {
            get => _grossWeight;
            set
            {
                if (value > 0)
                {
                    _grossWeight = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Масса брутто должна быть больше нуля!");
                }
            }
        }

        private decimal _purchasePrice;
        public decimal PurchasePrice
        {
            get => _purchasePrice;
            set
            {
                if (value > 0)
                {
                    _purchasePrice = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Цена покупки должна быть больше нуля!");
                }
            }
        }

        public BillsOfLading BillsOfLading { get; set; }

        public ConformityCertificate ConformityCertificate { get; set; }

        public ConformityDeclaration ConformityDeclaration { get; set; }

        public StateRegistrationCertificate StateRegistrationCertificate { get; set; }

        public VeterinaryCertificate VeterinaryCertificate { get; set; }

        public Product(string name, DateTime expirationDate, int netWeight, int grossWeight, decimal purchasePrice,
                       BillsOfLading billsOfLading, ConformityCertificate conformityCertificate, ConformityDeclaration conformityDeclaration,
                       StateRegistrationCertificate stateRegistrationCertificate, VeterinaryCertificate veterinaryCertificate)
        {
            Name = name;
            ExpirationDate = expirationDate;
            NetWeight = netWeight;
            GrossWeight = grossWeight;
            PurchasePrice = purchasePrice;
            BillsOfLading = billsOfLading;
            ConformityCertificate = conformityCertificate;
            ConformityDeclaration = conformityDeclaration;
            StateRegistrationCertificate = stateRegistrationCertificate;
            VeterinaryCertificate = veterinaryCertificate;
        }

        public Product(long? id, IDataAdapter dataAdapter, string name, DateTime expirationDate, int netWeight, int grossWeight, decimal purchasePrice,
                       BillsOfLading billsOfLading, ConformityCertificate conformityCertificate, ConformityDeclaration conformityDeclaration,
                       StateRegistrationCertificate stateRegistrationCertificate, VeterinaryCertificate veterinaryCertificate) : base(id, dataAdapter)
        {
            Name = name;
            ExpirationDate = expirationDate;
            NetWeight = netWeight;
            GrossWeight = grossWeight;
            PurchasePrice = purchasePrice;
            BillsOfLading = billsOfLading;
            ConformityCertificate = conformityCertificate;
            ConformityDeclaration = conformityDeclaration;
            StateRegistrationCertificate = stateRegistrationCertificate;
            VeterinaryCertificate = veterinaryCertificate;
        }

        public Product()
        {
        }

        public Product(int id)
        {
            List<Product> products = DataModel.Products.FindAll(s => s.Id == id);
            Product product = new Product();
            foreach (Product prod in products)
            {
                product = new Product(prod.Id, DataAdapter, prod.Name,
                prod.ExpirationDate, prod.NetWeight, prod.GrossWeight, prod.PurchasePrice, prod.BillsOfLading, prod.ConformityCertificate, prod.ConformityDeclaration, prod.StateRegistrationCertificate, prod.VeterinaryCertificate);
            }

            Id = product.Id;
            DataAdapter = product.DataAdapter;
            Name = product.Name;
            ExpirationDate = product.ExpirationDate;
            NetWeight = product.NetWeight;
            GrossWeight = product.GrossWeight;
            PurchasePrice = product.PurchasePrice;
            ExpirationDate = product.ExpirationDate;
            BillsOfLading = product.BillsOfLading;
            ConformityCertificate = product.ConformityCertificate;
            ConformityDeclaration = product.ConformityDeclaration;
            StateRegistrationCertificate = product.StateRegistrationCertificate;
            VeterinaryCertificate = product.VeterinaryCertificate;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Product", new Dictionary<string, object>()
            { { "Name", Name },  { "ExpirationDate", ExpirationDate.ToString("yyyy-MM-dd") },
                { "NetWeight", NetWeight }, { "GrossWeight", GrossWeight }, { "PurchasePrice", PurchasePrice }, { "Id_Bills_Of_Lading", BillsOfLading.Id },
            { "Id_Conformity_Certificate", ConformityCertificate.Id }, { "Id_Conformity_Declaration", ConformityDeclaration.Id },
                { "Id_State_Registration_Certificate", StateRegistrationCertificate.Id } , { "Id_Veterinary_Certificate", VeterinaryCertificate.Id }});

            DataModel.Products.Add(new Product(id, DataModel.DataAdapter, Name, ExpirationDate, NetWeight,
                GrossWeight, PurchasePrice, BillsOfLading, ConformityCertificate, ConformityDeclaration, StateRegistrationCertificate, VeterinaryCertificate));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Product", Id.Value);
            DataModel.Products.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Product", Id.Value, new Dictionary<string, object>()
            { { "Name", Name },  { "ExpirationDate", ExpirationDate.ToString("yyyy-MM-dd") },
                { "NetWeight", NetWeight }, { "GrossWeight", GrossWeight }, { "PurchasePrice", PurchasePrice }, { "Id_Bills_Of_Lading", BillsOfLading.Id },
            { "Id_Conformity_Certificate", ConformityCertificate.Id }, { "Id_Conformity_Declaration", ConformityDeclaration.Id },
                { "Id_State_Registration_Certificate", StateRegistrationCertificate.Id } , { "Id_Veterinary_Certificate", VeterinaryCertificate.Id }});
        }
    }
}
