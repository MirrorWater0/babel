using Godot;
using System;

public partial class Next : Resource
{
	[Export]
	public PackedScene NextScene;
	[Export]
	public EventData NextEvent;
	[Export]
	public Godot.Collections.Array<PackedScene> enemylist;
}
