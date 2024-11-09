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

void fragment()
{
   vec2 uv = UV;
	uv -= 0.5;
   vec3 col = vec3(0.);
   float t = fract(.1 * iTime);
   uv *= rotation(3. * TAU * (.3 - clamp(length(uv),0.,.3)));
   float s = -1.;
   for(float i = 0.; i < 3.; i++){
     float rad = .4 / pow(2.,i) * (.9 - .2 * i);
     uv *= rotation(-2. * s * (i + 1.) * TAU * t);
     float tri = eqTri(uv, rad);
     s *= -1.;
     col += .004 / abs(tri);
   }
   COLOR = vec4(col,1.0);
	COLOR.a = (COLOR.r+COLOR.g+COLOR.b) / 3.;
}
