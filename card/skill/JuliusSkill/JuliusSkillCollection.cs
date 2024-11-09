using Godot;
using System;

public partial class JuliusSkillCollection : Node
{
	static public Skill attack = GD.Load<Skill>("res://card/skill/JuliusSkill/attack.tres");
	static public Skill block = GD.Load<Skill>("res://card/skill/JuliusSkill/block.tres");
	public static Skill pierce = GD.Load<Skill>("res://card/skill/JuliusSkill/pierce.tres");
	public static Skill strategy = GD.Load<Skill>("res://card/skill/JuliusSkill/strategy.tres");
	public static Skill terminal = GD.Load<Skill>("res://card/skill/JuliusSkill/terminal.tres");

	public static Skill[] skills = new Skill[] { attack, block , pierce ,strategy,terminal };
}
