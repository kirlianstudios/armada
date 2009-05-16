float4x4 World;
float4x4 View;
float4x4 Projection;
sampler TextureSampler;

struct VS_INPUT {
	float4 Position : POSITION0;
	float2 Texcoord : TEXCOORD0;
	float4 Color : COLOR0;
};

VS_INPUT Transform(VS_INPUT Input)
{
	VS_INPUT Output;
	
	float4 worldPosition = mul(Input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	
	Output.Position = mul(viewPosition, Projection);
	Output.Texcoord = Input.Texcoord;
	Output.Color = Input.Color;
	
	return Output;
}

float4 ColorTexture(VS_INPUT Input) : COLOR0
{
	return Input.Color.rgba * tex2D(TextureSampler, Input.Texcoord);
}

technique TransformColorTexture
{
	pass P0
	{
		VertexShader = compile vs_2_0 Transform();
		PixelShader = compile ps_2_0 ColorTexture();
	}
}