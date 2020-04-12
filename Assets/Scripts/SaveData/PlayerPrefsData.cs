using UnityEngine;


namespace FirstShooter
{
    public sealed class PlayerPrefsData : IData<SerializableGameObject>
    {
        #region IData<SerializableGameObject>

        public void Save(SerializableGameObject data, string path = null)
        {
            PlayerPrefs.SetString("Name", data.Name);
            
            PlayerPrefs.SetFloat("PosX", data.Pos.X);
            PlayerPrefs.SetFloat("PosY", data.Pos.Y);
            PlayerPrefs.SetFloat("PosZ", data.Pos.Z);
            
            PlayerPrefs.SetFloat("RotX", data.Rot.X);
            PlayerPrefs.SetFloat("RotY", data.Rot.Y);
            PlayerPrefs.SetFloat("RotZ", data.Rot.Z);
            PlayerPrefs.SetFloat("RotW", data.Rot.W);
            
            PlayerPrefs.SetString("IsEnable", data.IsEnable.ToString());

            PlayerPrefs.Save();
        }

        public SerializableGameObject Load(string path = null)
        {
            var result = new SerializableGameObject();

            var key = "Name";
            if (PlayerPrefs.HasKey(key))
            {
                result.Name = PlayerPrefs.GetString(key);
            }

            key = "PosX";
            if (PlayerPrefs.HasKey(key))
            {
                result.Pos.X = PlayerPrefs.GetFloat(key);
            }
            
            key = "PosY";
            if (PlayerPrefs.HasKey(key))
            {
                result.Pos.Y = PlayerPrefs.GetFloat(key);
            }
            
            key = "PosZ";
            if (PlayerPrefs.HasKey(key))
            {
                result.Pos.Z = PlayerPrefs.GetFloat(key);
            }
            
            key = "RotX";
            if (PlayerPrefs.HasKey(key))
            {
                result.Rot.X = PlayerPrefs.GetFloat(key);
            }
            
            key = "RotY";
            if (PlayerPrefs.HasKey(key))
            {
                result.Rot.Y = PlayerPrefs.GetFloat(key);
            }
            
            key = "RotZ";
            if (PlayerPrefs.HasKey(key))
            {
                result.Rot.Z = PlayerPrefs.GetFloat(key);
            }
            
            key = "RotW";
            if (PlayerPrefs.HasKey(key))
            {
                result.Rot.W = PlayerPrefs.GetFloat(key);
            }

            key = "IsEnable";
            if (PlayerPrefs.HasKey(key))
            {
                result.IsEnable = PlayerPrefs.GetString(key).TryBool();
            }
            
            return result;
        }

        #endregion


        #region Methods
        
        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion
    }
}