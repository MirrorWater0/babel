using Godot;
using System;

public partial class DamgeLabel : Label
{
	Vector2 dir;
	Vector2 acc = new Vector2(0, 8);
	Vector2 velocity;
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		Position = new Vector2(0, 0);
		Random x = new Random();
		Random y = new Random();
		dir = new Vector2(x.Next(-100, 100), y.Next(-100, -90)).Normalized();
		velocity = dir * 1000;
        
		CreateTween().TweenProperty(this, "modulate", new Color(0.9f, 2f, 2f, 1), 1f);
		CreateTween().TweenProperty(this, "scale", new Vector2(2f, 2f), 1f);
		await ToSignal(GetTree().CreateTimer(4.0f), SceneTreeTimer.SignalName.Timeout);
        
		QueueFree();
	}
    
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		velocity += acc;
        GlobalPosition += velocity * (float)delta;
	}
}
