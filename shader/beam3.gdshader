shader_type canvas_item;

void fragment()
{
    // Normalize coordinates
    vec2 uv = UV;
    // vec2 uv = fragCoord;
    
    // Get mouse position in normalized coordinates
    vec2 mouse = (iMouse.xy - 0.5 * iResolution.xy) / iResolution.y;
    // vec2 mouse = iMouse.xy;
    
    // Center point (origin of the beam)
    vec2 center = vec2(0.0);
    
    // Animation parameters
    float growDuration = 0.5;    // Time to reach target
    float lingerDuration = 0.3;  // Time to stay at full intensity
    float fadeDuration = 0.8;    // Time to fade out
    float totalDuration = growDuration + lingerDuration + fadeDuration;
    
    // Beam thickness and glow parameters
    float coreThickness = 0.003;    // White core
    float beamThickness = 0.004;    // Main beam
    float glowThickness = 0.02;     // Outer glow
    
    // Calculate beam direction and target length
    vec2 beamDir = normalize(mouse - center);
    float targetLength = length(mouse - center);
    
    // Local time for the beam animation
    float localTime = mod(iTime, totalDuration);
    
    // Calculate current beam length and fade
    float currentLength;
    float fade = 1.0;
    float rootIntensity = 1.0;
    float impactIntensity = 0.0;
    
    if (localTime < growDuration) {
        // Growing phase
        currentLength = targetLength * (localTime / growDuration);
        fade = 1.0;
        rootIntensity = 1.0;
        impactIntensity = 0.0;
    } 
    else if (localTime < growDuration + lingerDuration) {
        // Lingering phase
        currentLength = targetLength;
        fade = 1.0;
        rootIntensity = 1.0;
        // Pulse the impact effect during linger
        float lingerTime = (localTime - growDuration) / lingerDuration;
        impactIntensity = 1.0 + 0.2 * sin(lingerTime * 30.0);
    }
    else {
        // Fading phase
        currentLength = targetLength;
        float fadeTime = localTime - (growDuration + lingerDuration);
        fade = 1.0 - (fadeTime / fadeDuration);
        rootIntensity = fade;
        impactIntensity = fade * 0.5;
    }
    
    // Distance from current pixel to the line segment (beam)
    vec2 p = uv - center;
    float h = clamp(dot(p, beamDir) / currentLength, 0.0, 1.0);
    vec2 projection = center + beamDir * h * currentLength;
    float dist = length(uv - projection);
    
    // Create the beam layers
    float core = smoothstep(coreThickness, 0.0, dist);
    float beam = smoothstep(beamThickness + glowThickness, beamThickness, dist);
    
    // Root circle parameters
    float rootRadius = 0.03;        // Size of the root circle
    float rootGlow = 0.04;          // Size of the root circle's glow
    float distFromRoot = length(uv - center);
    float rootCircle = smoothstep(rootRadius + rootGlow, rootRadius, distFromRoot);
    float rootCore = smoothstep(rootRadius * 0.5, 0.0, distFromRoot);
    
    // Impact effect at target
    vec2 targetPos = center + beamDir * currentLength;
    float distFromTarget = length(uv - targetPos);
    float impactRadius = 0.01;
    float impactGlow = 0.05;
    
    // Create expanding ring effect
    float ringSize = impactRadius * (1.0 + 0.3 * sin(localTime * 10.0));
    float ringThickness = 0.005;
    float ring = smoothstep(ringSize + ringThickness, ringSize, distFromTarget) 
                 - smoothstep(ringSize, ringSize - ringThickness, distFromTarget);
    
    // Create central impact glow
    float impactCore = smoothstep(impactRadius + impactGlow, impactRadius, distFromTarget);
    float impact = impactCore + ring * 0.5;
    
    // Root fade effect
    float rootFade = 1.0;
    if (localTime >= growDuration + lingerDuration) {
        float fadeTime = localTime - (growDuration + lingerDuration);
        float fadeProgress = fadeTime / fadeDuration;
        rootFade = smoothstep(fadeProgress, fadeProgress + 0.2, h);
    }
    
    // Color gradient for the main beam
    vec3 beamColor = mix(
        vec3(0.2, 0.6, 1.0), // Blue core
        vec3(0.1, 0.3, 1.0), // Darker blue edge
        dist / (beamThickness + glowThickness)
    );
    
    // Combine core and beam
    vec3 col = mix(beamColor, vec3(1.0), core) * beam * fade * rootFade;
    
    // Add bloom/glow effect
    float bloom = smoothstep(beamThickness + glowThickness, beamThickness, dist * 0.5);
    col += beamColor * bloom * 0.5 * fade * rootFade;
    
    // Add root circle with glow
    vec3 rootCol = mix(beamColor, vec3(1.0), rootCore);
    col += rootCol * rootCircle * rootIntensity;
    
    // Add impact effect
    vec3 impactCol = mix(beamColor * 1.5, vec3(1.0), 0.5);
    col += impactCol * impact * impactIntensity;
    
    // Output final color
    fragColor = vec4(col, 1.0);
}
