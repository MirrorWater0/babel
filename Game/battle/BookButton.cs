using Godot;
using System;

public partial class BookButton : ShineButton
{
	[Export]
	TextureRect bookDisplay;
	DetailBook detailBook = GD.Load<PackedScene>("res://ui_script/detailBook/DetailBook.tscn").Instantiate<DetailBook>();
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

    public override void pressed()
    {
		intereact.Play();
        base.pressed();
		if(bookDisplay.GetChildCount() == 0){
			bookDisplay.AddChild(detailBook);
			ZIndex = 2;
		}
		else{
			bookDisplay.RemoveChild(detailBook);
			ZIndex = 0;
		}
		GD.Print("pressed");	
    }



}
