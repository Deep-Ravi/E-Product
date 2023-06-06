namespace Assignment.UnitTest.HandlerTest
{
    public class ProductHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IValidator<CreateProductDTO>> _validator;
        private readonly IMapper _mapper;
        public ProductHandlerTest()
        {
            _unitOfWork = MockProductRespository.UnitOfWork();
            _validator = MockProductValidator.Validator();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task CreateCommand_ProductRecords()
        {
            var handler = new CreateProductCommandHandler(_unitOfWork.Object, _validator.Object, _mapper);
            var result = await handler.Handle(new CreateProductCommand(MockProductData.ValidData()), CancellationToken.None);
            if (result == null)
                Assert.IsType<BadRequestResult>(result);
            Assert.IsType<Guid>(result);
        }
        [Fact]
        public async Task UpdateCommand_ProductRecords()
        {
            var handler = new UpdateProductCommandHandler(_unitOfWork.Object, _validator.Object, _mapper);
            var result = await handler.Handle(new UpdateProductCommand(MockProductData.ProductMockRecords().FirstOrDefault()), CancellationToken.None);
            if (result == null)
                Assert.IsType<BadRequestResult>(result);
            Assert.IsType<Guid>(result);
        }
        [Fact]
        public async Task GetAllQuery_ProductRecords()
        {
            var handler = new GetAllProductQueryHandler(_unitOfWork.Object, _mapper);
            var result = await handler.Handle(new GetAllProductQuery() ,CancellationToken.None);
            if (result == null)
                Assert.IsType<BadRequestResult>(result);
            Assert.IsType<List<ProductDTO>>(result);
        }
        [Fact]
        public async Task DeleteCommand_ProductRecords()
        {
            var handler = new DeleteProductCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(new DeleteProductCommand(Guid.NewGuid()), CancellationToken.None);
            if (result == null)
                Assert.IsType<BadRequestResult>(result);
            Assert.IsType<bool>(result);
        }
    }
}