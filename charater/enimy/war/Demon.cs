using System;
using System.Collections.Generic;
using Godot;

public partial class Demon : EnemyState
{
    PackedScene beamScene = GD.Load<PackedScene>("res://effect/Demon/beam1.tscn");
    Cards cards;
    public override async void _Ready()
    {
        MaxLife = 60;
        base._Ready();
        ActionAnimation.Play("attack");
        attackLabel.Text = (15*power).ToString();
        Random random = new Random();
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
        target.Add(targetlist[random.Next(targetlist.Count)]);

    }

    public override async void action1(charaterState thistarget){
        cameraFocus(thistarget.GlobalPosition,0.4f);
        GD.Print("demo action1");
       PackedScene skilleffect = GD.Load<PackedScene>("res://effect/BaseSkillEffect/Enemyattack.tscn");
       Node2D effectNode = skilleffect.Instantiate<Node2D>();
       effectNode.GlobalPosition = thistarget.GlobalPosition;
       game.AddChild(effectNode);
       thistarget.receivedamge((int)(15*power));
       await ToSignal(effectNode.GetChild<AnimationPlayer>(0), AnimationPlayer.SignalName.AnimationFinished);
       ActionAnimation.Stop();
    }

    public void action2(){
        var burn = GD.Load<PackedScene>("res://effect/Echo/burn/burn.tscn").Instantiate<Burn>();
        burn.GlobalPosition = GlobalPosition;
        game.AddChild(burn);
        changeBuff("power",(float)Math.Round(power+0.6f,2));
        block += 20;
        updatablock();
        attackLabel.Text = (15*power).ToString();
    }

    public void action3(List<charaterState> thistarget){
        var beam = beamScene.Instantiate<Node2D>();
        beam.GlobalPosition = GlobalPosition;
        game.AddChild(beam);
        for(int i = 0; i < target.Count; i++){
            thistarget[i].receivedamge((int)(15*power));
        }
    }

    public override void TheTurnAction()
    {
        if(target != null){
            if(Game.Turn > 0 & Game.Turn <= 1){
                action1(target[0]);
                target.Clear();
                target.Add(this);
                attackLabel.Visible = false;
                ActionAnimation.Play("intensify");
            }
            else if(Game.Turn > 1 & Game.Turn <= 2){
                action2();
                ActionAnimation.Play("attack");
                target = targetlist;
                
            }
            else{
                action3(target);
                ActionAnimation.Play("attack");
                Random random = new Random();
                changeTarget("");
            }
        }
        else{
            changeTarget("");
        }
    }

}
