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


    }
}