Shader "Custom/WorldWithVision"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Stencil
            {
                Ref 1
                Comp Equal
            }
        }
    }
}