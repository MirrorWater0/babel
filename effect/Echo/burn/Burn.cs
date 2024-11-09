using Godot;
using System;

public partial class Burn : EffectBase
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

    public override void effect(charaterState actioner)
    {
		GlobalPosition = actioner.GlobalPosition;
        actioner.changeBuff("power",(float)Math.Round(actioner.power+0.3f,2));
    }
}
