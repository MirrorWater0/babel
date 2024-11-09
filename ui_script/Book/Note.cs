using Godot;
using System;
using System.Collections.Generic;

public partial class Note : Resource
{
	[Export]
	public int PageCount;
	[Export(PropertyHint.MultilineText)]
	public string text0;
	[Export(PropertyHint.MultilineText)]
	public string text1;
	[Export(PropertyHint.MultilineText)]
	public string text2;
	[Export(PropertyHint.MultilineText)]
	public string text3;
	[Export(PropertyHint.MultilineText)]
	public string text4;
	[Export(PropertyHint.MultilineText)]
	public string text5;
}
