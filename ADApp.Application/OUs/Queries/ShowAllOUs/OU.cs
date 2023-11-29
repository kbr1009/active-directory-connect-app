namespace ADApp.Application.OUs.Queries.ShowAllOUs
{
    public class OU : TreeNodeBase<OU>
    {
        private readonly string _name;

        public OU(string name) 
        { 
            _name = name;
        } 

        public string Name => _name;

        public override OU FindNode(string name)
        {
            if (Name == name) return this;

            foreach (var child in Children)
            {
                var found = child.FindNode(name);
                if (found != null) return found;
            }
            return null;
        }

        public override void DisplayTree(int depth = 0)
        {
            Console.WriteLine(new string(' ', depth * 2) + $"- {Name}");

            foreach (var child in Children)
            {
                child.DisplayTree(depth + 1);
            }
        }
    }
}
