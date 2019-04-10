Shader "Custom/SurfaceShaderCheckerPattern" 
{
	Properties 
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Divisions ("Num Divisions", Range(0.0,64.0)) = 2.0
	}
	
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		#pragma surface surf Lambert

		struct Input 
		{
			float2 uv_MainTex;
		};
		
		float _Divisions;
		
		void surf ( Input IN, inout SurfaceOutput o ) 
		{
			int u = IN.uv_MainTex.x * (int)_Divisions;
			int v = IN.uv_MainTex.y * (int)_Divisions;

			int T= fmod((float) u+v,2);
			
			//if ( (u+v)%2 )
			if (T)
			{	
				o.Albedo = 1;
			}
			else
			{
				o.Albedo = 0;
			}
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
