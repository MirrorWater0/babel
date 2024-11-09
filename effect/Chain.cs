using Godot;
using System;

public partial class Chain : TextureRect
{
	public int count = 1;
	AnimationPlayer animation;
	Area2D detector;
	Game game;
	Card_ui card;
	public override void _Ready()
	{
		animation = GetNode<AnimationPlayer>("AnimationPlayer");
		detector = GetNode<Area2D>("detector");
		game = GetTree().Root.GetNode<Game>("game");
		game.Connect(Game.SignalName._yourturn,Callable.From(TurnStart));

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		if(detector.GetOverlappingAreas().Count > 0){
			card = detector.GetOverlappingAreas()[0].GetParent<Card_ui>();
			card.SetProcess(false);
		}
		if(count < 1){
			card = detector.GetOverlappingAreas()[0].GetParent<Card_ui>();
			animation.Play("fade");
			await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
			card.SetProcess(true);
			QueueFree();
		}
	}

	public void TurnStart(){
		count--;
	}
}
