using Godot;
using System;
using System.Collections.Generic;

public partial class DetailBook : CanvasLayer
{
	static public List<Skill> Charater1CardsPile = new List<Skill>(){EchoSkillCollection.attack, EchoSkillCollection.block,
	EchoSkillCollection.windy, EchoSkillCollection.breathe};
	static public List<Skill> Charater2CardsPile = new List<Skill>(){JuliusSkillCollection.attack, JuliusSkillCollection.block,
	JuliusSkillCollection.pierce,JuliusSkillCollection.block,JuliusSkillCollection.strategy,JuliusSkillCollection.terminal};
	static public int Energe = 4;

	static public float Charater1Power = 1;
	static public float Charater1Rigidity = 1;
	static public int Charater1MixLife = 60;

	static public float Charater2Power = 1;
	static public float Charater2Rigidity = 1;
	static public int Charater2MixLife = 70;
	
	PackedScene cardScene = GD.Load<PackedScene>("res://card/Card_ui.tscn"); 
	GridContainer gridContainer;
	int displayIndex = 1;
	TextureRect TextureCharater;
	public override void _Ready()
	{
        TextureCharater = GetNode<TextureRect>("ColorRect/TextureCharater");
		gridContainer = GetNode<GridContainer>("board/ScrollContainer/GridContainer");
		for(int i = 0;i< Charater1CardsPile.Count;i++){
			Card_ui cardNode = cardScene.Instantiate<Card_ui>();
			cardNode.CardName = Charater1CardsPile[i];

			gridContainer.AddChild(cardNode);
			cardNode.SetProcess(false);
			cardNode.detector.QueueFree();
			displayIndex = 1;
			TextureCharater.Texture = CharaterChose.alreadyChose[0].portrait;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	static public void addCard(Skill skill){
		if(skill.charaterName == CharaterChose.alreadyChose[0].charaterName){
			GD.Print(Charater1CardsPile.Count);
			Charater1CardsPile.Add(skill);
			GD.Print(Charater1CardsPile.Count);
			GD.Print("oneadd");
		}
		else{
			Charater2CardsPile.Add(skill);
			GD.Print("twoadd");
		}
	}

	public void switchInfo(){
		switch(displayIndex){
			case 1:
				TextureCharater.Texture = CharaterChose.alreadyChose[1].portrait;
				for(int i = 0;i< gridContainer.GetChildCount();i++){
					gridContainer.GetChild<Card_ui>(i).QueueFree();
				}
				for(int i = 0;i< Charater2CardsPile.Count;i++){
					Card_ui cardNode = cardScene.Instantiate<Card_ui>();
					cardNode.CardName = Charater2CardsPile[i];

					gridContainer.AddChild(cardNode);
					cardNode.SetProcess(false);
					cardNode.detector.QueueFree();
					displayIndex = 2;
			  	}
			break;
			case 2:
				TextureCharater.Texture = CharaterChose.alreadyChose[0].portrait;
				for(int i = 0;i< gridContainer.GetChildCount();i++){
					gridContainer.GetChild<Card_ui>(i).QueueFree();
				}
				for(int i = 0;i< Charater1CardsPile.Count;i++){
					Card_ui cardNode = cardScene.Instantiate<Card_ui>();
					cardNode.CardName = Charater1CardsPile[i];

					gridContainer.AddChild(cardNode);
					cardNode.SetProcess(false);
					cardNode.detector.QueueFree();
					displayIndex = 1;
			  	}
			break;
		}
	}

	static public void ChangeCharaterProperties(int index,string property, float value){
		Type type = typeof(DetailBook);
		var field = type.GetField("Charater"+ index.ToString() + property);
		GD.Print(field.GetValue(null));
		GD.Print(value);
		field.SetValue(null,(float)Math.Round((float)field.GetValue(null) + value,2));
		GD.Print("ok");
	}
}
