using Godot;
using System;

public partial class PaticleBall : Node2D
{
	public Vector2 to = new Vector2(1800, 900);
	Vector2 from;
	Vector2 offset;
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		Random random = new Random();
		from = GlobalPosition;
		
		offset = (float)random.NextDouble()*(to - from).Normalized().Rotated(Mathf.Pi/1.3f)*800;
		GD.Print(offset);

		move();
		await ToSignal(GetTree().CreateTimer(0.7f), SceneTreeTimer.SignalName.Timeout);
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void updataPostion(float value){
		var position1 = from.Lerp(to,value);
		var position2 = new Vector2(0,0).Lerp(offset,Mathf.Ease((float)Math.Sin(value*Math.PI),-1));
		GlobalPosition = position1 + position2;
	}

	public void move(){
		var tween = CreateTween();
		tween.TweenMethod(Callable.From<float>(updataPostion),0.0,1.0,0.5f).SetEase(Tween.EaseType.InOut);
		
	}
}
