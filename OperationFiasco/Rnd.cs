using System;
using Microsoft.Xna.Framework;

namespace OperationFiasco
{
	public static class Rnd
	{
		static Random _r;
		static Random R { get { return _r ?? ( _r = new Random() ); } }
		public static Vector2 Vector( float minX, float maxX, int minY, int maxY )
		{
			return new Vector2( (float) R.NextDouble() * ( maxX - minX ) + minX, (float) R.NextDouble() * ( maxY - minY ) + minY );
		}

		public static int Number( int min = 0, int max = 1 )
		{
			return R.Next( min, max );
		}

		public static Vector2 CirclePos()
		{
			var rnd = Rnd.Vector( -1, 1, -1, 1 );
			rnd.Normalize();
			return rnd;
		}
	}
}