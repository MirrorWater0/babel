using Godot;

public partial class Skill : Resource
{
    [Export]
    public string name { set; get; }
    [Export]
    public string charaterName{set;get;}
    [Export]
    public int cost { set; get; }
    [Export]
    public Texture2D cardIcon { set; get; }
    [Export]
    public int releaseArea { set; get; }
    [Export]
    public bool once{set;get;}
    [Export]
    public PackedScene EffectScene{set;get;}
    [Export(PropertyHint.MultilineText)]
    public string description{set;get;} = "";
}
