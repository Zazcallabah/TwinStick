using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury;

namespace OperationFiasco
{
	public class Bullet
	{
		readonly long _tickspersecond = new TimeSpan( 0, 0, 0, 1 ).Ticks;
		readonly long _duration;
		readonly float _speed;
		readonly Texture2D _sprite;
		readonly float _direction;
		readonly Vector2 _startposition;
		readonly long _startmark;

		public bool Done { get; private set; }

		public Bullet( Texture2D sprite, TimeSpan duration, float speedpxs, float direction, Vector2 startposition, GameTime time )
		{
			_duration = duration.Ticks;

			_speed = speedpxs / _tickspersecond;
			_sprite = sprite;
			_direction = direction;
			_startposition = startposition;
			_startmark = time.TotalGameTime.Ticks;
		}

		public void Draw( SpriteBatch batch, GameTime time )
		{
			var lifespan = time.TotalGameTime.Ticks - _startmark;
			var currentposition = _startposition.MoveTo( _direction -(float)Math.PI/2, _speed * lifespan );
			if( lifespan > _duration )
			{
				EffectHandler.Instance.Trigger( Effect.Spark, currentposition );
				Done = true;
			}
			else
			{

				batch.Draw( _sprite, new Rectangle( (int) currentposition.X - 2, (int) currentposition.Y - 5, 5, 10 ), null, Color.Red, _direction, new Vector2(), SpriteEffects.None, 0f );
			}
		}
	}
}