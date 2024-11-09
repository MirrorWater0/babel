using Godot;
using System;
using System.Threading;

public partial class SoulDebris : TextureRect
{
	public static int count = 10;
	static Label countLabel;
	public override void _Ready()
	{
		countLabel = GetNode<Label>("count");
		countLabel.Text = count.ToString();
	}

	public override void _Process(double delta)
	{
	}

	static public void ChangeDebrisCount(int num){
		count += num;
		countLabel.Text = count.ToString();
	}
}
