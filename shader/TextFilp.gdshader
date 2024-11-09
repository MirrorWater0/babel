shader_type canvas_item;
#define iTime TIME
//#define fragCoord POINT_COORD
//#define iResolution 1.0 / SCREEN_PIXEL_SIZE
#define iChannel0 screen_texture
#define fragColor COLOR
uniform float process : hint_range(0.0, 2.0, 0.01);
/* 
    "Flip Page" by Lucian Stanculescu - 2019
    Free to use, credit if you want/can.

    A simple page flip - the page wraps around a rolling cylinder
 
    IMPROVEMENTS
    - normals and lighting
    - compute time when page exits completely
    - use a cone instead of the cylinder with changing radius/height/orientation
*/ 
uniform sampler2D screen_texture : hint_screen_texture, repeat_disable, filter_nearest;


#define r 0.3
#define rep 2.0

void fragment()
{
    
    vec2 iResolution = vec2(textureSize(screen_texture,0));
    vec2 fragCoord = SCREEN_UV * iResolution;
    
    float R = r * iResolution.x; // radius of rolling cylinder
    
    float v = 1.5 * iResolution.x / rep;
    
    float time = process / rep;
    
    vec2 s = fragCoord; // pixel coordinates
    
    vec2 u = normalize(vec2(5.0, 1.0)); // direction of movement
    
    vec2 o = vec2(time *rep* v, 0.0); // origin of cylinder
    
    float d = dot(s - o, u); // distance to generator of cylinder
    
    vec2 h = s - u * d; // projection on generator
    
    bool onCylinder = abs(d) < R;
    
    float angle = onCylinder ? asin(d / R) : 0.0;
    
    bool neg = d < 0.0;
    
    float a0 = 3.141592653 + angle;
    
    float a = onCylinder ? (neg ? -angle : (3.141592653 + angle)) : 0.0; // angle
    
    float l = R * a; // length of arc
    
    vec2 p = h - u * l; // unwrapped point from cylinder to plane
    
    bool outside = any(lessThan(p, vec2(0.0))) || any(greaterThan(p, iResolution.xy));
    
    bool previous = (!onCylinder ||outside) && neg;
    
    bool page = !onCylinder || outside;
    
    vec4 color;
    if (page) 
        color = texture(iChannel0, fragCoord / iResolution.xy);
    else
        color = texture(iChannel0, p / iResolution.xy);
    color *= (previous ? mix(0.1, 1.0, time): 1.0);
 
    l = R * a0; // length of arc
    
    p = h - u * l; // unwrapped point from cylinder to plane
    
    outside = any(lessThan(p, vec2(0.0))) || any(greaterThan(p, iResolution.xy));
    
    color = outside || !onCylinder ? color : texture(iChannel0, p / iResolution.xy);
    
    // Output to screen
    fragColor = color;
}