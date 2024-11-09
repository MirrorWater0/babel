shader_type canvas_item;

uniform float backValue : hint_range(0.0, 50.0, 0.1);
uniform sampler2D noise_tex;
uniform float speed : hint_range(0.0, 20.0, 0.1);
uniform float alpha : hint_range(0.0, 1.0, 0.1);
uniform vec3 color;
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 uv = UV;
	vec2 uv2 =UV;
	uv2.x = fract(uv2.x+TIME*speed);
	vec4 noise_color = texture(noise_tex,uv2);
	float noise = clamp(pow(noise_color.r,2.0),0.0,1.0)*0.3;

	vec3 horColour = vec3(0.25, 0.25, 0.25);


	vec3 backLines  = vec3(clamp(backValue*5.*(1.0-uv.x),0.0,backValue));

	//main beam
	uv = (2.0 * uv) - 1.0;
	float beamWidth = abs(1.0 / (20.0 * uv.y));
	vec3 horBeam = vec3(beamWidth);

	COLOR = vec4(((backLines * horBeam) * horColour), 1.0)*noise;


	COLOR.a = alpha*(COLOR.r+COLOR.g+COLOR.b) / 3.0;
	COLOR = vec4(color ,COLOR.a) ;
	COLOR.a = clamp(COLOR.a - 0.05,0.0,1.0);
	float middle = clamp(abs(uv.y),0.,1.);
	COLOR.rgb += clamp(1.0-40.*middle,0.,1.)*vec3(0.2,0.2,0.2);
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
