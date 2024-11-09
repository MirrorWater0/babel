using Godot;
using System;

public partial class Beam1 : Node2D
{
	AnimationPlayer animation;
	public override async void _Ready()
	{
		animation = GetNode<AnimationPlayer>("AnimationPlayer");
		await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
