using ADApp.Application.Accounts.Queries.ShowAllAccounts;
using ADApp.Application.OUs.Queries.ShowAllOUs;

namespace ADApp.ConsoleApp.Services
{
    public class ExportADUsersForExcel : ConsoleAppBase
    {
        private readonly IShowAllAccountsQuery _showAllAccountsQuery;
        private readonly IShowAllOUsQuery _showAllOUsQuery;

        public ExportADUsersForExcel(
            IShowAllAccountsQuery showAllAccountsQuery,
            IShowAllOUsQuery showAllOUsQuery) 
        { 
            _showAllAccountsQuery = showAllAccountsQuery;
            _showAllOUsQuery = showAllOUsQuery;
        }

        [Command("exportADUsersForExcel")]
        public void Execute()
        {
            var data = _showAllAccountsQuery.Execute();
            long counnt = 0;
            foreach (var account in data) 
            {
                counnt++;
                //Console.WriteLine($"アカウント {counnt}");
                //Console.WriteLine($"　アカウント名：{account.AccountName}");
                //Console.WriteLine($"　EmailAddress：{account.EmailAddress}");
            }

            var ous = _showAllOUsQuery.Execute();
            ous.DisplayTree();
        }
    }
}
