using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CharaterChose : Node2D
{
	TextureRect illustration;
	Label alreadyChose1;
	Label alreadyChose2;
	ChoseDisplay choseDisplay;
	ChoseDisplay nowDisplay;

	ChoseDisplay EchoDisplay = GD.Load<ChoseDisplay>("res://Game/interface/ChoseDisplay/ChoseEcho.tres");
	ChoseDisplay JuliusDisplay = GD.Load<ChoseDisplay>("res://Game/interface/ChoseDisplay/ChoseJulius.tres");
	List<ChoseDisplay> Displays = new List<ChoseDisplay>();
	// public static List<ChoseDisplay> alreadyChose = new List<ChoseDisplay>();
	public static List<ChoseDisplay> alreadyChose = new List<ChoseDisplay>()
	{GD.Load<ChoseDisplay>("res://Game/interface/ChoseDisplay/ChoseEcho.tres")
	,GD.Load<ChoseDisplay>("res://Game/interface/ChoseDisplay/ChoseJulius.tres")};
	public override void _Ready()
	{
        Displays = new List<ChoseDisplay> { EchoDisplay, JuliusDisplay }.ToList();
		nowDisplay = Displays[0];
		illustration = GetNode<TextureRect>("illustration");
		alreadyChose1 = GetNode<Label>("alreadyChose1");
		alreadyChose2 = GetNode<Label>("alreadyChose2");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		illustration.Texture = nowDisplay.illustration;
	}

	public void choseThis(){
		if(alreadyChose.Count < 2){
			alreadyChose.Add(nowDisplay);
			try{
				alreadyChose1.Text =  alreadyChose[0].charaterName;
		        alreadyChose2.Text =  alreadyChose[1].charaterName;
			}
			catch{}
		}
	}

	public void rightArrow(){
		nowDisplay = Displays[Math.Clamp((Displays.IndexOf(nowDisplay) + 1),0,Displays.Count - 1)];
	}
	public void leftArrow(){
		nowDisplay = Displays[Math.Clamp((Displays.IndexOf(nowDisplay) - 1),0,Displays.Count - 1)];
	}

	public async void confirm(){
		CreateTween().TweenProperty(this,"modulate",new Color(1,1,1,0),1f);
		// for(int i = 0;i< 2;i++){
		// 	DetailBook.Charater1CardsPile.Add(EchoSkillCollection.attack);
        //     DetailBook.Charater1CardsPile.Add(EchoSkillCollection.block);
        //     DetailBook.Charater2CardsPile.Add(JuliusSkillCollection.attack);
        //     DetailBook.Charater2CardsPile.Add(JuliusSkillCollection.block);
		// }

        // {
        //     DetailBook.Charater1CardsPile.Add(EchoSkillCollection.windy);
        //     DetailBook.Charater1CardsPile.Add(EchoSkillCollection.breathe);
        // }
	   await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
	   GetTree().ChangeSceneToFile("res://Game/battle/Game.tscn");
	}
}
