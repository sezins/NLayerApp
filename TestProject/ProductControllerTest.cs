using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nlayer.Apý.Controllers;
using Nlayer.Core.Entity;
using Nlayer.Core.Repositories;
using Nlayer.Core.Service;

namespace TestProject
{
    public class ProductControllerTest
    {
        private readonly Mock<IGenericRepository<Product>> _mockRepository;
        private readonly ProductsController _productsController;

        private List<Product> products;

        public ProductControllerTest()
        {
            _mockRepository = new Mock<IGenericRepository<Product>>();

            var mapperMock = new Mock<IMapper>();
            var serviceMock = new Mock<IService<Product>>();
            var productServiceMock = new Mock<IProductService>();

            _productsController = new ProductsController(mapperMock.Object, serviceMock.Object, productServiceMock.Object);

            products = new List<Product>() {  new Product {
            Id=1,
            CategoryId=1,
            Name="Kalem 1",
            Price=100,
            Stock=20,
            CreatedDate=DateTime.Now,
            },
            new Product {
            Id = 2,
            CategoryId = 2,
            Name = "Kitap 1",
            Price = 100,
            Stock = 20,
            CreatedDate = DateTime.Now,
            },
            new Product
            {
                 Id = 3,
                 CategoryId = 3,
                 Name = "Kalem 1",
                 Price = 100,
                 Stock = 20,
                 CreatedDate = DateTime.Now,
             }};
        }

        [Fact]
        public async void All_ActionExecutes()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(products.AsQueryable());

            var result = await _productsController.All();
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.Equal<int>(2, returnProducts.ToList().Count);

        }

        [Theory]
        [InlineData(0)]
        public async void GetProduct_IdInValid_ReturnNotFound(int id)
        {
            Product product = null;
            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(product);
            var result=await _productsController.GetById(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetProduct_IdInValid_ReturnOkResult(int id)
        {
            var product = products.First(x => x.Id == id);

            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(product);

            var result = await _productsController.GetById(id);
            var okResult=Assert.IsType<OkObjectResult>(result);
            var returnProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(id, returnProduct.Id);
            Assert.Equal(product.Name, returnProduct.Name);
            
        }

    }
}