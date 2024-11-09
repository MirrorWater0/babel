using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public partial class SelectCard : Node2D
{
	PackedScene cardScene = GD.Load<PackedScene>("res://card/Card_ui.tscn");

	Area2D area2D1;
	Area2D area2D2;
	Area2D area2D3;
	Area2D target;
	Game game;
	ColorRect blackmask2;	
	ColorRect blackmask;
	public override void _Ready()
	{
		game = GetTree().Root.GetNode<Game>("game");
		area2D1 = GetNode<Area2D>("CanvasLayer/Area2D1");
		area2D2 = GetNode<Area2D>("CanvasLayer/Area2D2");
		area2D3 = GetNode<Area2D>("CanvasLayer/Area2D3");
		blackmask2 = GetNode<ColorRect>("CanvasLayer/blackmask2");
		blackmask = GetNode<ColorRect>("CanvasLayer/blackmask");
        
		blackmask.Color = new Color(0,0,0,0);
		CreateTween().TweenProperty(blackmask,"color",new Color(0,0,0,0.6f),0.5f);
		
	   var type1 = Type.GetType(CharaterChose.alreadyChose[0].charaterName+"SkillCollection");
	   var type2 = Type.GetType(CharaterChose.alreadyChose[1].charaterName+"SkillCollection");

	   var cardlist1 = type1.GetField("skills").GetValue(null) as Skill[];
	   var cardlist2 = type2.GetField("skills").GetValue(null) as Skill[];
	   var cardlist = cardlist1.Concat(cardlist2).ToList();

		for(int i = 0; i < 3; i++){
	       var cardNode = cardScene.Instantiate<Card_ui>();
		   Random random = new Random();
		   cardNode.CardName = cardlist[random.Next(cardlist.Count)];
		   switch(i){
			   case 0:
			   area2D1.AddChild(cardNode);
			   cardNode.Position = new Vector2(0, 0) - new Vector2(100, 130);
			   break;
			   case 1:
			   area2D2.AddChild(cardNode);
			   cardNode.Position = new Vector2(0, 0) - new Vector2(100, 130);
			   break;
			   case 2:
			   area2D3.AddChild(cardNode);
			   cardNode.Position = new Vector2(0, 0) - new Vector2(100, 130);
			   break;
		   }
		//    cardNode.GlobalPosition = new Vector2(random.Next(0, 1920), random.Next(0, 1080));
		   cardNode.detector.QueueFree();
		   cardNode.SetProcess(false);
		   cardNode.animate.Play("appear");

	   }
		area2D1.Position = hollow();
		CreateTween().TweenProperty(area2D1,"position",new Vector2(536,500),0.2f);
		area2D2.Position = hollow();
		CreateTween().TweenProperty(area2D2,"position",new Vector2(917,500),0.2f);
		area2D3.Position = hollow();
		CreateTween().TweenProperty(area2D3,"position",new Vector2(1296,500),0.2f);
	}

	public Vector2 hollow(){
		Random random = new Random();
		int X = random.Next(0, 1920);
		int Y;
		bool inHollow;
		do{
			Y = random.Next(0,1080);
			if(X< 1500 & X > 400 & Y < 200 & Y > 800){
				inHollow = true;
			}
			else{
				inHollow = false;
			}
			return new Vector2(X,Y);
		}
		while(inHollow);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		if(target != null & Input.IsActionJustPressed("Lclick")){
			var thisCard = target.GetOverlappingAreas()[0].GetParent<Card_ui>();
			GD.Print(thisCard.CardName.name);
			DetailBook.addCard(thisCard.CardName);
			thisCard.animate.Play("cardfade");
			thisCard.CreateBall(new Vector2(1575,5));
			CreateTween().TweenProperty(blackmask2,"modulate",new Color(0,0,0,1),0.8f);
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
			GetTree().ChangeSceneToFile("res://Game/Event/Event.tscn");
		}
	}

	public void Hover1(){
		target = null;
		target = area2D1;
		GD.Print("hover1");
	}

	public void Hover2(){
		target = null;
		target = area2D2;
		GD.Print("hover2");
	}

	public void Hover3(){
		target = null;
		target = area2D3;
		GD.Print("hover3");
	}

	public void UnHover(){
		target = null;
	}
}
