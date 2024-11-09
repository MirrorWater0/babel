using Godot;
using System;

public partial class Strategy : EffectBase
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void effect(charaterState actioner){
		cards.draw(2);
		game.AddChild(handDiscard);
		handDiscard.HandDiscardLaunch(1);
	}
}
