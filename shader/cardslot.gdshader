shader_type canvas_item;


void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 uv = UV;
	float mask1 = step(uv.x,0.05)+step(0.95,uv.x)+step(uv.y,0.05)+step(0.95,uv.y);
	COLOR = vec4(1.0,1.0,0.90,1.0)*mask1;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
