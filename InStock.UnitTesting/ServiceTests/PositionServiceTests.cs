using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Exceptions;
using InStock.Lib.Services;
using NUnit.Framework;

namespace InStock.UnitTesting.ServiceTests
{
    [TestFixture]
    public class PositionServiceTests
        : TestBaseInStock
    {
        private readonly IStockRepository _repoStock;
        private readonly IPositionRepository _repoPosition;
        private readonly IPositionService _service;

        public PositionServiceTests()
        {
            _repoStock = A.Fake<IStockRepository>();
            
            _repoPosition = A.Fake<IPositionRepository>();

            _service = new PositionService(_repoPosition, _repoStock);
        }

        [Test]
        public void Delete_WhenSymbolCannotBeFound_ThenSymbolNotFoundExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(null);

            //Act

            Assert.Throws<SymbolNotFoundException>(() => _service.Delete(SomeUserId, SomeSymbol));
            
            //Assert
            A.CallTo(() => _repoPosition.Delete(A<int>._, A<string>._))
                .MustNotHaveHappened();
        }
    }
}