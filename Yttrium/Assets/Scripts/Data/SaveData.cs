using System;
using System.Collections.Generic;

[Serializable]
public class SaveData 
{
    public SaveDic<int, SiteInfo> siteInfoDic = new SaveDic<int, SiteInfo>();
    public int count = 0;

    public void Save()
    {
        siteInfoDic.keyValueDic.Values.ForEach(x => x.Save());
        siteInfoDic.Save();
    }
    public void Load()
    {
        siteInfoDic.Load();
        siteInfoDic.keyValueDic.Values.ForEach(x => x.Load());
    }
}

[Serializable]
public class SiteInfo
{
    public int id;
    public List<int> parentIdTrace = new List<int>();

    public string url;
    public string name;
    public string explanation;

    public bool isFolder;

    public SaveDic<int, SiteInfo> folder = new SaveDic<int, SiteInfo>();

    public SiteInfo() { }
    public SiteInfo(int id, string url, string name, string explanation)
    {
        this.id = id;
        this.url = url;
        this.name = name;
        this.explanation = explanation;
        isFolder = false;
    }
    public SiteInfo(int id, string name, string explanation)
    {
        this.id = id;
        this.name = name;
        this.explanation = explanation;
        isFolder = true;
    }

    public void Save()
    {
        folder.keyValueDic.Values.ForEach(x => x.Save());
        folder.Save();
    }
    public void Load()
    {
        folder.Load();
        folder.keyValueDic.Values.ForEach(x => x.Load());
    }
}





[Serializable]
public class SaveDic<K, V>
{
    public List<K> keyList = new List<K>();
    public List<V> valueList = new List<V>();

    public Dictionary<K, V> keyValueDic = new Dictionary<K, V>();

    public V this[K key]
    {
        get
        {
            return keyValueDic[key];
        }
        set
        {
            keyValueDic[key] = value;
        }
    }

    public void Load()
    {
        keyValueDic.Clear();
        for (int i = 0; i < keyList.Count; i++)
        {
            keyValueDic.Add(keyList[i], valueList[i]);
        }
    }

    public void Save()
    {
        keyList.Clear();
        valueList.Clear();
        foreach (K k in keyValueDic.Keys)
        {
            keyList.Add(k);
            valueList.Add(keyValueDic[k]);
        }
    }

    public void ClearDic()
    {
        keyValueDic.Clear();
    }
}