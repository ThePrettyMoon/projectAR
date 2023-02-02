Shader "SuperSystems/SpatialMapping"
{
    Properties
    {
        _WireThickness("Wire Thickness", RANGE(0, 800)) = 200
        _MeshColor("MeshColor", Color) = (0, 0, 0, 0)
        _EdgeColorMode("EdgeColorMode", Int) = 1
        _alpha("EdgeAlpha", Int) = 1
    }

        SubShader
    {
        // Each color represents a meter.
        // Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }
        // Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjectors" = "True" }
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry-100" }
        // 仅仅是把模型的深度信息写入深度缓冲中
        // 从而剔除模型中被自身遮挡的片元
        Pass {
            // 开启深度写入
            ZWrite On
            // 用于设置颜色通道的写掩码(Wirte Mask)
            // ColorMask RGB|A|0|(R/G/B/A组合)
            // 当为0时意味着该Pass不写入任何颜色通道，也就不会输出任何颜色
            ColorMask 0
        }

        Pass
        {
            // Wireframe shader based on the the following
            // http://developer.download.nvidia.com/SDK/10/direct3d/Source/SolidWireframe/Doc/SolidWireframe.pdf

            // 关闭灯效影响
		    Lighting off
 
		    // 打开深度写入
		    ZWrite Off	// 与默认的属性一致，可以不写

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _WireThickness;
            fixed4 _MeshColor;
            int _EdgeColorMode;
            int _alpha;

            struct appdata
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2g
            {
                float4 projectionSpaceVertex : SV_POSITION;
                float4 worldSpacePosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO_EYE_INDEX
            };

            struct g2f
            {
                float4 projectionSpaceVertex : SV_POSITION;
                float4 worldSpacePosition : TEXCOORD0;
                float4 dist : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2g vert(appdata v)
            {
                v2g o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT_STEREO_EYE_INDEX(o);

                o.projectionSpaceVertex = UnityObjectToClipPos(v.vertex);
                o.worldSpacePosition = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g i[3], inout TriangleStream<g2f> triangleStream)
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i[0]);

                float2 p0 = i[0].projectionSpaceVertex.xy / i[0].projectionSpaceVertex.w;
                float2 p1 = i[1].projectionSpaceVertex.xy / i[1].projectionSpaceVertex.w;
                float2 p2 = i[2].projectionSpaceVertex.xy / i[2].projectionSpaceVertex.w;

                float2 edge0 = p2 - p1;
                float2 edge1 = p2 - p0;
                float2 edge2 = p1 - p0;

                // To find the distance to the opposite edge, we take the
                // formula for finding the area of a triangle Area = Base/2 * Height,
                // and solve for the Height = (Area * 2)/Base.
                // We can get the area of a triangle by taking its cross product
                // divided by 2.  However we can avoid dividing our area/base by 2
                // since our cross product will already be double our area.
                float area = abs(edge1.x * edge2.y - edge1.y * edge2.x);
                float wireThickness = 800 - _WireThickness;

                g2f o;
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.worldSpacePosition = i[0].worldSpacePosition;
                o.projectionSpaceVertex = i[0].projectionSpaceVertex;
                o.dist.xyz = float3((area / length(edge0)), 0.0, 0.0) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                triangleStream.Append(o);

                o.worldSpacePosition = i[1].worldSpacePosition;
                o.projectionSpaceVertex = i[1].projectionSpaceVertex;
                o.dist.xyz = float3(0.0, (area / length(edge1)), 0.0) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                triangleStream.Append(o);

                o.worldSpacePosition = i[2].worldSpacePosition;
                o.projectionSpaceVertex = i[2].projectionSpaceVertex;
                o.dist.xyz = float3(0.0, 0.0, (area / length(edge2))) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                triangleStream.Append(o);
            }

            fixed4 frag(g2f i) : SV_Target
            {
                float minDistanceToEdge = min(i.dist[0], min(i.dist[1], i.dist[2])) * i.dist[3];

                // Early out if we know we are not on a line segment.
                if (minDistanceToEdge > 0.9)
                {
                    // if(_alpha == 0){
                        return fixed4(0,0,0,0);
                    // }else{
                        // return fixed4(0,0,0,1); //MeshColor;
                    // }
                }

                // Smooth our line out
                float t = exp2(-2 * minDistanceToEdge * minDistanceToEdge);

                const fixed4 colors[11] = {
                        fixed4(1.0, 1.0, 1.0, 1.0),  // White
                        fixed4(1.0, 0.0, 0.0, 1.0),  // Red
                        fixed4(0.0, 1.0, 0.0, 1.0),  // Green
                        fixed4(0.0, 0.0, 1.0, 1.0),  // Blue
                        fixed4(1.0, 1.0, 0.0, 1.0),  // Yellow
                        fixed4(0.0, 1.0, 1.0, 1.0),  // Cyan/Aqua
                        fixed4(1.0, 0.0, 1.0, 1.0),  // Magenta
                        fixed4(0.5, 0.0, 0.0, 1.0),  // Maroon
                        fixed4(0.0, 0.5, 0.5, 1.0),  // Teal
                        fixed4(1.0, 0.65, 0.0, 1.0), // Orange
                        fixed4(1.0, 1.0, 1.0, 1.0)   // White
                    };
                fixed4 wireColor;
                float cameraToVertexDistance = length(_WorldSpaceCameraPos - i.worldSpacePosition);
                //if (_EdgeColorMode == 1)
                //{
                //    int index = clamp(floor(cameraToVertexDistance), 0, 10);
                //    wireColor = colors[index];
                //}
                //if (_EdgeColorMode == 2)
                //{
                float max_depth = 3.0;
                float max_depth_threshold = 10.0;
                float r = 1.0; float g = 1.0; float b = 1.0;
                // if (cameraToVertexDistance >= max_depth)
                //     cameraToVertexDistance = max_depth;
                float scale_pose = cameraToVertexDistance / max_depth;

                float scale_step = 0.015625;
                if (scale_pose >= 0.0 && scale_pose <= 0.125) {
                    r = 0.0;
                    g = 0.0;
                    b = 0.984375 - scale_step * scale_pose * 255.0;
                }
                if (scale_pose > 0.375 && scale_pose <= 0.625) {
                    r = 1.0;
                    g = scale_step + scale_step * (scale_pose - 0.375) * 255.0;
                    b = 0.0;
                }
                if (scale_pose > 0.625 && scale_pose <= 1.0) {
                    r = 0.9765625 - scale_step * (scale_pose - 0.625) * 255.0;
                    g = 1.0;
                    b = 0.0234375 + scale_step * (scale_pose - 0.625) * 255.0;
                }
                if (scale_pose > 0.125 && scale_pose <= 0.375) {
                    r = 0.5 + scale_step * (scale_pose-0.125) * 255.0;
                    g = 0.0;
                    b = 0.0;
                }
                if (scale_pose > 1.0){
                    r = 1.0;
                    g = 1.0;
                    b = 1.0;
                }
                wireColor = fixed4(r, g, b, 1.0);
                //}

                fixed4 finalColor = lerp(float4(0,0,0,1), wireColor, 1.0);
                float tmp_scale = 0.0;
                if(scale_pose > 1.0){
                     tmp_scale = 0.4;
                }else{
                     tmp_scale = 1.0;
                }

                if(_alpha == 0){
                    // finalColor = lerp(float4(0,0,0,0), wireColor, 0.0);
                    finalColor.a = 0.0;
                }else{
                    finalColor.a = tmp_scale;
                }

                return finalColor;
            }
            ENDCG
        }
    }
}