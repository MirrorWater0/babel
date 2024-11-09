shader_type canvas_item;

#define ITR 32
#define EPS 0.001
#define iTime TIME
float hash(vec2 p){
	return fract(sin(dot(p,vec2(43.232,75.876)))*4526.3257);
}

float sdSphere(vec3 p){
	return length(p)-(sin(iTime)*0.5+1.0);
}

float displacement(vec3 p){
	return hash(vec2(p.x,p.y));
}

float mainDist(vec3 p){
	float dis=displacement(p);
	float d1=sdSphere(p);
	return d1+dis;
}


vec3 genNormal(vec3 p){
	vec3 normal=vec3(
		sdSphere(p+vec3(EPS,0.0,0.0))-sdSphere(p+vec3(-EPS,0.0,0.0)),
		sdSphere(p+vec3(0.0,EPS,0.0))-sdSphere(p+vec3(0.0,-EPS,0.0)),
		sdSphere(p+vec3(0.0,0.0,EPS))-sdSphere(p+vec3(0.0,0.0,-EPS))
		);
	return normalize(normal);
}



void fragment(){
	vec2 uv= UV;
	uv = 2.*uv -2.*vec2(0.5);
	vec3 ro=vec3(0.0,0.0,-10.0);
	vec3 lookat=vec3(0.0,0.0,0.0);
	float zoom=5.0;

	vec3 f=normalize(lookat-ro);
	vec3 r=normalize(cross(vec3(0.0,1.0,0.0),f));
	vec3 u=cross(f,r);
	vec3 c=ro+f*zoom;
	vec3 i=c+uv.x*r+uv.y*u;
	vec3 rd=normalize(i-ro);
   
    float d,t;
    vec3 p,n;

	vec3 col=vec3(1.0);
    
    for(int i=0;i<ITR;i++){
        p=ro+rd*t;
        d=mainDist(p);
        n=genNormal(p);
        if(d<EPS)break;
        t+=d;
    }
	col=n*0.5+0.5;

	COLOR=vec4(col,1.0);
}