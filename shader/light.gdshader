shader_type canvas_item;

uniform sampler2D noise_tex;
void vertex() {
	vec2 uv =UV;
	vec4 noise_color= texture(noise_tex,uv);
	uv.y += TIME;
	COLOR = noise_color;
}

void fragment() {
	vec2 uv =UV;
	vec4 noise_color= texture(noise_tex,uv);
	uv.y += TIME;
	COLOR = noise_color;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
