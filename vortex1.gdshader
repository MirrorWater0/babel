shader_type canvas_item;
uniform float offset: hint_range(0.0,20.0,0.01);
uniform float mask: hint_range(0.0,1.0,0.01);
uniform vec4 color;
uniform sampler2D noise_tex;
uniform float speed : hint_range(0.0, 1.0, 0.1);
void vertex() {
	// Called for every vertex the material is visible on.
}
vec2 rotate(vec2 p,float angle){
	return vec2(cos(angle)*p.x - sin(angle)*p.y,sin(angle)*p.x + cos(angle)*p.y);
}
void fragment() {
	vec2 noise_uv;
	
	
	float c =length(UV-vec2(0.5));
	vec2 uv = UV;
	uv = rotate(uv-vec2(0.5),offset*c) + vec2(0.5);
	uv.x = abs((uv.x - 0.5)*2.0);
	uv.x = fract(uv.x + speed*TIME);

	vec4 noise_color = texture(noise_tex,uv);
	noise_color.a = noise_color.r*(1.0 -distance(uv,vec2(0.5)));
	
	
	float mask_color = step(c,mask);
	vec4 tex_color =texture(TEXTURE,uv);
	tex_color *= (2.-2.*distance(clamp(uv-vec2(0.5),0.1,1.),vec2(0.5)));
	COLOR = tex_color *color;
	COLOR.a = COLOR.a*mask_color;
}
