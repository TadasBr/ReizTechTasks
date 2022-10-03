using System.Collections;

//Creating exact tree from example
Branch<int> tree = new Branch<int>(1)
{
            new Branch<int>(2)
            { 
                new Branch<int>(3),
            new Branch<int>(5)
            {
                new Branch<int>(5)
                {
                    new Branch<int>(4)  
                },
                new Branch<int>(6)
                {
                    new Branch<int>(4)
                    {
                        new Branch<int>(8)
                    },
                    new Branch<int>(7)
                },
                new Branch<int>(7)
            },
            }
        };

Console.WriteLine("Maximum depth of tree: {0}", FindDepthRecursively(tree));

int FindDepthRecursively(Branch<int> tree)
{
    if(!tree.Any())
    {
        return 1;
    }

    int maxDepth = 0;

    foreach (var branch in tree.branches)
    {
        maxDepth = Math.Max(maxDepth, FindDepthRecursively(branch));
    }

    return maxDepth + 1;
}

public class Branch<T> : IReadOnlyList<Branch<T>>
{
    public readonly List<Branch<T>> branches = new List<Branch<T>>();

    public Branch(T? value)
    {
        Value = value;
    }

    public Branch() : this(default(T)) { }

    public T? Value { get; set; }

    public Branch<T>? Parent { get; private set; }

    public void Add(Branch<T> child)
    {
        if (child is null)
            throw new ArgumentNullException(nameof(child));

        for (var parent = Parent; parent != null; parent = parent.Parent)
            if (parent == child)
                throw new ArgumentException("Trying to create a loop", nameof(child));

        if (child.Parent is not null)
            child.Parent.Remove(child);

        branches.Add(child);

        child.Parent = this;
    }

    public bool Remove(Branch<T> child)
    {
        if (child is null || !ReferenceEquals(child.Parent, this))
            return false;

        branches.Remove(child);
        child.Parent = null;
        return true;
    }

    public int Count => branches.Count;

    public Branch<T> this[int index] => branches[index];

    public IEnumerator<Branch<T>> GetEnumerator() => branches.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => branches.GetEnumerator();
}
