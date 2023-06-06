namespace Assignment.UnitTest.Validator
{
    public class ProductValidatorTest
    {
        private readonly CreateProductDTOValidator _createProductDTOValidator;

        public ProductValidatorTest()
        {
            _createProductDTOValidator = new CreateProductDTOValidator();
        }

        [Fact]
        public async Task Validate_ValidProductRecords()
        {
            var result=_createProductDTOValidator.TestValidate(MockProductData.ValidData());
            Assert.Equal(true,result.IsValid);
        }

        [Fact]
        public async Task Validate_InValidProductRecords()
        {
            var result = _createProductDTOValidator.TestValidate(MockProductData.InValidData());
            Assert.Equal(2, result.Errors.Count);
        }
    }
}
