using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
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

		readonly List<IWorldDrawable> _drawables;
		readonly List<IWorldDrawable> _addnext;
		TimeSpan? init;

		public World()
		{
			_drawables = new List<IWorldDrawable> { new Ship(), new Rock( 100 ) };
			_addnext = new List<IWorldDrawable>();
			Score = 0;
		}

		public int Score { get; set; }

		TimeSpan? LatestRockCreationTimeStamp { get; set; }
		readonly string[] _msg = new[] { "3", "2", "1", "GO" };
		readonly Color[] _clr = new[] { Color.Gray, Color.Gray, Color.White, Color.Red };

		public void Update( GameTime time )
		{
			if( init == null )
				init = time.TotalGameTime;
			var span = time.TotalGameTime - init;
			if( span > new TimeSpan( 0, 0, 4 ) )
			{
				if( LatestRockCreationTimeStamp == null || ( _drawables.Count < 190 && LatestRockCreationTimeStamp + new TimeSpan( 0, 0, 1 ) < time.TotalGameTime ) )
				{
					LatestRockCreationTimeStamp = time.TotalGameTime;
					var dist = Rnd.CirclePos();
					var inv = dist * -1;

					_drawables.Add( new Rock( 100, dist * 1000, inv, 25 + Rnd.Number( -3, 3 ) ) );

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
			var objspan = ( first.Size + second.Size ).LengthSquared() / 4.0;

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
				batch.DrawString( FontManager.ConsolasHuge, _msg[index], new Vector2( 70, 50 ), _clr[index] );
			}
			else
			{
				foreach( var drawable in _drawables )
					drawable.Draw( batch, time );
				var pos = new Vector2( 10, 30 );
				batch.Draw( SpriteManager.Dot, new Rectangle( (int) pos.X, (int) pos.Y, 130, 20 ), Color.Gray );
				batch.DrawString( FontManager.Consolas, string.Format( "Score: {0}", Score ), pos, Color.DarkRed );
			}
		}

		public void AddDrawable( IWorldDrawable drawable )
		{
			_addnext.Add( drawable );
		}
	}
}
