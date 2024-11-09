shader_type canvas_item;

uniform bool fire_on;
uniform float edge_size : hint_range(0.5, 1.0, 0.01);
uniform sampler2D noise_tex;
uniform float noise_size : hint_range(0.0,10.0,0.01);
varying vec4 modulate;
void vertex() {
	modulate =COLOR;
}

void fragment() {
	vec2 uv = UV;

	//noise
	vec2 noise_uv = UV;

	noise_uv.y = fract(noise_uv.y + TIME);
	vec4 noise_color = texture(noise_tex,noise_uv);
	float noise = clamp(pow(noise_color.r,noise_size),0.0,1.0) * 0.1;

	//COLOR = vec4(UV+noise,0.0,1.0);
	//边缘图像
	uv = (uv-vec2(0.5))*edge_size + vec2(0.50);
	uv = uv + noise;
	vec4 tex_color = texture(TEXTURE,uv);
	
	if(fire_on){
		COLOR = tex_color+vec4(0.0,5.0,10.0,0.0) ;
	}
	else{
		COLOR.a = COLOR.a * 0.0;
	}
	COLOR*=modulate;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
