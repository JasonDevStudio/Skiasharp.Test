namespace Skiasharp.Test.Lib;

/// <summary>
/// Summary的信息的具体实现
/// </summary>
public class PartSummaryItem
{ 
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="size">大小</param>
    /// <param name="title">标题</param>
    /// <param name="value">值</param>
    public PartSummaryItem(string title, string value)
    { 
        Title = title;
        Value = value;
    }
}