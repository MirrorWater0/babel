using Godot;
using System;

public partial class Guardian : EnemyState
{
	// Called when the node enters the scene tree for the first time.
	public async override void _Ready()
	{
        MaxLife = 60;
        base._Ready();
        ActionAnimation.Play("attack");
        attackLabel.Text = (15*power).ToString();
        Random random = new Random();
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
        target.Add(targetlist[random.Next(targetlist.Count)]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override async void action1(charaterState thistarget){
    	cameraFocus(thistarget.GlobalPosition,0.4f);
        GD.Print("demo action1");
       PackedScene skilleffect = GD.Load<PackedScene>("res://effect/BaseSkillEffect/Enemyattack.tscn");
       Node2D effectNode = skilleffect.Instantiate<Node2D>();
       effectNode.GlobalPosition = thistarget.GlobalPosition;
       game.AddChild(effectNode);
       thistarget.receivedamge((int)(20*power));
       await ToSignal(effectNode.GetChild<AnimationPlayer>(0), AnimationPlayer.SignalName.AnimationFinished);
       ActionAnimation.Stop();
    }

    public override void action2(charaterState thistarget){
        Block defense = GD.Load<PackedScene>("res://effect/BaseSkillEffect/block/block.tscn").Instantiate<Block>();
        game.AddChild(defense);
        defense.GlobalPosition = thistarget.GlobalPosition;

        thistarget.receiveblock((int)(15*rigidity));
        changeBuff("rigidity",(float)Math.Round(rigidity+0.2,2));
    }

    public override void TheTurnAction()
    {
        if(target == null){
            changeTarget("");
        }
        if(Game.Turn % 2 == 0){
            for(int i=0;i < game.enemies.GetChildCount();i++){
                action2(game.enemies.GetChild<EnemyState>(i));
                attackLabel.Text = (15*power).ToString();
                ActionAnimation.Play("attack");
            }
        }
        else{
            action1(targetlist[0]);
            ActionAnimation.Play("intensify");
        }
    }
}
