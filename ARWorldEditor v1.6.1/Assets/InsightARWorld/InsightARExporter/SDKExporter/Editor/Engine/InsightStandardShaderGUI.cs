using System;
using UnityEngine;
using UnityEditor;

public class InsightStandardShaderGUI : ShaderGUI
{
    private enum CullMode
    {
        None,
        Front,
        Back
    }
    private enum RenderMode
    {
        Opaque,
        AlphaTest,
        Fade,   // Old school alpha-blending mode, fresnel does not affect amount of transparency
        Transparent // Physically plausible transparency mode, implemented as alpha pre-multiply
    }

    private enum BlendMode
    {
        Normal,
        SoftAdditive,
        Multiply,
        DoubleMultiply,
        // Darken,
        // Lighten,
        // LinearDodge
    }

    private static class Styles
    {
        public static GUIContent uvSetLabel = EditorGUIUtility.TrTextContent("UV Set");

        public static GUIContent albedoText = EditorGUIUtility.TrTextContent("Albedo", "Albedo (RGB) and Transparency (A)");
        public static GUIContent alphaCutoffText = EditorGUIUtility.TrTextContent("Alpha Cutoff", "Threshold for alpha cutoff");
        public static GUIContent specularMapText = EditorGUIUtility.TrTextContent("Specular", "Specular (RGB) and Smoothness (A)");
        public static GUIContent metallicMapText = EditorGUIUtility.TrTextContent("Metallic", "Metallic (R) and Smoothness (A)");
        public static GUIContent smoothnessText = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness value");
        public static GUIContent smoothnessScaleText = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness scale factor");
        public static GUIContent smoothnessMapChannelText = EditorGUIUtility.TrTextContent("Source", "Smoothness texture and channel");
        public static GUIContent highlightsText = EditorGUIUtility.TrTextContent("Specular Highlights", "Specular Highlights");
        public static GUIContent reflectionsText = EditorGUIUtility.TrTextContent("Reflections", "Glossy Reflections");
        public static GUIContent normalMapText = EditorGUIUtility.TrTextContent("Normal Map", "Normal Map");
        public static GUIContent heightMapText = EditorGUIUtility.TrTextContent("Height Map", "Height Map (G)");
        public static GUIContent occlusionText = EditorGUIUtility.TrTextContent("Occlusion", "Occlusion (G)");
        public static GUIContent emissionText = EditorGUIUtility.TrTextContent("Color", "Emission (RGB)");
        public static GUIContent alphaTestText = EditorGUIUtility.TrTextContent("Alpha Cutoff");
        public static GUIContent transparencyText = EditorGUIUtility.TrTextContent("Transparency");
        public static GUIContent cullModeText = EditorGUIUtility.TrTextContent("Cull Mode");
        public static GUIContent refractionRatioText = EditorGUIUtility.TrTextContent("Refraction Ratio");
        public static GUIContent reflectivityText = EditorGUIUtility.TrTextContent("Reflectivity");
        public static GUIContent clearCoatText = EditorGUIUtility.TrTextContent("Clear Coat");
        public static GUIContent clearCoatRoughnessText = EditorGUIUtility.TrTextContent("ClearCoat Roughness");

        public static string renderingMode = "Rendering Mode";
        public static string blendingMode = "Blending Mode";
        public static string cullingMode = "Culling Mode";
        public static string advancedText = "Advanced Options";
        public static readonly string[] renderNames = Enum.GetNames(typeof(RenderMode));
        public static readonly string[] blendNames = {"正常", "柔和叠加", "正片叠底", "两倍相乘"};
        public static readonly string[] cullNames = Enum.GetNames(typeof(CullMode));
    }

    MaterialEditor m_MaterialEditor = null;

    MaterialProperty renderMode = null;
    MaterialProperty blendMode = null;
    MaterialProperty albedoMap = null;
    MaterialProperty albedoColor = null;
    MaterialProperty alphaCutoff = null;
    MaterialProperty transparency = null;
    MaterialProperty metallicMap = null;
    MaterialProperty metallic = null;
    MaterialProperty smoothnessScale = null;
    MaterialProperty reflections = null;
    MaterialProperty bumpScale = null;
    MaterialProperty bumpMap = null;
    MaterialProperty occlusionStrength = null;
    MaterialProperty occlusionMap = null;
    MaterialProperty emissionColorForRendering = null;
    MaterialProperty emissionMap = null;
    MaterialProperty cullMode = null;
    MaterialProperty refractionRatio = null;
    MaterialProperty reflectivity = null;

