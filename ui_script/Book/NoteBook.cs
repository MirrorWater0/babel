using Godot;
using System;
using System.Collections.Generic;

public partial class NoteBook : CanvasLayer
{
	Note note = GD.Load<Note>("res://ui_script/Book/Note0.tres");
	AnimationPlayer animation;
	Label Text;
	static List<string> page = new List<string>();
	public override void _Ready()
	{
		Text = GetNode<Label>("/root/Book/BookTexture/CanvasGroup/Text");
		animation = GetNode<AnimationPlayer>("/root/Book/AnimationPlayer");
		for(int i = 0; i < note.PageCount; i++){
			page.Add((string)note.Get("text"+i.ToString()));
		}
		Text.Text = page[0];
	}

	public void PreviousPage(){
		Text.Text = page[Math.Clamp(page.IndexOf(Text.Text) - 1,0,page.Count - 1)];
	}

	public void NextPage(){
		animation.Play("flipforward");
		Text.Text = page[Math.Clamp(page.IndexOf(Text.Text) + 1,0,page.Count - 1)];
	}
}
