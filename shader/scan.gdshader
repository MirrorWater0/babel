shader_type canvas_item;

uniform float offset: hint_range(0.0,20.0,0.01);
uniform float mask: hint_range(0.0,1.0,0.01);
uniform vec4 color;
uniform sampler2D noise_tex;
// --- Constants --- //
const float TWO_PI = 6.28318530718;

// --- Uniforms --- //
uniform float speed: hint_range(0.0, 10.0, 0.1) = 2.0;
group_uniforms line;
uniform vec4 line_color: source_color = vec4(0.0, 1.0, 1.0, 0.85);
group_uniforms trail;
uniform vec4 trail_color: source_color = vec4(0.0, 1.0, 1.0, 0.20);
uniform float trail_length: hint_range(0.0, 1.0, 0.1) = 0.10;
uniform sampler2D nois_tex;
// --- Functions --- //
vec2 rotate(vec2 _pos, float _angle) {
	return vec2(_pos.x * cos(_angle) - _pos.y * sin(_angle), _pos.x * sin(_angle) + _pos.y * cos(_angle));
}

void fragment() {
	vec2 pos = rotate(UV - 0.5, -TIME * speed);
	vec4 noise_color = texture(nois_tex,UV);

	float angle = (atan(pos.y, pos.x) + PI) / TWO_PI;
	float len = step(0.5, 1.0 - distance(pos, vec2(0.0)));
	COLOR = trail_color;
	COLOR.a *= smoothstep(1.0 - trail_length, 1.0, angle);
	COLOR = mix(COLOR, line_color, step(0.998, angle));
	COLOR *= len;

	//
		vec2 noise_uv;


	float c =length(UV-vec2(0.5));
	vec2 uv = UV;
	uv = rotate(uv-vec2(0.5),offset*c) + vec2(0.5);
	uv.x = abs((uv.x - 0.5)*2.0);
	uv.x = fract(uv.x + TIME);

	vec4 noise_color0 = texture(noise_tex,uv);
	noise_color.a = noise_color0.r*(1.0 -distance(uv,vec2(0.5)));


	float mask_color = step(c,mask);
	COLOR.a = COLOR.a*mask_color;
}
vec2 rotate2(vec2 p,float angle){
	return vec2(cos(angle)*p.x - sin(angle)*p.y,sin(angle)*p.x + cos(angle)*p.y);
}
