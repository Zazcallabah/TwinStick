using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OperationFiasco
{
	public class SpaceGame : Game
	{
		readonly GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		Ship _ship;

		public SpaceGame()
		{
			_graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch( GraphicsDevice );
			_ship = new Ship( Content );
			EffectHandler.Instance.Load( Content, _graphics );
		}

		protected override void Update( GameTime gameTime )
		{
			var ms = Mouse.GetState();
			// Allows the game to exit
			if( ms.RightButton == ButtonState.Pressed )
				Exit();

			if( ms.LeftButton == ButtonState.Pressed )
			{
				EffectHandler.Instance.Trigger( Effect.Explosion, ms.X, ms.Y );
			}

			_ship.Update( gameTime );

			EffectHandler.Instance.Update( gameTime );
			base.Update( gameTime );
		}

		protected override void Draw( GameTime gameTime )
		{
			GraphicsDevice.Clear( Color.CornflowerBlue );

			_spriteBatch.Begin();
			_ship.Draw( _spriteBatch, gameTime );
			_spriteBatch.End();
			EffectHandler.Instance.Draw();
			base.Draw( gameTime );
		}
	}
}
