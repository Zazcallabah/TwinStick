
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
	public interface IPowerup
	{
		void Apply( Ship ship );
	}

	public class MovementPowerup : IWorldDrawable
	{
		TimeSpan? spawntime;

		public MovementPowerup( int x, int y )
		{
			Position = new Vector2( x, y );
		}

		public void Apply( Ship ship )
		{
			ship.RotationSpeed += 0.1f;
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
			batch.Draw( SpriteManager.Dot, new Rectangle( (int) Position.X - 5, (int) Position.Y - 5, 10, 10 ), Color.Blue );
		}

		public void Collide( IWorldDrawable worldDrawable )
		{
			if( worldDrawable is Ship )
			{
				var s = (Ship) worldDrawable;
				Apply( s );
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

	public static class PowerupHandler
	{
		public static void AddRotation()
		{
			World.Instance.AddDrawable( new MovementPowerup( Rnd.Number( 0, 800 ), Rnd.Number( 0, 600 ) ) );
		}
	}
}
