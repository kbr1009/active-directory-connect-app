using Xunit.Abstractions;
using ADApp.Application.OUs.Queries.ShowAllOUs;

namespace ADApp.Tests.Application.OUs.Queries
{
    public class ShowAllOUsTests
    {
        private readonly ITestOutputHelper _output;

        public ShowAllOUsTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Tree構造から検索をかけ正しくオブジェクトが取得できる()
        {
            var root = new OU("root");
            var ca1 = new OU("ca1");
            var ca2 = new OU("ca2");
            var cb1 = new OU("cb1");

            root
                .AddChild(ca1)
                .AddChild(ca2);
            ca2.AddChild(cb1);

            var searchResult = root.FindNode("cb1");
            if (searchResult != null)
            {
                _output.WriteLine("Found: " + searchResult.Name);
            }
            else
            {
                _output.WriteLine("Not Found");
            }

            root.DisplayTree();

            Assert.True(searchResult != null);
            Assert.Equal(cb1.Name, searchResult.Name);
        }
    }
}
