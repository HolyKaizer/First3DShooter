using UnityEngine;
using System.IO;


namespace FirstShooter
{
    public sealed class SaveDataRepository
    {
        #region Fields
        
        private readonly IData<SerializableGameObject> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.tat";

        private readonly string _path;

        #endregion


        #region ClassLifeCycle

        public SaveDataRepository()
        {
            _data = new JsonData<SerializableGameObject>();

            _path = Path.Combine(Application.dataPath, _folderName);
        }
        
        #endregion


        #region Methods

        public void Save(BaseObjectScene obj)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            var serializableObj = new SerializableGameObject
            {
                Pos = obj.Position,
                Rot = obj.Rotation,
                Name = obj.Name,
                IsEnable = obj.enabled
            };
            
            _data.Save(serializableObj, Path.Combine(_path, _fileName));
        }
                
        public void Load(BaseObjectScene obj)
        {
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file)) return;
            
            var newObj = _data.Load(file);
            obj.Position = newObj.Pos;
            obj.Rotation = newObj.Rot;
            obj.name = newObj.Name;
            obj.SetActive(newObj.IsEnable);
        }

        #endregion

    }
}