using Godot;
using System;

public partial class Detector1 : Area2D
{
	[Signal]
	public delegate void ParameterMouseEnteredEventHandler(Area2D self);
	[Signal]
	public delegate void ParameterMouseExitedEventHandler(Area2D self);
	Material ParentMaterial;
	AudioStreamPlayer audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		ParentMaterial = (Material)GetParent<Card_ui>().Material;
		Connect(Area2D.SignalName.MouseEntered,Callable.From(mouseEnter));
		Connect(Area2D.SignalName.MouseExited,Callable.From(mouseExit));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void mouseEnter()
	{
		if(!GetParent<Card_ui>().drag){
			audio.Playing = true;
		}
		ParentMaterial.Set("shader_parameter/fire_on", true);
        CreateTween().TweenProperty(GetParent<Card_ui>(), "scale", new Vector2(1.2f, 1.2f), 0.07f).SetEase(Tween.EaseType.Out);
		EmitSignal(SignalName.ParameterMouseEntered, this);
		
	}

	public void mouseExit()
	{
		ParentMaterial.Set("shader_parameter/fire_on", false);
        CreateTween().TweenProperty(GetParent<Card_ui>(), "scale", new Vector2(1f, 1f), 0.07f).SetEase(Tween.EaseType.Out);
		EmitSignal(SignalName.ParameterMouseExited, this);
	}
}
