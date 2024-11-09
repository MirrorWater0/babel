using Godot;
using Microsoft.VisualBasic;
using System;

public partial class DanceOfSword : EffectBase
{
	public override async void _Ready()
	{
		base._Ready();
		
		await ToSignal(GetTree().CreateTimer(5f), SceneTreeTimer.SignalName.Timeout);
		QueueFree();
	}

    public override async void effect(charaterState target, charaterState actioner)
    {
		cameraFocus(target.GlobalPosition);
		GlobalPosition = target.GlobalPosition;
		int finaldamage = (int)(10*actioner.power);
        for (int i = 0; i < 3; i++){
			target.receivedamge(finaldamage);
		}
		await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);
		target.receivedamge((int)(finaldamage*actioner.power));
		actioner.changeBuff("power",(float)Math.Round(actioner.power-0.4f,2));
    }
}
