using ADApp.Infrastructure.ActiveDirectory;
using ADApp.Application.Accounts.Queries.ShowAllAccounts;
using System.DirectoryServices;

// 参考：
// https://csharp.hotexamples.com/jp/examples/-/DirectorySearcher/FindAll/php-directorysearcher-findall-method-examples.html
// ADの属性一覧
// https://www.ibm.com/docs/ja/security-verify?topic=directory-active-supported-attributes-error-handling

namespace ADApp.Infrastructure.Accounts.Queries
{
    public class ShowAllAccountsQuery : IShowAllAccountsQuery
    {
        private readonly ADSchemaContext _context;
        private const string FILTER_STRING = "(objectClass=user)";

        public ShowAllAccountsQuery(ADSchemaContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> Execute()
        {
            _context.ConnectDirectory();

            DirectorySearcher searcher = _context.Sercher;
            searcher.Filter = FILTER_STRING;

            SearchResultCollection results = searcher.FindAll();

            foreach (DirectoryEntry user in results
                .Cast<SearchResult>()
                .Select(x => x.GetDirectoryEntry()))
            {
                yield return new Account()
                {
                    AccountName = (string)user.Properties["name"].Value,
                    EmailAddress = (string)user.Properties["mail"].Value
                };
            }
            _context.DisConnectDirectory();
        }
    }
}
