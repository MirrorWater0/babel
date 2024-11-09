shader_type canvas_item;

uniform bool hover;
uniform float process;
uniform bool pressed;

varying vec4 modualate;
void vertex() {
	modualate = COLOR;
}

void fragment() {
	vec2 uv =UV;
	vec4 text = texture(TEXTURE,uv);
	COLOR = vec4(0.9,0.9,0.9,1.0);

	COLOR.a =1.0 -  2.0*fract(abs(0.5-uv.x));

	if(hover){
		COLOR.rgb += 0.5;
	}

	if(pressed){
		COLOR.rgb -= 0.5;
	}
	COLOR.a = modualate.a*COLOR.a;
	
}


//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
