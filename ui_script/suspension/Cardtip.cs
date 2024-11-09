using Godot;
using System;

public partial class Cardtip : TextureRect
{
	[Export]
	public Label description;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	static public Control Tooltip(string forText)
    {
		PackedScene cardtipScene = GD.Load<PackedScene>("res://ui_script/suspension/Cardtip.tscn");
		var cardtip = cardtipScene.Instantiate<Cardtip>();
		cardtip.description.Text = forText;
        return cardtip;
    }
}
