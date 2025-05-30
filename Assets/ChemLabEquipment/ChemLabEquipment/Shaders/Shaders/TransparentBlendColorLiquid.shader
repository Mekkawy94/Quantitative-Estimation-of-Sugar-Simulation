Shader "ChemLab/TransparentBlendColorLiquid"
{
    Properties
    {
        _Albedo("Albedo", Color) = (0,0,0,0)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Fill("Fill Level", Range(0,1)) = 1.0 // New property for controlling fill level
        [HideInInspector] __dirty( "", Int ) = 1
    }

    SubShader
    {
        Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+1" "IgnoreProjector" = "True" }
        Cull Back
        Blend DstColor SrcColor
        CGPROGRAM
        #pragma target 3.0
        #pragma surface surf Standard addshadow fullforwardshadows 
        struct Input
        {
            half filler;
        };

        uniform float4 _Albedo;
        half _Glossiness;
        half _Metallic;
        float _Fill; // Variable to hold the fill level

        void surf( Input i , inout SurfaceOutputStandard o )
        {
            o.Albedo = _Albedo.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            // Modify alpha based on fill level
            o.Alpha = _Albedo.a * _Fill;
        }

        ENDCG
    }
    Fallback "Diffuse"
}
