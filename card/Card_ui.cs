using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class Card_ui : Control
{
    Game game;
    public PackedScene skilleffect;
    charaterState owner;
    [Signal]
    public delegate void _releaseEventHandler(PlayerState playerState, Skill skill);
	public Skill CardName;

	public Area2D detector;
    public Area2D detector1;
	
	Node UI;

	Texture2D texture;
	bool release = false;

    Label name;
    Label cost;
    
    Cards cards;
    Enemies enemiesNode;

    bool once;
	[Export]
	public AnimationPlayer animate;
    public int Chain = 0;

    PackedScene cardtipScene = GD.Load<PackedScene>("res://ui_script/suspension/Cardtip.tscn");
    private AudioStreamPlayer appearAudio;
    public AudioStreamPlayer AppearAudio{
        get { return appearAudio?? GetNode<AudioStreamPlayer>("AudioManager/appear"); }
    }
	public override void _Ready()
	{
        appearAudio = GetNode<AudioStreamPlayer>("AudioManager/appear");

        cards = (Cards)GetTree().GetFirstNodeInGroup("cards");
        game = GetTree().GetFirstNodeInGroup("Game") as Game;
        if(game != null){
            inGame();
        }
        
		detector = GetNode<Area2D>("detector");
        detector1 = GetNode<Area2D>("detector1");
		GlobalPosition =new Vector2(40,870);

        name = GetNode<Label>("cardTexture/cardname");
        name.Text = CardName.name;
        cost = GetNode<Label>("cardTexture/cost");
        cost.Text = CardName.cost.ToString();

        skilleffect = CardName.EffectScene;


        _MakeCustomTooltip("cardtip");

        once = CardName.once;
    }

    private async void inGame(){
        GD.Print("inGame");
        game.Connect(Game.SignalName._yourturn,Callable.From(Start));
        cards = GetNode<Cards>("/root/game/cards");
        UI = GetTree().Root.GetNode<CanvasLayer>("/root/game/UI");
        ShineButton button1 = GetTree().GetFirstNodeInGroup("Button") as ShineButton;
        button1.Connect(ShineButton.SignalName.ButtonUp,Callable.From(end));
        enemiesNode = GetNode<Enemies>("/root/game/Enemies");

        await ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout);
        owner = game.Charaterlist.Find(x => x.name == CardName.charaterName);
        owner.Connect(charaterState.SignalName._dying,Callable.From<string>(exhustAll));
    }

    public bool drag = false;

	public override async void _Process(double delta)
	{
        Vector2 mouseGP = GetGlobalMousePosition();

        //drag card
        if (GetGlobalRect().HasPoint(mouseGP) & !drag)
        {
            if (Input.IsActionJustPressed("Lclick"))
			{
                drag = true;
                Reparent(UI);
            }

        }
		if(drag){
            GlobalPosition = GlobalPosition.Lerp(mouseGP - new Vector2(100,135),0.5f);
            if(Input.IsActionJustPressed("Lclick")){
                if (detector.GetOverlappingAreas().Count > CardName.releaseArea & Game.energe >= CardName.cost)
                {
                    game.updataEnerge(-CardName.cost);
                    
                    cardRlease();
                    cards.updataSort();
                    SetProcess(false);
                }
                else if(Game.energe < CardName.cost){
                    drag = false;
                    await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);
                    Reparent(cards);
                    cards.updataSort();
                }
            }
        }
        
        if(Input.IsActionJustPressed("Rclick"))
        {
            drag = false;
            Reparent(cards);
            cards.updataSort();
        }
    }

    public void end(){
        SetProcess(false);
    }

    public void Start(){

        SetProcess(true);
        double time = 1;
        _Process(time);

    }


    public async void cardRlease(){ 
                
        cardEffect();
        
        //cardanimation
        Reparent(game);
        SetProcess(false);
        detector1.QueueFree();
        
        if(once){
            animate.Play("exhust");

        }
        else{
            animate.Play("cardfade");
            cards.discard(CardName);
            CreateBall(GetTree().Root.GetNode<PileButton>("/root/game/UI/discardpileButton").GlobalPosition + new Vector2(60,60));
        }
        
            
        //await animation
        await ToSignal(animate, AnimationPlayer.SignalName.AnimationFinished);
        Cards.OneTurnPlayCardRecord.Add(CardName);
        Cards.releasePstionRecord.Add(GlobalPosition);
        QueueFree();
    }

    public void CreateBall(Vector2 to){
        PaticleBall paticleBall = GD.Load<PackedScene>("res://effect/PaticleBall.tscn").Instantiate<PaticleBall>();
        paticleBall.GlobalPosition = GlobalPosition + GetRect().Size / 2;
        paticleBall.to = to;
        game.AddChild(paticleBall);
    }

    public async void exhustAll(string name){
        if(name == CardName.charaterName){
            animate.Play("exhust");
            await ToSignal(animate, AnimationPlayer.SignalName.AnimationFinished);
            SetProcess(false);
            QueueFree();
        }
    }

    public void cardEffect(){
        Godot.Collections.Array<Area2D> area = detector.GetOverlappingAreas();
        
        
        //skillanimation
        EffectBase effect_instance = skilleffect.Instantiate<EffectBase>();
        game.AddChild(effect_instance);
        if(CardName.releaseArea == 0){
            effect_instance.effect(owner);
        }
        else{
            charaterState target = area[1].GetParent<charaterState>();
            effect_instance.effect(target,owner);
        }
        
    }

    public override GodotObject _MakeCustomTooltip(string forText)
    {
        var cardtip = cardtipScene.Instantiate<Cardtip>();
        cardtip.description.Text = CardName.description;
        return cardtip;
    }

}
