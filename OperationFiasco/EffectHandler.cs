
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ProjectMercury;
using ProjectMercury.Renderers;

namespace OperationFiasco
{
	public enum Effect
	{
		Explosion,
		Spark
	}

	public class EffectHandler
	{
		static bool _hasLoaded;
		static EffectHandler _instance;
		readonly SpriteBatchRenderer _particleRenderer = new SpriteBatchRenderer();
		readonly Dictionary<Effect, ParticleEffect> Effects = new Dictionary<Effect, ParticleEffect>();


		public static EffectHandler Instance
		{
			get { return _instance ?? ( _instance = new EffectHandler() ); }
		}

		public void Load( ContentManager content, GraphicsDeviceManager graphics )
		{
			_particleRenderer.GraphicsDeviceService = graphics;
			_particleRenderer.LoadContent( content );
			Add( content, "EffectLibrary\\BasicExplosion", Effect.Explosion );
			Add( content, "EffectLibrary\\DeathSpark", Effect.Spark );
			_hasLoaded = true;
		}

		void Add( ContentManager content, string effectpath, Effect key )
		{
			var effect = content.Load<ParticleEffect>( effectpath );
			effect.LoadContent( content );
			effect.Initialise();
			Effects.Add( key, effect );
		}

		public void Update( GameTime time )
		{
			if( _hasLoaded )
			{
				foreach( var e in Effects.Values )
					e.Update( (float) time.ElapsedGameTime.TotalSeconds );
			}
		}

		public void Draw()
		{
			if( _hasLoaded )
			{
				foreach( var e in Effects.Values )
					_particleRenderer.RenderEffect( e );
			}
		}

		public void Trigger( Effect effect, int x, int y )
		{
			Trigger( effect, new Vector2( x, y ) );
		}

		public void Trigger( Effect effect, Vector2 vector2 )
		{
			Effects[effect].Trigger( vector2 );
		}
	}
}
