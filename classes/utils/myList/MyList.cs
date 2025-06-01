namespace argumentParser.Classes.Utils;

class MyList<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;
    private int _count;
    public MyList()
    {
        _head = null;
        _tail = null;
        _count = 0;
    }
    public void Add(T item)
    {
        Node<T> newNode = new Node<T>(item);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail!.next = newNode;
            newNode.previous = _tail;
            _tail = newNode;
        }
        _count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }
    }

    public void DisplayItems()
    {
        Node<T>? current = _head;
        while (current != null)
        {
            Logger.DisplayMessage(current.value?.ToString() ?? "");
            current = current.next;
        }
    }
    public int Count()
    {
        return _count;
    }
    public bool IsEmpty()
    {
        return _count == 0;
    }
    public void Clear()
    {
        _head = null;
        _tail = null;
        _count = 0;
    }
}