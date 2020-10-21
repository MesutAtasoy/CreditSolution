using Credit.Application.Services.CreditEvaluation;
using Xunit;

namespace Credit.UnitTest
{
    public class CreditEvaluationTest
    {
        
        [Theory]
        [InlineData(2500, 400, 4, false)]
        [InlineData(2500, 750, 4, true)]
        [InlineData(5100, 750, 4, false)] // Aynı skor'da 5000 TL altındaki gelire ait başvuru onaylanırken, daha yüksek gelirli aynı skora ait başvuru onaylanmıyor. Mantık Hatası var. 
        [InlineData(5100, 1750, 4, true)]
        public void Evaluate_should_expected_result_credit_evaluation(
            decimal monthlyIncome,
            int creditScore,
            int multipleFactor,
            bool expectedStatus)
        {
            //Arrange
            var evaluation = new CreditEvaluation()
                .WithCreditScore(creditScore)
                .WithMonthlyIncome(monthlyIncome)
                .WithMultipleFactor(multipleFactor);

            //Act
            evaluation.Evaluate();

            //Assert
            Assert.Equal(expectedStatus, evaluation.Approved);
        }
        
        [Theory]
        [InlineData(2500, 750, 4, 10000)]
        [InlineData(5100, 1750, 4, 20400)]
        public void Evaluate_should_expected_credit_limit(
            decimal monthlyIncome,
            int creditScore,
            int multipleFactor,
            decimal expectedCreditLimit)
        {
            //Arrange
            var evaluation = new CreditEvaluation()
                .WithCreditScore(creditScore)
                .WithMonthlyIncome(monthlyIncome)
                .WithMultipleFactor(multipleFactor);

            //Act
            evaluation.Evaluate();

            //Assert
            Assert.Equal(expectedCreditLimit, evaluation.CreditLimit);
        }
    }
}