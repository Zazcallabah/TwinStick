using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;

namespace TwinStick
{
	public class Renderer : Form, ICanPaint
	{
		private Device _device;
		List<Bullet> _bullets;
		List<Keys> _currentlyPressedKeys = new List<Keys>();
		readonly Synchronizer sync;
		Ship _ship;

		public Renderer()
		{
			MouseMove += new MouseEventHandler( Renderer_MouseMove );
			KeyDown += new KeyEventHandler( Renderer_KeyDown );
			KeyUp += new KeyEventHandler( Renderer_KeyUp );
			Closing += new CancelEventHandler( Renderer_Closing );
			SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true );
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Text = "TwinStick";
			Size = new Size( 700, 500 );
			sync = new Synchronizer( this, 120 );
		}

		void Renderer_MouseMove( object sender, MouseEventArgs e )
		{
			_ship.Position = e.Location;
		}

		void Renderer_Closing( object sender, CancelEventArgs e )
		{
			sync.Break();
		}

		void Renderer_KeyUp( object sender, KeyEventArgs e )
		{
			_currentlyPressedKeys.RemoveAll( k => e.KeyCode.Equals( k ) );
		}

		void Renderer_KeyDown( object sender, KeyEventArgs e )
		{
			if( !_currentlyPressedKeys.Contains( e.KeyCode ) )
				_currentlyPressedKeys.Add( e.KeyCode );



		}

		public void InitializeDevice()
		{
			var presentParams = new PresentParameters
			{
				Windowed = true,
				EnableAutoDepthStencil = true,
				AutoDepthStencilFormat = DepthFormat.D16,
				SwapEffect = SwapEffect.Discard
			};

			_device = new Device( 0, DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, presentParams );
			_ship = new Ship();
			_bullets = new List<Bullet>();
			sync.Start();
		}

		public void Repaint()
		{
			try
			{

				foreach( var key in _currentlyPressedKeys )
				{
					if( key == Keys.X )
						_ship.Direction += 0.1;
					else if( key == Keys.C )
						_ship.Direction -= 0.1;
					else if( key == Keys.Space )
					{
						_bullets.Add( new Bullet( new TimeSpan( 0, 0, 0, 1 ), 200, _ship.Direction, _ship.Position ) );
					}
				}
				if( _currentlyPressedKeys.Any( k => k == Keys.W || k == Keys.S || k==Keys.A || k== Keys.D ))
					_bullets.Add( new Bullet( new TimeSpan( 0, 0, 0, 1 ), 200, _ship.Direction, _ship.Position ) );

				if( _currentlyPressedKeys.Count > 0 )
				{
					var k = _currentlyPressedKeys[0];
					if( k == Keys.W )
						_ship.Direction = Math.PI * 3 / 2.0;
					if( k == Keys.A )
						_ship.Direction = Math.PI; // * 2/2.0
					if( k == Keys.S )
						_ship.Direction = Math.PI * 1 / 2.0;
					if( k == Keys.D )
						_ship.Direction = 0.0; // Math.PI * 4/2.0;

					if( _currentlyPressedKeys.Count > 1 )
					{
						var k2 = _currentlyPressedKeys[1];
						if( k == Keys.W && k2 == Keys.A )
							_ship.Direction = Math.PI * 2.5 / 2.0;
						if( k == Keys.A && k2 == Keys.W )
							_ship.Direction = Math.PI * 2.5 / 2.0;
						if( k == Keys.W && k2 == Keys.D )
							_ship.Direction = Math.PI * 3.5 / 2.0;
						if( k == Keys.D && k2 == Keys.W )
							_ship.Direction = Math.PI * 3.5 / 2.0;

						if( k == Keys.S && k2 == Keys.A )
							_ship.Direction = Math.PI * 1.5 / 2.0;
						if( k == Keys.A && k2 == Keys.S )
							_ship.Direction = Math.PI * 1.5 / 2.0;
						if( k == Keys.S && k2 == Keys.D )
							_ship.Direction = Math.PI * 0.5 / 2.0;
						if( k == Keys.D && k2 == Keys.S )
							_ship.Direction = Math.PI * 0.5 / 2.0;

					}
				}
				_device.Clear( ClearFlags.Target, Color.Black, 1.0f, 0 );

				_device.BeginScene();
				_ship.Poke( DateTime.Now, _device );

				foreach( var b in _bullets )
					b.Poke( DateTime.Now, _device );

				_device.EndScene();
				_device.Present();

				var done = _bullets.Where( ( b ) => b.Done ).ToArray();
				foreach( var b in done )
					_bullets.Remove( b );
			}
			catch( GraphicsException e )
			{
				Debug.WriteLine( e );
				Application.Exit();
			}
		}
	}
}