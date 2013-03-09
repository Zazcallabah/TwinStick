using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using TwinStick.Helpers;

namespace TwinStick
{
	public class Ship : IActor
	{
		const double ShipSize = 20;
		public Point Position { get; set; }
		public double Direction { get; set; }

		public Ship()
		{
			Position = new Point( 100, 100 );
			Direction = 0;
		}

		public void Poke( DateTime timestamp, Device world )
		{

			var vertexes = new CustomVertex.TransformedColored[3];
			Point p1 = Position.MoveTo( Direction, ShipSize );
			Point p2 = Position.MoveTo( Direction + ( 1 / 2.3 ) * 2 * Math.PI, ShipSize );
			Point p3 = Position.MoveTo( Direction - ( 1 / 2.3 ) * 2 * Math.PI, ShipSize );

			vertexes[0].Position = new Vector4( p1.X, p1.Y, 0, 1.0f );
			vertexes[0].Color = System.Drawing.Color.FromArgb( 0, 255, 0 ).ToArgb();
			vertexes[1].Position = new Vector4( p2.X, p2.Y, 0, 1.0f );
			vertexes[1].Color = System.Drawing.Color.FromArgb( 0, 0, 255 ).ToArgb();
			vertexes[2].Position = new Vector4( p3.X, p3.Y, 0, 1.0f );
			vertexes[2].Color = System.Drawing.Color.FromArgb( 255, 0, 0 ).ToArgb();
			world.VertexFormat = CustomVertex.TransformedColored.Format;
			world.DrawUserPrimitives( PrimitiveType.TriangleList, 1, vertexes );
		}
	}
}