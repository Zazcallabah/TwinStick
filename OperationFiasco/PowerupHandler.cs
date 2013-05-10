using System.Collections.Generic;
using System.Linq;

namespace OperationFiasco
{
	public static class PowerupHandler
	{
		static readonly IList<Make> Makers = new List<Make> 
		{
			new Make( ( x, y ) => new MovementPowerup( x, y ), 30, 2 ),
			new Make( ( x, y ) => new ScorePowerup( x, y ), 50, 6 ),
			new Make( ( x, y ) => new WeaponPowerup( x, y ), 100, 2 )
		};

		public static void AddPowerup( int score )
		{
			var validmakers = Makers.Where( m => m.MinScore <= score ).ToList();
			if( validmakers.Count > 0 )
			{
				var maker = validmakers[Rnd.Number( 0, validmakers.Count - 1 )];
				var drawable = maker.Powerup();
				if( drawable == null )
					Makers.Remove( maker );
				else
				{
					World.Instance.AddDrawable( drawable );
				}
			}
		}
	}

	public class Make
	{
		readonly Creator _make;
		int _count;

		public Make( Creator make, int minscore = 0, int count = 1 )
		{
			_make = make;
			MinScore = minscore;
			_count = count;
		}

		public int MinScore { get; private set; }

		public IWorldDrawable Powerup()
		{
			if( _count < 1 )
				return null;
			_count--;
			return _make( Rnd.Number( 0, 800 ), Rnd.Number( 0, 600 ) );
		}
	}

	public delegate IWorldDrawable Creator( int x, int y );
}