    // clear coat
    MaterialProperty clearCoat = null;
    MaterialProperty clearCoatRoughness = null;
    MaterialProperty clearCoatNormalMap = null;
    MaterialProperty clearCoatNormalScale = null;

    bool m_FirstTimeApply = true;
    bool useEmission = false;
    bool showAdvanced = true;
    bool useShadowMapTypePCFSoft = true;
    bool useRefraction = false;
    bool useClearCoat = false;

    float tmpMetallic = -1;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        m_MaterialEditor = materialEditor;
        Material material = materialEditor.target as Material;

        FindAllProperties(properties);

        if (m_FirstTimeApply)
        {
            MaterialChanged(material);
            useEmission = material.IsKeywordEnabled("USE_EMISSION");
            useShadowMapTypePCFSoft = material.IsKeywordEnabled("SHADOWMAP_PCF_SOFT");
            useRefraction = material.IsKeywordEnabled("ENVMAP_MODE_REFRACTION");
            useClearCoat = material.IsKeywordEnabled("CLEARCOAT");
            m_FirstTimeApply = false;
        }

        ShaderPropertiesGUI(material);
    }

    void ShaderPropertiesGUI(Material material)
    {
        // Use default labelWidth
        EditorGUIUtility.labelWidth = 0f;

        // Detect any changes to the material
        EditorGUI.BeginChangeCheck();
        {
            RenderModePopup();
            // Primary properties
            GUILayout.Label("Main Maps", EditorStyles.boldLabel);

            // Main Texture
            m_MaterialEditor.TexturePropertySingleLine(Styles.albedoText, albedoMap, albedoColor);
            if (material.GetTexture("_MainTex"))
                m_MaterialEditor.TextureScaleOffsetProperty(albedoMap);

            // Metallic Gloss Map
            if (useRefraction)
            {
                if(tmpMetallic < -0.5f)
                {
                    tmpMetallic = metallic.floatValue;
                    metallic.floatValue = 0;
                }
                m_MaterialEditor.TexturePropertySingleLine(Styles.smoothnessText, metallicMap, smoothnessScale);
            }
            else
            {
                if(tmpMetallic > -0.5f)
                {
                    metallic.floatValue = tmpMetallic;
                    tmpMetallic = -1;
                }
                m_MaterialEditor.TexturePropertySingleLine(Styles.metallicMapText, metallicMap, metallic);
                m_MaterialEditor.ShaderProperty(smoothnessScale, Styles.smoothnessText, 2);
            }
            if (material.GetTexture("_MetallicGlossMap"))
                m_MaterialEditor.TextureScaleOffsetProperty(metallicMap);

            // Normal Map
            m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, bumpMap, bumpMap.textureValue != null ? bumpScale : null);
            if (material.GetTexture("_BumpMap"))
                m_MaterialEditor.TextureScaleOffsetProperty(bumpMap);
        
            // Occlusion
            m_MaterialEditor.TexturePropertySingleLine(Styles.occlusionText, occlusionMap, occlusionMap.textureValue != null ? occlusionStrength : null);
            if (material.GetTexture("_OcclusionMap"))
                m_MaterialEditor.TextureScaleOffsetProperty(occlusionMap);

            // Emission
            useEmission = EditorGUILayout.Toggle("Emission", useEmission);
            if(useEmission)
            {
                m_MaterialEditor.TexturePropertySingleLine(Styles.emissionText, emissionMap, emissionColorForRendering);
                if (material.GetTexture("_EmissionMap"))
                    m_MaterialEditor.TextureScaleOffsetProperty(emissionMap);
            }
            
            EditorGUILayout.Space();
            // // uv transform
            // GUILayout.Label("UV0 (用于除AO贴图以外的其他贴图)", EditorStyles.boldLabel);
            // EditorGUI.BeginChangeCheck();
            // m_MaterialEditor.TextureScaleOffsetProperty(albedoMap);
            // GUILayout.Label("UV1 (仅用于AO贴图)", EditorStyles.boldLabel);
            // m_MaterialEditor.TextureScaleOffsetProperty(occlusionMap);
            // if (EditorGUI.EndChangeCheck())
            // {
            //     albedoMap.textureScaleAndOffset = albedoMap.textureScaleAndOffset;
            //     bumpMap.textureScaleAndOffset = bumpMap.textureScaleAndOffset;
            //     metallicMap.textureScaleAndOffset = metallicMap.textureScaleAndOffset;
            //     occlusionMap.textureScaleAndOffset = occlusionMap.textureScaleAndOffset;
            //     emissionMap.textureScaleAndOffset = emissionMap.textureScaleAndOffset;
            // }

            EditorGUILayout.Space();
            showAdvanced = EditorGUILayout.BeginFoldoutHeaderGroup(showAdvanced, Styles.advancedText);
            if(showAdvanced)
            {
                // 拿不到内置编译选项的值
                // if(material.IsKeywordEnabled("SHADOWS_SCREEN") && material.IsKeywordEnabled("SHADOWS_SOFT"))
                    useShadowMapTypePCFSoft = EditorGUILayout.Toggle("ShadowMap PCF Soft", useShadowMapTypePCFSoft);

                // reflection or refraction
                useRefraction = EditorGUILayout.Toggle("Use Refraction", useRefraction);
                if(useRefraction){
                    m_MaterialEditor.ShaderProperty(refractionRatio, Styles.refractionRatioText, 2);
                    m_MaterialEditor.ShaderProperty(reflectivity, Styles.reflectivityText, 2);
                }

                useClearCoat = EditorGUILayout.Toggle("Use Clear Coat", useClearCoat);
                if(useClearCoat){
                    m_MaterialEditor.ShaderProperty(clearCoat, Styles.clearCoatText, 2);
                    m_MaterialEditor.ShaderProperty(clearCoatRoughness, Styles.clearCoatRoughnessText, 2);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText
                                                             , clearCoatNormalMap
                                                             , clearCoatNormalMap.textureValue != null ? clearCoatNormalScale : null);
                }
                
                // cull mode
                CullModePop();

                if(material.IsKeywordEnabled("ALPHATEST"))
                    m_MaterialEditor.ShaderProperty(alphaCutoff, Styles.alphaTestText);
                else if(material.IsKeywordEnabled("TRANSPARENCY"))
                    m_MaterialEditor.ShaderProperty(transparency, Styles.transparencyText);
                BlendModePopup();

                EditorGUILayout.Space();

            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
        }
        if (EditorGUI.EndChangeCheck())
        {
            albedoMap.textureScaleAndOffset = albedoMap.textureScaleAndOffset;
                bumpMap.textureScaleAndOffset = bumpMap.textureScaleAndOffset;
                metallicMap.textureScaleAndOffset = metallicMap.textureScaleAndOffset;
                occlusionMap.textureScaleAndOffset = occlusionMap.textureScaleAndOffset;
                emissionMap.textureScaleAndOffset = emissionMap.textureScaleAndOffset;
            // enable or disable the keyword based on checkbox
            SetKeyword(material, "USE_EMISSION", useEmission);
            SetKeyword(material, "SHADOWMAP_PCF_SOFT", useShadowMapTypePCFSoft);
            SetKeyword(material, "ENVMAP_MODE_REFRACTION", useRefraction);
            SetKeyword(material, "CLEARCOAT", useClearCoat);

            foreach (var obj in renderMode.targets)
                MaterialChanged((Material)obj);
        }

    }

    void FindAllProperties(MaterialProperty[] props)
    {
        renderMode = FindProperty("_RenderMode", props);
        blendMode = FindProperty("_BlendMode", props);
        albedoMap = FindProperty("_MainTex", props);
        albedoColor = FindProperty("_Color", props);
        alphaCutoff = FindProperty("_Cutoff", props);
        transparency = FindProperty("_transparency", props);
        bumpScale = FindProperty("_BumpScale", props);
        bumpMap = FindProperty("_BumpMap", props);
        metallicMap = FindProperty("_MetallicGlossMap", props, false);
        metallic = FindProperty("_Metallic", props, false);
        smoothnessScale = FindProperty("_GlossMapScale", props, false);
        reflections = FindProperty("_GlossyReflections", props, false);
        occlusionStrength = FindProperty("_OcclusionStrength", props);
        occlusionMap = FindProperty("_OcclusionMap", props);
        emissionColorForRendering = FindProperty("_EmissionColor", props);
        emissionMap = FindProperty("_EmissionMap", props);
        cullMode = FindProperty("_Cull", props);
        refractionRatio = FindProperty("refractionRatio", props);
        reflectivity = FindProperty("reflectivity", props);

        clearCoat = FindProperty("clearcoat", props);
        clearCoatRoughness = FindProperty("clearcoatRoughness", props);
        clearCoatNormalMap = FindProperty("clearcoatNormalMap", props);
        clearCoatNormalScale = FindProperty("clearcoatNormalScale", props);
    }

    void CullModePop()
    {
        EditorGUI.showMixedValue = cullMode.hasMixedValue;
        var mode = (CullMode)cullMode.floatValue;

        EditorGUI.BeginChangeCheck();
        mode = (CullMode)EditorGUILayout.Popup(Styles.cullingMode, (int)mode, Styles.cullNames);
        if (EditorGUI.EndChangeCheck())
        {
            m_MaterialEditor.RegisterPropertyChangeUndo("Culling Mode");
            cullMode.floatValue = (float)mode;
        }

        EditorGUI.showMixedValue = false;
    }

    void RenderModePopup()
    {
        EditorGUI.showMixedValue = renderMode.hasMixedValue;
        var mode = (RenderMode)renderMode.floatValue;

        EditorGUI.BeginChangeCheck();
        mode = (RenderMode)EditorGUILayout.Popup(Styles.renderingMode, (int)mode, Styles.renderNames);
        if (EditorGUI.EndChangeCheck())
        {
            m_MaterialEditor.RegisterPropertyChangeUndo("Rendering Mode");
            renderMode.floatValue = (float)mode;
        }

        EditorGUI.showMixedValue = false;
    }

    void BlendModePopup()
    {
        var render = (RenderMode)renderMode.floatValue;
        if(render == RenderMode.Opaque || render == RenderMode.AlphaTest)
            return;

        EditorGUI.showMixedValue = blendMode.hasMixedValue;
        var mode = (BlendMode)blendMode.floatValue;

        EditorGUI.BeginChangeCheck();
        mode = (BlendMode)EditorGUILayout.Popup(Styles.blendingMode, (int)mode, Styles.blendNames);
        if (EditorGUI.EndChangeCheck())
        {
            m_MaterialEditor.RegisterPropertyChangeUndo("Blending Mode");
            blendMode.floatValue = (float)mode;
        }

        EditorGUI.showMixedValue = false;
    }

    void MaterialChanged(Material material)
    {
        SetupMaterialWithRenderMode(material, (RenderMode)material.GetFloat("_RenderMode"));

        SetMaterialKeywords(material);
    }

    void SetupMaterialWithRenderMode(Material material, RenderMode renderMode)
    {
        BlendMode blendMode = (BlendMode)material.GetFloat("_BlendMode");
        switch (renderMode)
        {
            case RenderMode.Opaque:
                material.SetOverrideTag("RenderType", "");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("ALPHATEST");
                material.DisableKeyword("TRANSPARENCY");
                material.renderQueue = -1;
                break;
            case RenderMode.AlphaTest:
                material.SetOverrideTag("RenderType", "TransparentCutout");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("ALPHATEST");
                material.DisableKeyword("TRANSPARENCY");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                break;
            case RenderMode.Fade:
                material.SetOverrideTag("RenderType", "Transparent");
                SetupMaterialWithBlendMode(material, blendMode);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("ALPHATEST");
                material.DisableKeyword("TRANSPARENCY");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
            case RenderMode.Transparent:
                material.SetOverrideTag("RenderType", "Transparent");
                SetupMaterialWithBlendMode(material, blendMode);
                material.SetInt("_ZWrite", 0);
                material.EnableKeyword("TRANSPARENCY");
                material.DisableKeyword("ALPHATEST");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
        }
    }

    void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Normal:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                break;
            case BlendMode.SoftAdditive:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusDstColor);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
                break;
            case BlendMode.Multiply:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                break;
            case BlendMode.DoubleMultiply:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.SrcColor);
                break;
            default:
                break;
        }
    }
    
    void SetMaterialKeywords(Material material)
    {
        // Note: keywords must be based on Material value not on MaterialProperty due to multi-edit & material animation
        // (MaterialProperty value might come from renderer material property block)
        SetKeyword(material, "USE_MAP", material.GetTexture("_MainTex"));
        SetKeyword(material, "USE_NORMALMAP", material.GetTexture("_BumpMap"));
        SetKeyword(material, "USE_ROUGHNESSMAP", material.GetTexture("_MetallicGlossMap"));
        SetKeyword(material, "USE_METALNESSMAP", material.GetTexture("_MetallicGlossMap"));
        SetKeyword(material, "USE_EMISSIVEMAP", material.GetTexture("_EmissionMap"));
        SetKeyword(material, "USE_AOMAP", material.GetTexture("_OcclusionMap"));
        SetKeyword(material, "USE_CLEARCOAT_NORMALMAP", material.GetTexture("clearcoatNormalMap"));
    }

    void SetKeyword(Material m, string keyword, bool state)
    {
        if (state)
            m.EnableKeyword(keyword);
        else
            m.DisableKeyword(keyword);
    }
}
