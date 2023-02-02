using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight
{
    public static class MaterialExtension
    {
        public static string toString(this Material material)
        {
            return material.ToString();
        }

        public static string getTextureName(this Material material, string propertyname) {
            var id = Shader.PropertyToID(propertyname);
            if (material.HasProperty(id)) {
                return material.GetTexture(propertyname).name;
            }
            return "";
        }

        public static string getTextureName(this Material material, int propertyId)
        {
            if (material.HasProperty(propertyId))
            {
                var names = material.GetTexturePropertyNames();
                foreach (string str in names) {
                    if (Shader.PropertyToID(str) == propertyId) {
                        return str;
                    }
                }
            }
            return "";
        }


        public static int propertyToID(this Material material, string propertyname)
        {
            return Shader.PropertyToID(propertyname);
        }

        public static void setText(this Material material,
            string name,
            string text,
            float w,
            float h,
            int x0,
            float y0,
            float x1,
            float y1,
            string font,
            float font_width,
            float font_height,
            float flag,
            float char_stride,
            float line_stride,
            float direction,
            float alignment)
        {

            //todo
        }
        public static void setText(this Material material,
            int property,
            string text,
            float w,
            float h,
            int x0,
            float y0,
            float x1,
            float y1,
            string font,
            float font_width,
            float font_height,
            float flag,
            float char_stride,
            float line_stride,
            float direction,
            float alignment)
        {

            //todo
        }

        public static void setTextureByName(this Material material, string propertyname, string textureName)
        {
            //todo
            //can not load texture properly
        }

        public static void setTextureByName(this Material material, int propertyId, string textureName)
        {
            //todo
            //can not load texture properly
        }
    }

}

