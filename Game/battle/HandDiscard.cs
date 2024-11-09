using Godot;
using System;

public partial class HandDiscard : CanvasLayer
{
	Cards cards;
	Area2D target;
	public int count;
	Area2D detector;
	Game game;
	ShineButton button;
	Label remain;
	ColorRect blackmask;

	PackedScene paticleBall = GD.Load<PackedScene>("res://effect/PaticleBall.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		game = GetTree().Root.GetNode<Game>("game");
		detector = GetNode<Area2D>("blackmask/detector");
		cards = GetTree().Root.GetNode<Cards>("/root/game/cards");
		button = GetNode<ShineButton>("blackmask/ConfirmButton");
		blackmask = GetNode<ColorRect>("blackmask");
		button.Connect(ShineButton.SignalName.Pressed,Callable.From(confirm));
		remain = GetNode<Label>("blackmask/remain");
		
		button.Position = new Vector2(798,800);
		CreateTween().TweenProperty(button,"position",new Vector2(798,632),0.2f).SetEase(Tween.EaseType.Out);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Visible == true){
			if(Input.IsActionJustPressed("Lclick") & target != null){
				target.GetParent<Card_ui>().Reparent(this);
				target.GetParent<Card_ui>().detector.SetCollisionMaskValue(2, false);
				CreateTween().TweenProperty(target.GetParent<Card_ui>(), "global_position", new Vector2(860,250), 0.2f).SetEase(Tween.EaseType.Out);
				limitCount();
			}
		}
		remain.Text = "选择" + count.ToString() + "张卡牌丢弃";
	}

	public void HandDiscardLaunch(int Entercount){
		if(cards.GetChildCount() > 0){
			for (int i = 0; i < cards.GetChildCount(); i++){
				cards.GetChild<Card_ui>(i).detector1.Connect(Detector1.SignalName.ParameterMouseEntered,Callable.From<Area2D>(SelectTarget));
				cards.GetChild<Card_ui>(i).detector1.Connect(Detector1.SignalName.ParameterMouseExited,Callable.From<Area2D>(UnSelectTarget));
				cards.GetChild<Card_ui>(i).end();
			}
			count = Entercount;
			Visible = true;
		}
		else{
			QueueFree();
		}
	}

	public void SelectTarget(Area2D target){
		this.target = target;
	}
	public void UnSelectTarget(Area2D target){
		this.target = null;
	}

	public async void limitCount(){
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		if(detector.GetOverlappingAreas().Count > count){
			var thisCard = detector.GetOverlappingAreas()[0].GetParent<Card_ui>();
			thisCard.Reparent(cards);
			thisCard.detector.SetCollisionMaskValue(2, true);
			cards.updataSort();
		}
	}

	public async void confirm(){
		SetProcess(false);
		if(detector.GetOverlappingAreas().Count == count){
			for(int i = 0; i < detector.GetOverlappingAreas().Count; i++){
				var thisCard = detector.GetOverlappingAreas()[i].GetParent<Card_ui>();
				cards.discard(thisCard.CardName);
				thisCard.animate.Play("cardfade");
                
				var paticle = paticleBall.Instantiate<PaticleBall>();
				paticle.GlobalPosition = thisCard.GlobalPosition;
				game.AddChild(paticle);
				
				thisCard.QueueFree();
		    }
        
			for(int i = 0; i < cards.GetChildCount(); i++){
				cards.GetChild<Card_ui>(i).Start();
			}
			count = 0;
			CreateTween().TweenProperty(blackmask,"modulate",new Color(0.3f,1.5f,1.5f,0),0.3f);
			await ToSignal(GetTree().CreateTimer(0.4f), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
			
		}
		else{
			GD.Print("Please chose");
		}

	}
}
