Shader "Stereoscopy/InterleaveImage" {
    Properties {
        _MainTex("Tiled Texture", 2D) = "white" {}
        _GrayScaleAnimation("GrayScale Animation", float) = 0
		_RowModifier("Row_Modifier", range(-2880,2880)) = 0
    }
    SubShader {
            Pass
        {
       
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
 
            #include "UnityCG.cginc"
 
            struct appData{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD2;
            };
 
            struct v2f{
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD0;
                float4 clipPos : TEXCOORD1;
                float2 uv: TEXCOORD2;
            };
 
            sampler2D _MainTex;
 
            float	_GrayScaleAnimation;
			float _RowModifier;
            static const int TILED_IMAGE_DIMENSIONS =1440;
            static const int RESULT_IMAGE_WIDTH = 1920;
            static const int RESULT_IMAGE_HEIGHT = 1080;
            static const int CAM_WIDTH = 720;
            static const int CAM_HEIGHT = 360;
            static const int PATTERN_LENGTH = 8;
            static const int COORD_DISPLACEMENT = 2;
 
 
 
 
        int2 getCamera(int i){
                if (i==0){
                    return int2(1440,0);
                }
                else if(i==1){
                    return int2(1440,720);
                }
                else if(i==2){
                    return int2(1080,0);
                }
                else if(i==3){
                    return int2(1080,720);
                }
                else if(i==4){
                    return int2(720,0);
 
                }
                else if(i==5){
                    return int2(720,720);
 
                }
                else if(i==6){
                    return int2(360,0);
                }
                else{
                    return int2(360,720);
 
                }
                return int2(24,24);
            }
 
 
            v2f vert(appData IN){
                v2f OUT;
                //coloca el quad para que ocupe la totalidad de la pantalla.
                OUT.pos = float4(IN.vertex.xy*COORD_DISPLACEMENT,0,1);
               
                //como las coordenadas del espacio del objeto son coordenadas centricas,necesitamos cambiarlas a coordenadas positivas
                OUT.clipPos = float4(IN.vertex.xy+float2(0.5,0.5),0,1);
                OUT.normal = IN.normal;
                OUT.uv = float2(OUT.clipPos.x,OUT.clipPos.y);
                return OUT;
            }
            float3 getPatternRGB(int row, int column) {
                int normalPattern = (3 * ((RESULT_IMAGE_WIDTH - row-1 )*RESULT_IMAGE_HEIGHT + column)) % PATTERN_LENGTH;
                int displacement = (2 * (row) / 3) % PATTERN_LENGTH;
                int adjustment = (normalPattern - displacement);
 
                int p_R, p_G, p_B;
                p_R = adjustment<0 ? PATTERN_LENGTH + adjustment : adjustment;
                p_G = p_R + 1 > 7 ? 0 : p_R + 1;
                p_B = p_G + 1 > 7 ? 0 : p_G + 1;
               
                return int3(p_R, p_G, p_B);
            }
 
            float getRGBValue(int pattern, int row_cam, float col_cam, float RGB) {
                col_cam = (col_cam - RGB) / 8.0;
                int col_came =ceil(col_cam);
                float2 pixelcam = float2(getCamera(pattern).y + col_cam, getCamera(pattern).x - row_cam-1);
                float4 texelValue =  tex2D(_MainTex,float2(pixelcam.x, pixelcam.y) / TILED_IMAGE_DIMENSIONS);
               
                //return ((getCamera(pattern).x - row_cam) * 1440 + getCamera(pattern).y + col_came);
                float RGB_v = 0.0;
                return (RGB==3?texelValue.r:RGB==2?texelValue.g:texelValue.b);
            }
 
 
 
            float4 frag(v2f IN) : COLOR{
                //hayamos la row y la column dado el input uvE[0,1]
                int row= (1.0-IN.uv.y)*(RESULT_IMAGE_HEIGHT);
               // int column = IN.uv.x*(RESULT_IMAGE_WIDTH);
                float column = round(IN.uv.x*RESULT_IMAGE_WIDTH);
 
               
                //calculamos el patrón necesario,
                int3 P_RGB = getPatternRGB(row, column);
                //return float4(P_RGB.rgb, 1)*0.1;
                //Calculo de la información del patron y sentido
                int row_Camera = (row)/3;
                float col_Camera= (column)*3.0;
 
           
 
                float4 Color= float4(getRGBValue(P_RGB.x, row_Camera, col_Camera, 3.0),
                                     getRGBValue(P_RGB.y, row_Camera, col_Camera, 2.0),
                                     getRGBValue(P_RGB.z, row_Camera, col_Camera, 1.0), 1)  ;

                if (row %3 == 0 && row!=0 ) {
                    row = row - 1;
                    P_RGB = getPatternRGB(row, column);
                    row_Camera = (row / 3);
 
                    Color = (Color + float4(getRGBValue(P_RGB.x, row_Camera, col_Camera, 3),
                        getRGBValue(P_RGB.y, row_Camera, col_Camera, 2),
                        getRGBValue(P_RGB.z, row_Camera, col_Camera, 1), 1)) / 2;
                }
                
				//Color = Color*_GrayScaleAnimation + (float4(1,1,1,1)*(Color.r*0.21 + 0.72 *Color.g + 0.07* Color.b))*(1-_GrayScaleAnimation);

                return Color;
 
            }
            ENDCG
        }
    }
}