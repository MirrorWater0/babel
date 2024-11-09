using Godot;
using System;

public partial class CardTexture : TextureRect
{
	[Export]
	Area2D detector;
	Skill CardName;
	
	TextureRect cardIcon;
	
	public override void _Ready()
	{
		
		CardName = GetParent<Card_ui>().CardName;
		detector.Connect(Area2D.SignalName.AreaEntered,Callable.From<Area2D>(AreaEntered));
		detector.Connect(Area2D.SignalName.AreaExited,Callable.From<Area2D>(AreaExited));
		cardColor();
		cardIcon = GetParent<Card_ui>().GetNode<TextureRect>("cardTexture/cardIcon");
		cardIcon.Texture = GetParent<Card_ui>().CardName.cardIcon;
	}

    public override void _Process(double delta)
    {
    }
    public void AreaEntered(Area2D area)
	{
		
		if(detector.GetOverlappingAreas().Count == CardName.releaseArea +1){
           ((ShaderMaterial)Material).SetShaderParameter("interact",true);
		}
		
	}
	public void AreaExited(Area2D area)
	{
		if(detector.GetOverlappingAreas().Count != CardName.releaseArea +1){
           ((ShaderMaterial)Material).SetShaderParameter("interact",false);
		}
	}

	public void cardColor(){
		if( CardName.charaterName == "Echo" ){
			Texture = GD.Load<Texture2D>("res://asset/card/card1.svg");
		}
		else{
			Texture = GD.Load<Texture2D>("res://asset/card/card2.svg");
		}
	}
}
