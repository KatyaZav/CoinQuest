using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceLoader
{
    public List<T> Load<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path).ToList();
    }
}

