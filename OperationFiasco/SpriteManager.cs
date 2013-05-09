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

	public class FontManager
	{
		public static void Load( ContentManager manager )
		{
			Consolas = manager.Load<SpriteFont>( "Fonts\\Consolas" );
			ConsolasLarge = manager.Load<SpriteFont>( "Fonts\\ConsolasLarge" );
			ConsolasHuge = manager.Load<SpriteFont>( "Fonts\\ConsolasHuge" );
			ConsolasLink = manager.Load<SpriteFont>( "Fonts\\ConsolasLink" );
		}

		public static SpriteFont Consolas { get; private set; }
		public static SpriteFont ConsolasLarge { get; private set; }
		public static SpriteFont ConsolasHuge { get; private set; }
		public static SpriteFont ConsolasLink { get; private set; }
	}
}