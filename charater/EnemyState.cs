using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class EnemyState : charaterState
{
	public AnimationPlayer ActionAnimation;
	public Label attackLabel;
    Node2D player;
    public int SoulDebrisCount;
	Camera camera;
	public List<charaterState> target = new List<charaterState>();
	public List<charaterState> targetlist = new List<charaterState>();
	public override async void _Ready()
	{
		base._Ready();
		life = MaxLife;
	    lifeline.MaxValue = MaxLife;
        lifeline.Value = life;
		life = MaxLife;
		updatalife();
		updatablock();

		attackLabel = GetNode<Label>("actionLabel/attackLabel");
		ActionAnimation = GetNode<AnimationPlayer>("actionLabel/AnimationPlayer");

		detector.Connect(Area2D.SignalName.MouseEntered,Callable.From(displayAttackTarget));
		detector.Connect(Area2D.SignalName.MouseExited,Callable.From(undisplayAttackTarget));
		
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		game.Charaterlist[0].Connect(SignalName._dying,Callable.From<string>(changeTarget));
		game.Charaterlist[1].Connect(SignalName._dying,Callable.From<string>(changeTarget));
		targetlist = game.Charaterlist.ToList();
		
		camera = GetTree().Root.GetNode<Camera>("game/Camera2D");
	}

	public virtual void action1(charaterState target){
        GD.Print("action");
    }

	public virtual void action2(charaterState target){
		GD.Print("action2");
	}

    public void displayAttackTarget()
    {
		if(target != null){
			for(int i = 0;i < target.Count();i++){
				target[i].hoverTexture.SelfModulate = new Color(1, 0, 0, 1);
				target[i].hoverTexture.Visible = true;				
			}
		}
        
    }

	public void undisplayAttackTarget(){
		if(target != null){
			for(int i = 0;i < target.Count();i++){
				target[i].hoverTexture.SelfModulate = new Color(1, 1, 1, 1);
				target[i].hoverTexture.Visible = false;				
			}
		}

	}
    
	public virtual void TheTurnAction(){
    }

	public async void changeTarget(string name){
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		Random random0 = new Random();
		targetlist =game.Charaterlist.ToList();
		target[0] = targetlist[random0.Next(0,targetlist.Count())];
		if(name != ""){
			target.Clear();
			target.RemoveAll(x => x.name == name);
			target.Add(game.Charaterlist.Find(X => X.name != name));
		}
	}

    public override void dying()
    {
		SoulDebris.ChangeDebrisCount(10);
		for(int i = 0;i < target.Count();i++){
			target[i].hoverTexture.SelfModulate = new Color(1, 1, 1, 1);
		}
        base.dying();
    }

	public virtual void enimyStart(){
		block = 0;
		updatablock();
	}
	public async void cameraFocus(Vector2 pos,float time){
		camera.GlobalPosition = pos;
		Tween cameraTransform1 = camera.CreateTween();
		cameraTransform1.TweenProperty(camera,"global_position",pos,0.1f).SetEase(Tween.EaseType.Out);
		cameraTransform1.TweenProperty(camera,"zoom",new Vector2(1.4f,1.4f),0.1f).SetEase(Tween.EaseType.Out);
		
		await ToSignal(GetTree().CreateTimer(time), SceneTreeTimer.SignalName.Timeout);
		
		Tween cameraTransform2 = camera.CreateTween();
		cameraTransform2.TweenProperty(camera,"global_position",new Vector2(960,540),0.1f).SetEase(Tween.EaseType.Out);
		cameraTransform2.TweenProperty(camera,"zoom",new Vector2(1,1),0.1f).SetEase(Tween.EaseType.Out);
	}
}
