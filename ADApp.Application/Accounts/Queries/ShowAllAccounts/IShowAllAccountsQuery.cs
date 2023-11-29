namespace ADApp.Application.Accounts.Queries.ShowAllAccounts
{
    public interface IShowAllAccountsQuery
    {
        IEnumerable<Account> Execute();
    }
}
