
using System.Collections;

namespace MazeGame.Libs
{
    public class TreeNode(string id) : IEnumerable<TreeNode>
    {
        private readonly Dictionary<string, TreeNode> _children = [];

        public readonly string Id = id;

        public TreeNode? Parent { get; set; }


        public TreeNode GetChild(string id)
        {
            return _children[id];
        }

        public void Add(TreeNode item)
        {
            item.Parent?._children.Remove(item.Id);
            item.Parent = this;
            _children.Add(item.Id, item);
        }

        public int Count
        {
            get => _children.Count;
        }
        public IEnumerator<TreeNode> GetEnumerator()
        {
            return _children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
