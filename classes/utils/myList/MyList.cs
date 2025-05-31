using System.Collections;

namespace argumentParser.Classes.Utils;

class MyList<T>
{
    private Node<T> first;

    private Node<T> last;

    private int _length = 0;

    public void Add(T obj)
    {
        _length++;
        var current = new Node<T>(obj);
        if (first == null)
        {
            first = current;
            last = current;
        }
        else
        {
            var tempLast = last;
            last.next = current;
            current.previous = tempLast;
        }
    }

    public void DisplayItems()
    {
        for (int i = 0; i < _length; i++)
        {

        }
    }
}