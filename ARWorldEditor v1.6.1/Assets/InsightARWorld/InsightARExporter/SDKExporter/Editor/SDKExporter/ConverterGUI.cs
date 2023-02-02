using System.Collections;
using System.Collections.Generic;

namespace RenderEngine
{
	class ConverterGUI
	{
		public static ProtoGui.Canvas ConvertCanvas (ExporterConfig config, UnityEngine.Canvas canvas, UnityEngine.UI.CanvasScaler canvasScaler)
		{
			ProtoGui.Canvas ret = new ProtoGui.Canvas 
			{
				Enabled = canvas.isActiveAndEnabled,
				RenderMode = (uint)canvas.renderMode,
				PixelPerfect = canvas.pixelPerfect,				
				SortOrder = (uint)canvas.sortingOrder,
				WorldCameraHashId = (canvas.worldCamera != null) ? canvas.worldCamera.GetHashCode() : 0,
				PlaneDistance = canvas.planeDistance,
				SortingLayerId = canvas.sortingLayerID,
				SortingLayerName = canvas.sortingLayerName,
			};

			// NOTICE: since i3dEngine do not support tag system in version 1.7, worldCamera must be assigned during exporting.
			// when rendermode == worldspace and worldcamera == null, unity will find the camera with tag "MainCamera" and assign it.
			if (canvas.renderMode == UnityEngine.RenderMode.WorldSpace && canvas.worldCamera == null)
				ret.WorldCameraHashId = (UnityEngine.Camera.main != null) ? UnityEngine.Camera.main.GetHashCode() : 0;

			if (canvasScaler != null)
			{
				ret.UiScaleMode = (uint)canvasScaler.uiScaleMode;

				ret.ScaleFactor = canvasScaler.scaleFactor;

				ret.ReferenceResolution = UtilityConverter.ConvertVector2(canvasScaler.referenceResolution);
				ret.ScreenMatchMode = (uint)canvasScaler.screenMatchMode;
				ret.MatchWidthOrHeight = canvasScaler.matchWidthOrHeight;

				ret.PhysicalUnit = (uint)canvasScaler.physicalUnit;
				ret.FallbackScreenDpi = canvasScaler.fallbackScreenDPI;
				ret.DefaultSpriteDpi = canvasScaler.defaultSpriteDPI;

				ret.ReferencePixelsPerUnit = canvasScaler.referencePixelsPerUnit;
			}
			else 
			{
				ret.UiScaleMode = 0;
				ret.ScaleFactor = 1;
				ret.ReferencePixelsPerUnit = 100;
			}


			return ret;
		}

		public static ProtoGui.CanvasRenderer ConvertCanvasRenderer(ExporterConfig config, UnityEngine.CanvasRenderer canvasRenderer, UnityEngine.Transform tran)
		{
			ProtoGui.CanvasRenderer ret = new ProtoGui.CanvasRenderer();

			// export Graphic Element, allows only one element
			UnityEngine.UI.Image image = tran.GetComponent< UnityEngine.UI.Image > ();
			if (null != image)
			{
				ret.Image = ConverterGUI.ConvertImage(config, image);
				return ret;
			}

			UnityEngine.UI.RawImage rawImage = tran.GetComponent< UnityEngine.UI.RawImage > ();
			if (null != rawImage)
			{
				ret.RawImage = ConverterGUI.ConvertRawImage(config, rawImage);
				return ret;
			}

			UnityEngine.UI.Text text = tran.GetComponent< UnityEngine.UI.Text > ();
			if (null != text)
			{
				ret.Text = ConverterGUI.ConvertText(config, text);
				return ret;
			}

			// return empty ret here
			return ret;
		}

		public static ProtoGui.Sprite ConvertSprite(ExporterConfig config, UnityEngine.Sprite sprite)
		{
			if (sprite == null)
				return null;

			ProtoGui.Sprite ret = new ProtoGui.Sprite
			{ 
				Name = config.asset_path.GetPath( sprite.texture ) + ":" + sprite.name,
				Rect = UtilityConverter.ConvertRect(sprite.rect),
				Padding = UtilityConverter.ConvertVector4( UnityEngine.Sprites.DataUtility.GetPadding(sprite)),
				InnerUvRect = UtilityConverter.ConvertVector4(UnityEngine.Sprites.DataUtility.GetInnerUV(sprite)),
				OuterUvRect = UtilityConverter.ConvertVector4(UnityEngine.Sprites.DataUtility.GetOuterUV(sprite)),
				Border = UtilityConverter.ConvertVector4(sprite.border),
				PixelsPerUnit = sprite.pixelsPerUnit
			};

			ret.TextureFile = ExporterResource.ExportTextureTask(config, sprite.texture, new UnityEngine.Vector4(1,1,0,0));

			return ret;
		}

		public static ProtoGui.Image ConvertImage
			(ExporterConfig config
			, UnityEngine.UI.Image image)
		{
			ProtoGui.Image ret = new ProtoGui.Image();
			ret.Sprite = ConverterGUI.ConvertSprite(config, image.sprite);

			ret.ImageType = (uint)image.type;

			ret.MaterialFile = ExporterResource.ExportMaterial(config, 
				image.material,
				false, null, null);
			ret.Color = UtilityConverter.ConvertColor(image.color);
			ret.PreserveAspect = image.preserveAspect;
			ret.FillCenter = image.fillCenter;
			ret.HashId = image.GetHashCode();

			return ret;

			// System.Reflection.MethodInfo genMesh = image.GetType().GetMethod("OnPopulateMesh", 
			// 					System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
			// 					null,
			// 					new System.Type[] { typeof(UnityEngine.UI.VertexHelper)},
			// 					null);

			// UnityEngine.UI.VertexHelper vh = new UnityEngine.UI.VertexHelper();
			// genMesh.Invoke(image, new object[] { vh });
		}

