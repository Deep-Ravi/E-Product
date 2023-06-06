namespace Assignment.UnitTest.Validator
{
    public static class MockProductValidator
    {
        public static Mock<IValidator<CreateProductDTO>> Validator()
        {
            var mockValidator = new Mock<IValidator<CreateProductDTO>>();
            mockValidator.Setup(p => p.Validate(It.IsAny<CreateProductDTO>()))
                         .Returns(new ValidationResult());
            return mockValidator;
        }
    }
}
