using Godot;
using System;

public partial class Terminal : EffectBase
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
    public override void effect(charaterState target, charaterState actioner)
    {
		GlobalPosition = target.GlobalPosition;
		cameraFocus(target.GlobalPosition);
		int toalblock = 0;
		for(int i = 0; i < player.GetChildCount(); i++){
			toalblock += player.GetChild<charaterState>(i).block;
			player.GetChild<charaterState>(i).block = 0;
			player.GetChild<charaterState>(i).updatablock();
		}
		
		var damage = toalblock*2*actioner.power;
		target.receivedamge((int)damage);
    }
}
