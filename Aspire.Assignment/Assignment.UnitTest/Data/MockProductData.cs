namespace Assignment.UnitTest.Data
{
    public static class MockProductData
    {
        public static List<ProductDTO> ProductMockRecords()
        {
            return new List<ProductDTO>
            {
                new ProductDTO
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = "10.99",
                    ExpiryDate = DateTime.Now.AddDays(60),
                    AddedOn = DateTime.Now
                },
                new ProductDTO
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = "19.99",
                    ExpiryDate = DateTime.Now.AddDays(90),
                    AddedOn = DateTime.Now
                },
                new ProductDTO
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Description = "Description for Product 3",
                    Price = "14.99",
                    ExpiryDate = DateTime.Now.AddDays(45),
                    AddedOn = DateTime.Now
                },
                new ProductDTO
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 4",
                    Description = "Description for Product 4",
                    Price = "24.99",
                    ExpiryDate = DateTime.Now.AddDays(75),
                    AddedOn = DateTime.Now
                },
            };
        }
        public static CreateProductDTO ValidData()
        {
            return new CreateProductDTO
            {
                Name = "Product 5",
                Description = "Description for Product 5",
                Price = "100.00",
                ExpiryDate = DateTime.Now.AddDays(80),
            };
        }

        public static CreateProductDTO InValidData()
        {
            return new CreateProductDTO
            {
                Name = "Product 6",
                Description = "Description for Product 6",
                Price = "100",
                ExpiryDate = DateTime.Now.AddDays(-80),
            };
        }
    }
}