shader_type canvas_item;
uniform bool hit = false;

varying vec4 modulate;


// Animate from 0 to 1

uniform float progress: hint_range(0.0, 2.0) = 0.0;



uniform vec2 base_direction = vec2(0.0, 0.0);

uniform float pixel_scale:hint_range(0.0, 16.0) = 1.0;

uniform float base_dir_rand_factor: hint_range(0.0, 1.0) = 0.2;

uniform bool regionEnabled = false; // Enable Sprite Region



#define eps 10e-6

varying flat vec2 pivot;

varying flat vec2 region_rate;

varying flat vec2 TEXTURE_PIXEL_SIZE_fixed;



vec2 noise_vec2(vec2 uv) {

	uv = vec2(dot(uv, vec2(127.1,311.7)), dot(uv, vec2(269.5,183.3)));

	return fract(sin(uv) * 43758.5453123) * 0.5;

}



void vertex(){
	modulate = COLOR;
	if (regionEnabled) {

		vec2 my_region_rate = abs(VERTEX) *2.0 ;

		region_rate = TEXTURE_PIXEL_SIZE * my_region_rate;

		TEXTURE_PIXEL_SIZE_fixed = 1.0/ my_region_rate;

		if(VERTEX_ID == 0){

			pivot = UV ;

		}

		if(VERTEX_ID == 1){

			pivot = UV - vec2(0.0, region_rate.y);

		}

	}

	else{

		region_rate = vec2(1.0, 1.0);

		TEXTURE_PIXEL_SIZE_fixed = TEXTURE_PIXEL_SIZE;

		pivot = vec2(0.0, 0.0);

	}

}

void fragment() {

	vec2 uv = UV;

	uv = (UV - pivot )/region_rate ;

	vec2 direction = vec2(0.0);

	vec2 pixel_uv = vec2(uv.x - mod(uv.x, TEXTURE_PIXEL_SIZE_fixed.x * pixel_scale) + eps,

						uv.y - mod(uv.y, TEXTURE_PIXEL_SIZE_fixed.y * pixel_scale) + eps);

	//vec2 pixel_uv = vec2(uv.x - mod(uv.x, TEXTURE_PIXEL_SIZE.x * pixel_scale) + eps,

						//uv.y - mod(uv.y, TEXTURE_PIXEL_SIZE.y * pixel_scale) + eps);

	direction = base_direction + (noise_vec2( pixel_uv) *2.0 -0.5) * base_dir_rand_factor;

	direction = direction * vec2(progress);

	uv += direction ;

	uv = pixel_uv + direction;

	if(uv.x > 0.0 &&

		uv.x < 1.0 &&

		uv.y > 0.0 &&

		uv.y < 1.0 ){

			vec2 my_uv = pivot + uv * region_rate;

			COLOR = texture(TEXTURE, my_uv);

			//COLOR.a *= (1.0 - progress);

			}

	else{

		COLOR = vec4(0.0);

	}
	if(hit){
		vec4 tex_color = texture(TEXTURE,uv);
		COLOR = vec4(1.0,1,1.0,tex_color.q);
	}
	COLOR =COLOR*modulate;
}