using Godot;
using System;

public partial class Windy : EffectBase
{
	Sprite2D ball;
	Vector2 dir;
	Path2D path;
	PathFollow2D pathFollow2D;
	Line trail;
	// Called when the node enters the scene tree for the first time.
	public async override void _Ready()
	{
		// trail = GetNode<Line>("Line2D");
		// trail.trailLength = 60;
		base._Ready();
		

		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		cards.draw(3);
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
    }
}
