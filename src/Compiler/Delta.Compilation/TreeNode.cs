using System;
using System.Collections.Generic;

namespace Delta.Compilation
{
    public class TreeNode : ITreeNode
    {
        private object payload = null;
        private ITreeNode parent = null;
        private List<ITreeNode> children = new List<ITreeNode>();

        #region ITreeNode Members

        public ITreeNode ParentNode
        {
            get { return parent; }
        }

        public ITreeNode[] ChildNodes
        {
            get { return children.ToArray(); }
        }

        public int ChildCount
        {
            get { return children.Count; }
        }

        public object GetPayload()
        {
            return payload;
        }

        public void SetPayload(object value)
        {
            payload = value;
        }

        #endregion
    }

    public class TreeNode<TPayload> : TreeNode
    {
        public TPayload Payload
        {
            get { return (TPayload)base.GetPayload(); }
            protected set { base.SetPayload(value); }
        }
    }
}
