using System.Collections.Generic;
using System;

[Serializable]
public class StartConstList<T> 
{
    private T start;
    private List<T> list = new List<T>();

    public StartConstList(T start)
    {
        this.start = start;
        list.Add(start);
    }
    public void ChangeStart(T start)
    {
        this.start = start;
    }
    public T Start => start;
    public int Count => list.Count - 1;

    public void Add(T t)
    {
        list.Add(t);
    }
    public void Remove(T t)
    {
        for(int i = list.Count-1; i>0; i--)
        {
            if (list[i].Equals(t))
            {
                list.RemoveAt(i);
            }
        }
    }
    public void RemoveAt(int idx)
    {
        if (idx >= 0)
        {
            list.RemoveAt(idx + 1);
        }
    }
    public T Pop()
    {
        T t = list[Count];
        list.RemoveAt(Count);
        return t;
    }
    public T Dequeue()
    {
        if (Count > 0)
        {
            T t = list[1];
            list.RemoveAt(1);
            return t;
        }
        else
        {
            return start;
        }
    }

    public T this[int index]
    {
        get
        {
            if (index >= 0)
            {
                return list[index + 1];
            }
            return list[Count];
        }
        set
        {
            if(index >= 0)
            {
                list[index + 1] = value;
            }
        }
    }
}
