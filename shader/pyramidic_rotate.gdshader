shader_type canvas_item;

#define rotation(angle) mat2(vec2(cos(angle), -sin(angle)), vec2(sin(angle), cos(angle)));
#define iTime TIME
// Thanks IQ
float eqTri( in vec2 p, in float r )
{
    const float k = sqrt(3.0);
    p.x = abs(p.x) - r;
    p.y = p.y + r/k;
    if( p.x+k*p.y>0.0 ) p = vec2(p.x-k*p.y,-k*p.x-p.y)/2.0;
    p.x -= clamp( p.x, -2.0*r, 0.0 );
    return -length(p)*sign(p.y);
}
float happy_star(vec2 uv, float anim)
{
    uv = abs(uv);
    vec2 pos = min(uv.xy/uv.yx, anim);
    float p = (2.0 - pos.x - pos.y);
    return (2.0+p*(p*p-1.5)) / (uv.x+uv.y);      
}
void fragment()
{
   vec2 uv = UV;
	uv -= 0.5;
   vec3 col = vec3(0.);
   
    vec2 uv2 = UV;
	uv2 -= 0.5;
   vec3 col2 = vec3(0.);
   float t2 = fract(.1 * iTime);
   uv2 *= rotation(3. * t2*TAU * (.3 - clamp(length(uv2),0.,.3)));
   float s2 = -1.;
   for(float i = 0.; i < 3.; i++){
     float rad = .4 / pow(2.,i) * (.9 - .2 * i);
     uv2 *= rotation(-2. + iTime * (i + 1.) * TAU * t2);
     float tri = eqTri(uv2, rad);
     s2 *= -1.;
     col2 += .004 / abs(tri);
   }
   
   
   float t = fract(.1 * iTime);
   uv *= rotation(3. * iTime * (.3 - clamp(length(uv),0.,.3)));
   float s = -1.;
   for(float i = 0.; i < 3.; i++){
     float rad = .4 / pow(2.,i) * (.9 - .2 * i);
   
     float tri = eqTri(uv, rad);
     s *= -1.;
     col += .004 / abs(tri);
   }
   COLOR = vec4(col,1.0);
 
    uv *= 2.0 * ( cos(iTime * 2.0) -2.5); // scale
    float anim = sin(iTime * 12.0) * 0.1 + 1.0;  // anim between 0.9 - 1.1 
    COLOR*= vec4(happy_star(uv, anim) * vec3(0.35,0.2,1.15)*1.+ col2, 1.0);
	COLOR.a = (COLOR.r+COLOR.g+COLOR.b) / 3.;
}
