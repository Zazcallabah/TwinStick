using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using TwinStick.Helpers;

namespace TwinStick
{
	public class Bullet : IActor
	{
		readonly long tickspersecond = new TimeSpan(0,0,0,1).Ticks;
		readonly long _duration;
		readonly double _speed;
		readonly double _direction;
		readonly Point _startposition;
		DateTime _startmark;

		public bool Done { get; private set; }

		public Bullet( TimeSpan duration, double speedpxs, double direction, Point startposition )
		{
			_duration = duration.Ticks;

			_speed = speedpxs/tickspersecond;
			_direction = direction;
			_startposition = startposition;
			_startmark = DateTime.Now;
		}

		public void Poke( DateTime timestamp, Device world )
		{
			var lifespan = timestamp.Ticks - _startmark.Ticks;
			if( lifespan > _duration )
				Done = true;
			else
			{
				var currentposition = _startposition.MoveTo( _direction, _speed * lifespan );
				var vertexes = new CustomVertex.TransformedColored[3];
				Point p1 = currentposition.MoveTo( _direction, 4 );
				Point p2 = currentposition.MoveTo( _direction + ( 1 / 3.0 ) * 2 * Math.PI, 4 );
				Point p3 = currentposition.MoveTo( _direction - ( 1 / 3.0 ) * 2 * Math.PI, 4 );

				vertexes[0].Position = new Vector4( p1.X, p1.Y, 0, 1.0f );
				vertexes[0].Color = System.Drawing.Color.FromArgb( 255, 0, 0 ).ToArgb();
				vertexes[1].Position = new Vector4( p2.X, p2.Y, 0, 1.0f );
				vertexes[1].Color = System.Drawing.Color.FromArgb( 0, 0, 0 ).ToArgb();
				vertexes[2].Position = new Vector4( p3.X, p3.Y, 0, 1.0f );
				vertexes[2].Color = System.Drawing.Color.FromArgb( 0, 0, 0 ).ToArgb();
				world.VertexFormat = CustomVertex.TransformedColored.Format;
				world.DrawUserPrimitives( PrimitiveType.TriangleList, 1, vertexes );
			}
		}
	}
}