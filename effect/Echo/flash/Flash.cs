using Godot;
using System;

public partial class Flash : EffectBase
{
	
	// Called when the node enters the scene tree for the first time.
	public async override void _Ready()
	{
		
		base._Ready();
		await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
		game.AddChild(handDiscard);
        handDiscard.HandDiscardLaunch(1);
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void effect(charaterState target,charaterState actioner)
	{
		cameraFocus(target.GlobalPosition);
		GlobalPosition = target.GlobalPosition;
		int finaldamage = (int)(10*actioner.power);
		target.receivedamge(finaldamage);
	}
}
