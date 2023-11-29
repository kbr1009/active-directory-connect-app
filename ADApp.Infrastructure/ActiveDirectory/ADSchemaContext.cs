using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace ADApp.Infrastructure.ActiveDirectory
{
    public class ADSchemaContext
    {
        private readonly IOptions<DirectoryEntryOptions> _connectOptions;

        public ADSchemaContext(IOptions<DirectoryEntryOptions> options) 
        {
            _connectOptions = options;
        }

        private DirectorySearcher? _sercher;
        public DirectorySearcher Sercher => _sercher 
            ?? throw new ArgumentNullException("ADへのコネクションがないためSercherは使用不可");

        public void ConnectDirectory()
        {
            if (_connectOptions is null)
            {
                throw new Exception("コネクション情報がない");
            }

            var entry = new DirectoryEntry(
                _connectOptions.Value.Path,
                _connectOptions.Value.User,
                _connectOptions.Value.Password);

            // 接続確認
            try
            {
                var obj = entry.NativeObject;
            }
            catch
            {
                throw new ArgumentException("ADへの接続に失敗した");
            }

            _sercher = new DirectorySearcher(entry);
        }

        public void DisConnectDirectory()
        {
            _sercher?.Dispose();
        }
    }
}
