namespace CreditScore.Contract.Constants
{
    public struct CreditScoreByIdentityNumberCacheKeys
    {
        public static string SectionPattern = "CreditScore.Score.";

        public struct ByIdentityNumber
        {
            public const string Pattern = "CreditScore.Score.ByIdentityNumber.";
            public const string Key ="CreditScore.Score.ByIdentityNumber.{identityNumber}";
            public const int TTL = 60 * 60 * 30;
        }
    }
}