using Godot;
using System;

public partial class ShineButton : TextureButton
{
	public AudioStreamPlayer intereact;
    public Node2D root;
	public override void _Ready()
	{
		root = (Node2D)GetTree().GetCurrentScene();
		if(root.GetType() == typeof(Game)){
			root.Connect(Game.SignalName._yourturn,Callable.From(turnStart));
			GD.Print("connect");
		}
		intereact = root.GetNode<AudioStreamPlayer>("AudioManager/intereact");
		Connect(SignalName.ButtonDown,Callable.From(pressed));
		Connect(SignalName.ButtonUp,Callable.From(released));
	}
    public override void _Process(double delta)
    {
        if(GetGlobalRect().HasPoint(GetGlobalMousePosition())){
			((ShaderMaterial)Material).SetShaderParameter("Hover", true);
        }
		else{
			((ShaderMaterial)Material).SetShaderParameter("Hover", false);
		}

	}

	public virtual void pressed(){
		((ShaderMaterial)Material).SetShaderParameter("pressed", true);
	}
	public virtual void released(){
		((ShaderMaterial)Material).SetShaderParameter("pressed", false);
	}

	public void turnStart(){
		Disabled = false;
	}

	public void turnEnd(){
		Disabled = true;
	}

}
