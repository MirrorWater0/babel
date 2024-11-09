using Godot;
using System;

public partial class dynamicButton : TextureButton
{
	
	ColorRect shadow;
	public override void _Ready()
	{
		var a = Event.eventData;
		shadow = GetNode<ColorRect>("shadow");
		Connect(SignalName.MouseEntered,Callable.From(mouseEntered));
		Connect(SignalName.MouseExited,Callable.From(mouseExited));
		PivotOffset = new Vector2(75, 350);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void mouseEntered()
	{
		CreateTween().TweenProperty(this,"scale",new Vector2(1.1f,1.1f),0.2f);
		CreateTween().TweenProperty(shadow,"position",new Vector2(0f,15f),0.2f);
	}

	public void mouseExited()
	{
		CreateTween().TweenProperty(this,"scale",new Vector2(1f,1f),0.2f);
		CreateTween().TweenProperty(shadow,"position",new Vector2(0f,0f),0.2f);
	}


}
