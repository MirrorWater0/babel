using Godot;

public partial class Lifebar : ColorRect
{
    charaterState charater;
    public override void _Ready()
    {
        charater = GetParent<charaterState>();
        ((ShaderMaterial)Material).SetShaderParameter("init_life", charater.life);
    }
    public override void _Process(double delta)
    {
        ((ShaderMaterial)Material).SetShaderParameter("life", charater.life);
    }
}
