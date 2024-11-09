using Godot;
using System;

public partial class InterfaceButton : Button
{
	PackedScene next = GD.Load<PackedScene>("res://Game/Event/Event.tscn");
	EventData beginEvent = GD.Load<EventData>("res://Game/Event/BeginEvent1.tres");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(GetRect().HasPoint(GetViewport().GetMousePosition())) {
			Material.Set("shader_parameter/hover", true);
		}
		else {
			Material.Set("shader_parameter/hover", false);
		}
	}

	public async void pressed() {
		Material.Set("shader_parameter/pressed", true);
		CreateTween().TweenProperty(GetParent<Node2D>(),"modulate",new Color(1,1,1,0),1f);
		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
		
		Event.eventData = beginEvent;
		GetTree().ChangeSceneToPacked(next);
	}
	public void released() {
		Material.Set("shader_parameter/pressed", false);
	}
}
