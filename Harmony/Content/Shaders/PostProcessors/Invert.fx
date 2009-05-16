sampler TextureSampler;

struct VS_INPUT
{
	float4 Position : POSITION0;
	float2 Texcoord : TEXCOORD0;
};

VS_INPUT NullVertexShader(VS_INPUT Input)
{
	VS_INPUT Output;
	
	Output.Position = Input.Position;
	Output.Texcoord = Input.Texcoord;
	
	return Output;
}

float4 InvertTexture(VS_INPUT Input) : COLOR
{
	return (float4)1.0 - tex2D(TextureSampler, Input.Texcoord);
}

technique PostInvert
{
	pass P0
	{
		VertexShader = compile vs_2_0 NullVertexShader();
		PixelShader = compile ps_2_0 InvertTexture();
	}
}