using UnityEngine;
using System.Collections.Generic;


#region enum
[System.Serializable]
public class ShaderLabType
{
	public static ProtoWorld.BLEND_FACTOR GetProtoBlendMode(BlendMode b)
	{
		switch (b)
		{
			case BlendMode.kBlendZero: return ProtoWorld.BLEND_FACTOR.Zero;
			case BlendMode.kBlendOne: return ProtoWorld.BLEND_FACTOR.One;
			case BlendMode.kBlendDstColor: return ProtoWorld.BLEND_FACTOR.DestColor;
			case BlendMode.kBlendSrcColor: return ProtoWorld.BLEND_FACTOR.SrcColor;
			case BlendMode.kBlendOneMinusDstColor: return ProtoWorld.BLEND_FACTOR.OneMinusDestColor;
			case BlendMode.kBlendSrcAlpha: return ProtoWorld.BLEND_FACTOR.SrcAlpha;
			case BlendMode.kBlendOneMinusSrcColor: return ProtoWorld.BLEND_FACTOR.OneMinusSrcColor;
			case BlendMode.kBlendDstAlpha: return ProtoWorld.BLEND_FACTOR.DestAlpha;
			case BlendMode.kBlendOneMinusDstAlpha: return ProtoWorld.BLEND_FACTOR.OneMinusDestAlpha;
			case BlendMode.kBlendSrcAlphaSaturate: return ProtoWorld.BLEND_FACTOR.SrcAlphaSaturate;
			case BlendMode.kBlendOneMinusSrcAlpha: return ProtoWorld.BLEND_FACTOR.OneMinusSrcAlpha;
			default: return ProtoWorld.BLEND_FACTOR.One;
		}
	}

	public static ProtoWorld.BLEND_OPERATOR GetProtoBlendOp(BlendOp b)
	{
		switch (b)
		{
			case BlendOp.kBlendOpAdd: return ProtoWorld.BLEND_OPERATOR.Add;
			case BlendOp.kBlendOpSub: return ProtoWorld.BLEND_OPERATOR.Sub;
			case BlendOp.kBlendOpRevSub: return ProtoWorld.BLEND_OPERATOR.RevSub;
			case BlendOp.kBlendOpMin: return ProtoWorld.BLEND_OPERATOR.Minimun;
			case BlendOp.kBlendOpMax: return ProtoWorld.BLEND_OPERATOR.Maximun;
			default: return ProtoWorld.BLEND_OPERATOR.Off;
		}
	}

	public static ProtoWorld.CULL_MODE GetProtoCullMode(CullMode c)
	{
		switch (c)
		{
			case CullMode.kCullOff: return ProtoWorld.CULL_MODE.CullNone;
			case CullMode.kCullFront: return ProtoWorld.CULL_MODE.CullFront;
			case CullMode.kCullBack: return ProtoWorld.CULL_MODE.CullBack;
			case CullMode.kCullFrontAndBack: return ProtoWorld.CULL_MODE.CullBoth;
			default: return ProtoWorld.CULL_MODE.CullBack;
		}
	}

	public enum Property {
		kColor = 0,
		kVector,
		kFloat,
		kRange,
		kTexture2D,
		kTexture3D,
		kTextureCube,
	};

		// https://docs.unity3d.com/Manual/SL-Blend.html
	public enum BlendOp
	{
		kBlendOpAdd = 0,
		kBlendOpSub,
		kBlendOpRevSub,
		kBlendOpMin,
		kBlendOpMax,
		kBlendOpLogicalClear,
		kBlendOpLogicalSet,
		kBlendOpLogicalCopy,
		kBlendOpLogicalCopyInverted,
		kBlendOpLogicalNoop,
		kBlendOpLogicalInvert,
		kBlendOpLogicalAnd,
		kBlendOpLogicalNand,
		kBlendOpLogicalOr,
		kBlendOpLogicalNor,
		kBlendOpLogicalXor,
		kBlendOpLogicalEquiv,
		kBlendOpLogicalAndReverse,
		kBlendOpLogicalAndInverted,
		kBlendOpLogicalOrReverse,
		kBlendOpLogicalOrInverted,
		kBlendOpMultiply,
		kBlendOpScreen,
		kBlendOpOverlay,
		kBlendOpDarken,
		kBlendOpLighten,
		kBlendOpColorDodge,
		kBlendOpColorBurn,
		kBlendOpHardLight,
		kBlendOpSoftLight,
		kBlendOpDifference,
		kBlendOpExclusion,
		kBlendOpHSLue,
		kBlendOpHSLColor,
		kBlendOpHSLLuminosity,
		kBlendOpOff,
		kBlendOpCount,
	};

	public enum BlendMode
	{
		kBlendZero = 0,
		kBlendOne,
		kBlendDstColor,
		kBlendSrcColor,
		kBlendOneMinusDstColor,
		kBlendSrcAlpha,
		kBlendOneMinusSrcColor,
		kBlendDstAlpha,
		kBlendOneMinusDstAlpha,
		kBlendSrcAlphaSaturate,
		kBlendOneMinusSrcAlpha,
		kBlendCount
	};

	public enum CompareFunction
	{
		kFuncUnknown = -1,
		kFuncDisabled = 0,
		kFuncNever,
		kFuncLess,
		kFuncEqual,
		kFuncLEqual,
		kFuncGreater,
		kFuncNotEqual,
		kFuncGEqual,
		kFuncAlways,
		kFuncCount
	};

