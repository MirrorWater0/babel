using Godot;
using System;

public partial class Echo : PlayerState
{
    int passivityCount = 1;
    Cards cards;
	public override void _Ready()
	{
        name = "Echo";
        base._Ready();
        
        cards = GetTree().Root.GetNode<Cards>("/root/game/cards");

    }

    public override void _Process(double delta)
    {
        var count = Cards.OneTurnPlayCardRecord.Count;
        if(count > 1 ){
            if(Cards.OneTurnPlayCardRecord[Math.Clamp(count -1,0,999)] == Cards.OneTurnPlayCardRecord[Math.Clamp(count -2,0,999)] & passivityCount > 0
            & Cards.OneTurnPlayCardRecord[Math.Clamp(count -1,0,999)].charaterName != "Echo" ){
            passivity(Cards.OneTurnPlayCardRecord[count -1]);
            passivityCount--;
            }
        }
        else if(passivityCount == 0){
            
        }
    }

    public override async void passivity(Skill skill)
    {
        var TrigggerEffect = GD.Load<PackedScene>("res://effect/passitity/passitityTrigger.tscn").Instantiate<PassitityTrigger>();
        game.AddChild(TrigggerEffect);
        TrigggerEffect.GlobalPosition = GlobalPosition;
        var cardNode = GD.Load<PackedScene>("res://card/Card_ui.tscn").Instantiate<Card_ui>();
        cardNode.CardName = skill;
        
        game.AddChild(cardNode);
        CreateTween().TweenProperty(cardNode, "global_position", Cards.releasePstionRecord[Cards.OneTurnPlayCardRecord.Count - 1], 0.3f);
        await ToSignal(GetTree().CreateTimer(0.6f), SceneTreeTimer.SignalName.Timeout);
        
        cardNode.cardEffect();
        cardNode.animate.Play("exhust");

        await ToSignal(cardNode.animate, AnimationPlayer.SignalName.AnimationFinished);
        cardNode.QueueFree();
    }

    public override void turnStart()
    {
        Cards.OneTurnPlayCardRecord.Clear();
        Cards.releasePstionRecord.Clear();
        passivityCount = 1;
    }
}
