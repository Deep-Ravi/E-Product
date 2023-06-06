namespace Assignment.UnitTest.Respository
{
    public static class MockProductRespository
    {
        public static Mock<IUnitOfWork> UnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(r => r.Product.GetAll()).Returns(new List<Product>());
            mockUow.Setup(r => r.Product.Add(new Product()));
            mockUow.Setup(r => r.Product.Update(new Product()));
            mockUow.Setup(r => r.Product.Delete(Guid.NewGuid().ToString()));
            mockUow.Setup(r => r.CommitAsync());
            return mockUow;
        }
    }
}
