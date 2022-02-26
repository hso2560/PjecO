using System;
using System.Collections.Generic;

public static class SiteData
{
    public static SiteManager sm => SiteManager.Instance;
    public static Dictionary<int, SiteInfo> siteInfoDict = new Dictionary<int, SiteInfo>();
    public static StartConstList<int> parentList = new StartConstList<int>(-1);

    public static void AddSite(SiteInfo data, bool newSite)
    {
        if (newSite)
        {
            sm.savedData.count++;

            List<int> list = new List<int>() { parentList.Start };
            SaveDic<int,SiteInfo> dic = sm.savedData.siteInfoDic;
            for(int i = 0; i<parentList.Count; i++)
            {
                list.Add(parentList[i]);
                dic = dic[parentList[i]].folder;
            }

            data.parentIdTrace = list;
            dic[data.id] = data;
            siteInfoDict[data.id] = data;
        }
    }

    public static void RemoveSite(int id)
    {
        siteInfoDict.Remove(id);

        List<int> list = new List<int>() { parentList.Start };
        SaveDic<int, SiteInfo> dic = sm.savedData.siteInfoDic;
        for (int i = 0; i < parentList.Count; i++)
        {
            list.Add(parentList[i]);
            dic = dic[parentList[i]].folder;
        }

        dic.keyValueDic.Remove(id);
    }
}
