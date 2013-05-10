using Microsoft.Xna.Framework;

namespace OperationFiasco
{
	public static class EnemyHandler
	{
		public static void AddSlowRock()
		{
			var dist = Rnd.CirclePos();
			var inv = dist * -1;
			World.Instance.AddDrawable( new Rock( 100, ( dist * 700 ) + new Vector2( 400, 400 ), inv, 25 + Rnd.Number( -3, 3 ) ) );
		}

		public static void AddFastRock()
		{
			var dist = Rnd.CirclePos();
			var inv = dist * -1;
			World.Instance.AddDrawable( new Rock( 60, ( dist * 600 ) + new Vector2( 400, 400 ), inv, 65 + Rnd.Number( -13, 10 ) ) );
		}
	}
}
