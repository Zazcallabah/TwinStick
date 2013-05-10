using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
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