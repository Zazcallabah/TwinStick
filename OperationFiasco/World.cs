﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OperationFiasco
{
	public class MessageFlash
	{
		public string Message { get; set; }
		public TimeSpan? StartMark { get; set; }
		public TimeSpan LifeSpan { get; set; }
		public Vector2 Position { get; set; }
	}

	public class World : IWorld
	{
		static World _instance;
		public static World Instance
		{
			get
			{
				if( _instance == null )
					throw new InvalidOperationException();
				return _instance;
			}
			set { _instance = value; }
		}

		readonly List<MessageFlash> _messages;
		readonly List<IWorldDrawable> _drawables;
		readonly List<IWorldDrawable> _addnext;
		TimeSpan? init;
		public World()
		{
			_drawables = new List<IWorldDrawable> { new Ship() };
			_addnext = new List<IWorldDrawable>();
			_messages = new List<MessageFlash>();
			Score = 0;
		}

		public int Score { get; set; }

		TimeSpan? LatestRockCreationTimeStamp { get; set; }
		readonly string[] _msg = new[] { "3", "2", "1", "GO" };
		readonly Color[] _clr = new[] { Color.Gray, Color.Gray, Color.White, Color.Red };
		readonly PlayOnce[] _snd = new[]
		{ 
			new PlayOnce( SoundManager.Beep ),
			new PlayOnce( SoundManager.Beep ), 
			new PlayOnce( SoundManager.Beep ),
			new PlayOnce( SoundManager.Bip ) 
		};

		public TimeSpan SpawnInterval()
		{
			var param = 1000 - Score;
			if( param < 100 )
				param = 100;
			return new TimeSpan( 0, 0, 0, 0, param );
		}

		public void Update( GameTime time )
		{
			if( init == null )

				init = time.TotalGameTime;
			var span = time.TotalGameTime - init;
			if( span > new TimeSpan( 0, 0, 4 ) )
			{
				if( LatestRockCreationTimeStamp == null || ( _drawables.Count < 490 && LatestRockCreationTimeStamp + SpawnInterval() < time.TotalGameTime ) )
				{
					LatestRockCreationTimeStamp = time.TotalGameTime;
					var trigger = Rnd.Number( 0, 100 );
					if( trigger < 50 )
						EnemyHandler.AddFastRock();
					else if( trigger < 94 )
						EnemyHandler.AddSlowRock();
					else
						PowerupHandler.AddPowerup( Score );
				}

				for( var i = 0; i < _drawables.Count; i++ )
					for( var j = i + 1; j < _drawables.Count; j++ )
						if( Intersects( _drawables[i], _drawables[j] ) )
						{
							_drawables[i].Collide( _drawables[j] );
							_drawables[j].Collide( _drawables[i] );
						}

				foreach( var drawable in _drawables )
					drawable.Update( time );

				foreach( var d in _addnext )
					_drawables.Add( d );
				_addnext.Clear();

				var done = _drawables.Where( ( b ) => b.Done ).ToArray();
				foreach( var b in done )
				{
					_drawables.Remove( b );
				}

			}
		}

		bool Intersects( IWorldDrawable first, IWorldDrawable second )
		{
			var dist = ( first.Position - second.Position ).LengthSquared();
			var objspan = ( first.Size + second.Size ).LengthSquared() / 4.8;

			return dist < objspan;
		}

		public void Draw( SpriteBatch batch, GameTime time )
		{
			if( init == null )
				return;
			var span = time.TotalGameTime - init;
			if( span < new TimeSpan( 0, 0, 4 ) )
			{
				var index = (int) span.Value.TotalSeconds;
				batch.DrawString( FontManager.ConsolasHuge, _msg[index], new Vector2( 370, 200 ), _clr[index] );
				var mp = Mouse.GetState();
				var position = new Vector2( mp.X, mp.Y );
				batch.Draw( SpriteManager.Dot, new Rectangle( (int) position.X, (int) position.Y, 2, 2 ), Color.Gray );
				_snd[index].Play();
			}
			else
			{
				if( SoundManager.BackgroundMusic.State == SoundState.Stopped )
					SoundManager.BackgroundMusic.Play();
				foreach( var drawable in _drawables )
					drawable.Draw( batch, time );
				var pos = new Vector2( 10, 30 );

				foreach( var message in _messages )
				{
					if( message.StartMark == null )
						message.StartMark = time.TotalGameTime;
					batch.DrawString( FontManager.Consolas, message.Message, message.Position, Color.LightGray );
				}

				var expired = _messages.Where( m => m.StartMark + m.LifeSpan < time.TotalGameTime ).ToList();
				foreach( var e in expired )
					_messages.Remove( e );

				batch.Draw( SpriteManager.Dot, new Rectangle( (int) pos.X, (int) pos.Y, 130, 20 ), Color.Gray );
				batch.DrawString( FontManager.Consolas, string.Format( "Score: {0}", Score ), pos, Color.DarkRed );
			}
		}

		public void AddMessage( string message, Vector2 position )
		{
			_messages.Add( new MessageFlash { LifeSpan = new TimeSpan( 0, 0, 3 ), Message = message, Position = position } );
		}

		public void AddDrawable( IWorldDrawable drawable )
		{
			_addnext.Add( drawable );
		}
	}
}
