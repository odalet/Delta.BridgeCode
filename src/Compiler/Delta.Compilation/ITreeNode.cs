using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Compilation
{
    public interface ITreeNode
    {
        ITreeNode ParentNode { get; }
        ITreeNode[] ChildNodes { get; }

        int ChildCount { get; }

        object GetPayload();
        void SetPayload(object value);
    }

    public interface ITreeNode<T> : ITreeNode
    {
        
    }
}
