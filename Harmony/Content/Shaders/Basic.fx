float4x4 World;
float4x4 View;
float4x4 Projection;

float3 EyePosition;

float3 AmbientColor;
float3 DiffuseColor;
float SpecularPower;

float3 LightDirection;
float3 LightDiffuseColor;
float3 LightSpecularColor;

sampler TextureSampler;

struct VS_INPUT
{
	float4 Position : POSITION0;
	float2 Texcoord : TEXCOORD0;
	float4 Color : COLOR0;
	float3 Normal : NORMAL0;
};

struct VS_OUTPUT
 {
	float4 Position : POSITION0;
	float2 Texcoord : TEXCOORD0;
	float4 Color : COLOR0;
	float3 Normal : TEXCOORD1;
	float3 ViewDirection : TEXCOORD2;
};

VS_OUTPUT Transform(VS_INPUT Input)
{
	VS_OUTPUT Output;
	
	float4 worldPosition = mul(Input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	
	Output.Position = mul(viewPosition, Projection);
	Output.Texcoord = Input.Texcoord;
	Output.Color = Input.Color;
	Output.Normal = mul(Input.Normal, World);
	Output.ViewDirection = EyePosition - worldPosition;
	
	return Output;
}

struct PS_INPUT
{
	float2 Texcoord : TEXCOORD0;
	float4 Color : COLOR0;
	float3 Normal : TEXCOORD1;
	float3 ViewDirection : TEXCOORD2;
};

float4 BasicShader(PS_INPUT Input) : COLOR0
{
	float3 Normal = normalize(Input.Normal);
	float3 ViewDirection = normalize(Input.ViewDirection);
	LightDirection = normalize(-LightDirection);
	
	float EdgeComponent = dot(Normal, ViewDirection);
	float3 TotalAmbient = saturate(AmbientColor * EdgeComponent);
	
	float NDotL = dot(Normal, LightDirection);
	float3 DiffuseAverage = (DiffuseColor + LightDiffuseColor) * 0.5f;
	float3 TotalDiffuse = saturate(DiffuseAverage * NDotL);
	
	float3 Reflection = normalize(2.0f * Normal * NDotL - LightDirection);
	float RDotV = max(0.0f, dot(Reflection, ViewDirection));
	float3 TotalSpecular = saturate(LightSpecularColor * pow(RDotV, SpecularPower));
	
	float4 TextureColor = Input.Color.rgba * tex2D(TextureSampler, Input.Texcoord);
	float4 LightingColor = float4(TotalAmbient + TotalDiffuse + TotalSpecular, 1.0f);
	
	return TextureColor > 0 ? TextureColor * LightingColor : LightingColor;
}

technique BasicShader
{
	pass P0 
	{
		VertexShader = compile vs_2_0 Transform();
		PixelShader = compile ps_2_0 BasicShader();
	}
}