using Godot;
using System;
using System.Linq;

public partial class Echoing : EffectBase
{
	charaterState target;
	Node2D enemies;
	public override async void _Ready()
	{
		base._Ready();
		game.Connect(Game.SignalName._yourturn,Callable.From(over));
		
		var ChainScene = GD.Load<PackedScene>("res://effect/Chain.tscn");
		enemies = GetNode<Node2D>("/root/game/Enemies");
		
		cards = GetTree().Root.GetNode<Cards>("/root/game/cards");
		await ToSignal(GetTree().CreateTimer(0.3f), SceneTreeTimer.SignalName.Timeout);

		for(int i = 0; i < cards.GetChildCount(); i++){
			if(cards.GetChild<Card_ui>(i).CardName.charaterName == "Echo"){
				var chain = ChainScene.Instantiate<Chain>();
				cards.GetChild<Card_ui>(i).AddChild(chain);
				chain.Position = new Vector2(0, 0);
			}
		}

		for(int i = 0; i < enemies.GetChildCount(); i++){
			enemies.GetChild<EnemyState>(i).targetlist.Remove(game.Charaterlist.Find(x => x.name == "Echo"));
			enemies.GetChild<EnemyState>(i).changeTarget("Echo");
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public override void effect(charaterState actioner)
    {
		GlobalPosition = actioner.GlobalPosition;
		GD.Print(game.Charaterlist,game.Charaterlist.Count);
		target = game.Charaterlist.Find(x => x.name != "Echo");
		if(target != null){
			target.changeBuff("power",(float)Math.Round(target.power*2,2));
			target.changeBuff("rigidity",(float)Math.Round(target.rigidity*2,2));
		}
    }

	public void over(){
		try{
			target.changeBuff("power",(float)Math.Round(target.power/2,2));
			target.changeBuff("rigidity",(float)Math.Round(target.rigidity/2,2));
		}
		catch{}
		for(int i = 0; i < enemies.GetChildCount(); i++){
			var thislist = enemies.GetChild<EnemyState>(i).targetlist;
			var find = game.Charaterlist.Find(x => x.name == "Echo");
			thislist.Add(find);
		}
		QueueFree();
	}
}
