shader_type canvas_item;

uniform bool hit = false;
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 uv = UV;
	if(hit){
		vec4 tex_color = texture(TEXTURE,uv);
		COLOR = vec4(1.0,1,1.0,tex_color.q);
	}
}
