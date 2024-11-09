shader_type canvas_item;

uniform vec4 crystal_color = vec4(0.6, 0.8, 1.0, 1.0);  // Color of the crystal
uniform float refraction_strength : hint_range(0.0, 1.0) = 0.05;  // Strength of the refraction
uniform float fresnel_strength : hint_range(0.0, 1.0) = 0.8;  // Strength of the fresnel effect

void fragment() {
    vec2 uv = FRAGCOORD.xy / SCREEN_PIXEL_SIZE;
    vec2 tex_coord = UV - vec2(0.5);
    vec2 view_dir = normalize(tex_coord);
    
    // Fresnel effect
    float fresnel = pow(1.0 - dot(view_dir, view_dir), fresnel_strength);
    
    // Refraction effect
    vec2 refracted_uv = UV + tex_coord * refraction_strength;
    vec4 env_color = texture(TEXTURE, refracted_uv);
    
    // Combine the color with the environment color and fresnel
    vec4 final_color = mix(env_color * crystal_color, crystal_color, fresnel);
    
    COLOR = final_color;
}