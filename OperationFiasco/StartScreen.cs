using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OperationFiasco
{
	public class StartScreen : IWorld
	{
		public void Update( GameTime time )
		{
			if( Keyboard.GetState().GetPressedKeys().Length > 0 )
				SpaceGame.Instance.Reset();
		}

		Color FlashColor( GameTime time, Color first, Color second )
		{
			return time.TotalGameTime.Seconds % 2 == 0 ? first : second;
		}

		public void Draw( SpriteBatch batch, GameTime time )
		{
			batch.DrawString( FontManager.ConsolasHuge, "Operation Fiasco", new Vector2( 70, 50 ), FlashColor( time, Color.DarkRed, Color.Red ) );
			batch.DrawString( FontManager.Consolas, "space - shoot", new Vector2( 140, 230 ), Color.White );
			batch.DrawString( FontManager.Consolas, "mouse - move", new Vector2( 140, 250 ), Color.White );
			batch.DrawString( FontManager.Consolas, "x,c   - rotate", new Vector2( 140, 270 ), Color.White );
			batch.DrawString( FontManager.Consolas, "esc   - quit game", new Vector2( 140, 290 ), Color.White );
			batch.DrawString( FontManager.ConsolasLarge, "press any key", new Vector2( 450, 220 ), FlashColor( time, Color.DarkGreen, Color.White ) );
			batch.DrawString( FontManager.ConsolasLink, "https://github.com/Zazcallabah/TwinStick - fork me, right?", new Vector2( 20, 425 ), Color.Gray );
		}
	}
}