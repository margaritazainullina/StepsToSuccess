using System;

namespace Model
{
	public class Character
	{
		public Int64 Id { get; set; }
		public String Title { get; set; }
		public String Gender { get; set; }
		public int Level { get; set; }
		
		public Character (Int64 id, String title, String gender, int level)
		{
			Id = id;
			Title = title;
			Gender = gender;
			Level = level;
		}
	}
}