		public static ProtoGui.RawImage ConvertRawImage(ExporterConfig config, UnityEngine.UI.RawImage rawImage)
		{
			ProtoGui.RawImage ret = new ProtoGui.RawImage();
			ret.MaterialFile = ExporterResource.ExportMaterial(config, 
				rawImage.material,
				false, null, null);
			ret.TextureFile = rawImage.texture == null ? string.Empty : ExporterResource.ExportTextureTask(config, rawImage.texture, new UnityEngine.Vector4(1,1,0,0));

			ret.UvRect = UtilityConverter.ConvertRect(rawImage.uvRect);
			ret.Color = UtilityConverter.ConvertColor(rawImage.color);
			ret.HashId = rawImage.GetHashCode();
			return ret;
		}

		public static ProtoGui.Text ConvertText(ExporterConfig config, UnityEngine.UI.Text text)
		{
			ProtoGui.Text ret = new ProtoGui.Text();

			ret.Text_ = text.text;
			ret.FontName = text.font.name;
			ret.FontFile = text.font.name; // TODO
			ret.FontStyle = (uint)text.fontStyle;
			ret.FontSize = (uint)text.fontSize;
			ret.LineSpacing = text.lineSpacing;
			ret.SupportRichText = text.supportRichText;

			ret.Alignment = (uint)text.alignment;
			ret.AlignByGeometry = text.alignByGeometry;
			ret.HorizonalOverflow = text.horizontalOverflow == UnityEngine.HorizontalWrapMode.Overflow ? true : false;
			ret.VerticalOverflow = text.verticalOverflow == UnityEngine.VerticalWrapMode.Overflow ? true : false;
			ret.ResizeTextForBestFit = text.resizeTextForBestFit;

			ret.Color = UtilityConverter.ConvertColor(text.color);

			// since SDK's font engine export a R8U format image, so default UI material won't work
			// use a special material and shader instead
			string buildin_material_path = ExporterUtility.GetBuildinMaterialPath(text.material.name);
			if (string.Empty != buildin_material_path)
			{
				UnityEngine.Material textMaterial = new UnityEngine.Material(text.material.shader);
				textMaterial.name = "Text UI Material";
				textMaterial.CopyPropertiesFromMaterial(text.material);

				string oldShaderName = textMaterial.shader.name;
				textMaterial.shader.name = "UI/Text";

				ret.MaterialFile = ExporterResource.ExportMaterial(config, 
					textMaterial,
					false, null, null);

				textMaterial.shader.name = oldShaderName;
			}
			else
			{
				ret.MaterialFile = ExporterResource.ExportMaterial(config, 
					text.material,
					false, null, null);
			}

			ret.UseCustomFontName = false;
			ret.HashId = text.GetHashCode();
			return ret;
		}

		public static ProtoGui.Button ConvertButton(ExporterConfig config, UnityEngine.UI.Button button)
		{
			ProtoGui.Button ret = new ProtoGui.Button
			{
				Selectable = ConvertSelectable(config, button),

			};

			return ret;
		}

		public static ProtoGui.InputField ConvertInputField(ExporterConfig config, UnityEngine.UI.InputField inputField)
		{
			ProtoGui.InputField ret = new ProtoGui.InputField
			{
				Selectable = ConvertSelectable(config, inputField),
				CharacterLimit = (uint)inputField.characterLimit,
				CharacterValidation = (uint)inputField.characterValidation,
				ContentType = (uint)inputField.contentType,
				InputType = (uint)inputField.inputType,
				KeyboardType = (uint)inputField.keyboardType,
				LineType = (uint)inputField.lineType,
				MultiLine = inputField.multiLine,
				PlaceholderHashid = inputField.placeholder.GetHashCode(),
				ShouldHideMobileInput = inputField.shouldHideMobileInput,
				Text = inputField.text,
				TextComponentHashid = inputField.textComponent.GetHashCode(),				
			};

			return ret;
		}

		static ProtoGui.Selectable ConvertSelectable(ExporterConfig config, UnityEngine.UI.Selectable selectable)
		{
			ProtoGui.Selectable ret = new ProtoGui.Selectable
			{
				Interactable = selectable.interactable,
				Transition = (uint)selectable.transition,		

				NormalColor = UtilityConverter.ConvertColor(selectable.colors.normalColor),
				HighlightedColor = UtilityConverter.ConvertColor(selectable.colors.highlightedColor),
				PressedColor = UtilityConverter.ConvertColor(selectable.colors.pressedColor),
				DisabledColor = UtilityConverter.ConvertColor(selectable.colors.disabledColor),
				ColorMultiplier = selectable.colors.colorMultiplier,
				FadeDuration = selectable.colors.fadeDuration
			};

			ret.HighlightedSprite = ConverterGUI.ConvertSprite(config, selectable.spriteState.highlightedSprite);
			ret.PressedSprite = ConverterGUI.ConvertSprite(config, selectable.spriteState.pressedSprite);
			ret.DisabledSprite = ConverterGUI.ConvertSprite(config, selectable.spriteState.disabledSprite);
			ret.HashId = selectable.GetHashCode();
			return ret;
		}
	}
}