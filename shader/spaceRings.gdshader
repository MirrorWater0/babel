shader_type canvas_item;

#define time TIME*1.25
#define p0 0.5,0.5,0.8 ,0.5,0.5,0.5, 1.0,1.0,1.0, 0.0,0.33,0.67
const float numParticles = 25.0;
const float numRings = 5.;
const float offsetMult = 30.;
const float tau = 6.23813;

vec3 palette(in float t,in float a0,in float a1,in float a2,in float b0,in float b1,in float b2
	,in float c0,in float c1,in float c2,in float d0,in float d1,in float d2){
	return vec3(a0,a1,a2) + vec3(b0,b1,b2)*cos(tau*(vec3(c0,c1,c2)*t+vec3(d0,d1,d2)) );
}
vec3 particleColor(vec2 uv,float radius,float offset, float periodOffset){
	vec3 color = palette(0.4 + offset /4.0,p0);
	uv /= pow(periodOffset, .75) * sin(periodOffset * TIME) + sin(periodOffset + TIME);
	vec2 pos = vec2(cos(offset*offsetMult + time + periodOffset),sin(offset*offsetMult+time*5.0+periodOffset*tau));
	float dist = radius / distance(uv,pos);
	return color*pow(dist,2.0)*1.75;
}
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 uv = UV - vec2(0.5);
	uv *= 6.45;


	COLOR.rgb = vec3(0);
	for(float n = 0.0;n <= numRings;n++){
		for(float i = 0.0; i <= numParticles;i ++){

			COLOR.rgb += particleColor(uv,0.03,i/ numParticles,n/2.);
		}
	COLOR.a = (COLOR.r + COLOR.b + COLOR.g)/3.;
	}
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
