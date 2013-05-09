using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
	public class Bullet : StraightMover
	{
		readonly long _startmark;
		readonly long _duration;

		public Bullet( TimeSpan duration, float speedpxs, Vector2 direction, Vector2 startposition, GameTime time )
			: base( startposition, direction, speedpxs, new Vector2( 5, 5 ), 0f )
		{
			_startmark = time.TotalGameTime.Ticks;
			_duration = duration.Ticks;
			Rotation = (float) Math.Atan2( Direction.X, Direction.Y );
		}

		protected override Color Color { get { return Color.Red; } }
		protected override Texture2D Texture { get { return SpriteManager.Dot; } }

		public override void Draw( SpriteBatch batch, GameTime time )
		{
			var lifespan = time.TotalGameTime.Ticks - _startmark;

			if( lifespan > _duration )
			{
				EffectHandler.Instance.Trigger( Effect.Spark, Position );
				Done = true;
			}
			else
			{
				base.Draw( batch, time );
			}
		}

		public override void Collide( IWorldDrawable worldDrawable )
		{
			if( worldDrawable is Bullet || worldDrawable is Ship )
				return;
			Done = true;
			EffectHandler.Instance.Trigger( Effect.Explosion, worldDrawable.Position );
		}
	}
}