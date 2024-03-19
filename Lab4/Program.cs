using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Runtime.InteropServices;

class Program
{
    public static void Main(string[] args)
    {
        IntNode head = new IntNode() { Data = 5, Next = new IntNode() { Data = 7, Next = new IntNode() { Data = 2 } } };
        Node<String> names = new() { Data = "Adam" };
        WseiStack<string> stack = new WseiStack<string>();
        stack.Push("Adam");
        stack.Push("Ewa");
        stack.Push("Karol");
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
        
        WseiStack<double> liczbystack = new WseiStack<double>();
        liczbystack.Push(4.5);
        liczbystack.Push(10.2);
        liczbystack.Push(227.9);
        double sum = 0;
        while (!liczbystack.IsEmpty())
        {
            sum += liczbystack.Pop();
        }
        Console.WriteLine(sum);
        PizzaBox<PepperoniPizza> box = new();
        box.Content = new PepperoniPizza() { Ingredients = new[] { "Pepperoni", "tomato" } };
        box.PrintContent();
        var r = GirlsAndBoys(new[] {"Adam", "Ewa", "Karol", "Beata"});
        Console.WriteLine($"Girls: {r.Item1}");
        Console.WriteLine($"Boys: {r.Item2}");
        (string names, int age) tuple = ("Adam", 23);
        int a = 56;
        int b = 12;
        //
        (a, b) = (b, a);
        Console.WriteLine(a);
    }
    
    // (a + b) * c
    // a b + c *
    // + a b * c
    public static (int, int) GirlsAndBoys(IEnumerable<string> names)
    {
        int total = 0;
        int girls = 0;
        foreach (var name in names)
        {
            ++total;
            if (name.ToLower().EndsWith("a"))
            {
                ++girls;
            }
        }

        ValueTuple<int, int> result = new ValueTuple<int, int>(girls, total - girls);
        // return result;
        return (girls, total - girls);
    }
}

class PizzaBox<T> where T : Pizza, new()
{
    public T Content { get; set; } = new T() { Ingredients = new[] { "pane", "cheese" } };

    public void PrintContent()
    {
        Console.WriteLine(string.Join(",", Content.Ingredients));

    }
}

class PepperoniPizza : Pizza
{
    public PepperoniPizza Pizza {get; set;} 
}

class Pizza 
{
    public string[] Ingredients { get; set; }
    
}

    interface IWseiStack<T>
{
    void Push(T item);
    T Pop();
}
// [ , , , , , , , , , ..]
// last = 1
// [2 , 3 , , , , , , , ,]
// last = 2 Push(6)
// [2 , 3 , 6 , , , , , , ,]

class WseiStackArray<T> : IWseiStack<T>
{
    private T[] _array = new T[100];
    private int _last = -1;

    public void Push(T item)
    {
        if (_last < _array.Length - 1)
        {
            _array[++_last] = item;
        }
        else
        {
            throw new Exception("Stack is full!");
        }
    }

    public T Pop()
        {
            if (_last > -1)
            {
                return _array[_last--];
            }
            else
            {
                throw new Exception("Stack is full!");
            }
        }
}

class WseiStack<T>: IWseiStack<T>
{
    private Node<T>? _top;
    public void Push(T item)
    {
        _top = new Node<T>() { Data = item, Next = _top };
    }
    public bool IsEmpty()
    {
        return _top == null;
    }
    public T Pop()
    {
        if (_top == null)
        {
            throw new Exception("Stack is empty");
        }
        var data= _top.Data;
        _top = _top.Next;
        return data;
    }
}

class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }
}

class IntNode
{
    public int Data { get; set; }
    
    public IntNode Next { get; set; }
    
}

class StringNode
{
    public string Data { get; set; }
    public StringNode Next { get; set; }
}