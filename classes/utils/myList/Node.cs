namespace argumentParser.Classes.Utils;

class Node<T>
{
    public Node<T>? next;

    public Node<T>? previous;
    public T? value;

    public Node(T val)
    {
        value = val;
    }
}