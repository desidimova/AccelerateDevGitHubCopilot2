using NSubstitute;
using Library.ApplicationCore;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests.Infrastructure.JsonLoanRepositoryTests;

public class GetLoanTest
{
    private readonly ILoanRepository _mockLoanRepository;
    private readonly JsonLoanRepository _jsonLoanRepository;
    private readonly IConfiguration _configuration;
    private readonly JsonData _jsonData;

    public GetLoanTest()
{
    _mockLoanRepository = Substitute.For<ILoanRepository>();

    // Setup configuration to use the JSON files copied to test output directory
    var configData = new Dictionary<string, string>
    {
        {"JsonPaths:Authors", "Json/Authors.json"},
        {"JsonPaths:Books", "Json/Books.json"},
        {"JsonPaths:BookItems", "Json/BookItems.json"},
        {"JsonPaths:Patrons", "Json/Patrons.json"},
        {"JsonPaths:Loans", "Json/Loans.json"}
    };
    _configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(configData)
        .Build();

    _jsonData = new JsonData(_configuration);
    _jsonLoanRepository = new JsonLoanRepository(_jsonData);
}
    [Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns loan when loan ID is found")]
    public async Task GetLoan_ReturnsLoan_WhenLoanIdExists()
    {
        // Arrange
        int existingLoanId = 1; // Loan ID 1 exists in Loans.json
        var expectedLoan = new Loan
        {
            Id = existingLoanId,
            BookItemId = 17,
            PatronId = 22,
            LoanDate = new DateTime(2023, 12, 8, 0, 40, 43).AddTicks(18088620),
            DueDate = new DateTime(2023, 12, 22, 0, 40, 43).AddTicks(18088620),
            ReturnDate = null
        };
        _mockLoanRepository.GetLoan(existingLoanId).Returns(expectedLoan);
    
        // Act
        var actualLoan = await _jsonLoanRepository.GetLoan(existingLoanId);
    
        // Assert
        Assert.NotNull(actualLoan);
        Assert.Equal(expectedLoan.Id, actualLoan.Id);
    }

    [Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns null when loan ID is not found")]
    public async Task GetLoan_ReturnsNull_WhenLoanIdDoesNotExist()
    {
        // Arrange
        int nonExistingLoanId = 9999; // Assuming this ID does not exist in Loans.json
        _mockLoanRepository.GetLoan(nonExistingLoanId).Returns((Loan?)null);

        // Act
        var actualLoan = await _jsonLoanRepository.GetLoan(nonExistingLoanId);

        // Assert
        Assert.Null(actualLoan);
    }
}