using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using Harmony.Cameras;
using Harmony.Components;
using Harmony.Controls;
using Harmony.Devices.Inputs;
using Harmony.Effects;
using Harmony.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Model=Harmony.Objects.Model;

namespace Harmony
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferMultiSampling = true;

            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            Components.Add(new GameObjectManager(this));
            Components.Add(new CameraManager(this));
            Components.Add(new EffectManager(this));
            Components.Add(new InputManager(this));
            Components.Add(new ControlManager(this));
            Components.Add(new ComponentManager(this));

            // Window
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        /// <summary>
        /// Gets or sets the graphics.
        /// </summary>
        /// <value>The graphics.</value>
        private GraphicsDeviceManager Graphics { get; set; }

        /// <summary>
        /// Gets or sets the sprite batch.
        /// </summary>
        /// <value>The sprite batch.</value>
        private SpriteBatch SpriteBatch { get; set; }



        public void ToggleFullScreen()
        {
            Graphics.ToggleFullScreen();
        }

        protected virtual void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;

            CameraManager.ActiveCamera.Viewport = GraphicsDevice.Viewport;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="a_gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime a_gameTime)
        {
            // TODO: Add your update logic here

            base.Update(a_gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="a_gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime a_gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(a_gameTime);
        }
    }
}