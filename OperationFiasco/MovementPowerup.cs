using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
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
			World.Instance.AddMessage( "Engine upgraded", Position - new Vector2( 30, 0 ) );
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
	public class WeaponPowerup : IWorldDrawable
	{
		TimeSpan? spawntime;

		public WeaponPowerup( int x, int y )
		{
			Position = new Vector2( x, y );
		}

		public void Apply( Ship ship )
		{
			ship.WeaponPower += 400;
			World.Instance.AddMessage( "Weapons upgraded", Position - new Vector2( 30, 0 ) );
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
			batch.Draw( SpriteManager.Dot, new Rectangle( (int) Position.X - 5, (int) Position.Y - 5, 10, 10 ), Color.Pink );
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

}