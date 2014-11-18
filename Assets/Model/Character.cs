using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace Model
{
	[Serializable]
	public class Character  //Singletone
	{
		public Int64 Id { get; set; }
		public String Title { get; set; }
		public String Gender { get; set; }
		public int Level { get; set; }

		private static Character instance;

		public static Character Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new Character();
				}
				return instance;
			}
		}

		public virtual Enterprise Enterprise { get; set; }

		private Character() {}	

		public void SaveDataToFile(FileStream fs)
		{
			IFormatter bform = new BinaryFormatter();
			try
			{
				bform.Serialize(fs, this);
				//Debug.Log("Successful serialization");
			}
			catch (Exception ex)
			{
				//Debug.Log("Serializing error: " + ex.Message);
			}
		}
		
		public Character LoadDataFromFile(FileStream fs)
		{
			IFormatter bform = new BinaryFormatter();
			Character data = null;
			try
			{
				data = (Character)bform.Deserialize(fs);
				//Debug.Log("Successful deserialization");
			}
			catch (Exception ex)
			{

				//Debug.Log("Deserializing error: " + ex.Message);
			}
			return data;
		}
	}
}