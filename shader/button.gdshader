shader_type canvas_item;

uniform bool pressed;
uniform bool Hover;
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 uv = UV;
	vec4 tex_color = texture(TEXTURE,uv);


	if(Hover){
		COLOR = tex_color + vec4(0.0,1.0,0.5,tex_color.a* 0.3) *(1.0 -  step(tex_color.b,0.8));
	}
	if(pressed){
		COLOR = (tex_color - vec4(0.6,0.6,0.5,0.0) )* (1.0 -  step(tex_color.b,0.8)) + tex_color * step(tex_color.b,0.8);
	}
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
