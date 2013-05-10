using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
	public class SpriteManager
	{
		public static void Load( ContentManager manager )
		{
			Ship = manager.Load<Texture2D>( "ShipSprite" );
			Rock = manager.Load<Texture2D>( "RockSprite" );
			Dot = manager.Load<Texture2D>( "Dot" );
		}

		public static Texture2D Dot { get; private set; }

		public static Texture2D Rock { get; private set; }

		public static Texture2D Ship { get; private set; }
	}
}