	public enum CullMode
	{
		kCullUnknown = -1,
		kCullOff = 0,
		kCullFront,
		kCullBack,
		kCullFrontAndBack,
		kCullCount
	};

	public enum ColorWriteMask
	{	
		kColorWriteNone = 0,
		kColorWriteA = 1,
		kColorWriteB = 2,
		kColorWriteG = 4,
		kColorWriteR = 8,
		KColorWriteAll = 15
	};

	// https://docs.unity3d.com/Manual/SL-Stencil.html
	public enum StencilOp
	{
		kStencilOpKeep = 0,
		kStencilOpZero,
		kStencilOpReplace,
		kStencilOpIncrSat,
		kStencilOpDecrSat,
		kStencilOpInvert,
		kStencilOpIncrWrap,
		kStencilOpDecrWrap,
		kStencilOpCount
	};

	public enum ShaderParamType
	{
		kShaderParamFloat = 0,
		kShaderParamInt,
		kShaderParamBool,
		kShaderParamTypeCount
	};

	public enum FogMode
	{
		kFogUnknown = -1,
		kFogDisabled = 0,
		kFogLinear,
		kFogExp,
		kFogExp2,
		kFogModeCount
	};
};
#endregion

[System.Serializable]
public class ShaderLabShader
{
    public string name;
    public ShaderLabProperty[] properties;
	public ShaderLabSubShader[] subshaders;
	public string[] fallbacks;
	public string[] customeditors;
	public string IDCode;
	public string srcpath;

};

[System.Serializable]
public class ShaderLabFloat
{
	public ShaderLabFloat() { value = 0; reference = ""; }
	public void SetValue(float value)
	{
		this.value = value; 
		reference = "";
	}

	public void SetReference(string reference)
	{
		this.reference = reference;
		value = 0;
	}

	public float value;
	public string reference;
};

[System.Serializable]
public class ShaderLabProperty
{
	public string[] attributes;
	public string variable;
	public string description;
	public ShaderLabType.Property type;
	public Vector4 value;
	public string textureName;
};

[System.Serializable]
public class ShaderLabState
{
	public static float GetRealValue(ShaderLabFloat sl_float, ProtoWorld.Material pb_material)
	{
		if (string.IsNullOrEmpty(sl_float.reference))
			return sl_float.value;
		
		foreach (var item in pb_material.Floats)
		{
			if (item.Name == sl_float.reference)
				return item.Value;
		}

		foreach (var item in pb_material.Ranges)
		{
			if (item.Name == sl_float.reference)
				return item.Value;
		}
		
		return sl_float.value;
	}

	public string GetSpecficTagValue(string name)
	{
		for (int i = 0; i < tags.Length; i += 2)
		{
			if (tags[i].Equals(name))
				return tags[i + 1];
		}
		return "";
	}
	public string[] tags; //tags 按照数组存放，因为每个tag有两个字符串，所以该数组长度是总tags个数的两倍
	public ShaderLabFloat lod;

	public ShaderLabFloat colorMask; // ShaderLabType.ColorWriteMask
	public ShaderLabFloat alphaToMask; // bool

	public ShaderLabFloat offsetFactor;
	public ShaderLabFloat offsetUnits;

	public ShaderLabFloat zTest; // ShaderLabType.CompareFunction
	public ShaderLabFloat zWrite; // bool
	public ShaderLabFloat culling; // ShaderLabType.CullMode

	public ShaderLabFloat blendOp; // ShaderLabType.BlendOp
	public ShaderLabFloat blendOpAlpha; // ShaderLabType.BlendOp
	public ShaderLabFloat srcBlend; // ShaderLabType.BlendMode
	public ShaderLabFloat destBlend; // ShaderLabType.BlendMode
	public ShaderLabFloat srcBlendAlpha; // ShaderLabType.BlendMode
	public ShaderLabFloat destBlendAlpha; // ShaderLabType.BlendMode

	public ShaderLabFloat stencilRef;
	public ShaderLabFloat stencilReadMask;
	public ShaderLabFloat stencilWriteMask;

	public ShaderLabFloat stencilComp; // ShaderLabType.CompareFunction
	public ShaderLabFloat stencilOpPass; // ShaderLabType.StencilOp
	public ShaderLabFloat stencilOpFail; // ShaderLabType.StencilOp
	public ShaderLabFloat stencilOpZFail; // ShaderLabType.StencilOp

}
[System.Serializable]
public class ShaderLabSubShader
{
	public ShaderLabState state;
	public ShaderLabPass[] passes;
}

[System.Serializable]
public class ShaderLabPass
{
	public string name;
	public ShaderLabState state;
	// public ShaderLabVariant[] variants;
	public ShaderLabPlatform[] platforms;
}

// [System.Serializable]
// public class ShaderLabVariant
// {
// 	public string[] keywords;
// 	public ShaderLabPlatform[] platforms;
// }

[System.Serializable]
public class ShaderLabPlatform
{
	public string name;
	public string[] keywords;
	public int tier;
	public ShaderLabGLSL vertex;
	public ShaderLabGLSL fragment;
}

[System.Serializable]
public class ShaderLabGLSL
{
	public string[] uniform_name;
	public string[] uniform_string;
	public string[] attribute_name;
	public string[] attribute_string;
	public string[] varying_name;
	public string[] varying_string;
	public string main_string;
}

