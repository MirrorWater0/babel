shader_type canvas_item;

void fragment(){
	vec2 uv = UV;
	if(pow(uv.x,2)+pow(uv.y,2)<0.5){
		COLOR = vec4(0);
	}
}