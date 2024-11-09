using Godot;
using System;

public partial class PlayerState : charaterState
{

	public override void _Ready()
	{

		base._Ready();
		int index = game.Charaterlist.IndexOf(this);
		MaxLife = (int)typeof(DetailBook).GetField("Charater" + (index + 1).ToString() + "MixLife").GetValue(null);
		lifeline.MaxValue = MaxLife;
		life = MaxLife;
		updatalife();
		
		game.Connect(Game.SignalName._yourturn,Callable.From(turnStart));
	}


	public override void _Process(double delta)
	{
	}
    public override void dying()
    {
		game.Charaterlist.Remove(this);
        base.dying();
    }

	public virtual void passivity(){
		
	}
	public virtual void passivity(Skill skill){
		
	}

	public virtual void turnStart(){
		
	}
}
