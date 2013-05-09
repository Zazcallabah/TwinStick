using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OperationFiasco
{
	public class EndScreen : IWorld
	{
		TimeSpan? startmark;

		public EndScreen( int score )
		{
			Score = score;
		}

		protected int Score { get; set; }

		public void Update( GameTime time )
		{
			if( startmark == null )
				startmark = time.TotalGameTime;
			else if( startmark + new TimeSpan( 0, 0, 2 ) < time.TotalGameTime )
			{
				if( Keyboard.GetState().GetPressedKeys().Length > 0 )
					SpaceGame.Instance.Reset();
			}
		}

		public void Draw( SpriteBatch batch, GameTime time )
		{
			batch.DrawString( FontManager.Consolas, string.Format( "Final score: {0}", Score ), new Vector2( 80, 170 ), Color.Red );
			batch.DrawString( FontManager.ConsolasLarge, "U R DED", new Vector2( 80, 100 ), Color.Red );
			if( startmark + new TimeSpan( 0, 0, 2 ) < time.TotalGameTime )
				batch.DrawString( FontManager.Consolas, "press any key", new Vector2( 450, 420 ), Color.Gray );
		}
	}

	public interface IWorld
	{
		void Update( GameTime time );

		void Draw( SpriteBatch batch, GameTime time );
	}
}