using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Core.Enums;
using Core.Models;

namespace Client.Models;

public partial class KnowledgeTagAva : ObservableObject
{
    public KnowledgeTagEnum Tag { get; set; }
    public string ToSwedish => Tag.ToSwedish();

    [ObservableProperty]
    public partial bool IsChecked { get; set; }

    public KnowledgeTagAva(KnowledgeTagEnum tag, bool isChecked)
    {
        Tag = tag;
        IsChecked = isChecked;
    }
}
