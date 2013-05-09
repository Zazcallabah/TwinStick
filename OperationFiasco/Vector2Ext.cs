using System;
using Microsoft.Xna.Framework;

namespace OperationFiasco
{
	public static class Vector2Ext
	{
		public static Vector2 MoveTo( this Vector2 origin, double angle, double distance )
		{
			double x = origin.X;
			double y = origin.Y;
			x += Math.Cos( angle ) * distance;
			y += Math.Sin( angle ) * distance;
			return new Vector2( (int) x, (int) y );
		}

		public static Vector2 PolarVector( this float arg, float abs = 1 )
		{
			var x = (float) Math.Cos( arg );
			var y = (float) Math.Sin( arg );
			return new Vector2( x * abs, y * abs );
		}

		public static Vector2 Shift2Center( this Vector2 position, float angle, float shiftdistance, float argumentfraction = 0.125f )
		{
			var dirang = (float) ( angle + 2f * Math.PI * argumentfraction );
			return position - dirang.PolarVector( shiftdistance );
		}
	}
}
