shader_type canvas_item;

uniform float offset :hint_range(0.0, 1.0, 0.1);
uniform float light : hint_range(0.0, 1.0, 0.1);
uniform vec3 outline_color: source_color;
uniform float progress: hint_range(0.0, 1.0);
uniform float width: hint_range(0.0, 0.006, 0.001) = 0.001;
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 uv = UV;

	
	
	vec4 line_neg_y = vec4(outline_color.rgb, texture(TEXTURE, vec2(UV.x, UV.y - width)).a);
	vec4 line_pos_y = vec4(outline_color.rgb, texture(TEXTURE, vec2(UV.x, UV.y + width)).a);
	vec4 line_neg_x = vec4(outline_color.rgb, texture(TEXTURE, vec2(UV.x - width, UV.y)).a);
	vec4 line_pos_x = vec4(outline_color.rgb, texture(TEXTURE, vec2(UV.x + width, UV.y)).a);
	vec4 outline_y = mix(line_neg_y, line_pos_y, line_pos_y);
	vec4 outline_x = mix(line_neg_x, line_pos_x, line_pos_x);
	vec4 outline = mix(outline_y, outline_x, outline_x);
	
	COLOR = (COLOR + vec4(0.8,8.0,8.0,COLOR.a)*light);
	COLOR.rgba = mix(COLOR, outline, progress * (1.0 - COLOR.a));
	
	COLOR.a = COLOR.a*clamp((1.0-uv.g)*10.0 - offset*10.0,0.0,1.0) ;
}