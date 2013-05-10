using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace OperationFiasco
{
	public class SoundManager
	{
		static SoundEffectInstance _bgmusic;
		static SoundEffectInstance _menumusic;
		static IList<SoundEffect> _blips = new List<SoundEffect>();
		static IList<SoundEffect> _xpls = new List<SoundEffect>();
		static SoundEffect _bg;
		static SoundEffect _menu;

		public static void Load( ContentManager manager )
		{
			_bg = manager.Load<SoundEffect>( "Sounds\\bg" );
			Beep = manager.Load<SoundEffect>( "Sounds\\beep" );
			Bip = manager.Load<SoundEffect>( "Sounds\\bip" );
			_menu = manager.Load<SoundEffect>( "Sounds\\menu" );
			_blips.Add( manager.Load<SoundEffect>( "Sounds\\blip1" ) );
			_blips.Add( manager.Load<SoundEffect>( "Sounds\\blip2" ) );
			_blips.Add( manager.Load<SoundEffect>( "Sounds\\blip3" ) );
			_xpls.Add( manager.Load<SoundEffect>( "Sounds\\xpl1" ) );
			_xpls.Add( manager.Load<SoundEffect>( "Sounds\\xpl2" ) );
			_xpls.Add( manager.Load<SoundEffect>( "Sounds\\xpl3" ) );
		}
		public static SoundEffect Beep { get; private set; }
		public static SoundEffect Bip { get; private set; }
		public static SoundEffectInstance BackgroundMusic
		{
			get
			{
				if( _bgmusic == null )
				{
					_bgmusic = _bg.CreateInstance();
					_bgmusic.IsLooped = true;
					_bgmusic.Volume = 0.5f;
				}
				return _bgmusic;
			}
		}

		public static SoundEffectInstance MenuMusic
		{
			get
			{
				if( _menumusic == null )
				{
					_menumusic = _menu.CreateInstance();
					_menumusic.IsLooped = true;
				}
				return _menumusic;
			}
		}

		public static SoundEffect Blip()
		{
			return _blips[Rnd.Number( 0, _blips.Count )];
		}
		public static SoundEffect Explode()
		{
			return _xpls[Rnd.Number( 0, _xpls.Count )];
		}

	}
}