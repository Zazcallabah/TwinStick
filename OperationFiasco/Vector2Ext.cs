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
	}
}
