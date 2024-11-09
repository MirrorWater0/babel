using Godot;
using System;

public partial class Line : Line2D
{
	[Export]
	public Node2D target;
	[Export]
	public int trailLength = 30;
	[Export]
	public Vector2 offset = new Vector2(0, 0);
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition = Vector2.Zero;
		try
		{
			AddPoint(ToLocal(target.GlobalPosition)+offset);
		}
		catch
		{
			QueueFree();
		}
		if(GetPointCount() > trailLength){
			RemovePoint(0);
		}
	}
}
