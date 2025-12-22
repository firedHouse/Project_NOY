using System;

[Serializable]
public class StringData : ITableData
{
    public string string_ID;
    public string kr;
    public string en;
    public string jp;
    public string desc;

    public string PrimaryID => string_ID.ToString();
}
