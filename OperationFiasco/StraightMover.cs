using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
	public abstract class StraightMover : IWorldDrawable
	{
		readonly long _tickspersecond = new TimeSpan( 0, 0, 0, 1 ).Ticks;
		readonly float _shiftsize;

		public float Speed { get; private set; }

		public bool Done { get; protected set; }

		protected StraightMover( Vector2 startposition, Vector2 direction, float speedpxs, Vector2 size, float rotation )
		{
			Speed = speedpxs;
			Position = startposition;
			Direction = direction;
			Direction.Normalize();
			Size = size;
			Rotation = rotation;
			_shiftsize = Size.Length() / 2;
		}

		public virtual void Update( GameTime time )
		{
			Position += Direction * ( ( Speed / _tickspersecond ) * time.ElapsedGameTime.Ticks );
		}

		public virtual void Draw( SpriteBatch batch, GameTime time )
		{
			var drawpos = Position.Shift2Center( Rotation, _shiftsize );
			batch.Draw( Texture, new Rectangle( (int) drawpos.X, (int) drawpos.Y, (int) ( Size.X ), (int) ( Size.Y ) ),
				null, Color, Rotation, new Vector2(), SpriteEffects.None, 0 );
		}

		public abstract void Collide( IWorldDrawable worldDrawable );

		protected abstract Color Color { get; }

		protected abstract Texture2D Texture { get; }

		public Vector2 Size { get; private set; }

		public Vector2 Position
		{
			get;
			protected set;
		}

		public float Rotation { get; protected set; }

		public Vector2 Direction { get; private set; }
	}
}