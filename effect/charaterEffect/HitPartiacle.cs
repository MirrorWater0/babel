using Godot;
using System;

public partial class HitPartiacle : GpuParticles2D
{
	// Called when the node enters the scene tree for the first time.
	GpuParticles2D P0;
	public override async void _Ready()
	{
		P0 = GetNode<GpuParticles2D>("P0");
		P0.Emitting = true;
		await ToSignal(GetTree().CreateTimer(4f), SceneTreeTimer.SignalName.Timeout);
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
