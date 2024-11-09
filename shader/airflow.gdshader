shader_type canvas_item;

uniform float alpha : hint_range(0.0, 0.3, 0.01);

uniform sampler2D noise_tex;
varying vec4 modulate;
void vertex() {
	modulate =COLOR;
}

void fragment() {
	vec2 uv = UV;
	vec2 noise_uv = UV;

	noise_uv = fract(noise_uv - TIME*0.1);

	vec4 noise_color = texture(noise_tex,noise_uv);
	float noise = clamp(pow(noise_color.r,2.0),0.0,1.0)*0.5;


	//uv = (uv-vec2(0.5))*0.2 + vec2(0.25);
	uv += noise - 0.3;
	uv = (uv-vec2(0.5)) + vec2(0.5);
	vec4 tex_color = texture(TEXTURE,uv);
	COLOR = tex_color;
	COLOR.rgb = (1.0 - tex_color.rgb)*4.0;
	COLOR.a = COLOR.a*2.0*alpha*modulate.a;

}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
