using Godot;
using System;

public partial class Julius : PlayerState
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		name = "Julius";
		base._Ready();
		passivity();
	}

    public override async void passivity()
    {
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
        var count = game.GetNode<Node2D>("/root/game/Enemies").GetChildCount();
		GD.Print(count);
		for(int i = 0; i < game.Charaterlist.Count; i++){
			game.Charaterlist[i].changeBuff("rigidity",game.Charaterlist[i].rigidity+(float)count*0.2f);
			GD.Print("passivity");
		}
    }
}
