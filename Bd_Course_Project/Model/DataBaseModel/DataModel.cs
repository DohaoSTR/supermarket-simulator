using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public static class DataModel
    {
        public static IDataAdapter DataAdapter = new MySQLDataAdapter();

        public static List<BillsOfLading> BillsOfLadings = new List<BillsOfLading>();
        public static List<ConformityCertificate> ConformityCertificates = new List<ConformityCertificate>();
        public static List<ConformityDeclaration> ConformityDeclarations = new List<ConformityDeclaration>();
        public static List<StateRegistrationCertificate> StateRegistrationCertificates = new List<StateRegistrationCertificate>();
        public static List<VeterinaryCertificate> VeterinaryCertificates = new List<VeterinaryCertificate>();

        public static List<Discount> Discounts = new List<Discount>();
        public static List<SpecialOffer> SpecialOffers = new List<SpecialOffer>();
        public static List<DiscountCard> DiscountCards = new List<DiscountCard>();
        public static List<GiftCard> GiftCards = new List<GiftCard>();

        public static List<Product> Products = new List<Product>();
        public static List<ArrivalProduct> ArrivalProducts = new List<ArrivalProduct>();
        public static List<StoredProduct> StoredProducts = new List<StoredProduct>();
        public static List<SoldProduct> SoldProducts = new List<SoldProduct>();

        public static List<WriteOffProduct> WriteOffProducts = new List<WriteOffProduct>();

        static DataModel()
        {
            DataAdapter.Connect(new ConnectionSettings()
            {
                Host = "s1.kts.tu-bryansk.ru",
                Port = "3306",
                DefaultSchema = "IAS18_MuzES",
                User = "IAS18.MuzES",
                Password = "E!n7aN8-6s",
                CharSet = "utf8"
            });

            LoadData();
        }

        private static void LoadData()
        {
            BillsOfLadings = LoadBillsOfLading();
            ConformityCertificates = LoadConformityCertificates();
            ConformityDeclarations = LoadConformityDeclarations();
            StateRegistrationCertificates = LoadStateRegistrationCertificates();
            VeterinaryCertificates = LoadVeterinaryCertificates();

            Discounts = LoadDiscounts();
            SpecialOffers = LoadSpecialOffers();
            DiscountCards = LoadDiscountCards();
            GiftCards = LoadGiftCards();

            Products = LoadProducts();
            StoredProducts = LoadStoredProducts();
            SoldProducts = LoadSoldProducts();
            ArrivalProducts = LoadArrivalProducts();

            WriteOffProducts = LoadWriteOffProducts();
        }

        public static List<WriteOffProduct> LoadWriteOffProducts()
        {
            string q = "select * from `Write_Off_Products`;";
            List<Dictionary<string, string>> writeOffProductsResult = DataAdapter.GetQueryResult(q);

            List<WriteOffProduct> writeOffProducts = new List<WriteOffProduct>();
            foreach (Dictionary<string, string> g in writeOffProductsResult)
            {
                Product prod = new Product(Convert.ToInt32(g["Id_Product"]));

                WriteOffProduct writeOffProduct = new WriteOffProduct(Convert.ToInt32(g["Id"]), DataAdapter, prod,
                Convert.ToDateTime(g["DateArrival"]), Convert.ToInt32(g["NumberOfProducts"]), Convert.ToDateTime(g["DateWriteOff"]));
                writeOffProducts.Add(writeOffProduct);
            }

            return writeOffProducts;
        }

        public static List<BillsOfLading> LoadBillsOfLading()
        {
            string q = "select * from `Bills_Of_Lading`;";
            List<Dictionary<string, string>> billOfLadingResult = DataAdapter.GetQueryResult(q);

            List<BillsOfLading> billsOfLadings = new List<BillsOfLading>();
            foreach (Dictionary<string, string> g in billOfLadingResult)
            {
                BillsOfLading billOfLading = new BillsOfLading(Convert.ToInt32(g["Id"]), DataAdapter, Convert.ToInt32(g["RegistrationNumber"]),
                        g["Customer"], g["Consignor"], g["LoadingPoint"], g["ShippingPoint"]);
                billsOfLadings.Add(billOfLading);
            }

            return billsOfLadings;
        }

        public static List<ConformityCertificate> LoadConformityCertificates()
        {
            string q = "select * from `Conformity_Certificate`;";
            List<Dictionary<string, string>> conformityCertificateResult = DataAdapter.GetQueryResult(q);

            List<ConformityCertificate> conformityCertificates = new List<ConformityCertificate>();
            foreach (Dictionary<string, string> g in conformityCertificateResult)
            {
                ConformityCertificate conformityCertificate = new ConformityCertificate(Convert.ToInt32(g["Id"]), DataAdapter, Convert.ToInt32(g["RegistrationNumber"]),
                    g["CodeTransportUnion"], Convert.ToDateTime(g["RegistrationDate"]), Convert.ToDateTime(g["EndDate"]));
                conformityCertificates.Add(conformityCertificate);
            }

            return conformityCertificates;
        }

        public static List<ConformityDeclaration> LoadConformityDeclarations()
        {
            string q = "select * from `Conformity_Declaration`;";
            List<Dictionary<string, string>> conformityDeclarationResult = DataAdapter.GetQueryResult(q);

            List<ConformityDeclaration> conformityDeclarations = new List<ConformityDeclaration>();
            foreach (Dictionary<string, string> g in conformityDeclarationResult)
            {
                ConformityDeclaration conformityDeclaration = new ConformityDeclaration(Convert.ToInt32(g["Id"]), DataAdapter, Convert.ToInt32(g["RegistrationNumber"]),
                    g["CodeTransportUnion"], Convert.ToDateTime(g["RegistrationDate"]), Convert.ToDateTime(g["EndDate"]));
                conformityDeclarations.Add(conformityDeclaration);
            }

            return conformityDeclarations;
        }

        public static List<StateRegistrationCertificate> LoadStateRegistrationCertificates()
        {
            string q = "select * from `State_Registration_Certificate`;";
            List<Dictionary<string, string>> certificatesResult = DataAdapter.GetQueryResult(q);

            List<StateRegistrationCertificate> certificates = new List<StateRegistrationCertificate>();
            foreach (Dictionary<string, string> g in certificatesResult)
            {
                StateRegistrationCertificate certificate = new StateRegistrationCertificate(Convert.ToInt32(g["Id"]), DataAdapter, Convert.ToInt32(g["RegistrationNumber"]),
                    g["CodeTransportUnion"], Convert.ToDateTime(g["RegistrationDate"]));
                certificates.Add(certificate);
            }

            return certificates;
        }

        public static List<VeterinaryCertificate> LoadVeterinaryCertificates()
        {
            string q = "select * from `Veterinary_Certificate`;";
            List<Dictionary<string, string>> certificatesResult = DataAdapter.GetQueryResult(q);

            List<VeterinaryCertificate> certificates = new List<VeterinaryCertificate>();
            foreach (Dictionary<string, string> g in certificatesResult)
            {
                VeterinaryCertificate certificate = new VeterinaryCertificate(Convert.ToInt32(g["Id"]), DataAdapter, Convert.ToInt32(g["RegistrationNumber"]),
                    g["Form"], Convert.ToDateTime(g["RegistrationDate"]));
                certificates.Add(certificate);
            }

            return certificates;
        }

        public static List<DiscountCard> LoadDiscountCards()
        {
            string q = "select * from `Discount_Card`;";
            List<Dictionary<string, string>> result = DataAdapter.GetQueryResult(q);

            List<DiscountCard> cards = new List<DiscountCard>();
            foreach (Dictionary<string, string> g in result)
            {
                DiscountCard card = new DiscountCard(Convert.ToInt32(g["Id"]), DataAdapter, Convert.ToInt32(g["RegistrationNumber"]),
                    Convert.ToInt32(g["BonusValue"]));
                cards.Add(card);
            }

            return cards;
        }

        public static List<GiftCard> LoadGiftCards()
        {
            string q = "select * from `Gift_Card`;";
            List<Dictionary<string, string>> result = DataAdapter.GetQueryResult(q);

            List<GiftCard> cards = new List<GiftCard>();
            foreach (Dictionary<string, string> g in result)
            {
                GiftCard card = new GiftCard(Convert.ToInt32(g["Id"]), DataAdapter, Convert.ToInt32(g["RegistrationNumber"]),
                    Convert.ToInt32(g["FaceValue"]));
                cards.Add(card);
            }

            return cards;
        }

        public static List<Discount> LoadDiscounts()
        {
            string q = "select * from `Discount`;";
            List<Dictionary<string, string>> result = DataAdapter.GetQueryResult(q);

            List<Discount> discounts = new List<Discount>();
            foreach (Dictionary<string, string> g in result)
            {
                Discount discount = new Discount(Convert.ToInt32(g["Id"]), DataAdapter, Convert.ToDecimal(g["Percent"]),
                    Convert.ToDateTime(g["DateStart"]), Convert.ToDateTime(g["DateEnd"]));
                discounts.Add(discount);
            }

            return discounts;
        }

        public static List<SpecialOffer> LoadSpecialOffers()
        {
            string q = "select * from `Special_Offer`;";
            List<Dictionary<string, string>> result = DataAdapter.GetQueryResult(q);

            List<SpecialOffer> specialOffers = new List<SpecialOffer>();
            foreach (Dictionary<string, string> g in result)
            {
                SpecialOffer specialOffer = new SpecialOffer(Convert.ToInt32(g["Id"]), DataAdapter, g["Name"], 
                    Convert.ToDateTime(g["DateStart"]), Convert.ToDateTime(g["DateEnd"]));
                specialOffers.Add(specialOffer);
            }

            return specialOffers;
        }

        public static List<Product> LoadProducts()
        {
            string q = "select * from `Product`;";
            List<Dictionary<string, string>> productsResult = DataAdapter.GetQueryResult(q);

            List<Product> products = new List<Product>();
            foreach (Dictionary<string, string> g in productsResult)
            {
                BillsOfLading billsOfLading = new BillsOfLading(Convert.ToInt32(g["Id_Bills_Of_Lading"]));
                ConformityCertificate conformityCertificate = new ConformityCertificate(Convert.ToInt32(g["Id_Conformity_Certificate"]));
                ConformityDeclaration conformityDeclaration = new ConformityDeclaration(Convert.ToInt32(g["Id_Conformity_Declaration"]));
                StateRegistrationCertificate stateRegistrationCertificate = new StateRegistrationCertificate(Convert.ToInt32(g["Id_State_Registration_Certificate"]));
                VeterinaryCertificate veterinaryCertificate = new VeterinaryCertificate(Convert.ToInt32(g["Id_Veterinary_Certificate"]));

                Product product = new Product(Convert.ToInt32(g["Id"]), DataAdapter, g["Name"], Convert.ToDateTime(g["ExpirationDate"]),
                Convert.ToInt32(g["NetWeight"]), Convert.ToInt32(g["GrossWeight"]), Convert.ToDecimal(g["PurchasePrice"]), billsOfLading,
                conformityCertificate, conformityDeclaration, stateRegistrationCertificate, veterinaryCertificate);
                products.Add(product);
            }

            return products;
        }

        public static List<SoldProduct> LoadSoldProducts()
        {
            string q = "select * from `Sold_Products`;";
            List<Dictionary<string, string>> productsResult = DataAdapter.GetQueryResult(q);

            List<SoldProduct> soldProducts = new List<SoldProduct>();
            foreach (Dictionary<string, string> g in productsResult)
            {
                DiscountCard discountCard = new DiscountCard();
                if (g["Id_Discount_Card"] != null)
                {
                    discountCard = new DiscountCard(Convert.ToInt32(g["Id_Discount_Card"]));
                }

                GiftCard giftCard = new GiftCard();
                if (g["Id_Gift_Card"] != null)
                {
                    giftCard = new GiftCard(Convert.ToInt32(g["Id_Gift_Card"]));
                }

                Discount discount = new Discount();
                if (g["Id_Discount"] != null)
                {
                     discount = new Discount(Convert.ToInt32(g["Id_Discount"]));
                }

                SpecialOffer specialOffer = new SpecialOffer();
                if (g["Id_Special_Offer"] != null)
                {
                     specialOffer = new SpecialOffer(Convert.ToInt32(g["Id_Special_Offer"]));
                }
                Product product = new Product(Convert.ToInt32(g["Id_Product"]));

                SoldProduct soldProduct = new SoldProduct(Convert.ToInt32(g["Id"]), DataAdapter, product,
                    Convert.ToDateTime(g["SoldDate"]), Convert.ToInt32(g["NumberOfProducts"]), discount, specialOffer, discountCard, giftCard);
                soldProducts.Add(soldProduct);
            }

            return soldProducts;
        }

        public static List<StoredProduct> LoadStoredProducts()
        {
            string q = "select * from `Stored_Products`;";
            List<Dictionary<string, string>> storedProdResult = DataAdapter.GetQueryResult(q);

            List<StoredProduct> storedProducts = new List<StoredProduct>();
            foreach (Dictionary<string, string> g in storedProdResult)
            {
                Product prod = new Product(Convert.ToInt32(g["Id_Product"]));

                StoredProduct storedProduct = new StoredProduct(Convert.ToInt32(g["Id"]), DataAdapter, prod,
                Convert.ToDateTime(g["DateArrival"]), Convert.ToInt32(g["NumberOfProducts"]));
                storedProducts.Add(storedProduct);
            }

            return storedProducts;
        }

        public static List<ArrivalProduct> LoadArrivalProducts()
        {
            string q = "select * from `Arrivals_Products`;";
            List<Dictionary<string, string>> result = DataAdapter.GetQueryResult(q);

            List<ArrivalProduct> arrivalProducts = new List<ArrivalProduct>();
            foreach (Dictionary<string, string> g in result)
            {
                Product prod = new Product(Convert.ToInt32(g["Id_Product"]));

                ArrivalProduct arrivalProduct = new ArrivalProduct(Convert.ToInt32(g["Id"]), DataAdapter, prod,
                Convert.ToDateTime(g["DateArrival"]), Convert.ToInt32(g["NumberOfProducts"]));
                arrivalProducts.Add(arrivalProduct);
            }

            return arrivalProducts;
        }
    }
}
