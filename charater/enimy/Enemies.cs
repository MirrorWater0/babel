using Godot;
using System;
using System.Linq;

public partial class Enemies : Node2D
{
    Node2D player;
    [Signal]
    public delegate void _alreadyEventHandler();
    SelectCard victory = GD.Load<PackedScene>("res://Game/battle/SelectCard.tscn").Instantiate<SelectCard>();
    public override void _Ready()
    {
        player = GetNode<Node2D>("/root/game/player");

        for(int i = 0;i< Game.enemylist.Count;i++){
            
            EnemyState enemy_instance = Game.enemylist[i].Instantiate<EnemyState>();
            AddChild(enemy_instance);

             enemy_instance.GlobalPosition = new Vector2(1800, 500) - new Vector2(300*i,0);
             enemy_instance.Modulate = new Color(1, 1, 1, 0.0f);
            CreateTween().TweenProperty(enemy_instance, "modulate", new Color(1, 1, 1, 1), 0.4f);
        }


        
        
        EmitSignal(SignalName._already);
        GD.Print("enemies ready");

    }

    public override void _Process(double delta)
    {
        if(GetChildCount() == 0){
            GetNode<Game>("/root/game").AddChild(victory);
            QueueFree();
        }
    }

}
