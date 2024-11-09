shader_type canvas_item;

#define CS(a) vec2(cos(a),sin(a))
uniform float size : hint_range(0.0, 0.002, 0.0001);

uniform sampler2D noise;
void vertex() {
	// Called for every vertex the material is visible on.
}

uniform vec3 destColor = vec3(0.3, 0.8, 0.6);
void fragment() {


    vec2 uv = UV;
	vec2 uv2 = UV;
	uv2.y = fract(UV.y+TIME);
    vec4 noise_color =texture(noise,uv2);
    vec2 p = 3.*(UV - 0.5);

    float f = 0.0;

    for (float i = 0.0; i < 10.0; i++) {

        vec2 P = abs(p);

        f += size / (0.01*P.y + 0.005*P.x + P.x * P.y);

    }

    // Output to screen
    COLOR = vec4(vec3(destColor * f),1.0);
    COLOR.a = noise_color.r*(COLOR.r+COLOR.g+COLOR.b)/3.0;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
