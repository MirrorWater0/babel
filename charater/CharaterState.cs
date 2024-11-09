using System;
using System.Reflection.Metadata.Ecma335;
using Godot;

public partial class charaterState : Sprite2D
{
    [Signal]
    public delegate charaterState _dyingEventHandler(string name);
    public string name;
    public Label lifeLabel;
    private Label BlockLabel;
    public Label blockLabel{
        get{return BlockLabel ?? GetNode<Label>("block");}
    }
    public TextureProgressBar lifeline;

    //property
    public float power{get;set;} = 1;
    public Label powerLabel;
    public float rigidity{get;set;} = 1;
    public Label rigidityLabel;
    public float speed{get;set;} = 1;
    public Label speedLabel;
    
    [Export]
    public int MaxLife;
    public int life;
    [Export]
    public int block;
    
    public Game game;

    GpuParticles2D hoverParticle;
    GpuParticles2D hitParticle;
    public TextureRect hoverTexture;
    public Area2D detector;
    
    AnimationPlayer animation;
    PackedScene paticleScene0 = GD.Load<PackedScene>("res://effect/charaterEffect/HitPartiacle.tscn");
    AudioStreamPlayer HitBlockAudio;
    AudioStreamPlayer BlockBreakAudio;
    AudioStreamPlayer HitAudio;
    
    public override void _Ready()
	{
        
        powerLabel = GetNode<Label>("buff/power");
        powerLabel.Text = power.ToString();
        rigidityLabel = GetNode<Label>("buff/rigidity");
        rigidityLabel.Text = rigidity.ToString();
        
        BlockLabel = GetNode<Label>("block");

        game = GetTree().Root.GetNode<Game>("game");

        lifeline = GetNode<TextureProgressBar>("lifeline");

        lifeLabel = lifeline.GetChild<Label>(0);

        block = 0;

        hoverParticle = GetNode<GpuParticles2D>("particle/hover");
        hoverTexture = GetNode<TextureRect>("hoverTex");
        detector = GetNode<Area2D>("detector");

        detector.Connect(Area2D.SignalName.MouseEntered,Callable.From(MouseEntered));
        detector.Connect(Area2D.SignalName.MouseExited,Callable.From(MouseExited));

        animation = GetNode<AnimationPlayer>("AnimationPlayer");

        HitBlockAudio = GetNode<AudioStreamPlayer>("AudioManager/HitBlock");
        BlockBreakAudio = GetNode<AudioStreamPlayer>("AudioManager/BlockBreak");
        HitAudio = GetNode<AudioStreamPlayer>("AudioManager/Hit");
    }

    public void receivedamge(int damage)
    {
        bool hasblock = false;
        if(block > 0){
            HitBlockAudio.Play();
            hasblock = true;
        }
        else{
            HitAudio.Play();
        }
        life = life - Math.Clamp(damage - block,0,999);
        block = Math.Clamp(block - damage,0,999);
        if(hasblock){
            updatablock();
            if(block == 0){
                BlockBreakAudio.Play();
                HitAudio.Play();
            }
        }
        updatalife();
        GD.Print("receivedamge");
        
        DamgeLabel damgeLabel = GD.Load<PackedScene>("res://ui_script/damgeLabel.tscn").Instantiate<DamgeLabel>();
        damgeLabel.Text = damage.ToString();
        AddChild(damgeLabel);
        
        
        hitParticle = paticleScene0.Instantiate<GpuParticles2D>();
        hitParticle.Emitting = true;
        AddChild(hitParticle);

        animation.Play("hit");
        if(life <= 0){
            dying();
        }
    }


    public void updatalife(){
        CreateTween().TweenProperty(lifeline,"value",life,0.5f);
        lifeLabel.Text = life.ToString();
    }

        public void receiveblock(int Block)
    {
        block += Block;
        updatablock();
    }
    public void updatablock(){
        blockLabel.Text = block.ToString();
        var tween1 = CreateTween();
        tween1.TweenProperty(blockLabel,"scale",new Vector2(1.5f,1.5f),0.1f);
        tween1.TweenProperty(blockLabel,"scale",new Vector2(1f,1f),0.3f);
        var tween2 = CreateTween();
        tween2.TweenProperty(blockLabel,"modulate",new Color(10,10,10,1),0.1f);
        tween2.TweenProperty(blockLabel,"modulate",new Color(1,1,1,1),0.3f);

        if(block > 0){
            GetNode<TextureProgressBar>("lifeline").Material.Set("shader_parameter/hasblock",true);
        }
        else{
            GetNode<TextureProgressBar>("lifeline").Material.Set("shader_parameter/hasblock",false);
        }
    }
    

    public virtual void MouseEntered(){
        hoverParticle.Emitting = true;
        hoverTexture.Scale = new Vector2(1.3f,1.3f);
        hoverTexture.Visible = true;
        CreateTween().TweenProperty(hoverTexture,"scale",new Vector2(1f,1f),0.1f);
    }

    public virtual void MouseExited(){
        hoverParticle.Emitting = false;
        hoverTexture.Visible = false;
    }

    public void changeBuff(string type, float value){
        Set(type,value);
        var label = GetNode<Label>("buff/"+type);
        label.Text = Math.Round((float)Get(type),2).ToString();
        var tween1 = CreateTween();
        tween1.TweenProperty(label,"scale",new Vector2(1.5f,1.5f),0.1f);
        tween1.TweenProperty(label,"scale",new Vector2(1f,1f),0.3f);
        var tween2 = CreateTween();
        tween2.TweenProperty(label,"modulate",new Color(10,10,10,1),0.1f);
        tween2.TweenProperty(label,"modulate",new Color(1,1,1,1),0.3f);
    }

    public virtual async void dying(){
        await ToSignal(GetTree().CreateTimer(0.6f), SceneTreeTimer.SignalName.Timeout);
        CreateTween().TweenProperty(this,"modulate",new Color(1,1,1,0),1f);
        CreateTween().TweenProperty(this.Material,"shader_parameter/progress",4.0f,1f);
        EmitSignal(SignalName._dying,name);
        await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        QueueFree();
    }

}
