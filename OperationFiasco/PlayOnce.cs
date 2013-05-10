using Microsoft.Xna.Framework.Audio;

namespace OperationFiasco
{
	public class PlayOnce
	{
		readonly SoundEffect _effect;
		bool _played;
		public PlayOnce( SoundEffect effect )
		{
			_effect = effect;
		}
		public void Play()
		{
			if( _played )
				return;
			_played = true;
			_effect.Play();
		}
	}
}