// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: scene.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ProtoWorld {

  /// <summary>Holder for reflection information generated from scene.proto</summary>
  public static partial class SceneReflection {

    #region Descriptor
    /// <summary>File descriptor for scene.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SceneReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgtzY2VuZS5wcm90bxILcHJvdG9fd29ybGQaDGVudGl0eS5wcm90byLpAQoM",
            "UXVhbGl0eUxldmVsEgwKBG5hbWUYASABKAkSGQoRcGl4ZWxfbGlnaHRfY291",
            "bnQYZSABKA0SFwoPdGV4dHVyZV9xdWFsaXR5GGYgASgNEhwKFGFuaXNvdHJv",
            "cGljX3RleHR1cmVzGGcgASgNEhUKDWFudGlfYWxpYXNpbmcYaCABKA0SEAoH",
            "c2hhZG93cxjJASABKA0SGgoRc2hhZG93X3Jlc29sdXRpb24YygEgASgNEhoK",
            "EXNoYWRvd19wcm9qZWN0aW9uGMsBIAEoDRIYCg9zaGFkb3dfZGlzdGFuY2UY",
            "zAEgASgCImMKD1F1YWxpdHlTZXR0aW5ncxIdChVkZWZhdWx0X3F1YWxpdHlf",
            "bGV2ZWwYASABKA0SMQoOcXVhbGl0eV9sZXZlbHMYAiADKAsyGS5wcm90b193",
            "b3JsZC5RdWFsaXR5TGV2ZWwikQEKBVNjZW5lEg8KB3ZlcnNpb24YASABKAkS",
            "DAoEbmFtZRhlIAEoCRIMCgRmaWxlGGYgASgJEiIKBHJvb3QYyQEgASgLMhMu",
            "cHJvdG9fd29ybGQuRW50aXR5EjcKEHF1YWxpdHlfc2V0dGluZ3MYrQIgASgL",
            "MhwucHJvdG9fd29ybGQuUXVhbGl0eVNldHRpbmdzQgJIA2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::ProtoWorld.EntityReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ProtoWorld.QualityLevel), global::ProtoWorld.QualityLevel.Parser, new[]{ "Name", "PixelLightCount", "TextureQuality", "AnisotropicTextures", "AntiAliasing", "Shadows", "ShadowResolution", "ShadowProjection", "ShadowDistance" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ProtoWorld.QualitySettings), global::ProtoWorld.QualitySettings.Parser, new[]{ "DefaultQualityLevel", "QualityLevels" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ProtoWorld.Scene), global::ProtoWorld.Scene.Parser, new[]{ "Version", "Name", "File", "Root", "QualitySettings" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class QualityLevel : pb::IMessage<QualityLevel> {
    private static readonly pb::MessageParser<QualityLevel> _parser = new pb::MessageParser<QualityLevel>(() => new QualityLevel());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<QualityLevel> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ProtoWorld.SceneReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public QualityLevel() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public QualityLevel(QualityLevel other) : this() {
      name_ = other.name_;
      pixelLightCount_ = other.pixelLightCount_;
      textureQuality_ = other.textureQuality_;
      anisotropicTextures_ = other.anisotropicTextures_;
      antiAliasing_ = other.antiAliasing_;
      shadows_ = other.shadows_;
      shadowResolution_ = other.shadowResolution_;
      shadowProjection_ = other.shadowProjection_;
      shadowDistance_ = other.shadowDistance_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public QualityLevel Clone() {
      return new QualityLevel(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    /// <summary>
    /// name
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "pixel_light_count" field.</summary>
    public const int PixelLightCountFieldNumber = 101;
    private uint pixelLightCount_;
    /// <summary>
    /// rendering
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint PixelLightCount {
      get { return pixelLightCount_; }
      set {
        pixelLightCount_ = value;
      }
    }

    /// <summary>Field number for the "texture_quality" field.</summary>
    public const int TextureQualityFieldNumber = 102;
    private uint textureQuality_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint TextureQuality {
      get { return textureQuality_; }
      set {
        textureQuality_ = value;
      }
    }

    /// <summary>Field number for the "anisotropic_textures" field.</summary>
    public const int AnisotropicTexturesFieldNumber = 103;
    private uint anisotropicTextures_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint AnisotropicTextures {
      get { return anisotropicTextures_; }
      set {
        anisotropicTextures_ = value;
      }
    }

    /// <summary>Field number for the "anti_aliasing" field.</summary>
    public const int AntiAliasingFieldNumber = 104;
    private uint antiAliasing_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint AntiAliasing {
      get { return antiAliasing_; }
      set {
        antiAliasing_ = value;
      }
    }

    /// <summary>Field number for the "shadows" field.</summary>
    public const int ShadowsFieldNumber = 201;
    private uint shadows_;
    /// <summary>
    /// shadows
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Shadows {
      get { return shadows_; }
      set {
        shadows_ = value;
      }
    }

    /// <summary>Field number for the "shadow_resolution" field.</summary>
    public const int ShadowResolutionFieldNumber = 202;
    private uint shadowResolution_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint ShadowResolution {
      get { return shadowResolution_; }
      set {
        shadowResolution_ = value;
      }
    }

    /// <summary>Field number for the "shadow_projection" field.</summary>
    public const int ShadowProjectionFieldNumber = 203;
    private uint shadowProjection_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint ShadowProjection {
      get { return shadowProjection_; }
      set {
        shadowProjection_ = value;
      }
    }

    /// <summary>Field number for the "shadow_distance" field.</summary>
    public const int ShadowDistanceFieldNumber = 204;
    private float shadowDistance_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float ShadowDistance {
      get { return shadowDistance_; }
      set {
        shadowDistance_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as QualityLevel);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(QualityLevel other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (PixelLightCount != other.PixelLightCount) return false;
      if (TextureQuality != other.TextureQuality) return false;
      if (AnisotropicTextures != other.AnisotropicTextures) return false;
      if (AntiAliasing != other.AntiAliasing) return false;
      if (Shadows != other.Shadows) return false;
      if (ShadowResolution != other.ShadowResolution) return false;
      if (ShadowProjection != other.ShadowProjection) return false;
      if (ShadowDistance != other.ShadowDistance) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (PixelLightCount != 0) hash ^= PixelLightCount.GetHashCode();
      if (TextureQuality != 0) hash ^= TextureQuality.GetHashCode();
      if (AnisotropicTextures != 0) hash ^= AnisotropicTextures.GetHashCode();
      if (AntiAliasing != 0) hash ^= AntiAliasing.GetHashCode();
      if (Shadows != 0) hash ^= Shadows.GetHashCode();
      if (ShadowResolution != 0) hash ^= ShadowResolution.GetHashCode();
      if (ShadowProjection != 0) hash ^= ShadowProjection.GetHashCode();
      if (ShadowDistance != 0F) hash ^= ShadowDistance.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (PixelLightCount != 0) {
        output.WriteRawTag(168, 6);
        output.WriteUInt32(PixelLightCount);
      }
      if (TextureQuality != 0) {
        output.WriteRawTag(176, 6);
        output.WriteUInt32(TextureQuality);
      }
      if (AnisotropicTextures != 0) {
        output.WriteRawTag(184, 6);
        output.WriteUInt32(AnisotropicTextures);
      }
      if (AntiAliasing != 0) {
        output.WriteRawTag(192, 6);
        output.WriteUInt32(AntiAliasing);
      }
      if (Shadows != 0) {
        output.WriteRawTag(200, 12);
        output.WriteUInt32(Shadows);
      }
      if (ShadowResolution != 0) {
        output.WriteRawTag(208, 12);
        output.WriteUInt32(ShadowResolution);
      }
      if (ShadowProjection != 0) {
        output.WriteRawTag(216, 12);
        output.WriteUInt32(ShadowProjection);
      }
      if (ShadowDistance != 0F) {
        output.WriteRawTag(229, 12);
        output.WriteFloat(ShadowDistance);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (PixelLightCount != 0) {
        size += 2 + pb::CodedOutputStream.ComputeUInt32Size(PixelLightCount);
      }
      if (TextureQuality != 0) {
        size += 2 + pb::CodedOutputStream.ComputeUInt32Size(TextureQuality);
      }
      if (AnisotropicTextures != 0) {
        size += 2 + pb::CodedOutputStream.ComputeUInt32Size(AnisotropicTextures);
      }
      if (AntiAliasing != 0) {
        size += 2 + pb::CodedOutputStream.ComputeUInt32Size(AntiAliasing);
      }
      if (Shadows != 0) {
        size += 2 + pb::CodedOutputStream.ComputeUInt32Size(Shadows);
      }
      if (ShadowResolution != 0) {
        size += 2 + pb::CodedOutputStream.ComputeUInt32Size(ShadowResolution);
      }
      if (ShadowProjection != 0) {
        size += 2 + pb::CodedOutputStream.ComputeUInt32Size(ShadowProjection);
      }
      if (ShadowDistance != 0F) {
        size += 2 + 4;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(QualityLevel other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.PixelLightCount != 0) {
        PixelLightCount = other.PixelLightCount;
      }
      if (other.TextureQuality != 0) {
        TextureQuality = other.TextureQuality;
      }
      if (other.AnisotropicTextures != 0) {
        AnisotropicTextures = other.AnisotropicTextures;
      }
      if (other.AntiAliasing != 0) {
        AntiAliasing = other.AntiAliasing;
      }
      if (other.Shadows != 0) {
        Shadows = other.Shadows;
      }
      if (other.ShadowResolution != 0) {
        ShadowResolution = other.ShadowResolution;
      }
      if (other.ShadowProjection != 0) {
        ShadowProjection = other.ShadowProjection;
      }
      if (other.ShadowDistance != 0F) {
        ShadowDistance = other.ShadowDistance;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 808: {
            PixelLightCount = input.ReadUInt32();
            break;
          }
          case 816: {
            TextureQuality = input.ReadUInt32();
            break;
          }
          case 824: {
            AnisotropicTextures = input.ReadUInt32();
            break;
          }
          case 832: {
            AntiAliasing = input.ReadUInt32();
            break;
          }
          case 1608: {
            Shadows = input.ReadUInt32();
            break;
          }
          case 1616: {
            ShadowResolution = input.ReadUInt32();
            break;
          }
          case 1624: {
            ShadowProjection = input.ReadUInt32();
            break;
          }
          case 1637: {
            ShadowDistance = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  public sealed partial class QualitySettings : pb::IMessage<QualitySettings> {
    private static readonly pb::MessageParser<QualitySettings> _parser = new pb::MessageParser<QualitySettings>(() => new QualitySettings());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<QualitySettings> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ProtoWorld.SceneReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public QualitySettings() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public QualitySettings(QualitySettings other) : this() {
      defaultQualityLevel_ = other.defaultQualityLevel_;
      qualityLevels_ = other.qualityLevels_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public QualitySettings Clone() {
      return new QualitySettings(this);
    }

    /// <summary>Field number for the "default_quality_level" field.</summary>
    public const int DefaultQualityLevelFieldNumber = 1;
    private uint defaultQualityLevel_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint DefaultQualityLevel {
      get { return defaultQualityLevel_; }
      set {
        defaultQualityLevel_ = value;
      }
    }

    /// <summary>Field number for the "quality_levels" field.</summary>
    public const int QualityLevelsFieldNumber = 2;
    private static readonly pb::FieldCodec<global::ProtoWorld.QualityLevel> _repeated_qualityLevels_codec
        = pb::FieldCodec.ForMessage(18, global::ProtoWorld.QualityLevel.Parser);
    private readonly pbc::RepeatedField<global::ProtoWorld.QualityLevel> qualityLevels_ = new pbc::RepeatedField<global::ProtoWorld.QualityLevel>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::ProtoWorld.QualityLevel> QualityLevels {
      get { return qualityLevels_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as QualitySettings);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(QualitySettings other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (DefaultQualityLevel != other.DefaultQualityLevel) return false;
      if(!qualityLevels_.Equals(other.qualityLevels_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (DefaultQualityLevel != 0) hash ^= DefaultQualityLevel.GetHashCode();
      hash ^= qualityLevels_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (DefaultQualityLevel != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(DefaultQualityLevel);
      }
      qualityLevels_.WriteTo(output, _repeated_qualityLevels_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (DefaultQualityLevel != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(DefaultQualityLevel);
      }
      size += qualityLevels_.CalculateSize(_repeated_qualityLevels_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(QualitySettings other) {
      if (other == null) {
        return;
      }
      if (other.DefaultQualityLevel != 0) {
        DefaultQualityLevel = other.DefaultQualityLevel;
      }
      qualityLevels_.Add(other.qualityLevels_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            DefaultQualityLevel = input.ReadUInt32();
            break;
          }
          case 18: {
            qualityLevels_.AddEntriesFrom(input, _repeated_qualityLevels_codec);
            break;
          }
        }
      }
    }

  }

  public sealed partial class Scene : pb::IMessage<Scene> {
    private static readonly pb::MessageParser<Scene> _parser = new pb::MessageParser<Scene>(() => new Scene());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Scene> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ProtoWorld.SceneReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Scene() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Scene(Scene other) : this() {
      version_ = other.version_;
      name_ = other.name_;
      file_ = other.file_;
      Root = other.root_ != null ? other.Root.Clone() : null;
      QualitySettings = other.qualitySettings_ != null ? other.QualitySettings.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Scene Clone() {
      return new Scene(this);
    }

    /// <summary>Field number for the "version" field.</summary>
    public const int VersionFieldNumber = 1;
    private string version_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Version {
      get { return version_; }
      set {
        version_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 101;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "file" field.</summary>
    public const int FileFieldNumber = 102;
    private string file_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string File {
      get { return file_; }
      set {
        file_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "root" field.</summary>
    public const int RootFieldNumber = 201;
    private global::ProtoWorld.Entity root_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoWorld.Entity Root {
      get { return root_; }
      set {
        root_ = value;
      }
    }

    /// <summary>Field number for the "quality_settings" field.</summary>
    public const int QualitySettingsFieldNumber = 301;
    private global::ProtoWorld.QualitySettings qualitySettings_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoWorld.QualitySettings QualitySettings {
      get { return qualitySettings_; }
      set {
        qualitySettings_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Scene);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Scene other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Version != other.Version) return false;
      if (Name != other.Name) return false;
      if (File != other.File) return false;
      if (!object.Equals(Root, other.Root)) return false;
      if (!object.Equals(QualitySettings, other.QualitySettings)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Version.Length != 0) hash ^= Version.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (File.Length != 0) hash ^= File.GetHashCode();
      if (root_ != null) hash ^= Root.GetHashCode();
      if (qualitySettings_ != null) hash ^= QualitySettings.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Version.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Version);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(170, 6);
        output.WriteString(Name);
      }
      if (File.Length != 0) {
        output.WriteRawTag(178, 6);
        output.WriteString(File);
      }
      if (root_ != null) {
        output.WriteRawTag(202, 12);
        output.WriteMessage(Root);
      }
      if (qualitySettings_ != null) {
        output.WriteRawTag(234, 18);
        output.WriteMessage(QualitySettings);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Version.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Version);
      }
      if (Name.Length != 0) {
        size += 2 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (File.Length != 0) {
        size += 2 + pb::CodedOutputStream.ComputeStringSize(File);
      }
      if (root_ != null) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(Root);
      }
      if (qualitySettings_ != null) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(QualitySettings);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Scene other) {
      if (other == null) {
        return;
      }
      if (other.Version.Length != 0) {
        Version = other.Version;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.File.Length != 0) {
        File = other.File;
      }
      if (other.root_ != null) {
        if (root_ == null) {
          root_ = new global::ProtoWorld.Entity();
        }
        Root.MergeFrom(other.Root);
      }
      if (other.qualitySettings_ != null) {
        if (qualitySettings_ == null) {
          qualitySettings_ = new global::ProtoWorld.QualitySettings();
        }
        QualitySettings.MergeFrom(other.QualitySettings);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Version = input.ReadString();
            break;
          }
          case 810: {
            Name = input.ReadString();
            break;
          }
          case 818: {
            File = input.ReadString();
            break;
          }
          case 1610: {
            if (root_ == null) {
              root_ = new global::ProtoWorld.Entity();
            }
            input.ReadMessage(root_);
            break;
          }
          case 2410: {
            if (qualitySettings_ == null) {
              qualitySettings_ = new global::ProtoWorld.QualitySettings();
            }
            input.ReadMessage(qualitySettings_);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code