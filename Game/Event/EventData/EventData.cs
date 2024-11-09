using Godot;
using System;
using System.Collections.Generic;

public partial class EventData : Resource
{
	[Export]
	public EventText text;

	[ExportGroup("Chose1")]
	[Export]
	public string ButtonText1;
	[Export]
	public Godot.Collections.Dictionary<string, float> changeBuff1 = new Godot.Collections.Dictionary<string, float>(){
		{"Power",1},
		{"Rigidity",1},
	};
	[Export]
	public Next next1;
	[Export]
	public int SoulDebrisCost1;
	
	[ExportGroup("Chose2")]
	[Export]
	public string ButtonText2;
	[Export]
	public Godot.Collections.Dictionary<string, float> changeBuff2 = new Godot.Collections.Dictionary<string, float>(){
		{"Power",1},
		{"Rigidity",1},
	};
	[Export]
	public Next next2;
	[Export]
	public int SoulDebrisCost2;

	[ExportGroup("Chose3")]
	[Export]
	public string ButtonText3;
	[Export]
	public Godot.Collections.Dictionary<string, float> changeBuff3= new Godot.Collections.Dictionary<string, float>(){
		{"Power",1},
		{"Rigidity",1},
	};

	[Export]
	public Next next3;
	[Export]
	public int SoulDebrisCost3;
}
