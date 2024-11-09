using Godot;
using System;

public partial class Pierce : EffectBase
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		base._Ready();
		await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override async void effect(charaterState target,charaterState actioner){
		
		GlobalPosition = target.GlobalPosition;
		cameraFocus(target.GlobalPosition);
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		target.block = 0;
		target.updatablock();
		target.receivedamge((int)(12*actioner.power + 1f*actioner.power));
	}
}
