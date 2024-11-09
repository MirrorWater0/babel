using Godot;
using System;

public partial class ChoseDisplay : Resource
{
	[Export]
	public Texture2D illustration;
	[Export]
	public Texture2D portrait;
	[Export]
	public PackedScene charaterScene;
	[Export]
	public string charaterName;
	[Export]
	public Texture2D charaterIcon;
	
}
