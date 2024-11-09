using Godot;
using System;

public partial class PassitityTrigger : Node2D
{
	public AnimationPlayer animation;
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
