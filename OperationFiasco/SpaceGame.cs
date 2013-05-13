using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OperationFiasco
{
	public class SpaceGame : Game
	{
		readonly GraphicsDeviceManager _graphics;
		static SpaceGame _instance;
		SpriteBatch _spriteBatch;
		IWorld _currentScreen;

		public SpaceGame()
		{
			_graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
		}

		public static SpaceGame Instance
		{
			get { return _instance ?? ( _instance = new SpaceGame() ); }
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch( GraphicsDevice );
			SoundManager.Load( Content );
			SpriteManager.Load( Content );
			FontManager.Load( Content );
			_currentScreen = new StartScreen();
			EffectHandler.Instance.Load( Content, _graphics );
		}

		public void Reset()
		{
			World.Instance = new World();
			_currentScreen = World.Instance;
		}

		public void End( int score )
		{
			_currentScreen = new EndScreen( score );
		}

		protected override void Update( GameTime gameTime )
		{
			// Allows the game to exit
			if( Keyboard.GetState().IsKeyDown( Keys.Escape ) )
				Exit();

			_currentScreen.Update( gameTime );

			EffectHandler.Instance.Update( gameTime );
			base.Update( gameTime );
		}

		protected override void Draw( GameTime gameTime )
		{
			GraphicsDevice.Clear( Color.Black );
			_spriteBatch.Begin();
			_currentScreen.Draw( _spriteBatch, gameTime );
			_spriteBatch.End();
			EffectHandler.Instance.Draw();
			base.Draw( gameTime );
		}
	}
}
