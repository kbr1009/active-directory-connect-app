namespace ADApp.Application.OUs.Queries.ShowAllOUs
{
    public abstract class TreeNodeBase<T> : ITreeNode<TreeNodeBase<T>> where T : class
    {
        /// <summary>
        /// 親への参照フィールド
        /// </summary>
        protected TreeNodeBase<T> _parent;

        /// <summary>
        /// 親への参照プロパティ
        /// </summary>
        public virtual TreeNodeBase<T> Parent
        {
            get => _parent;
            set => _parent = value;
        }

        /// <summary>
        /// 子ノードのリストフィールド
        /// </summary>
        protected IList<TreeNodeBase<T>> _children;

        /// <summary>
        /// 子ノードのリストプロパティ
        /// </summary>
        public virtual IList<TreeNodeBase<T>> Children
        {
            get => _children ??= new List<TreeNodeBase<T>>();
            set => _children = value;
        }

        /// <summary>
        /// 検索をかける
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual T FindNode(string name) => null;

        /// <summary>
        /// Treeを表示する
        /// </summary>
        /// <param name="depth"></param>
        public virtual void DisplayTree(int depth = 0) { }

        /// <summary>
        /// 子ノードを追加する。
        /// </summary>
        /// <param name="child">追加したいノード</param>
        /// <returns>追加後のオブジェクト</returns>
        public virtual TreeNodeBase<T> AddChild(TreeNodeBase<T> child)
        {
            if (child == null)
                throw new ArgumentNullException("Adding tree child is null.");

            this.Children.Add(child);
            child.Parent = this;

            return this;
        }

        /// <summary>
        /// 子ノードを削除する。
        /// </summary>
        /// <param name="child">削除したいノード</param>
        /// <returns>削除後のオブジェクト</returns>
        public virtual TreeNodeBase<T> RemoveChild(TreeNodeBase<T> child)
        {
            this.Children.Remove(child);
            return this;
        }

        /// <summary>
        /// 子ノードを削除する。
        /// </summary>
        /// <param name="child">削除したいノード</param>
        /// <returns>削除の可否</returns>
        public virtual bool TryRemoveChild(TreeNodeBase<T> child)
        {
            return this.Children.Remove(child);
        }

        /// <summary>
        /// 子ノードを全て削除する。
        /// </summary>
        /// <returns>子ノードを全削除後のオブジェクト</returns>
        public virtual TreeNodeBase<T> ClearChildren()
        {
            this.Children.Clear();
            return this;
        }

        /// <summary>
        /// 自身のノードを親ツリーから削除する。
        /// </summary>
        /// <returns>親のオブジェクト</returns>
        public virtual TreeNodeBase<T> RemoveOwn()
        {
            TreeNodeBase<T> parent = this.Parent;
            parent.RemoveChild(this);
            return parent;
        }

        /// <summary>
        /// 自身のノードを親ツリーから削除する。
        /// </summary>
        /// <returns>削除の可否</returns>
        public virtual bool TryRemoveOwn()
        {
            TreeNodeBase<T> parent = this.Parent;
            return parent.TryRemoveChild(this);
        }
    }
}
