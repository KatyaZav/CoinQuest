using System.Data;

public class ItemsData
{
    public int _id;
    public bool _isGetted;
    public bool _isSaw;
    public int _count;

    public ItemsData(int id)
    {
        _id = id;
        _isGetted = false;
        _isSaw = false;
    }

    public int ID => _id;
    public bool IsSaw => _isSaw;
    public bool IsGetted => _isGetted;
    public int Count => _count;

    public void AddCount(int count = 1)
    {
        _count += count;
    }

    public void RemoveCount(int count)
    {
        if (count < 0)
            throw new System.ArgumentException($"Can't remove {count}");

        if (count > Count)
            throw new System.Exception("Not enough count's to get!");

        _count -= count;
    }

    public void Get()
    {
        _isGetted = true;
        _count += 1;
    } 

    public void See()
    {
        _isSaw = true;
    }
}
