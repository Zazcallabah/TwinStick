using System.Windows.Forms;

namespace TwinStick
{
	static class Program
	{
		static void Main()
		{
			using( var mainForm = new Renderer() )
			{
				mainForm.InitializeDevice();
				Application.Run( mainForm );
			}
		}
	}
}
