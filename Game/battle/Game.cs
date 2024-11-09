using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Godot;

public partial class Game : Node2D
{
    [Signal]
    public delegate void _yourturnEventHandler();
    [Export]
    Cards cards;
    [Export]
    Node2D charaters;
    [Export]
    public Enemies enemies;
    [Export]
    TextureRect crystal;
    Light2D crystalLight;
    [Export]
    AnimationPlayer gameAnimate;
    
    static public int Turn;
    static public int battleCount;
    GradientTexture2D tex;
    Tween LightTween;
    
    static public Godot.Collections.Array<PackedScene> enemylist = new Godot.Collections.Array<PackedScene>(){
        GD.Load<PackedScene>("res://charater/enimy/war/Demon.tscn"),
    };
    static public int energe;
    
    public static PackedScene charater1 = CharaterChose.alreadyChose[0].charaterScene;
    public static PackedScene charater2 = CharaterChose.alreadyChose[1].charaterScene;
    private List<charaterState> charaterlist = new List<charaterState>();
    public List<charaterState> Charaterlist{get => charaterlist;}
    Label turnLabel;
    public override async void _Ready()
    {
        GD.Print(enemylist);
        initializeCharater();
        battleCount++;
        Turn = 0;
        turnLabel = GetNode<Label>("UI0/TopColumn/turnLabel");
        turnLabel.Text = "TURN :"+ Turn.ToString();

        //instantiate charaters of player
        

    
        await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        
       //initialize energe
        energe = DetailBook.Energe;
        crystal.GetChild<Label>(0).Text = energe.ToString();
        crystalLight = crystal.GetChild<Light2D>(1);
        
        GD.Print("game ready");
        yourturnStart();
    }
    
    public void initializeCharater(){
        charaterlist = new List<charaterState>(){charater1.Instantiate<charaterState>(),
                                                 charater2.Instantiate<charaterState>()}.ToList();

        charaterlist[0].GlobalPosition = new Vector2(280, 520);
        charaterlist[1].GlobalPosition = new Vector2(500, 520);

        charaterlist[0].power = DetailBook.Charater1Power;
        charaterlist[1].power = DetailBook.Charater2Power;
        charaterlist[0].rigidity = DetailBook.Charater1Rigidity;
        charaterlist[1].rigidity = DetailBook.Charater2Rigidity;

        charaters.AddChild(charaterlist[0]);
        charaters.AddChild(charaterlist[1]);
        
    }
    
    public async void yourturnStart(){
        Cards.OneTurnPlayCardRecord.Clear();
        Cards.releasePstionRecord.Clear();
        Game.Turn ++;
        turnLabel.Text = "TURN :"+ Turn.ToString();
        for (int i = 0; i < charaters.GetChildCount(); i++)
        {
            charaters.GetChild<charaterState>(i).block = 0;
            charaters.GetChild<charaterState>(i).updatablock();

        }
        gameAnimate.Play("yourturn");
        await ToSignal(gameAnimate, AnimationPlayer.SignalName.AnimationFinished);

        //restore energe
        energe = DetailBook.Energe;
        crystal.GetChild<Label>(0).Text = energe.ToString();
        restoreEffect();
        cards.draw(4);
    }

    public void updataEnerge(int value){
        energe += value;
        //change energe
        crystal.GetChild<Label>(0).Text = energe.ToString();
        restoreEffect();  
    }
    
    private async void Endpress()
    {
        for (int i = 0; i < enemies.GetChildCount(); i++)
        {

            EnemyState thisEnemy = enemies.GetChild<EnemyState>(i);
            thisEnemy.enimyStart();
            thisEnemy.TheTurnAction();
            
            await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
        }


        EmitSignal(SignalName._yourturn);
        
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
        yourturnStart();
    }

    
    public void restoreEffect(){
        LightTween = CreateTween();
        float percentage = Math.Clamp((float)energe/DetailBook.Energe,0f,1f);
        GD.Print(percentage);
        Vector2 destinate = new Vector2(3f, 3f)*percentage;
        LightTween.TweenProperty(crystal.GetChild(1), "scale", destinate, 0.4f);
    }

}
