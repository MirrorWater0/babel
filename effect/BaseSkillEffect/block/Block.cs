using Godot;
using System;

public partial class Block : EffectBase
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		base._Ready();
		await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
		QueueFree();
	}

	public override void effect(charaterState actioner){
		GlobalPosition = actioner.GlobalPosition;
		int finalblock = (int)(10*actioner.rigidity);
		actioner.receiveblock(finalblock);
	}
}
