namespace PropertyListings.Entity
{
    public class Property
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; } // Property photo URL
        public string Address { get; set; } // Address (title)
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public decimal Price { get; set; } // Price formatted in GBP
        public int Bedrooms { get; set; } // Number of bedrooms
        public int Bathrooms { get; set; } // Number of bathrooms
        public bool IsFavorite { get; set; } // Favourite toggle
    }
}
