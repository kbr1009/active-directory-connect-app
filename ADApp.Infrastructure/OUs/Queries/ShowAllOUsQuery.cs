using ADApp.Application.OUs.Queries.ShowAllOUs;
using ADApp.Infrastructure.ActiveDirectory;
using System.DirectoryServices;

namespace ADApp.Infrastructure.OUs.Queries
{
    public class ShowAllOUsQuery : IShowAllOUsQuery
    {
        private readonly ADSchemaContext _context;
        private const string FILTER_STRING = "(objectCategory=organizationalUnit)";

        public ShowAllOUsQuery(ADSchemaContext context) 
        {
            _context = context;
        }

        public OU Execute()
        {
            _context.ConnectDirectory();

            DirectorySearcher searcher = _context.Sercher;
            searcher.Filter = FILTER_STRING;

            IEnumerable<DirectoryEntry> results = searcher
                .FindAll()
                .Cast<SearchResult>()
                .Select(x => x.GetDirectoryEntry())
                .ToList();

            DirectoryEntry rootDir = searcher.SearchRoot;

            string rootDirName = (string)rootDir.Properties["name"].Value;
            var rootNode = new OU(rootDirName);

            List<DirectoryEntry> okCollection = new List<DirectoryEntry>();
            for (;;)
            {
                if (results.Count() == okCollection.Count) break;

                foreach (DirectoryEntry ou in results)
                {
                    var pearentName = (string)ou.Parent.Properties["name"].Value;
                    if (string.IsNullOrEmpty(pearentName)) continue;
                    OU parent = rootNode.FindNode(pearentName);
                    if (parent is null) continue;
                    parent.AddChild(new OU((string)ou.Properties["name"].Value));
                    okCollection.Add(ou);
                }
            }
            _context.DisConnectDirectory();

            return rootNode;
        }
    }
}
