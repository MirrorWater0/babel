using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;


public partial class Cards : CanvasLayer
{
    Game game;
    charaterState charater;

    bool release = false;
    PackedScene cardScene;
    
    [Export]
    public  GridContainer drawCardpileLayer;
    [Export]
    public  GridContainer discardPileLayer;
    
    TextureRect blackmask;

    static public List<Skill> OneTurnPlayCardRecord = new List<Skill>();
    static public List<Vector2> releasePstionRecord = new List<Vector2>();

    public List<Skill> drawpile = new List<Skill>();
    public List<Skill> discardpile = new List<Skill>();

    ShineButton drawpileButton;
    ShineButton discardpileButton;

    Node2D player;
    
    // Called when the node enters the scene tree for the first time.
    public override async void _Ready()
    {   

        drawpile = DetailBook.Charater1CardsPile.Concat(DetailBook.Charater2CardsPile).ToList();

        ListRandom(drawpile);
        for (int i = 0; i < drawpile.Count; i++){
            pileLayerAdd(drawCardpileLayer, drawpile[i]);
        }
        drawpileButton = GetNode<ShineButton>("/root/game/UI/drawpileButton");
        discardpileButton = GetNode<ShineButton>("/root/game/UI/discardpileButton");

        

        drawpileButton.Connect(ShineButton.SignalName.Pressed,Callable.From(changeVisiblity));
        discardpileButton.Connect(ShineButton.SignalName.Pressed,Callable.From(changeVisiblity));
        
        game = GetTree().Root.GetNode<Game>("game");
        
        await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
        player = GetNode<Node2D>("/root/game/player");
        player.GetChild<charaterState>(0).Connect(charaterState.SignalName._dying,Callable.From<string>(exhust));
        player.GetChild<charaterState>(1).Connect(charaterState.SignalName._dying,Callable.From<string>(exhust));
    }

    public override void _Process(double delta)
    {

        if (Input.IsActionJustPressed("ui_down"))
        {
            draw(1);
        }
    }

    public async void draw(int count)
    {
        if(drawpile.Count < count){
           shuffle();
        }
        
        await ToSignal(GetTree().CreateTimer(0.1f),SceneTreeTimer.SignalName.Timeout);
        for (int i = 0; i < count; i++)
        {

            if(GetChildCount() < 9 & drawpile.Count != 0)
            {
                
                cardScene = GD.Load<PackedScene>("res://card/Card_ui.tscn");
                Card_ui cardNode = cardScene.Instantiate<Card_ui>();
                Skill data = drawpile[0];

                drawCardpileLayer.GetChild<Card_ui>(0).QueueFree();
                drawCardpileLayer.RemoveChild(drawCardpileLayer.GetChild<Card_ui>(0));
                
                drawpile.RemoveAt(0);
                
                cardNode.CardName = data;
                AddChild(cardNode);
                cardNode.AppearAudio.Play();
                
                cardNode.animate.Play("appear");
                await ToSignal(GetTree().CreateTimer(0.1f),SceneTreeTimer.SignalName.Timeout);
                updataSort();
            }
            else
            {
                GD.Print("full");
            }
            
        }
        await ToSignal(GetTree().CreateTimer(0.3f),SceneTreeTimer.SignalName.Timeout);
        updataSort();
    }

    public void updataSort()
    {
         for (int i = 0; i < GetChildCount(); i++)
            {
                float gap = Math.Clamp(1800 / (GetChildCount() + 1),0,250);
                Vector2 target = (i + Math.Clamp(9/ GetChildCount(),1,5)) * gap * Vector2.Right + new Vector2(-40* 9/ GetChildCount(), 820);
                Tween sortmove = GetChild(i).CreateTween();
                sortmove.TweenProperty(GetChild<Card_ui>(i), "global_position", target, 0.2f);
            }
    }

    public void discard(Skill skill){
        discardpile.Add(skill);
        pileLayerAdd(discardPileLayer, skill);
        GD.Print("discard");
    }

    public void changeVisiblity(){
        if(Visible == true){
            Visible = false;
        }
        else{
            Visible = true;
        }
    }

    public async void pileLayerAdd(GridContainer pileLayer,Skill skill){
        cardScene = GD.Load<PackedScene>("res://card/Card_ui.tscn");
        var cardNode = cardScene.Instantiate<Card_ui>();
        cardNode.CardName = skill;
        
        pileLayer.AddChild(cardNode);
        await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);

        cardNode.SetProcess(false);
        cardNode.detector.QueueFree();
        cardNode.detector1.SetCollisionLayerValue(3,false);
        cardNode.detector1.SetCollisionLayerValue(10,true);
        game.Disconnect(Game.SignalName._yourturn,Callable.From(cardNode.Start));
        
    }
    
    public void shuffle(){
        drawpile = drawpile.Concat(discardpile).ToList();
        
        GD.Print("drawpile cards",drawpile.Count);

        for(int i = 0; i < discardPileLayer.GetChildCount(); i++){
            pileLayerAdd(drawCardpileLayer, discardPileLayer.GetChild<Card_ui>(i).CardName);
            discardPileLayer.GetChild<Card_ui>(i).QueueFree();
            GD.Print("ok");
        }
        
        discardpile.Clear();
    }

    public void exhust(string name){
        var type = Type.GetType(name+"SkillCollection");
        var fields = type.GetFields().Select(x => x.GetValue(null)).ToHashSet();
        drawpile = drawpile.Where(x => !fields.Contains(x)).ToList();
        discardpile = discardpile.Where(x => !fields.Contains(x)).ToList();
    }

    public static void ListRandom<T>(List<T> sources)
    {
        Random rd = new Random();
        int index = 0;
        T temp;
        for (int i = 0; i < sources.Count; i++)
        {
            index = rd.Next(0, sources.Count - 1);
            if (index != i)
            {
                temp = sources[i];
                sources[i] = sources[index];
                sources[index] = temp;
            }
        }
    }

}
