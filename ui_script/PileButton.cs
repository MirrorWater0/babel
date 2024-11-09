using Godot;
using System;

public partial class PileButton : ShineButton
{
	
	Cards cards;
	[Export]
	ColorRect blackmask;
	ScrollContainer scrollContainer;
	public override void _Ready()
	{
		base._Ready();
		scrollContainer = blackmask.GetChild<ScrollContainer>(0);
		cards = GetNode<Cards>("/root/game/cards");		
	}

	public override async void pressed(){
		intereact.Play();
		((ShaderMaterial)Material).SetShaderParameter("pressed", true);
		if(ZIndex == 0){
			ZIndex = 2;
			blackmask.Visible = true;
			scrollContainer.Modulate = new Color(1, 1, 1, 0.3f);
			CreateTween().TweenProperty(scrollContainer, "modulate", new Color(1, 1, 1, 1), 0.1f).SetEase(Tween.EaseType.InOut);
			for(int i = 0; i < scrollContainer.GetChild<GridContainer>(0).GetChildCount(); i++){
				var thiscard = scrollContainer.GetChild<GridContainer>(0).GetChild<Card_ui>(i);
				thiscard.animate.Play("appear");
				GD.Print(thiscard.detector1.GlobalPosition);
			}			
		}
		else{
			ZIndex = 0;
			// CreateTween().TweenProperty(scrollContainer, "scale", new Vector2(0.2f, 0.2f), 0.1f).SetEase(Tween.EaseType.InOut);
			await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);
			blackmask.Visible = false;
		}
        
		
	}

	public override void released(){
		base.released();
	}
    
}
