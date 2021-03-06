﻿
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OperationFiasco
{
	public class Ship : StraightMover
	{
		public long Reloading { get; private set; }

		protected override Color Color { get { return Color.White; } }
		protected override Texture2D Texture { get { return SpriteManager.Ship; } }

		public Ship()
			: base( new Vector2(), new Vector2(), 0, new Vector2( 30, 15 ), 0 )
		{
			RotationSpeed = 0.1f;
			WeaponPower = 800;
		}

		public override void Update( GameTime time )
		{
			var mp = Mouse.GetState();
			Position = new Vector2( mp.X, mp.Y );

			var key = Keyboard.GetState();
			if( key.IsKeyDown( Keys.C ) )
				Rotation += RotationSpeed;
			else if( key.IsKeyDown( Keys.X ) )
				Rotation -= RotationSpeed;

			if( key.IsKeyDown( Keys.Space ) )
				Fire( time );
		}

		public void Fire( GameTime time )
		{
			if( Reloading + new TimeSpan( 0, 0, 0, 0, 100 ).Ticks < DateTime.Now.Ticks )
			{
				var v = Rotation.PolarVector();
				World.Instance.AddDrawable( new Bullet( new TimeSpan( 0, 0, 0, 0, WeaponPower ), 200, v, Position + ( v * 20 ), time ) );
				Reloading = DateTime.Now.Ticks;
				SoundManager.Blip().Play();
			}
		}

		public override void Collide( IWorldDrawable worldDrawable )
		{
			if( worldDrawable is Bullet )
				return;
			if( worldDrawable is Rock )
			{
				EffectHandler.Instance.Trigger( Effect.Explosion, Position );
				SpaceGame.Instance.End( World.Instance.Score );
			}
		}
		public int WeaponPower { get; set; }
		public new bool Done
		{
			get { return false; }
		}

		public float RotationSpeed { get; set; }
	}
}
