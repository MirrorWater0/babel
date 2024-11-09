using Godot;
using System;


public partial class EffectBase : Node2D
{
	public AnimationPlayer animation;
	public Camera2D camera;
	public Cards cards;
	public HandDiscard handDiscard = GD.Load<PackedScene>("res://ui_script/HandDiscard.tscn").Instantiate<HandDiscard>();
	public Game game;
	public Node2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<Node2D>("/root/game/player");
		animation = GetNode<AnimationPlayer>("AnimationPlayer");
		camera = GetTree().Root.GetNode<Camera2D>("game/Camera2D");
		cards = GetNode<Cards>("/root/game/cards");
		game = GetTree().Root.GetNode<Game>("game");
	}

	public virtual void effect(charaterState target,charaterState actioner){
		GD.Print("effect1");
	}

	public virtual void effect(charaterState actioner){
		GD.Print("effect2");
	}

	public async void cameraFocus(Vector2 position){
		GlobalPosition = position;
		Tween cameraTransform1 = camera.CreateTween();
		cameraTransform1.TweenProperty(camera,"global_position",GlobalPosition,0.1f).SetEase(Tween.EaseType.Out);
		cameraTransform1.TweenProperty(camera,"zoom",new Vector2(1.4f,1.4f),0.1f).SetEase(Tween.EaseType.Out);
		
		await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
		
		Tween cameraTransform2 = camera.CreateTween();
		cameraTransform2.TweenProperty(camera,"global_position",new Vector2(960,540),0.1f).SetEase(Tween.EaseType.Out);
		cameraTransform2.TweenProperty(camera,"zoom",new Vector2(1,1),0.1f).SetEase(Tween.EaseType.Out);
	}
}
