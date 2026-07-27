/*{
  "DESCRIPTION": "Maps the image onto a two-colour gradient \u2014 the bold poster look used for title cards, drops and lyric moments.",
  "CATEGORIES": ["Guillotine", "Stylize"],
  "INPUTS": [
  {
    "NAME": "inputImage",
    "TYPE": "image"
  },
  {
    "NAME": "strength",
    "TYPE": "float",
    "DEFAULT": 0.85,
    "MIN": 0.0,
    "MAX": 1.0
  },
  {
    "NAME": "hue",
    "TYPE": "float",
    "DEFAULT": 0.0,
    "MIN": 0.0,
    "MAX": 1.0
  }
]
}*/
// Cheap palette sweep: hue slides both colours around the wheel together.
vec3 paletteA(float h) { return 0.5 + 0.5 * cos(6.28318 * (h + vec3(0.0, 0.33, 0.67))); }

void main() {
  vec4 c = IMG_THIS_PIXEL(inputImage);
  float luma = dot(c.rgb, vec3(0.299, 0.587, 0.114));

  // Two ends of the ramp, a half-turn apart, so shadows and highlights always contrast.
  vec3 dark  = paletteA(hue) * 0.25;
  vec3 light = paletteA(hue + 0.5);

  vec3 mapped = mix(dark, light, smoothstep(0.0, 1.0, luma));
  gl_FragColor = vec4(mix(c.rgb, mapped, strength), c.a);
}
