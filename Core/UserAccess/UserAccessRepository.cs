namespace AuthorizationsTest.Core.UserAccess
{
    public interface IUserAccessRepository
    {
        bool HasAccess(string userId, string[] products, CancellationToken cancellationToken);
    }

    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly Dictionary<string, string[]> userIdToProductAccessMap = new Dictionary<string, string[]>()
        {
            { "2a3aa5e6-071d-4d40-8cf9-5f986c8e5ded", new string[] { Constants.ProductCodes.StandardUser, Constants.ProductCodes.PremiumUser } }, // john
            { "509bac4d-0648-4131-b0f5-52ce03a4069d", new string[] { Constants.ProductCodes.LimitedUser } }, // mani
            { "6bdeb8ac-2eb4-4cc1-a3ab-ed848a6967a9", new string[] { Constants.ProductCodes.StandardUser } }, // karthik
            { "fc764407-c2d7-46b2-8652-9f7eb71dd5af", new string[] { Constants.ProductCodes.StandardUser, Constants.ProductCodes.PremiumUser } } // gita
        };

        public bool HasAccess(string userId, string[] products, CancellationToken cancellationToken)
        {
            var userExistsInList = userIdToProductAccessMap.TryGetValue(userId, out var userProductIds);
            if (!userExistsInList || userProductIds?.Any() == false) return false;

            return products.All(p => userProductIds.Contains(p));
        }
    }
}
