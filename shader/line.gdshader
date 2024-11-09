shader_type canvas_item;
#define S(a,b,c) smoothstep(a,b,c)
#define iTime TIME
uniform float alpha : hint_range(0.0, 1.0, 0.1);
vec4 Line(vec2 uv, float speed, float height, vec3 col) {
    uv.y += S(1., 0., abs(uv.x)) * sin(iTime * speed + uv.x * height) * .2;
    return vec4(S(.06 * S(.2, .9, abs(uv.x)), 0., abs(uv.y) - .004) * col, 1.0) * S(1., .3, abs(uv.x));
}

void fragment() {
    vec2 uv = UV;
	uv -= 0.5;
    COLOR = vec4 (0.);
    for (float i = 0.; i <= 5.; i += 1.) {
        float t = i / 5.;
        COLOR += Line(uv, 1. + t, 4. + t, vec3(.2 + t * .7, .2 + t * .4, 0.3));
    }
	COLOR.a = alpha*(COLOR.r+COLOR.g+COLOR.b)/3.;
}