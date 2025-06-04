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
        // If the list is empty or index is out of range, throw an exception
        if (index < 0 || index >= _count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }
        // Start with the head of the list
        Node<T>? current = _head;
        // Traverse to the node at the specified index
        for (int i = 0; i < index; i++)
        {
            if (current == null)
            {
                throw new InvalidOperationException("List is shorter than expected.");
            }
            current = current.next;
        }

        // Check if current element is head, tails or somewhere in the middle
        if (current == _head)
        {
            if (_head == _tail)
            {
                // If there's only one item in the list
                _head = null;
                _tail = null;
            }
            else
            {
                _head = current!.next;
                _head!.previous = null;
            }
        }
        else if (current == _tail)
        {
            _tail = current!.previous;
            _tail!.next = null;

        }
        else
        {
            current!.next!.previous = current.previous;
            current!.previous!.next = current.next;
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