using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
	public class Rock : StraightMover
	{
		readonly int _size;

		public Rock( int size ) : this( size, Rnd.Vector( 30, 400, 10, 600 ), Rnd.Vector( -1, 1, -1, 1 ), 10 ) { }
		public Rock( int size, Vector2 position, Vector2 direction, float speedpxs )
			: base( position, direction, speedpxs, new Vector2( size, size ), 0f )
		{
			_size = size;
		}
		protected override Color Color { get { return Color.DarkKhaki; } }
		protected override Texture2D Texture { get { return SpriteManager.Rock; } }

		public override void Update( GameTime time )
		{
			var length = Position.LengthSquared();
			if( length < -1000 || length > 1000000 )
				Done = true;
			Rotation += time.ElapsedGameTime.Ticks * 2f / new TimeSpan( 0, 0, 1 ).Ticks;
			base.Update( time );
		}

		public override void Collide( IWorldDrawable worldDrawable )
		{
			if( worldDrawable is Rock )
				return;
			Done = true;
			if( worldDrawable is Bullet )
				World.Instance.Score += (int)((1.0/_size)*100.0);
			if( _size > 30 )
			{
				World.Instance.AddDrawable( new Rock( _size / 2, Position, Rnd.Vector( -1, 1, -1, 1 ), Speed * 4 ) );
				World.Instance.AddDrawable( new Rock( _size / 2, Position, Rnd.Vector( -1, 1, -1, 1 ), Speed * 5 ) );
				World.Instance.AddDrawable( new Rock( _size / 2, Position, Rnd.Vector( -1, 1, -1, 1 ), Speed * 6 ) );
			}
		}
	}
}