using Godot;
using System;


public partial class Attack : EffectBase
{
	// 	public AnimationPlayer animation;
	// public Camera2D camera;
	
	// Called when the node enters the scene tree for the first time.
	public  override async void _Ready()
	{
		
		base._Ready();
		await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
		QueueFree();
	}
    

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}



	public override void effect(charaterState target,charaterState actioner){
		GlobalPosition = target.GlobalPosition;
		cameraFocus(target.GlobalPosition);
		int finaldamage = (int)(10*actioner.power);
		target.receivedamge(finaldamage);
	}
}
