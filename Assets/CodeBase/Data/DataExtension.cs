using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtension
    {
        public static Vector3Data AsVectorData(this Vector3 vector) => 
            new Vector3Data(vector.x, vector.y, vector.z);

        public static Vector3 AsVectorUnity(this Vector3Data vector3data) =>
            new Vector3(vector3data.X, vector3data.Y, vector3data.Z);

        public static T DoSerialized<T>(this string json) => 
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object data) =>
            JsonUtility.ToJson(data);

        public static Vector3 AddY(this Vector3 vector, float value) =>
            new Vector3(vector.x, vector.y + value, vector.z);
    }
}