shader_type canvas_item;

uniform bool interact;
uniform float offset : hint_range(-1.0, 4.0, 0.1);
uniform float time : hint_range(0.0, 1.0, 0.1);
uniform sampler2D noise_tex;
uniform sampler2D noise_edge_tex;
uniform float burn_process : hint_range(0.0, 1.0, 0.1);
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 uv = UV;
	
	vec4 tex_color = texture(TEXTURE,uv);
	COLOR.a = COLOR.a*clamp((1.0-uv.g)*10.0 - offset,0.0,1.0) 
	* clamp((uv.g)*10.0 - offset,0.0,1.0) *
	 clamp((1.0-uv.r)*10.0 - offset,0.0,1.0)
	 * clamp((uv.r)*10.0 - offset,0.0,1.0);
	
	if(interact){
		COLOR = COLOR + vec4(0.3,0.3,0.3,0.0);
	}
	
	vec2 uv2 = UV;
	vec4 noise_color = texture(noise_tex,uv);
	vec4 noise_edge_color = texture(noise_edge_tex,uv2);
	float nowAlpha = COLOR.a;
	vec4 melt = step(noise_color.r,burn_process)*COLOR;
	
	vec4 burn_edge = step(noise_color.r,burn_process+0.03*noise_edge_color.r) *vec4(1.0,0.0,0.0,1.0)*(1.0 - melt.a)*nowAlpha;
	if(burn_process <1.0){
		COLOR = melt + burn_edge;
	}
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
