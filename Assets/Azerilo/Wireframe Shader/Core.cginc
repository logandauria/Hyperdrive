float _ZMode;
float _WireSize;
fixed4 _WireColor;

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

v2f vert (appdata v)
{
    v2f m;
    m.vertex = UnityObjectToClipPos(v.vertex);
    m.vertex.z -= (0.001*_ZMode);
    m.uv = v.uv;
    return m;
}

float dist(float2 pt1, float2 pt2)
{
    float2 v = pt2 - pt1;
    return dot(v,v);
}

float distm(float2 v, float2 w, float2 p) {
  float l2 = dist(v, w); 
  float t = max(0, min(1, dot(p - v, w - v) / l2));
  float2 pr = v + t * (w - v); 
  return distance(p, pr);
}

fixed4 frag (v2f i) : SV_Target
{
    float lwp = _WireSize;
    float lAA = 1;
    float2 uVec = float2(ddx(i.uv.x),ddy(i.uv.x)); 
    float2 vVec = float2(ddx(i.uv.y),ddy(i.uv.y)); 
    float vLn = length(uVec);
    float uLn = length(vVec);
    float uvDL = length(uVec+vVec);
    float mUDis = lwp * vLn;
    float mVDis = lwp * uLn;
    float muvDD = lwp * uvDL;
    float LEUD = i.uv.x;
    float REUD = (1.0-LEUD);
    float beVD = i.uv.y;
    float teVD = 1.0 - beVD;
    float mUD = min(LEUD,REUD);
    float mVD = min(beVD,teVD);
    float uvDioD = distm(float2(0.0,1.0),float2(1.0,0.0),i.uv);
    float noUD = mUD / mUDis;
    float noVD = mVD / mVDis;
    float noUVdd = uvDioD / muvDD;
    float cnorD = min(noUD,noVD);
    cnorD = min(cnorD,noUVdd);
    float LA = 1.0 - smoothstep(1.0,1.0 + (lAA/lwp),cnorD);
    LA *= _WireColor.a;
    
    return fixed4(_WireColor.rgb,LA);
}