using System;
using System.Drawing;

namespace TwinStick.Helpers
{
	public static class PointExt
	{
		public static Point MoveTo( this Point origin, double angle, double distance )
		{
			double x = origin.X;
			double y = origin.Y;
			x += Math.Cos( angle ) * distance;
			y += Math.Sin( angle ) * distance;
			return new Point( (int) x, (int) y );
		}
	}
}
