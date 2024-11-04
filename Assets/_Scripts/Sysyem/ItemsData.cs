public class ItemsData
{
    private int _id;
    public bool _isGetted;
    public bool _isSaw;

    public ItemsData(int id)
    {
        _id = id;
        _isGetted = false;
        _isSaw = false;
    }

    public int ID => _id;
    public bool IsSaw => _isSaw;
    public bool IsGetted => _isGetted;

    public void Get()
    {
        _isGetted = true;
    } 

    public void See()
    {
        _isSaw = true;
    }
}
