using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
	public class ScorePowerup : IWorldDrawable
	{
		TimeSpan? spawntime;

		public ScorePowerup( int x, int y )
		{
			Position = new Vector2( x, y );
		}


		public void Update( GameTime time )
		{
			if( spawntime == null )
				spawntime = time.TotalGameTime;
			if( spawntime + new TimeSpan( 0, 0, 5 ) < time.TotalGameTime )
				Done = true;
		}

		public void Draw( SpriteBatch batch, GameTime time )
		{
			batch.Draw( SpriteManager.Dot, new Rectangle( (int) Position.X - 5, (int) Position.Y - 5, 10, 10 ), Color.Yellow );
		}

		public void Collide( IWorldDrawable worldDrawable )
		{
			if( worldDrawable is Ship )
			{
				World.Instance.Score += 10;
				World.Instance.AddMessage( "+10", Position );
				Done = true;
			}

		}

		public Vector2 Size
		{
			get { return new Vector2( 10, 10 ); }
		}

		public Vector2 Position
		{
			get;
			private set;
		}

		public bool Done
		{
			get;
			private set;
		}
	}
}