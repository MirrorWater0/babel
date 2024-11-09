shader_type canvas_item;

uniform int init_life;
uniform int life;
void fragment() {
	vec2 uv = UV;
	float mask = step(uv.x,float(life) / float(init_life));
	COLOR = COLOR*mask;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
