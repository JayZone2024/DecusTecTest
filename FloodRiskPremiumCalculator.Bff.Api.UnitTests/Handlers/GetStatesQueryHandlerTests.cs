using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.Handlers;
using FloodRiskPremiumCalculator.Bff.Api.Repositories;
using FluentAssertions;
using Moq;


namespace FloodRiskPremiumCalculator.Bff.Api.UnitTests.Handlers
{
    public class GetStatesQueryHandlerTests : TestBase
    {
        private readonly Mock<IStateRatingRepository> _repository;
        private readonly GetStatesQueryHandler _handler;

        public GetStatesQueryHandlerTests()
        {
            _repository = new Mock<IStateRatingRepository>();

            _repository
                .Setup(m => m.GetAllAsync(CancellationToken))
                .ReturnsAsync(BuildFakeStateRateModels);

            _handler = new GetStatesQueryHandler(_repository.Object);
        }

        [Fact]
        public async Task WhenHandleGetStatesThenHandlerShouldReturnListOfStateDtos()
        {
            // Arrange
            var expectedResult = new List<StateDto>
            {
                new() { State = Florida },
                new() { State = California }
            };

            // Act
            var operationResult = await _handler.Handle(new GetStatesQuery(), CancellationToken);

            // Assert
            operationResult.IsSuccessful.Should().BeTrue();
            operationResult.Result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
