
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OperationFiasco
{
	public class Ship : IExistInWorld
	{
		readonly Texture2D _sprite;
		List<Bullet> _bullets;
		public long Reloading { get; private set; }

		public Ship( ContentManager manager )
		{
			_bullets = new List<Bullet>();
			_sprite = manager.Load<Texture2D>( "ShipSprite" );
		}

		public void Update( GameTime time )
		{
			var mp = Mouse.GetState();
			Position = new Vector2( mp.X, mp.Y );

			var key = Keyboard.GetState();
			if( key.IsKeyDown( Keys.X ) )
				Direction += 0.1f;
			else if( key.IsKeyDown( Keys.C ) )
				Direction -= 0.1f;


			if( key.IsKeyDown( Keys.W ) )
				Direction = 0;
			if( key.IsKeyDown( Keys.D ) )
				Direction = (float) Math.PI / 2.0f;
			if( key.IsKeyDown( Keys.S ) )
				Direction = (float) Math.PI;
			if( key.IsKeyDown( Keys.A ) )
				Direction = (float) ( Math.PI * 3f / 2f );
			if( key.IsKeyDown( Keys.W ) && key.IsKeyDown( Keys.A ) )
				Direction = (float) ( Math.PI * 3.5 / 2.0 );
			if( key.IsKeyDown( Keys.W ) && key.IsKeyDown( Keys.D ) )
				Direction = (float) ( Math.PI * 0.5 / 2.0 );
			if( key.IsKeyDown( Keys.S ) && key.IsKeyDown( Keys.A ) )
				Direction = (float) ( Math.PI * 2.5 / 2.0 );
			if( key.IsKeyDown( Keys.S ) && key.IsKeyDown( Keys.D ) )
				Direction = (float) ( Math.PI * 1.5 / 2.0 );

			if( key.GetPressedKeys().Any(
			k => k == Keys.W ||
				k == Keys.S ||
				k == Keys.A ||
				k == Keys.D ||
				k == Keys.Space ) )
				Fire( time );


			var done = _bullets.Where( ( b ) => b.Done ).ToArray();
			foreach( var b in done )
			{
				_bullets.Remove( b );
			}

		}

		public void Fire( GameTime time )
		{
			if( Reloading + new TimeSpan( 0, 0, 0, 0, 100 ).Ticks < DateTime.Now.Ticks )
			{
				_bullets.Add( new Bullet( _sprite, new TimeSpan( 0, 0, 0, 1 ), 200, Direction, Position, time ) );
				Reloading = DateTime.Now.Ticks;
			}
		}

		public void Draw( SpriteBatch batch, GameTime time )
		{
			batch.Draw( _sprite, new Rectangle( (int) Position.X - 7, (int) Position.Y - 12, 15, 25 ), null, Color.White, Direction, new Vector2(), SpriteEffects.None, 0f );
			foreach( var b in _bullets )
				b.Draw( batch, time );
		}

		public Vector2 Position { get; set; }

		public float Direction { get; set; }
	}

	public interface IExistInWorld {}
}
