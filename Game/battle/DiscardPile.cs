using Godot;
using System;
using System.Collections.Generic;

public partial class DiscardPile : CanvasLayer
{
	Cards cards;
	PackedScene cardScene = GD.Load<PackedScene>("res://card/Card_ui.tscn");
	TextureRect blackmask;
	ShineButton discardpileButton;
	public override void _Ready()
	{
		blackmask = GetTree().Root.GetNode<TextureRect>("/root/game/UI/blackmask");
		cards = GetNode<Cards>("/root/game/cards");
		
		discardpileButton = GetNode<ShineButton>("/root/game/UI/discardpileButton");

		discardpileButton.Connect(ShineButton.SignalName.Pressed,Callable.From(changeVisiblity));
		
	}

	public void updateDiscardCardpile(List<Skill> list){
		for(int i = 1; i < GetChildCount()+1; i++){
			GetChild<Card_ui>(i).QueueFree();
		}
        for (int i = 0; i < list.Count; i++){
			Card_ui cardNode = cardScene.Instantiate<Card_ui>();
			Skill data = list[i];
			
			cardNode.CardName = data;
            
			AddChild(cardNode);
          

			cardNode.detector.QueueFree();
			cardNode.SetProcess(false);
		}
	}

	public void changeVisiblity(){
		if(Visible == true){
			Visible = false;
			blackmask.Visible = false;
            
		}else{
			Visible = true;
			blackmask.Visible = true;
			for(int i = 1; i <= GetChildCount(); i++){
				var cardNode = GetChild<Card_ui>(i);
				cardNode.Position = new Vector2(1800, 800);
				float gap = 1800 /8;

				Vector2 PositionTarget; 
				switch(i){
					case int n when(n < 8):
					PositionTarget = (i + 1) * gap * Vector2.Right + new Vector2(0, 100);
					cardNode.CreateTween().TweenProperty(cardNode, "global_position", PositionTarget, 0.2f);
					break;
					case int n when(n >= 8 & n < 16):
					PositionTarget = (i + 1) * gap * Vector2.Right + new Vector2(0, 300);
					cardNode.CreateTween().TweenProperty(cardNode, "global_position", PositionTarget, 0.2f);
					break;
			}
			}
		}

		GD.Print("changeVisiblity");
	}
}
