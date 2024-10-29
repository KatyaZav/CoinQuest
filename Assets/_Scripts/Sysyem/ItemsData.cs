using UnityEngine;

public class ItemsData
{
    public Items Item;
    
    private bool _isGetted;
    private bool _isSaw;
 
    public ItemsData(Items item)
    {
        Item = item;
        _isGetted = false;
        _isSaw = false;
    }

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
