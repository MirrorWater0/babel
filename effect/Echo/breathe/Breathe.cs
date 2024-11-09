using Godot;
using System;

public partial class Breathe : EffectBase
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		base._Ready();
		game.updataEnerge(2);
		await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
		QueueFree();
	}


}
