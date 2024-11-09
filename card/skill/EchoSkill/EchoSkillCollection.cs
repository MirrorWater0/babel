using Godot;
using System;

public partial class EchoSkillCollection : Node
{
	public static Skill attack = GD.Load<Skill>("res://card/skill/EchoSkill/attack.tres");
	public static Skill block = GD.Load<Skill>("res://card/skill/EchoSkill/block.tres");
	public static Skill windy = GD.Load<Skill>("res://card/skill/EchoSkill/windy.tres");
	public static Skill burn = GD.Load<Skill>("res://card/skill/EchoSkill/burn.tres");
	public static Skill breathe = GD.Load<Skill>("res://card/skill/EchoSkill/breathe.tres");
	public static Skill DanceOfSword = GD.Load<Skill>("res://card/skill/EchoSkill/DanceOfSword.tres");
	public static Skill flash = GD.Load<Skill>("res://card/skill/EchoSkill/flash.tres");
	public static Skill echoing = GD.Load<Skill>("res://card/skill/EchoSkill/echoing.tres");

	public static Skill[] skills = new Skill[] {attack,block,windy,burn,breathe,DanceOfSword,flash,echoing};
	
}
