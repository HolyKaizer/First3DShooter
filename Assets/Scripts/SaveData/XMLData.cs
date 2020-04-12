using System.IO;
using System.Xml;
using UnityEngine;


namespace FirstShooter
{

	public sealed class XMLData : IData<SerializableGameObject>
	{
		#region IData<SerializableGameObject>

		public void Save(SerializableGameObject player, string path = "")
		{
			var xmlDoc = new XmlDocument();

			XmlNode rootNode = xmlDoc.CreateElement("Player");
			xmlDoc.AppendChild(rootNode);

			var element = xmlDoc.CreateElement(Crypto.CryptoXOR("Name"));
			element.SetAttribute("value", Crypto.CryptoXOR(player.Name));
			rootNode.AppendChild(element);

			element = xmlDoc.CreateElement("PosX");
			element.SetAttribute("value", player.Pos.X.ToString());
			element.SetAttribute("X", player.Pos.X.ToString());
			rootNode.AppendChild(element);

			element = xmlDoc.CreateElement("PosY");
			element.SetAttribute("value", player.Pos.Y.ToString());
			rootNode.AppendChild(element);

			element = xmlDoc.CreateElement("PosZ");
			element.SetAttribute("value", player.Pos.Z.ToString());
			rootNode.AppendChild(element);
			
			element = xmlDoc.CreateElement("RotX");
			element.SetAttribute("value", player.Rot.X.ToString());
			rootNode.AppendChild(element);
			
			element = xmlDoc.CreateElement("RotY");
			element.SetAttribute("value", player.Rot.Y.ToString());
			rootNode.AppendChild(element);
			
			element = xmlDoc.CreateElement("RotZ");
			element.SetAttribute("value", player.Rot.Z.ToString());
			rootNode.AppendChild(element);
			
			element = xmlDoc.CreateElement("RotW");
			element.SetAttribute("value", player.Rot.W.ToString());
			rootNode.AppendChild(element);

			element = xmlDoc.CreateElement("IsEnable");
			element.SetAttribute("value", player.IsEnable.ToString());
			rootNode.AppendChild(element);

			XmlNode userNode = xmlDoc.CreateElement("Info");
			var attribute = xmlDoc.CreateAttribute("Unity");
			attribute.Value = Application.unityVersion;
			userNode.Attributes.Append(attribute);
			userNode.InnerText = "System Language: " +
			                     Application.systemLanguage;
			rootNode.AppendChild(userNode);

			xmlDoc.Save(path);
		}

		public SerializableGameObject Load(string path = "")
		{
			var result = new SerializableGameObject();
			if (!File.Exists(path)) return result;
			using (var reader = new XmlTextReader(path))
			{
				while (reader.Read())
				{
					var key = Crypto.CryptoXOR("Name");
					if (reader.IsStartElement(key))
					{
						result.Name = Crypto.CryptoXOR(reader.GetAttribute("value"));
					}

					key = "PosX";
					if (reader.IsStartElement(key))
					{
						result.Pos.X = reader.GetAttribute("value").TrySingle();
					}

					key = "PosY";
					if (reader.IsStartElement(key))
					{
						result.Pos.Y = reader.GetAttribute("value").TrySingle();
					}

					key = "PosZ";
					if (reader.IsStartElement(key))
					{
						result.Pos.Z = reader.GetAttribute("value").TrySingle();
					}
					
					key = "RotX";
					if (reader.IsStartElement(key))
					{
						result.Rot.X = reader.GetAttribute("value").TrySingle();
					}
					
					key = "RotY";
					if (reader.IsStartElement(key))
					{
						result.Rot.Y = reader.GetAttribute("value").TrySingle();
					}
					
					key = "RotZ";
					if (reader.IsStartElement(key))
					{
						result.Rot.Z = reader.GetAttribute("value").TrySingle();
					}
					
					key = "RotW";
					if (reader.IsStartElement(key))
					{
						result.Rot.W = reader.GetAttribute("value").TrySingle();
					}
					
					key = "IsEnable";
					if (reader.IsStartElement(key))
					{
						result.IsEnable = reader.GetAttribute("value").TryBool();
					}
				}
			}

			return result;
		}

		#endregion
	}
}