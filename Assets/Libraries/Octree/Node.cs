using System.Numerics;

namespace Octree
{
    public class Node<TValue>
    {
        public Vector3 position;
        public Node<TValue>[] children;
        public TValue value;
    }
}
