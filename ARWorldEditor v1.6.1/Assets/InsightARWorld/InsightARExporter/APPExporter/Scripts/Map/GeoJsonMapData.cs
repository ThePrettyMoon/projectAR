using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

namespace ARWorldEditor
{
    [Serializable]
    public class GeoJsonMapData
    {
        public string type;
        public List<GeoJsonFeature> features;
    }

    [Serializable]
    public class GeoJsonFeature
    {
        public GeoJsonGeometry geometry;
        public string type;
        public GeoJsonProperties properties;

        public string prefabAssetPath; //存储预制体prefab

        [JsonIgnore]
        public bool isActive ; 

        [JsonIgnore]
        public bool scaleDown ; 

        [JsonIgnore]
        public Transform prefab;  //预制体

        [JsonIgnore]
        public bool replacePrefab; //是否替换预制体

        [JsonIgnore]
        public int algIndex;  //算法模式索引

        public GeoJsonFeature Clone()
        {
            GeoJsonFeature geoJsonFeature = new GeoJsonFeature();
            geoJsonFeature.geometry = this.geometry.Clone();
            geoJsonFeature.type = this.type;
            geoJsonFeature.properties = this.properties.Clone();
            geoJsonFeature.isActive = this.isActive;
            geoJsonFeature.scaleDown = this.scaleDown;
            geoJsonFeature.prefab = this.prefab;
            geoJsonFeature.algIndex = this.algIndex;
            return geoJsonFeature;
        }
    }

    [Serializable]
    public class GeoJsonGeometry
    {
        public List<object> coordinates;
        public string type;

        [JsonIgnore]
        public Vector3 position;

        [JsonIgnore]
        public Vector3 rotation;

        [JsonIgnore]
        public Vector3 scale;

        public GeoVector3 uposition;
        public GeoVector3 urotation;
        public GeoVector3 uscale;

        public GeoJsonGeometry Clone()
        {
            GeoJsonGeometry geojsonGeometry = new GeoJsonGeometry();
            geojsonGeometry.type = this.type;
            geojsonGeometry.position = this.position;
            geojsonGeometry.rotation = this.rotation;
            geojsonGeometry.scale = this.scale;
            List<object> list = new List<object>();
            if (this.coordinates != null)
            {
                for(int i = 0; i < this.coordinates.Count; i++)
                {
                    list.Add(this.coordinates[i]);
                }
            }
            geojsonGeometry.coordinates = list;
            return geojsonGeometry;
        }
    }

    [Serializable]
    public class GeoJsonProperties
    {
        [DefaultValue("")]
        public string x_content_id;
        public string x_content_radius;
        [DefaultValue("")]
        public string x_preview_content_id;
        public string x_anchor;
        public string level;
        public string type;
        [DefaultValue("")]
        public string x_content_type;
        [DefaultValue("")]
        public string x_preview_content_type;
        public string x_name_radius;
        public string amenity;
        public string name;
        public string x_preview_content_radius;
        public string id;
        public string direction;
        public string height;
        [DefaultValue("")]
        public string x_content_alg_mode;

        public GeoJsonProperties Clone()
        {
            GeoJsonProperties properties = new GeoJsonProperties();
            properties.x_content_id = this.x_content_id;
            properties.x_content_radius = this.x_content_radius;
            properties.x_preview_content_id = this.x_preview_content_id;
            properties.x_anchor = this.x_anchor;
            properties.level = this.level;
            properties.type = this.type;
            properties.x_content_type = this.x_content_type;
            properties.x_preview_content_type = this.x_preview_content_type;
            properties.x_name_radius = this.x_name_radius;
            properties.amenity = this.amenity;
            properties.name = this.name;
            properties.x_preview_content_radius = this.x_preview_content_radius;
            properties.id = this.id;
            properties.direction = this.direction;
            properties.height = this.height;
            properties.x_content_alg_mode = this.x_content_alg_mode;
            return properties;
        }
    }

    /// <summary>
    /// swap：主场景的算法挂起，UE使用新算法（或者不使用算法？）
    ///overlay：UE的算法与主场景的算法叠加
    ///unchange：UE使用主场景的算法
    ///unsupport：不支持
    /// </summary>
    public enum AlgorithmType
    {
        unchange,
        overlay,
        swap,
        unsupport
    }

    [Serializable]
    public struct GeoVector3
    {
        public float x;
        public float y;
        public float z;

        public GeoVector3(float _x, float _y,float _z)
        {
            this.x = _x;
            this.y = _y;
            this.z = _z;
        }

        public static GeoVector3 zero
        {
            get
            {
                return new GeoVector3(0, 0, 0);
            }
        }

        public static GeoVector3 one
        {
            get
            {
                return new GeoVector3(1, 1, 1);
            }
        }

        public static GeoVector3 GetVector3(Vector3 vect)
        {
            GeoVector3 _vect = new GeoVector3(vect.x, vect.y, vect.z);
            return _vect;
        }

        public Vector3 GetVector3()
        {
            return new Vector3(this.x, this.y, this.z);
        }

        public void ToVector3(Vector3 vect)
        {
            this.x = vect.x;
            this.y = vect.y;
            this.z = vect.z;
        }
    }
}