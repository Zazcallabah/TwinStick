using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OperationFiasco
{
	public interface IWorldDrawable
	{
		void Update( GameTime time );
		void Draw( SpriteBatch batch, GameTime time );
		void Collide( IWorldDrawable worldDrawable );
		Vector2 Size { get; }
		Vector2 Position { get; }
		bool Done { get; }
	}
}