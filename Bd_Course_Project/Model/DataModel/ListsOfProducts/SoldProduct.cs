using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class SoldProduct : DataBaseEntity
    {
        public Product Product { get; set; }

        public DateTime SoldDate { get; set; }

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

        public Discount Discount { get; set; }

        public SpecialOffer SpecialOffer { get; set; }

        public DiscountCard DiscountCard { get; set; }

        public GiftCard GiftCard { get; set; }

        public decimal Price => Product.PurchasePrice * NumberOfProducts - (Discount.Percent * Product.PurchasePrice * NumberOfProducts) / 100;

        public SoldProduct(Product product, DateTime soldDate, int numberOfProducts, Discount discount, SpecialOffer specialOffer, DiscountCard discountCard, GiftCard giftCard)
        {
            Product = product;
            SoldDate = soldDate;
            NumberOfProducts = numberOfProducts;
            Discount = discount;
            SpecialOffer = specialOffer;
            DiscountCard = discountCard;
            GiftCard = giftCard;
        }

        public SoldProduct(long? id, IDataAdapter dataAdapter, Product product, DateTime soldDate, int numberOfProducts, Discount discount, SpecialOffer specialOffer, DiscountCard discountCard, GiftCard giftCard) : base(id, dataAdapter)
        {
            Product = product;
            SoldDate = soldDate;
            NumberOfProducts = numberOfProducts;
            Discount = discount;
            SpecialOffer = specialOffer;
            DiscountCard = discountCard;
            GiftCard = giftCard;
        }

        public SoldProduct()
        {

        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Sold_Products", new Dictionary<string, object>()
            { { "Id_Product", Product.Id }, { "SoldDate", SoldDate.ToString("yyyy-MM-dd") },
                { "NumberOfProducts", NumberOfProducts}, { "Id_Discount", Discount.Id }, { "Id_Special_Offer", SpecialOffer.Id }, { "Id_Discount_Card", DiscountCard.Id }, { "Id_Gift_Card", GiftCard.Id } });

            DataModel.SoldProducts.Add(new SoldProduct(id, DataModel.DataAdapter, Product, SoldDate, NumberOfProducts, Discount, SpecialOffer, DiscountCard, GiftCard));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Sold_Products", Id.Value);
            DataModel.SoldProducts.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Sold_Products", Id.Value, new Dictionary<string, object>()
            { { "Id_Product", Product.Id }, { "SoldDate", SoldDate.ToString("yyyy-MM-dd") },
                { "NumberOfProducts", NumberOfProducts}, { "Id_Discount", Discount.Id }, { "Id_Special_Offer", SpecialOffer.Id }, { "Id_Discount_Card", DiscountCard.Id }, { "Id_Gift_Card", GiftCard.Id } });
        }
    }
}
