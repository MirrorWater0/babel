using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;

public partial class Event : Node2D
{
	public static EventData eventData = GD.Load<EventData>("res://Game/Event/EventData/BeginEvent/BeginEvent1.tres");
	Label Text;
	VBoxContainer chose;
	ChoseButton Chose1;
	ChoseButton Chose2;
	ChoseButton Chose3;
	Control giveControl;
	HBoxContainer give;
	TextureButton give1;
	TextureButton give2;
	List<string> page = new List<string>();
	Button ContinueButton;
	Godot.Collections.Dictionary<string, float> change;
	PackedScene next;
	AudioStreamPlayer bgm;
	public override void _Ready()
	{
		bgm = GetNode<AudioStreamPlayer>("AudioManager/bgm");
		bgm.VolumeDb = -20;
		bgm.Play();
		CreateTween().TweenProperty(bgm,"volume_db",-5f,1f);
		for (int i = 0; i < eventData.text.PageCount; i++)
		{
			GD.Print((string)eventData.text.Get("text"+i.ToString()));
			var text = (string)eventData.text.Get("text"+i.ToString());
			page.Add(text);
		}
	
		Text = GetNode<Label>("Text");
		chose = GetNode<VBoxContainer>("chose");
		giveControl = GetNode<Control>("giveControl");
		giveControl.Visible = false;
		give = GetNode<HBoxContainer>("giveControl/give");
		give1 = give.GetChild<dynamicButton>(0);
		give2 = give.GetChild<dynamicButton>(1);
		give1.TextureNormal = CharaterChose.alreadyChose[0].charaterIcon;
		give2.TextureNormal = CharaterChose.alreadyChose[1].charaterIcon;
		ContinueButton = GetNode<Button>("continue");
		ContinueButton.Disabled = true;
		ContinueButton.Visible = false;

		for (int i = 0; i < 1; i++)
		{
			give.GetChild<BaseButton>(i).Disabled = true;
		}
		chose.Visible = false;
		for (int i = 0; i < 2; i++)
		{
			chose.GetChild<BaseButton>(i).Disabled = true;
		}
		Text.Text = page[0];
		
		Chose1 = GetNode<ChoseButton>("chose/chose1");
		Chose2 = GetNode<ChoseButton>("chose/chose2");
		Chose3 = GetNode<ChoseButton>("chose/chose3");

		for(int i = 1;i <= 3;i++){
			switch(i){
				case 1:
				var dic1 = eventData.changeBuff1;
				var cost1 = (int)eventData.Get("SoulDebrisCost" + (i + 1).ToString());
				Chose1.text = "Power:" +"\n" + dic1["Power"]+ "\n" +"Rigidity:" +"\n" + dic1["Rigidity"] + "\n"+ "SoulDebrisCost:" + cost1.ToString() ;
				Chose1.Text = eventData.ButtonText1;
				break;
				case 2:
				var dic2 = eventData.changeBuff2;
				var cost2 = (int)eventData.Get("SoulDebrisCost" + (i + 1).ToString());
				Chose2.text = "Power:" +"\n" + dic2["Power"] + "\n" +"Rigidity:" +"\n" + dic2["Rigidity"]+ "\n"+ "SoulDebrisCost:" + cost2.ToString() ;
				Chose2.Text = eventData.ButtonText2;
				break;
				case 3:
				var dic3 = eventData.changeBuff3;
				var cost3 = (int)eventData.Get("SoulDebrisCost" + (i + 1).ToString());
				Chose3.text = "Power:" +"\n" + dic3["Power"] +"\n" +"Rigidity:" +"\n" + dic3["Rigidity"]+ "\n"+ "SoulDebrisCost:" + cost3.ToString() ;
				Chose3.Text = eventData.ButtonText3;
				break;
			}
		}
	}


	public void NextPage(){
		Text.Text = page[Math.Clamp(page.IndexOf(Text.Text) + 1,0,page.Count - 1)];
		if(Text.Text == page[page.Count - 1]){
			chose.Visible = true;
			for (int i = 0; i < 2; i++){
				var cost = (int)eventData.Get("SoulDebrisCost" + (i + 1).ToString());
				if(SoulDebris.count >= cost){
					chose.GetChild<BaseButton>(i).Disabled = false;
				}
			}
		}
	}

	public void PreviousPage(){
		Text.Text = page[Math.Clamp(page.IndexOf(Text.Text) - 1,0,page.Count - 1)];
		chose.Visible = false;
	}

	public void chose1(){
		giveControl.Visible = true;
		change = eventData.changeBuff1;
		give.Visible = true;
		for (int i = 0; i < 1; i++){
			give.GetChild<BaseButton>(i).Disabled = false;
		}
		Game.enemylist = eventData.next1.enemylist;
		next = eventData.next1.NextScene;
		SoulDebris.ChangeDebrisCount(-eventData.SoulDebrisCost1);
	}

	public void chose2(){
		giveControl.Visible = true;
		change = eventData.changeBuff2;
		give.Visible = true;
		for (int i = 0; i < 1; i++){
			give.GetChild<BaseButton>(i).Disabled = false;
		}
		Game.enemylist = eventData.next2.enemylist;
		next = eventData.next2.NextScene;
		SoulDebris.ChangeDebrisCount(-eventData.SoulDebrisCost2);
	}

	public void chose3(){
		giveControl.Visible = true;
		change = eventData.changeBuff3;
		give.Visible = true;
		for (int i = 0; i < 1; i++){
			give.GetChild<BaseButton>(i).Disabled = false;
		}
		Game.enemylist = eventData.next3.enemylist;
		next = eventData.next3.NextScene;
		SoulDebris.ChangeDebrisCount(-eventData.SoulDebrisCost3);
	}

	public void Give1(){
		foreach(KeyValuePair<string, float> item in change){
			DetailBook.ChangeCharaterProperties(1,item.Key, item.Value);
		}
		for (int i = 0; i < 1; i++)
		{
			give.GetChild<BaseButton>(i).Disabled = true;
			giveControl.Visible = false;
		}
		chose.QueueFree();
		ContinueButton.Visible = true;
		ContinueButton.Disabled = false;
		next = eventData.next1.NextScene;
	}

	public void Give2(){
		foreach(KeyValuePair<string, float> item in change){
			DetailBook.ChangeCharaterProperties(2,item.Key, item.Value);
		}
		for (int i = 0; i < 1; i++)
		{
			give.GetChild<BaseButton>(i).Disabled = true;
			giveControl.Visible = false;
		}
		chose.QueueFree();
		ContinueButton.Visible = true;
		ContinueButton.Disabled = false;
	}

	public void continueNext(){
		GetTree().ChangeSceneToPacked(next);
	}
}
