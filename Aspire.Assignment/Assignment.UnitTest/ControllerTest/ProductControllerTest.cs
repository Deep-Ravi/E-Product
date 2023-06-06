namespace Assignment.UnitTest.ControllerTest
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task Get_Return_AllProducts()
        {
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.IsAny<GetAllProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => MockProductData.ProductMockRecords());
            var controller = new ProductController(mediator.Object);
            var result = await controller.Get() as OkObjectResult;
            if (result.Value == null)
                Assert.IsType<BadRequestResult>(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Create_ProductRecords()
        {
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid());
            var controller = new ProductController(mediator.Object);
            var result = await controller.Post(MockProductData.ValidData()) as ObjectResult;
            if (result.StatusCode != (int)HttpStatusCode.Created)
                Assert.IsType<BadRequestResult>(result);
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Update_ProductDetails()
        {
            var product = MockProductData.ProductMockRecords().FirstOrDefault();
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product.Id);
            var controller = new ProductController(mediator.Object);
            var result = await controller.Put(MockProductData.ProductMockRecords().FirstOrDefault()) as ObjectResult;
            if (result.StatusCode != (int)HttpStatusCode.OK)
                Assert.IsType<BadRequestResult>(result);
            Assert.IsType<ObjectResult>(result);
        }
        [Fact]
        public async Task Delete_ProductRecords()
        {
            var mediator = new Mock<IMediator>();
            var controller = new ProductController(mediator.Object);
            mediator.Setup(p => p.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            await controller.Delete(Guid.NewGuid().ToString());
        }
    }
}