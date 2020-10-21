namespace Credit.Application.Services.CreditEvaluation
{
    public class CreditEvaluation 
    {
        public decimal MonthlyIncome { get; set; }
        public int CreditScore { get; set; }
        public int MultipleFactor { get; set; }
        public decimal CreditLimit { get; private set; }
        public bool Approved  { get; private set; }


        public CreditEvaluation WithMonthlyIncome(decimal monthlyIncome)
        {
            MonthlyIncome = monthlyIncome;
            return this;
        }

        public CreditEvaluation WithCreditScore(int creditScore)
        {
            CreditScore = creditScore;
            return this;
        }
        
        public CreditEvaluation WithMultipleFactor(int multipleFactor)
        {
            MultipleFactor = multipleFactor;
            return this;
        }

        public CreditEvaluation Evaluate()
        {
            this.Approved = false;
            if (CreditScore >= 500 && CreditScore < 1000 && MonthlyIncome < 5000)
            {
                this.CreditLimit  = 10000;
                this.Approved = true;
            }

            if (CreditScore >= 1000)
            {
                this.CreditLimit  =  this.MonthlyIncome * this.MultipleFactor;
                this.Approved = true;
            }

            return this;
        }
    }
}