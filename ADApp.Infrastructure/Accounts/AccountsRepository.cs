using ADApp.Infrastructure.ActiveDirectory;
using ADApp.Domain.Accounts;

namespace ADApp.Infrastructure.Accounts
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ADSchemaContext _aDSchemaContext;

        public AccountsRepository(ADSchemaContext aDSchemaContext) 
        { 
            _aDSchemaContext = aDSchemaContext;
        }
    }
}
