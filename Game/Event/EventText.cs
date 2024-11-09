using Godot;
using System;

public partial class EventText : Resource
{
	[Export]
	public int PageCount;
	[Export(PropertyHint.MultilineText)]
	public string text0;

	[Export(PropertyHint.MultilineText)]
	public string text1;

	[Export(PropertyHint.MultilineText)]
	public string text2;
}
